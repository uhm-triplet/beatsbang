using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PPText : MonoBehaviour
{
    GameManagerRhythm gm;
    public TextMeshProUGUI score;
    public TextMeshProUGUI multiplier;
    public TextMeshProUGUI streak;

    void Awake()
    {
        gm = GameObject.Find("GameManagerRhythm").GetComponent<GameManagerRhythm>();

    }

    // Update is called once per frame
    void Update()
    {
        score.text = gm.score + "";
        multiplier.text = gm.multiplier + "";
        streak.text = gm.streak + "";
    }
}
