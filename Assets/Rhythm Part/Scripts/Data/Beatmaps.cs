public class Beatmaps
{
    public class Beatmap // can be a struct. Depends on usage.
    {
        // should really use properties, I know, 
        // and these should probably not be static
        public float[] leftNote;
        public float[] rightNote;

        public Beatmap(float[] l, float[] r)
        {
            leftNote = l;
            rightNote = r;
        }
    }
    static float[] demo1Left =  { 2.85f, 3.05f, 3.25f, 3.45f, 3.65f, //basic pattern: 0.2f, next pattern 0.9f
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
    static float[] demo1Right = { 4.05f, 5.75f, 7.55f, // pattern 1: 
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
    static Beatmap demo1 = new Beatmap(demo1Left, demo1Right);

    public static Beatmap[] mySongs = new Beatmap[] { demo1 };


}