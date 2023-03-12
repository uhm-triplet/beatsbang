using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerRhythm : MonoBehaviour
{
    enum Mode
    {
        Health, Ammo
    }
    public int score;
    public int multiplier = 1;
    public int streak = 0;

    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    [SerializeField] PlayerState playerState;

    [SerializeField] Camera camera;
    Mode currentMode = Mode.Ammo;
    bool oneDown;
    bool twoDown;

    public Color color1 = Color.red;
    public Color color2 = Color.green;

    private void Update()
    {
        GetInput();
        Swap();
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
        oneDown = Input.GetKeyDown(KeyCode.Alpha1);
        twoDown = Input.GetKeyDown(KeyCode.Alpha2);
    }

    void Swap()
    {
        if (oneDown)
        {
            camera.backgroundColor = color2;
            currentMode = Mode.Health;
        }
        if (twoDown)
        {
            camera.backgroundColor = color1;
            currentMode = Mode.Ammo;
        }
    }

    public void AddStreak()
    {
        streak++;
        GiveGrenade();

        if (streak >= 24)
        {
            multiplier = 4;
        }
        else if (streak >= 16)
        {
            multiplier = 3;
        }
        else if (streak >= 8)
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
            playerState.ammo += multiplier * 2;
            if (playerState.ammo > playerState.maxAmmo)
                playerState.ammo = playerState.maxAmmo;

        }
        else if (currentMode == Mode.Health)
        {
            playerState.health += multiplier;
            if (playerState.health > playerState.maxHealth)
                playerState.health = playerState.maxHealth;
        }
    }

}
