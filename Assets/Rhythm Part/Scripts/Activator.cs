using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activator : MonoBehaviour
{
    SpriteRenderer sr;
    public KeyCode key;
    bool active = false;
    public bool isResetZone = false;
    bool barActive = false;
    GameObject note, gm;
    GameObject holdBar;
    Color original;

    // public bool createMode;
    // public GameObject createNote;

    public GameObject normalEffect, goodEffect, perfectEffect, missEffect;
    public Transform effectZone;


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
            gm.GetComponent<GameManagerRhythm>().AddStreak();

            if (Mathf.Abs(note.transform.position.y) > 0.75f)
            {
                AddNormalScore();
                Debug.Log("Normal Hit");
                Instantiate(normalEffect, effectZone.position, normalEffect.transform.rotation);
            }

            else if (Mathf.Abs(note.transform.position.y) > 0.50f)
            {
                AddGoodScore();
                Debug.Log("Good Hit");
                Instantiate(goodEffect, effectZone.position, goodEffect.transform.rotation);
            }

            else if (note.transform.position.y >= 0)
            {
                AddPerfectScore();
                Debug.Log("Perfect Hit");
                Instantiate(perfectEffect, effectZone.position, perfectEffect.transform.rotation);
            }
            if (note)
            {
                note.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                note.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(note.gameObject, 0.1f);
            }

            //AddScore();
            active = false;
        }
        else if (Input.GetKeyDown(key) && !active && isResetZone)
        {
            if (note)
            {
                note.gameObject.GetComponent<SpriteRenderer>().enabled = false;
                note.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                Destroy(note.gameObject, 0.1f);
            }

            OnFail();
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

        Instantiate(missEffect, effectZone.position, missEffect.transform.rotation);
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

    void AddNormalScore()
    {
        // PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gm.GetComponent<GameManagerRhythm>().GetScore());
        gm.GetComponent<GameManagerRhythm>().score += gm.GetComponent<GameManagerRhythm>().NormalHit();
    }

    void AddGoodScore()
    {
        // PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gm.GetComponent<GameManagerRhythm>().GetScore());
        gm.GetComponent<GameManagerRhythm>().score += gm.GetComponent<GameManagerRhythm>().GoodHit();
    }

    void AddPerfectScore()
    {
        // PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + gm.GetComponent<GameManagerRhythm>().GetScore());
        gm.GetComponent<GameManagerRhythm>().score += gm.GetComponent<GameManagerRhythm>().PerfectHit();
    }

    // IEnumerator Clicked()
    // {
    //     Color original = sr.color;
    //     sr.color = new Color(0, 0, 0);
    //     yield return new WaitForSeconds(0.1f);
    //     sr.color = original;
    // }
}
