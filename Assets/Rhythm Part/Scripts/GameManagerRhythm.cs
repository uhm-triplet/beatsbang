using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerRhythm : MonoBehaviour
{
    enum Mode
    {
        Health, Ammo, Focus
    }
    public int score;
    public int multiplier = 1;
    public int streak = 0;

    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    [SerializeField] PlayerState playerState;

    int focus;
    int maxFocus = 300;
    Mode currentMode = Mode.Ammo;
    bool qDown;
    bool wDown;
    bool eDown;

    [SerializeField] GameObject hpParticle;
    [SerializeField] GameObject ammoParticle;
    [SerializeField] GameObject focusParticle;


    [SerializeField] GameObject hpImage;
    [SerializeField] GameObject ammoImage;
    [SerializeField] GameObject focusImage;

    [SerializeField] Image playerBar;

    [SerializeField] Color purple;



    private void Update()
    {
        GetInput();
        Swap();
        UIChange();
    }


    void UIChange()
    {
        switch (currentMode)
        {
            case Mode.Health:
                playerBar.color = Color.red;
                playerBar.fillAmount = (float)playerState.health / (float)playerState.maxHealth;
                hpImage.SetActive(true);
                ammoImage.SetActive(false);
                focusImage.SetActive(false);
                break;
            case Mode.Ammo:
                playerBar.color = Color.yellow;
                playerBar.fillAmount = (float)playerState.ammo / (float)playerState.maxAmmo;
                hpImage.SetActive(false);
                ammoImage.SetActive(true);
                focusImage.SetActive(false);
                break;
            case Mode.Focus:

                playerBar.fillAmount = (float)focus / (float)maxFocus;
                if (focus < 100)
                    playerBar.color = Color.blue;
                else if (focus < 200)
                    playerBar.color = purple;
                else if (focus < 300)
                    playerBar.color = Color.red;
                else if (focus >= 300)
                {
                    playerBar.color = Color.yellow;
                }
                hpImage.SetActive(false);
                ammoImage.SetActive(false);
                focusImage.SetActive(true);
                break;
        }
    }

    void GiveGrenade()
    {
        if (streak != 0 && streak % 50 == 0)
        {
            if (playerState.hasGrenades == playerState.maxHasGrenades)
                return;
            playerState.grenades[playerState.hasGrenades].SetActive(true);
            playerState.hasGrenades++;
        }
    }

    void GetInput()
    {
        qDown = Input.GetKeyDown(KeyCode.Q);
        wDown = Input.GetKeyDown(KeyCode.W);
        eDown = Input.GetKeyDown(KeyCode.E);
    }

    void Swap()
    {
        if (qDown)
        {
            hpParticle.SetActive(true);
            ammoParticle.SetActive(false);
            focusParticle.SetActive(false);
            currentMode = Mode.Health;
            focus = 0;
            playerState.focus = 1;

        }
        if (wDown)
        {
            hpParticle.SetActive(false);
            ammoParticle.SetActive(true);
            focusParticle.SetActive(false);
            currentMode = Mode.Ammo;
            focus = 0;
            playerState.focus = 1;

        }
        if (eDown)
        {
            hpParticle.SetActive(false);
            ammoParticle.SetActive(false);
            focusParticle.SetActive(true);
            currentMode = Mode.Focus;
        }
    }

    public void AddStreak()
    {
        streak++;
        GiveGrenade();

        if (streak >= 100)
        {
            multiplier = 4;
        }
        else if (streak >= 50)
        {
            multiplier = 3;
        }
        else if (streak >= 25)
        {
            multiplier = 2;
        }
        else
        {
            multiplier = 1;
        }
    }

    public void ResetStreak()
    {
        streak = 0;
        multiplier = 1;
    }

    public int NormalHit()
    {
        GetSupply(multiplier);
        return scorePerNote * multiplier;
    }

    public int GoodHit()
    {
        GetSupply(multiplier);
        return scorePerGoodNote * multiplier;
    }

    public int PerfectHit()
    {
        GetSupply(multiplier);
        return scorePerPerfectNote * multiplier;
    }

    public int GetScore()
    {
        GetSupply(multiplier);
        return scorePerNote * multiplier;
    }

    void GetSupply(int multiplier)
    {
        if (currentMode == Mode.Ammo)
        {
            playerState.ammo += multiplier + 1;
            if (playerState.ammo > playerState.maxAmmo)
                playerState.ammo = playerState.maxAmmo;

        }
        else if (currentMode == Mode.Health)
        {
            playerState.health += multiplier;
            if (playerState.health > playerState.maxHealth)
                playerState.health = playerState.maxHealth;
        }
        else if (currentMode == Mode.Focus)
        {
            focus += multiplier;
            if (focus < 100)
                playerState.focus = 1;
            else if (focus < 200)
                playerState.focus = 2;
            else if (focus < 300)
                playerState.focus = 3;
            else if (focus >= 300)
            {
                focus = maxFocus;
                playerState.focus = 4;
            }

        }
    }

}
