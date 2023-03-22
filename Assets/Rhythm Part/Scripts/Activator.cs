using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Activator : MonoBehaviour
{
    [SerializeField] SpriteRenderer sr;
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

    public GameObject fastNote;

    SongManager sm;

    AudioSource hit;
    AudioSource miss;

    void Awake()
    {
        sm = GameObject.Find("SongManager").GetComponent<SongManager>();

        miss = GameObject.Find("SFX/Rhythm/Miss").GetComponent<AudioSource>();
        hit = GameObject.Find("SFX/Rhythm/Hit").GetComponent<AudioSource>();
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
            sr.color = new Color(0, 1, 1);
            // holding = true;

        }
        if (Input.GetKeyUp(key))
        {
            sr.color = original;
            // holding = false;
        }
        if (Input.GetKeyDown(key))
        {
            hit.Play();
            if (miss.isPlaying)
            {
                miss.Stop();
            }
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
                Instantiate(normalEffect, effectZone.position, normalEffect.transform.rotation);
            }

            else if (Mathf.Abs(note.transform.position.y) > 0.50f)
            {
                AddGoodScore();
                Instantiate(goodEffect, effectZone.position, goodEffect.transform.rotation);
            }

            else if (Mathf.Abs(note.transform.position.y) >= 0)
            {
                AddPerfectScore();
                Instantiate(perfectEffect, effectZone.position, perfectEffect.transform.rotation);
            }

            Destroy(note.gameObject);

            active = false;
        }

        else if (Input.GetKeyDown(key) && !active && fastNote)
        {
            Destroy(fastNote.gameObject);
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
        miss.Play();
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
