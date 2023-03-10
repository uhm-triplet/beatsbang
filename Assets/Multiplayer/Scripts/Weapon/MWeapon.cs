using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MWeapon : MonoBehaviour
{

    public enum Type { Melee, Range }
    public Type type;
    public int damage;
    public float rate;
    public int maxAmmo;
    public int curAmmo;

    public BoxCollider meleeArea;
    public TrailRenderer trailEffect;
    [SerializeField] Transform bulletPos;
    public GameObject bullet;
    [SerializeField] float bulletVelocity;
    public Transform bulletCasePos;
    public GameObject bulletCase;

    MPlayerAim playerAim;

    void Start()
    {
        playerAim = GetComponentInParent<MPlayerAim>();
    }


    public void Use()
    {

        if (type == Type.Melee)
        {
            StopCoroutine("Swing");
            StartCoroutine("Swing");
        }
        else if (type == Type.Range && curAmmo > 0)
        {
            curAmmo--;
            StartCoroutine("Shot");
        }

    }


    IEnumerator Swing()
    {
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        trailEffect.enabled = true;

        yield return new WaitForSeconds(0.8f);
        meleeArea.enabled = false;

        yield return new WaitForSeconds(0.3f);
        trailEffect.enabled = false;
    }
    IEnumerator Shot()
    {

        bulletPos.LookAt(playerAim.aimPos);
        GameObject instantBullet = Instantiate(bullet, bulletPos.position, bulletPos.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.AddForce(bulletPos.forward * bulletVelocity, ForceMode.Impulse);

        yield return null;
        GameObject instantBulletCase = Instantiate(bulletCase, bulletCasePos.position, bulletCasePos.rotation);
        Rigidbody bulletCaseRigid = instantBulletCase.GetComponent<Rigidbody>();
        Vector3 caseVec = bulletCasePos.forward * Random.Range(-4, -3) + Vector3.up * Random.Range(2, 3);
        bulletCaseRigid.AddForce(caseVec, ForceMode.Impulse);
        bulletCaseRigid.AddTorque(Vector3.up * 10, ForceMode.Impulse);


    }
}
