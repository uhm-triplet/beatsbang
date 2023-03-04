using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldNote : MonoBehaviour
{
    public enum Type
    {
        Left,
        Right
    }
    [SerializeField] GameObject holdNoteBottom;
    [SerializeField] GameObject holdNoteTop;
    [SerializeField] GameObject holdNoteBar;

    [HideInInspector] public Type type;
    public float speed;
    public float hitTime;
    public float releaseTime;
    [HideInInspector] public Vector2 SpawnPos;
    [HideInInspector] public Vector2 HitPos;
    [HideInInspector] public Vector2 RemovePos;
    [HideInInspector] public float TimeBeforeHit;
    [HideInInspector] public float SpawnTime;
    Activator activatorL;
    Activator activatorR;

    void Awake()
    {
        activatorL = GameObject.Find("Activators/Activator1").GetComponent<Activator>();
        activatorR = GameObject.Find("Activators/Activator2").GetComponent<Activator>();
    }
    void Start()
    {
        speed = Vector2.Distance(SpawnPos, HitPos) / TimeBeforeHit;
        SetShape();
    }

    void SetShape()
    {
        float size = speed * (releaseTime - hitTime);
        holdNoteBottom.transform.position = new Vector3(SpawnPos.x, SpawnPos.y, 0);
        holdNoteTop.transform.position = new Vector3(SpawnPos.x, SpawnPos.y + size, 0);
        holdNoteBar.transform.position = new Vector3(SpawnPos.x, SpawnPos.y + size / 2, 0);
        holdNoteBar.transform.localScale = new Vector3(1, size, 1);
    }

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, RemovePos, speed * Time.deltaTime);

        if (transform.position == new Vector3(RemovePos.x, RemovePos.y, 0))
        {
            if (type == Type.Left)
                activatorL.OnFail();
            else if (type == Type.Right)
                activatorR.OnFail();
            Destroy(gameObject);
        }
    }
}
