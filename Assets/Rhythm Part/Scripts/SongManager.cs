using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongManager : MonoBehaviour
{

    //the current position of the song (in seconds)
    public float songPosition;

    //how much time (in seconds) has passed since the song started
    float dsptimesong;

    // Decides the speed of the note. Higher this value, slower the note.
    float timeBeforeHit = 2f;

    //keep all the position-in-time of notes in the song
    float[] notesLeft = { 2.55f, 3.09f, 4.09f, 4.54f, 5.11f, 5.67f, 6.18f, 6.71f, 7.55f, 8.09f, 9.09f, 9.54f, 10.11f, 10.67f, 11.18f, 11.71f };
    float[] notesRight = { 2.55f, 3.09f, 4.09f, 4.54f, 5.11f, 5.67f, 6.18f, 6.71f, 7.55f, 8.09f, 9.09f, 9.54f, 10.11f, 10.67f, 11.18f, 11.71f };

    //the index of the next note to be spawned
    int nextIndexLeft = 0;
    int nextIndexRight = 0;

    Vector2 spawnPosLeft = new Vector2(0, 40);
    Vector2 spawnPosRight = new Vector2(5, 40);
    Vector2 hitPosLeft = new Vector2(0, 0);
    Vector2 hitPosRight = new Vector2(5, 0);
    Vector2 removePosLeft = new Vector2(0, -20);
    Vector2 removePosRight = new Vector2(5, -20);
    //the number of beats in each loop
    [SerializeField] GameObject note;

    void Start()
    {
        //record the time when the song starts
        dsptimesong = (float)AudioSettings.dspTime;

        //start the song
        GetComponent<AudioSource>().Play();
    }
    void Update()
    {
        //calculate the position in seconds
        songPosition = (float)(AudioSettings.dspTime - dsptimesong);

        if (nextIndexLeft < notesLeft.Length && (notesLeft[nextIndexLeft] - timeBeforeHit) < songPosition)
        {
            Note n = Instantiate(note, spawnPosLeft, Quaternion.identity).GetComponent<Note>();
            //initialize the fields of the music note
            n.SpawnPos = spawnPosLeft;
            n.HitPos = hitPosLeft;
            n.RemovePos = removePosLeft;
            n.TimeBeforeHit = timeBeforeHit;
            n.SpawnTime = songPosition;

            nextIndexLeft++;
        }

        if (nextIndexRight < notesRight.Length && (notesRight[nextIndexRight] - timeBeforeHit) < songPosition)
        {
            Note n = Instantiate(note, spawnPosRight, Quaternion.identity).GetComponent<Note>();
            // Debug.Log($"{songPosition}");
            //initialize the fields of the music note
            n.SpawnPos = spawnPosRight;
            n.HitPos = hitPosRight;
            n.RemovePos = removePosRight;
            n.TimeBeforeHit = timeBeforeHit;
            n.SpawnTime = songPosition;

            nextIndexRight++;
        }
    }
}