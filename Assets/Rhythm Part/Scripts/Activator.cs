using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    bool barActive = false;
    GameObject note, gm;
    GameObject holdBar;
    Color original;

    // public bool createMode;
    // public GameObject createNote;

    SongManager sm;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sm = GameObject.Find("SongManager").GetComponent<SongManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManagerRhythm");
        original = sr.color;
    }

    // Update is called once per frame
    void Update()
    {
        ActivatorClick();
        SingleNote();
    }

    void ActivatorClick()
    {
        if (Input.GetKey(key))
        {
            Color original = sr.color;
            sr.color = new Color(0, 0, 0);
            // holding = true;

        }
        if (Input.GetKeyUp(key))
        {
            sr.color = original;
            // holding = false;
        }
    }
    void SingleNote()
    {
        if (Input.GetKeyDown(key) && active)
        {
            Destroy(note.gameObject);
            gm.GetComponent<GameManagerRhythm>().AddStreak();
            AddScore();
            active = false;
        }

        else if (Input.GetKeyDown(key) && !active)
        {
            gm.GetComponent<GameManagerRhythm>().ResetStreak();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Note")
        {
            active = true;
            note = other.gameObject;
        }
    }

    public void OnFail()
    {
        active = false;
        gm.GetComponent<GameManagerRhythm>().ResetStreak();
    }

    // void OnTriggerExit2D(Collider2D other)
    // {
    //     active = false;
    //     gm.GetComponent<GameManagerRhythm>().ResetStreak();
    // }

    void AddScore()
    {
        // PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gm.GetComponent<GameManagerRhythm>().GetScore());
        gm.GetComponent<GameManagerRhythm>().score += gm.GetComponent<GameManagerRhythm>().GetScore();
    }

    // IEnumerator Clicked()
    // {
    //     Color original = sr.color;
    //     sr.color = new Color(0, 0, 0);
    //     yield return new WaitForSeconds(0.1f);
    //     sr.color = original;
    // }
}
