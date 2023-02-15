using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class PPText : MonoBehaviour
{
    public string name;

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetInt(name)+"";
    }
}
