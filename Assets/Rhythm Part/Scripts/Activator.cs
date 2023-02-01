using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    GameObject note;
    Color original;

    void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        original = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key)) {
            StartCoroutine(Clicked());
        }

        if (Input.GetKeyDown(key) && active)
        {
            Destroy(note.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        active = true;
        Debug.Log("touched");
        if (other.gameObject.tag == "Note")
        {
            note = other.gameObject;
        }

    }

    void OnTriggerExit2D(Collider2D other)
    {
        active = false;
    }

    IEnumerator Clicked() {
        Color original = sr.color;
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        sr.color = original;
    }
}
