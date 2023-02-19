using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    bool fDown;
    // bool f2Down;
    bool rDown;
    bool gDown;
    bool isFireReady;
    // bool isFire2Ready;
    float fireDelay;
    // float fire2Delay;
    [HideInInspector] public bool isReloading;

    public GameObject grenadeObj;
    [SerializeField] Transform grenadePos;
    Animator animator;
    PlayerState playerState;
    PlayerMove playerMove;
    PlayerAim playerAim;


    private AudioSource shootSound;
    private AudioSource swingSound;
    private AudioSource reloadSound;

    void GetInput()
    {
        fDown = Input.GetButton("Fire1");
        // f2Down = Input.GetButton("Fire2");
        gDown = Input.GetButtonDown("Grenade");
        rDown = Input.GetButtonDown("Reload");
    }

    void Awake()
    {
        shootSound = GameObject.Find("SFX/Player/Shoot").GetComponent<AudioSource>();
        swingSound = GameObject.Find("SFX/Player/Swing").GetComponent<AudioSource>();
        reloadSound = GameObject.Find("SFX/Player/Reload").GetComponent<AudioSource>();
        playerMove = GetComponentInParent<PlayerMove>();
        playerState = GetComponentInParent<PlayerState>();
        playerAim = GetComponentInParent<PlayerAim>();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Attack();
        Grenade();
        Reload();
    }


    void Attack()
    {
        if (playerState.equipWeapon == null || playerState.equipWeapon.curAmmo == 0) return;

        fireDelay += Time.deltaTime;
        isFireReady = playerState.equipWeapon.rate < fireDelay;

        // fire2Delay += Time.deltaTime;
        // isFire2Ready = playerState.equipWeapon2.rate < fire2Delay;

        if (fDown && isFireReady && !isReloading && !playerMove.isDodge)
        {
            if (playerState.equipWeapon.type == Weapon.Type.Melee) swingSound.Play();
            else shootSound.Play();

            animator.SetTrigger(playerState.equipWeapon.type == Weapon.Type.Melee ? "doSwing" : "doShot");
            playerState.equipWeapon.Use();
            fireDelay = 0;

        }
        // if (f2Down && isFire2Ready && !playerMove.isDodge)
        // {
        //     playerState.equipWeapon2.Use();
        //     animator.SetTrigger("doShot");
        //     fire2Delay = 0;
        // }
    }

    void Grenade()
    {
        if (playerState.hasGrenades == 0) return;
        if (gDown && !isReloading)
        {
            grenadePos.LookAt(playerAim.aimPos);
            GameObject instantGrenade = Instantiate(grenadeObj, grenadePos.position, grenadePos.rotation);
            Rigidbody grenadeRigid = instantGrenade.GetComponent<Rigidbody>();
            grenadeRigid.AddForce(grenadePos.forward * 20, ForceMode.Impulse);
            grenadeRigid.AddForce(grenadePos.up * 10, ForceMode.Impulse);
            grenadeRigid.AddTorque(Vector3.back * 10, ForceMode.Impulse);

            playerState.hasGrenades -= 1;
            playerState.grenades[playerState.hasGrenades].SetActive(false);
        }
    }

    void Reload()
    {
        if (playerState.equipWeapon == null) return;
        if (playerState.equipWeapon.type == Weapon.Type.Melee) return;
        if (playerState.ammo == 0) return;
        if (rDown && isFireReady && !isReloading)
        {
            animator.SetTrigger("doReload");
            isReloading = true;
            reloadSound.Play();
            Invoke("ReloadOut", 3f);
        }
    }

    void ReloadOut()
    {


        int requiredAmmo = playerState.equipWeapon.maxAmmo - playerState.equipWeapon.curAmmo;
        int reAmmo = playerState.ammo < requiredAmmo ? playerState.ammo : requiredAmmo;
        playerState.equipWeapon.curAmmo += reAmmo;
        playerState.ammo -= reAmmo;
        isReloading = false;
        // 장전 갯수 로직 정확하게 바꾸기
    }
}
