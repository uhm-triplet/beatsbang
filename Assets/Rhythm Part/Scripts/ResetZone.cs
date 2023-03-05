using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    public enum ActivatorType
    {
        Left, Right
    }

    public ActivatorType type;
    Activator activatorL, activatorR;
    private void Awake()
    {
        activatorL = GameObject.Find("Activators/Activator1").GetComponent<Activator>();
        activatorR = GameObject.Find("Activators/Activator2").GetComponent<Activator>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            if (type == ActivatorType.Left) activatorL.isResetZone = true;
            else activatorR.isResetZone = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            if (type == ActivatorType.Left) activatorL.isResetZone = false;
            else activatorR.isResetZone = false;
        }
    }

}
