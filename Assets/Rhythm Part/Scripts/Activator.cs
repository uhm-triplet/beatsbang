using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    GameObject note, gm;
    Color original;

    public bool createMode;
    public GameObject createNote;


    void Awake() {
        sr = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start() {
        gm = GameObject.Find("GameManagerRhythm");
        original = sr.color;
    }

    // Update is called once per frame
    void Update() {
        if (createMode) {
            if (Input.GetKeyDown(key)) {
                Instantiate(createNote, transform.position, Quaternion.identity);
            }
        }

        else {
            if (Input.GetKeyDown(key)) {
                StartCoroutine(Clicked());
            }

            if (Input.GetKeyDown(key) && active)
            {
                Destroy(note.gameObject);
                gm.GetComponent<GameManagerRhythm>().AddStreak();
                AddScore();
                active = false;
            } 
            
            else if (Input.GetKeyDown(key) &&! active) {
                gm.GetComponent<GameManagerRhythm>().ResetStreak();
            }
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
        gm.GetComponent<GameManagerRhythm>().ResetStreak();
    }

    void AddScore() {
        PlayerPrefs.SetInt("Score",PlayerPrefs.GetInt("Score")+gm.GetComponent<GameManagerRhythm>().GetScore());
        Debug.Log(PlayerPrefs.GetInt("Score"));
    }

    IEnumerator Clicked() {
        Color original = sr.color;
        sr.color = new Color(0, 0, 0);
        yield return new WaitForSeconds(0.1f);
        sr.color = original;
    }
}
