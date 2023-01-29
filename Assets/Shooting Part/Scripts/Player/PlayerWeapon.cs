using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerWeapon : NetworkBehaviour
{
    bool fDown;
    bool rDown;
    bool gDown;
    bool isFireReady;
    [HideInInspector] public bool isReloading;
    float fireDelay;

    public GameObject grenadeObj;
    [SerializeField] Transform grenadePos;
    Animator animator;
    PlayerItem playerItem;
    PlayerMove playerMove;
    PlayerAim playerAim;

    void GetInput()
    {
        fDown = Input.GetButton("Fire1");
        gDown = Input.GetButtonDown("Grenade");
        rDown = Input.GetButtonDown("Reload");
    }
    public override void OnNetworkSpawn()
    {
        playerMove = GetComponent<PlayerMove>();
        playerItem = GetComponent<PlayerItem>();
        playerAim = GetComponent<PlayerAim>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner) return;
        GetInput();
        Attack();
        GrenadeServerRpc();
        Reload();
    }


    void Attack()
    {
        // equipWeapon = GameObject.Find("Player").GetComponent<PlayerItem>().equipWeapon;
        if (playerItem.equipWeapon == null || playerItem.equipWeapon.curAmmo == 0) return;

        fireDelay += Time.deltaTime;
        isFireReady = playerItem.equipWeapon.rate < fireDelay;

        if (fDown && isFireReady && !isReloading && !playerMove.isDodge)
        {
            playerItem.equipWeapon.Use();
            animator.SetTrigger(playerItem.equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            fireDelay = 0;
        }
    }


    // void Grenade()
    // {
    //     if (playerItem.hasGrenades == 0) return;
    //     if (gDown && !isReloading)
    //     {
    //         grenadePos.LookAt(playerAim.aimPos);
    //         GameObject instantGrenade = Instantiate(grenadeObj, grenadePos.position, grenadePos.rotation);
    //         Rigidbody grenadeRigid = instantGrenade.GetComponent<Rigidbody>();
    //         grenadeRigid.AddForce(grenadePos.forward * 20, ForceMode.Impulse);
    //         grenadeRigid.AddForce(grenadePos.up * 10, ForceMode.Impulse);
    //         grenadeRigid.AddTorque(Vector3.back * 10, ForceMode.Impulse);

    //         playerItem.hasGrenades -= 1;
    //         playerItem.grenades[playerItem.hasGrenades].SetActive(false);
    //     }
    // }
    void Reload()
    {
        if (playerItem.equipWeapon == null) return;

        if (playerItem.equipWeapon.type == Weapon.Type.Melee) return;

        if (playerItem.ammo == 0) return;

        if (rDown && isFireReady && !isReloading)
        {
            animator.SetTrigger("doReload");
            isReloading = true;
            Debug.Log("Reload");

            Invoke("ReloadOut", 3f);
        }
    }

    void ReloadOut()
    {
        int requiredAmmo = playerItem.equipWeapon.maxAmmo - playerItem.equipWeapon.curAmmo;
        int reAmmo = playerItem.ammo < requiredAmmo ? playerItem.ammo : requiredAmmo;
        playerItem.equipWeapon.curAmmo += reAmmo;
        playerItem.ammo -= reAmmo;
        isReloading = false;
    }

    [ServerRpc(RequireOwnership = false)]
    private void GrenadeServerRpc()
    {
        // Debug.Log(playerItem.hasGrenades);
        // if (playerItem.hasGrenades == 0) return;

        //  && !isReloading
        if (gDown)
        {

            grenadePos.LookAt(playerAim.aimPos);
            GameObject instantGrenade = Instantiate(grenadeObj, grenadePos.position, grenadePos.rotation);
            playerItem.hasGrenades -= 1;
            playerItem.grenades[playerItem.hasGrenades].SetActive(false);
            instantGrenade.GetComponent<NetworkObject>().Spawn();
            // Rigidbody grenadeRigid = instantGrenade.GetComponent<Rigidbody>();
            // grenadeRigid.AddForce(grenadePos.forward * 20, ForceMode.Impulse);
            // grenadeRigid.AddForce(grenadePos.up * 10, ForceMode.Impulse);
            // grenadeRigid.AddTorque(Vector3.back * 10, ForceMode.Impulse);

        }
    }
}
