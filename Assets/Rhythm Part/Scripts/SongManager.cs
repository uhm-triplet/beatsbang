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
    float[] notesLeft = { 2.85f, 3.05f, 3.25f, 3.45f, 3.65f, //basic pattern: 0.2f, next pattern 0.9f
                         4.55f, 4.75f, 4.95f, 5.15f, 5.35f,
                         6.25f, 6.45f, 6.65f, 6.85f, 7.05f,
                        7.95f, 8.15f, 8.35f, 8.55f, 8.75f,
                        9.65f, 9.85f, 10.05f, 10.25f, 10.45f,
                        11.35f, 11.55f, 11.75f, 11.95f, 12.15f,
                        13.05f, 13.25f, 13.45f, 13.65f, 13.85f,
                            14.85f, 15.05f, 15.25f, 15.45f,
                            16.55f, 16.75f, 16.95f, 17.15f,
                            18.25f, 18.45f, 18.65f, 18.85f,
                            19.95f, 20.15f, 20.35f, 20.55f,
                        // left right switch
                        21.55f, 21.75f, 21.95f, 22.75f, 23.35f, 23.75f, 24.55f,
                        25.05f, 25.65f,
                        26.75f, 27.15f, 27.55f,

                        28.35f, 28.80f, 29.65f, 30.05f, 30.55f, 30.75f, 31.35f,

                        //연타
                        31.95f, 32.35f, 32.55f,

                        33.70f, 34.10f, 34.30f,

                        37.05f, 37.45f, 37.85f, 38.25f,

                        // temp
                            43.90f, 44.75f, 45.60f, 46.45f,
                            50.75f, 51.60f, 52.45f, 53.30f,
                            57.60f, 58.45f, 59.30f, 60.15f,
                            64.45f, 65.30f, 66.15f, 67.00f,
                            71.30f, 72.15f, 73.00f, 73.85f,
                            78.15f, 79.00f, 79.85f, 80.70f,
                            85.00f, 85.85f, 86.70f, 87.55f,
                            91.85f, 92.70f, 93.55f, 94.40f,
                          };
    float[] notesRight = { 4.05f, 5.75f, 7.55f, // pattern 1: 
                            7.95f, 8.55f, 9.15f,
                            9.65f, 10.25f, 10.85f,
                            11.35f, 11.95f, 12.55f,
                            13.05f, 13.65f, 14.25f,
                            14.75f, 14.95f, 15.15f, 15.75f, 15.95f, 16.45f, 16.85f, 17.65f,
                            18.45f, 18.85f, 19.45f,
                            20.35f, 20.55f, 21.15f,
                            
                            // left right switch
                            23.45f, 23.65f, 23.85f, 24.05f, 24.25f,
                            25.15f, 25.35f, 25.55f, 25.75f, 25.95f,
                            26.85f, 27.05f, 27.25f, 27.45f, 27.65f,
                            28.55f, 28.75f, 29.15f, 29.35f,
                            30.25f, 30.45f, 30.85f, 31.05f,

                            //연타
                            31.75f, 32.25f, 32.45f, 33.00f,

                            33.50f, 34.00f, 34.20f,

                            35.35f, 35.75f, 36.15f, 36.55f,

                            //Temp
                            43.90f, 44.75f, 45.60f, 46.45f,
                            50.75f, 51.60f, 52.45f, 53.30f,
                            57.60f, 58.45f, 59.30f, 60.15f,
                            64.45f, 65.30f, 66.15f, 67.00f,
                            71.30f, 72.15f, 73.00f, 73.85f,
                            78.15f, 79.00f, 79.85f, 80.70f,
                            85.00f, 85.85f, 86.70f, 87.55f,
                            91.85f, 92.70f, 93.55f, 94.40f,


    };

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
    Vector2 removePosLeft = new Vector2(0, -20);
    Vector2 removePosRight = new Vector2(5, -20);
    //the number of beats in each loop
    [SerializeField] GameObject note;
    [SerializeField] GameObject holdNote;

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