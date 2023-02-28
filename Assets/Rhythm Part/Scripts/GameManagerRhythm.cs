using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerRhythm : MonoBehaviour
{
    public int score;
    public int multiplier = 1;
    public int streak = 0;

    public int scorePerNote = 100;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    public void AddStreak()
    {
        streak++;
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
        return scorePerNote * multiplier;
    }

    public int GoodHit()
    {
        return scorePerGoodNote * multiplier;
    }

    public int PerfectHit()
    {
        return scorePerPerfectNote * multiplier;
    }

    public int GetScore()
    {
        return scorePerNote * multiplier;
    }

}
