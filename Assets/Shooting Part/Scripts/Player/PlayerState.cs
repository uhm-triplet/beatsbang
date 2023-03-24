using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public GameObject[] weapons;
    public int hasWeapon = -1;

    public GameObject[] grenades;
    public int hasGrenades;
    public int ammo;
    public int health;
    public int focus = 1;

    public int maxAmmo;
    public int maxHealth;
    public int maxHasGrenades;

    public int score;

    bool isDamage;
    public bool isDead = false;

    GameObject nearObject;
    public Weapon equipWeapon;
    // public Weapon equipWeapon2;
    Animator animator;
    MeshRenderer[] meshs;
    Rigidbody rigid;

    Vector3 impact = Vector3.zero;
    private CharacterController controller;

    public GameManager gameManager;


    bool oneDown;
    bool twoDown;
    bool threeDown;

    private AudioSource hitSound;

    [SerializeField] GameObject damageScreen;
    [SerializeField] GameObject lowHpScreen;
    bool isDecreasing = true;


    void Awake()
    {
        hitSound = GameObject.Find("SFX/Player/Hit").GetComponent<AudioSource>();
        impact.z = -50;
        animator = GetComponentInChildren<Animator>();
        rigid = GetComponent<Rigidbody>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Interaction();
        getInput();
        changeDmgScreen();
        LowHp();
        // Swap();
    }

    void getInput()
    {

        oneDown = Input.GetKeyDown(KeyCode.Alpha1);
        twoDown = Input.GetKeyDown(KeyCode.Alpha2);
        threeDown = Input.GetKeyDown(KeyCode.Alpha3);
    }

    void Swap()
    {
        if (oneDown)
        {
            SwapLogic(0);
        }
        if (twoDown)
        {
            SwapLogic(1);

        }
        if (threeDown)
        {
            SwapLogic(2);

        }
    }

    void SwapLogic(int weaponNo)
    {

        hasWeapon = weaponNo;
        if (equipWeapon != null) equipWeapon.gameObject.SetActive(false);
        equipWeapon = weapons[hasWeapon].GetComponent<Weapon>();
        equipWeapon.gameObject.SetActive(true);

        animator.SetTrigger("doSwap");
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch (item.type)
            {
                case Item.Type.Grenade:
                    if (hasGrenades == maxHasGrenades)
                        break;
                    grenades[hasGrenades].SetActive(true);
                    hasGrenades += item.value;
                    break;
            }
            Destroy(other.gameObject);
        }
        else if (other.tag == "EnemyBullet")
        {
            if (!isDamage)
            {
                Bullet enemyBullet = other.GetComponent<Bullet>();
                health -= enemyBullet.damage;
                bool isBossAttack = other.name == "BossMeleeArea";

                StartCoroutine(OnDamage(isBossAttack));
            }
            if (other.GetComponent<Rigidbody>() != null)
            {
                other.gameObject.GetComponent<Bullet>().OnHit();

            }
        }

    }

    void OnDie()
    {
        if (isDead) return;
        isDead = true;
        gameManager.GameOver();
        animator.SetTrigger("doDie");
    }

    IEnumerator OnDamage(bool isBossAttack)
    {
        isDamage = true;
        hitSound.Play();
        if (health <= 0)
        {
            OnDie();
            health = 0;
        }

        var color = damageScreen.GetComponent<Image>().color;
        color.a = 0.8f;
        damageScreen.GetComponent<Image>().color = color;


        if (isBossAttack)
        {
            //find better logic
            controller.Move(impact * 10 * Time.deltaTime);

        }

        yield return new WaitForSeconds(0.2f);
        if (isBossAttack)
            rigid.velocity = Vector3.zero;
        isDamage = false;

    }

    void changeDmgScreen()
    {

        if (damageScreen.GetComponent<Image>().color.a > 0)
        {
            var color = damageScreen.GetComponent<Image>().color;
            color.a -= 0.02f;
            damageScreen.GetComponent<Image>().color = color;

        }
    }

    void LowHp()
    {
        if (health <= 30)
        {
            Debug.Log(isDecreasing);
            lowHpScreen.SetActive(true);
            if (isDecreasing)
                LowHpDown();
            else
                LowHpUp();

        }
        else
        {
            lowHpScreen.SetActive(false);
        }
    }

    void LowHpDown()
    {
        if (lowHpScreen.GetComponent<Image>().color.a > 0)
        {
            var color = lowHpScreen.GetComponent<Image>().color;
            color.a -= 0.005f;
            lowHpScreen.GetComponent<Image>().color = color;
            if (color.a <= 0)
            {
                isDecreasing = false;
            }
        }

    }

    void LowHpUp()
    {
        if (lowHpScreen.GetComponent<Image>().color.a < 0.15)
        {
            var color = lowHpScreen.GetComponent<Image>().color;
            color.a += 0.005f;
            lowHpScreen.GetComponent<Image>().color = color;
            if (color.a >= 0.15)
            {
                isDecreasing = true;
            }
        }
    }


}
