using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject meshObj;
    public GameObject effectObj;
    public Rigidbody rigid;

    private AudioSource throwSound;
    private AudioSource explodeSound;
    // Start is called before the first frame update
    void Awake()
    {
        throwSound = GameObject.Find("SFX/Grenade/Throw").GetComponent<AudioSource>();
        explodeSound = GameObject.Find("SFX/Grenade/Explosion").GetComponent<AudioSource>();
    }

    void Start()
    {
        throwSound.Play();
        // AudioSource.PlayClipAtPoint(throwSound, transform.position);
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;
        meshObj.SetActive(false);
        effectObj.SetActive(true);

        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 25, Vector3.up, 0, LayerMask.GetMask("Enemy"));
        foreach (RaycastHit hit in rayHits)
        {
            hit.transform.GetComponent<Enemy>().HitByGrenade(transform.position);
        }
        explodeSound.Play();
        // AudioSource.PlayClipAtPoint(explodeSound, transform.position);
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
