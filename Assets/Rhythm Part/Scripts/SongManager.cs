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
    float timeBeforeHit = 2.5f;

    //keep all the position-in-time of notes in the song
    float[] notesLeft;
    float[] notesRight;

    float[] holdNotesLeft = { 2.85f, 4.05f, 8.85f, 12.85f };
    float[] holdNotesRight = { };


    //the index of the next note to be spawned
    int nextIndexLeft = 0;
    int nextIndexRight = 0;

    int holdNextIndexLeft = 0;
    int holdNextIndexRight = 0;

    Vector2 spawnPosLeft = new Vector2(0, 40);
    Vector2 spawnPosRight = new Vector2(5, 40);
    Vector2 hitPosLeft = new Vector2(0, 0);
    Vector2 hitPosRight = new Vector2(5, 0);
    Vector2 removePosLeft = new Vector2(0, -3);
    Vector2 removePosRight = new Vector2(5, -3);
    //the number of beats in each loop
    [SerializeField] GameObject note;
    [SerializeField] GameObject holdNote;

    [SerializeField] AudioSource audioSource;
    [SerializeField] int songNumber;

    void Awake()
    {
        Beatmaps.Beatmap beatmap = Beatmaps.mySongs[songNumber];
        notesLeft = beatmap.leftNote;
        notesRight = beatmap.rightNote;
    }

    void Start()
    {
        //record the time when the song starts
        dsptimesong = (float)AudioSettings.dspTime;

        //start the song
        audioSource.Play();
    }
    void Update()
    {
        //calculate the position in seconds
        songPosition = (float)(AudioSettings.dspTime - dsptimesong);
        SingleNote(songPosition);
        // HoldingNote(songPosition);
    }

    void SingleNote(float songPosition)
    {
        if (nextIndexLeft < notesLeft.Length && (notesLeft[nextIndexLeft] - timeBeforeHit) < songPosition)
        {
            Note n = Instantiate(note, spawnPosLeft, Quaternion.identity).GetComponent<Note>();
            //initialize the fields of the music note
            n.SpawnPos = spawnPosLeft;
            n.HitPos = hitPosLeft;
            n.RemovePos = removePosLeft;
            n.TimeBeforeHit = timeBeforeHit;
            n.SpawnTime = songPosition;
            n.type = Note.Type.Left;

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
            n.type = Note.Type.Right;
            nextIndexRight++;
        }
    }

    void HoldingNote(float songPosition)
    {
        if (holdNextIndexLeft < holdNotesLeft.Length && (holdNotesLeft[holdNextIndexLeft] - timeBeforeHit) < songPosition)
        {
            HoldNote n = Instantiate(holdNote, spawnPosLeft, Quaternion.identity).GetComponent<HoldNote>();
            //initialize the fields of the music note
            n.SpawnPos = spawnPosLeft;
            n.HitPos = hitPosLeft;
            n.RemovePos = removePosLeft;
            n.TimeBeforeHit = timeBeforeHit;
            n.SpawnTime = songPosition;
            n.type = HoldNote.Type.Left;

            n.hitTime = holdNotesLeft[holdNextIndexLeft];
            n.releaseTime = holdNotesLeft[holdNextIndexLeft + 1];

            holdNextIndexLeft += 2;
        }

        if (holdNextIndexRight < holdNotesRight.Length && (holdNotesRight[holdNextIndexRight] - timeBeforeHit) < songPosition)
        {
            HoldNote n = Instantiate(holdNote, spawnPosRight, Quaternion.identity).GetComponent<HoldNote>();
            //initialize the fields of the music note
            n.SpawnPos = spawnPosRight;
            n.HitPos = hitPosRight;
            n.RemovePos = removePosRight;
            n.TimeBeforeHit = timeBeforeHit;
            n.SpawnTime = songPosition;
            n.type = HoldNote.Type.Right;

            n.releaseTime = holdNotesRight[holdNextIndexRight + 1];

            holdNextIndexRight += 2;
        }
    }
}