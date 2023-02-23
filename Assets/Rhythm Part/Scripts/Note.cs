using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    [HideInInspector] public Vector2 SpawnPos;
    [HideInInspector] public Vector2 HitPos;
    [HideInInspector] public Vector2 RemovePos;
    [HideInInspector] public float TimeBeforeHit;
    [HideInInspector] public float SpawnTime;
    Activator activator;



    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        activator = GameObject.Find("Activators/Activator1").GetComponent<Activator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        speed = Vector2.Distance(SpawnPos, HitPos) / TimeBeforeHit;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, RemovePos, speed * Time.deltaTime);

        if (transform.position == new Vector3(RemovePos.x, RemovePos.y, 0))
        {
            activator.OnFail();
            Destroy(gameObject);
        }
    }
}
