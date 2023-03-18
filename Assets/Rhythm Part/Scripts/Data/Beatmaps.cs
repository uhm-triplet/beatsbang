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

                        40.35f, 40.55f, 40.75f, 40.95f, 41.15f,

                        42.50f, 43.35f,
                        

                        // temp
                            43.90f, 44.75f, 45.60f, 46.45f,

//빈 부분
                            46.65f, 47.95f,  48.35f,  48.75f, 49.95f,  50.35f, 
//빈 부분

                            50.75f, 51.60f, 52.45f, 53.30f,
                            //
                            53.90f, 54.10f, 54.50f, 54.90f,
                            55.50f, 55.70f, 55.90f,
                            56.20f, 56.60f, 57.20f, 57.40f,
                            //

                            57.60f, 58.025f, 58.45f, 58.875f, 59.30f, 59.725f, 60.15f,

                            //
                            60.35f,60.75f,61.15f,61.55f,61.95f,
                            62.35f,62.75f,63.15f, 63.55f,63.95f,
                            //

                            64.45f, 65.30f, 65.58f, 65.87f, 66.15f, 66.43f, 66.72f, 67.00f,

                            //
                            67.20f, 67.80f,68.60f, 68.80f,69.20f, 69.40f,
                            69.80f,70.20f,70.60f, 71.00f,
                            //

                            71.30f, 71.72f, 72.15f, 72.575f, 73.00f, 73.425f, 73.85f,

                            //  
                            74.05f,74.45f,74.85f, 75.05f,
                            75.45f,75.85f,76.05f,76.45f,
                            76.85f,77.05f,77.45f, 77.85f,
                            //

                            78.15f, 79.00f, 79.283f, 79.566f, 79.85f, 80.70f,

                            //
                            81.10f,81.50f,82.10f,82.50f,
                            83.10f,83.50f,84.10f,84.50f,
                            //

                            85.00f, 85.85f, 86.70f, 87.55f,

                            //
                            88.15f, 88.35f, 88.75f, 89.15f,
                            89.75f, 89.95f, 90.35f, 90.75f,
                            91.35f, 91.55f,
                            //
                            91.85f, 92.70f, 93.55f, 94.40f,

                            95.25f
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

                            38.65f, 38.85f, 39.05f, 39.25f, 39.45f,

                            42.05f, 42.25f,  42.70f, 42.90f, 

                            //Temp
                            44.325f, 44.75f, 45.175f, 46.00f,

                            46.85f, 48.15f, 48.55f, 50.15f, 50.55f,

                            50.75f, 51.175f, 51.60f, 52.025f, 52.45f, 52.875f, 53.30f,

                            //
                            53.50f, 53.70f,  54.30f,
                            54.70f,  55.10f, 55.30f,
                             56.10f,
                            56.40f,  56.80f, 57.00f, 
                            //

                            57.60f, 58.45f, 59.30f, 60.15f,

                            //
                             60.55f,  60.95f,
                            61.35f,  61.75f,  62.15f,
                             62.55f,  62.95f,
                            63.35f, 63.75f,  64.15f,
                            //

                            64.45f, 64.73f, 65.02f, 65.30f, 66.15f, 66.43f, 66.72f, 67.00f,

                            //
                            67.40f, 67.60f, 68.00f,
                            68.20f, 68.40f,  69.00f,
                             69.60f,  70.00f,
                             70.40f,  70.80f, 
                            //

                            71.30f, 71.72f, 72.15f, 72.575f, 73.00f, 73.425f, 73.85f,

                            //
                            74.25f,  74.65f,
                            75.25f,  75.65f,
                             76.25f,  76.65f,
                             77.25f,  77.65f,
   
                            //

                            78.15f, 78.433f, 78.716f, 79.00f, 79.85f, 80.133f, 80.416f, 80.70f,

                            //
                            80.90f,  81.30f,  81.70f,
                            81.90f,  82.30f,  82.70f,
                            82.90f,  83.30f,  83.70f,
                            83.90f,  84.30f,  84.70f, 
                            //

                            85.00f, 85.85f, 86.70f, 87.55f,

                            //
                            87.75f, 87.95f,  88.55f,
                             88.95f,  89.35f, 89.55f,
                             90.15f,  90.55f,
                             90.95f, 91.15f, 

                            //

                            91.85f, 92.70f, 93.55f, 94.40f,
                            95.25f
                    };
    static Beatmap demo1 = new Beatmap(demo1Left, demo1Right);


    static float[] Stage2Left =
    {
    2.907f, 3.923f, 4.777f, 4.977f, 5.20f, 5.435f, 6.155f,

    7.482f, 8.50f, 8.859f, 9.352f, 9.600f, 9.819f, 10.203f, 11.219f, 12.073f, 12.273f, 12.496f, 12.749f, 13.451f,

    //pattern 1
    14.844f, 15.862f, 16.221f, 16.714f, 16.962f, 17.181f, 17.565f, 18.581f, 19.435f, 19.635f, 19.858f, 20.111f, 20.813f,

    //pattern 1
    22.28f, 23.298f, 23.657f, 24.15f, 24.398f, 24.617f, 25.001f, 26.017f, 26.871f, 27.071f, 27.294f, 27.547f, 28.249f,

    //pattern 2
    37.097f, 37.984f, 38.395f, 38.938f, 39.706f, 40.249f, 40.911f, 41.798f, 42.209f, 42.752f, 43.52f, 44.063f,

    //pattern 1 switch
    44.971f, 45.83f, 46.79f, 47.681f, 48.574f, 49.467f, 49.985f, 50.185f, 50.422f,

    //pattern 1 switch
    52.36f, 53.219f, 54.179f, 55.07f, 55.963f, 56.856f, 57.374f, 57.574f, 57.811f,

    //pattern 1
    59.178f, 60.196f, 60.555f, 61.048f, 61.296f, 61.515f, 61.899f, 62.915f, 63.769f, 63.969f, 64.192f, 64.445f, 65.147f,

    66.541f, 67.559f, 67.918f, 68.411f, 68.659f, 68.878f, 69.262f, 70.278f, 71.132f, 71.332f, 71.555f, 71.808f, 72.51f,

    //pattern 3 - 1
    74.474f, 75.333f, 76.293f, 76.677f, 77.693f, 78.547f, 78.747f, 78.97f, 79.223f, 79.925f,

    //pattern 3 - 2
    81.319f, 82.337f, 82.696f, 83.189f, 83.437f, 83.656f, 84.547f, 85.44f, 86.333f, 86.851f, 87.051f, 87.288f,

    //pattern 2 - both hand
    88.721f, 89.608f, 90.019f, 90.562f, 91.33f, 91.873f, 92.535f, 93.422f, 93.833f, 94.376f, 95.144f, 95.687f,

    96.11f, 96.997f, 97.408f, 97.951f, 98.719f, 99.262f, 99.924f, 100.811f, 101.222f, 101.765f, 102.533f, 103.076f

    };
    static float[] Stage2Right = {
    3.414f, 4.307f, 5.20f, 5.718f, 5.918f, 6.155f,

    8.00f, 8.859f, 9.819f, 10.71f, 11.603f, 12.496f, 13.014f, 13.214f, 13.451f,

    //pattern 1
    15.362f, 16.221f, 17.181f, 18.072f, 18.965f, 19.858f, 20.376f, 20.576f, 20.813f,

    //pattern 1
    22.798f, 23.657f, 24.617f, 25.508f, 26.401f, 27.294f, 27.812f, 28.012f, 28.249f,

    //pattern 2
    29.708f, 30.595f, 31.006f, 31.549f, 32.317f, 32.860f, 33.522f, 34.409f, 34.82f, 35.363f, 36.131f, 36.674f,

    //pattern 1 switch
    44.453f, 45.471f, 45.83f, 46.323f, 46.571f, 46.79f, 47.174f, 48.19f, 49.044f, 49.244f, 49.467f, 49.72f, 50.422f,

    //pattern 1 switch
    51.842f, 52.86f, 53.219f, 53.712f, 53.96f, 54.179f, 54.563f, 55.579f, 56.433f, 56.633f, 56.856f, 57.109f, 57.811f,

    //pattern 1
    59.696f, 60.555f, 61.515f, 62.406f, 63.299f, 64.192f, 64.71f, 64.91f, 65.147f,

    //pattern 1
    67.059f, 67.918f, 68.878f, 69.769f, 70.662f, 71.555f, 72.073f, 72.273f, 72.51f,

    //pattern 3 - 1
    73.956f, 74.974f, 75.333f, 75.826f, 76.074f, 76.293f, 77.184f, 78.077f, 78.97f, 79.488f, 79.688f, 79.925f,

    //pattern 3 - 2
    81.837f, 82.696f, 83.656f, 84.04f, 85.056f, 85.91f, 86.11f, 86.333f, 86.586f, 87.288f,

    //pattern 2 - both hand
    88.721f, 89.608f, 90.019f, 90.562f, 91.33f, 91.873f, 92.535f, 93.422f, 93.833f, 94.376f, 95.144f, 95.687f,

    96.11f, 96.997f, 97.408f, 97.951f, 98.719f, 99.262f, 99.924f, 100.811f, 101.222f, 101.765f, 102.533f, 103.076f

                    };
    static Beatmap Stage2 = new Beatmap(Stage2Left, Stage2Right);




    static float[] Stage4Right = {
    //run
    2.0f, 3.0f, 4.0f, 5.0f, 6.0f ,7.0f,
    //2
    13.84f, 14.34f, 14.62f, 14.858f, 15.20f, 15.37f, 15.71f, 15.97f, 16.22f, 
    //1.1
    19.124f,
    //2.1
    19.304f, 19.804f, 20.084f, 20.322f, 20.664001f, 20.834f, 21.174f, 21.434002f, 21.684f
    };

    static float[] Stage4Left = {
    //run
    2.5f, 3.5f, 4.5f, 5.5f, 6.5f, 7.5f,
    //1
    11.28f, 11.63f, 11.91f, 12.13f, 12.46f, 12.66f, 12.98f, 13.25f, 13.50f, 13.66f,
    // 1.1
    16.401f, 16.744f, 16.744f, 17.094f, 17.374f, 17.594f, 17.924f, 18.124f, 18.444f, 18.964f,
    //2.1
    21.852f,
    //1.2
    22.195f, 22.545f, 22.825f, 23.045f

    };

    static Beatmap Stage4 = new Beatmap(Stage4Left, Stage4Right);

    public static Beatmap[] mySongs = new Beatmap[] { demo1, Stage2, Stage4 };


}