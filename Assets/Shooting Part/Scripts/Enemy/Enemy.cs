using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum Type { A, B, C, D };
    public Type enemyType;
    public int maxHealth;
    public int currentHealth;
    public GameManager gameManager;
    public Transform target;
    public BoxCollider meleeArea;
    public bool isChase;
    public bool isAttack;
    public bool isDead;
    public bool isBoss;
    public GameObject bullet;

    [HideInInspector] public Rigidbody rigid;
    [HideInInspector] public BoxCollider boxCollider;
    [HideInInspector] public MeshRenderer[] meshs;
    [HideInInspector] public NavMeshAgent nav;
    [HideInInspector] public Animator animator;
    public GameObject effectObj;

    public AudioSource hitSound;
    public AudioSource dieSound;
    public AudioSource bossDieSound;

    void Awake()
    {
        hitSound = GameObject.Find("SFX/Enemy/Hit").GetComponent<AudioSource>();
        dieSound = GameObject.Find("SFX/Enemy/Die").GetComponent<AudioSource>();
        bossDieSound = GameObject.Find("SFX/Enemy/BossDie").GetComponent<AudioSource>();
        rigid = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();


    }

    void Update()
    {
        if (nav.enabled && enemyType != Type.D && Vector3.Magnitude(target.position - transform.position) < 300)
        {
            if (enemyType != Type.D)
                ChaseStart();
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
        }
    }

    void Targeting()
    {
        if (enemyType == Type.D || isDead) return;
        float targetRadius = 0;
        float targetRange = 0;

        switch (enemyType)
        {
            case Type.A:
                targetRadius = 1.5f;
                targetRange = 3f;
                break;
            case Type.B:
                targetRadius = 1f;
                targetRange = 6f;
                break;
            case Type.C:
                targetRadius = 0.5f;
                targetRange = 60f;
                break;

        }
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,
        targetRadius,
        transform.forward,
        targetRange,
        LayerMask.GetMask("Player"));

        if (rayHits.Length > 0 && !isAttack)
        {
            StartCoroutine(Attack());
        }

    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        animator.SetBool("isAttack", true);

        switch (enemyType)
        {
            case Type.A:
                yield return new WaitForSeconds(0.2f);
                meleeArea.enabled = true;
                yield return new WaitForSeconds(1f);
                meleeArea.enabled = false;
                yield return new WaitForSeconds(1f);
                break;

            case Type.B:
                yield return new WaitForSeconds(0.1f);
                rigid.AddForce(transform.forward * 40, ForceMode.Impulse);
                meleeArea.enabled = true;
                yield return new WaitForSeconds(0.5f);
                rigid.velocity = Vector3.zero;
                meleeArea.enabled = false;
                yield return new WaitForSeconds(2f);
                break;

            case Type.C:


                yield return new WaitForSeconds(0.4f);
                GameObject instantBullet = Instantiate(bullet, transform.position, transform.rotation);
                Rigidbody rigidBullet = instantBullet.GetComponent<Rigidbody>();
                rigidBullet.velocity = transform.forward * 20;

                yield return new WaitForSeconds(2f);

                break;

        }



        isChase = true;
        isAttack = false;
        animator.SetBool("isAttack", false);

    }

    void FixedUpdate()
    {
        FreezeVelocity();
        Targeting();
    }

    void FreezeVelocity()
    {
        if (isChase)
        {
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }

    void ChaseStart()
    {
        isChase = true;
        animator.SetBool("isWalk", true);
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            currentHealth -= bullet.damage;

            Destroy(other.gameObject);
            StartCoroutine(OnDamage());

        }

    }
    public void HitByGrenade(Vector3 explosionPos)
    {
        currentHealth -= 100;
        Vector3 reactVec = transform.position - explosionPos;
        StartCoroutine(OnDamage());
    }
    // Update is called once per frame
    IEnumerator OnDamage()
    {
        if (!isDead)
        {
            hitSound.Play();
            if (currentHealth <= 0)
            {
                isDead = true;
                isChase = false;
                nav.enabled = false;
                animator.SetTrigger("doDie");

                switch (enemyType)
                {
                    case Type.A:
                        gameManager.enemyACnt--;
                        break;
                    case Type.B:
                        gameManager.enemyBCnt--;
                        break;
                    case Type.C:
                        gameManager.enemyCCnt--;
                        break;
                    case Type.D:
                        gameManager.enemyDCnt--;
                        break;
                }

                if (isBoss)
                {
                    effectObj.SetActive(true);
                    bossDieSound.Play();
                }
                else
                {
                    dieSound.Play();
                }
                foreach (MeshRenderer mesh in meshs)
                    mesh.material.color = Color.gray;
                rigid.AddForce(Vector3.up * 2, ForceMode.Impulse);
                if (enemyType != Type.D)
                    Destroy(gameObject, 1);
            }
            foreach (MeshRenderer mesh in meshs)
                mesh.material.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            if (currentHealth > 0)
            {
                foreach (MeshRenderer mesh in meshs)
                    mesh.material.color = Color.white;

                yield return new WaitForSeconds(0.1f);

            }


        }
    }
}
