using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public enum Type { Melee, Range }
    public Type type;
    public float rate;


    [SerializeField] Transform bulletPos;
    public GameObject bullet;
    public GameObject bulletBlue;
    public GameObject bulletPurple;
    public GameObject bulletRed;
    public GameObject bulletRainbow;
    [SerializeField] float bulletVelocity;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    PlayerAim playerAim;
    PlayerState playerState;


    void Start()
    {
        playerAim = GetComponentInParent<PlayerAim>();
        playerState = GetComponentInParent<PlayerState>();
    }


    public void Use(int focus)
    {
        if (type == Type.Range && playerState.ammo > 0)
        {
            playerState.ammo--;
            StartCoroutine(Shot(focus));
        }

    }



    IEnumerator Shot(int focus)
    {

        bulletPos.LookAt(playerAim.aimPos);
        bullet = bulletBlue;
        if (focus == 2)
        {
            bullet = bulletPurple;
        }
        else if (focus == 3)
        {
            bullet = bulletRed;

        }
        else if (focus == 4)
        {
            bullet = bulletRainbow;
        }

        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.AddForce(bulletPos.forward * bulletVelocity, ForceMode.Impulse);

        yield return null;
        // GameObject instantBulletCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        // Rigidbody bulletCaseRigid = instantBulletCase.GetComponent<Rigidbody>();
        // Vector3 caseVec = bulletCasePos.forward * Random.Range(-4, -3) + Vector3.up * Random.Range(2, 3);
        // bulletCaseRigid.AddForce(caseVec, ForceMode.Impulse);
        // bulletCaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);


    }
}
