using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Grenade : NetworkBehaviour
{
    public PlayerWeapon parent;
    public GameObject effectObj;
    public Rigidbody rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid.AddForce(transform.forward * 20, ForceMode.Impulse);
        rigid.AddForce(transform.up * 10, ForceMode.Impulse);
        rigid.AddTorque(Vector3.back * 10, ForceMode.Impulse);
        StartCoroutine(Explosion());
    }

    // Update is called once per frame
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3f);
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;


        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, 15, Vector3.up, 0, LayerMask.GetMask("Player"));
        foreach (RaycastHit hit in rayHits)
        {
            hit.transform.GetComponent<EnemyTest>().HitByGrenade(transform.position);
        }
        effectObj.SetActive(true);
        yield return new WaitForSeconds(3f);

        if (IsOwner)
            parent.DestroyGrenadeServerRpc();
    }



}
