public static class WaveManager
{
    /*public enum Phase : int // score
    {
        low = 400,
        mid = 1800,
        high = 6075,
        highest = 15975,
        ultimate1 = 20000,
        ultimate2 = 50000,
        ultimate3 = 60000
    }*/
    public enum Phase : int // time
    {
        low = 30,
        mid = 60,
        high = 120,
        highest = 240,
        ultimate1 = 360,
        ultimate2 = 480,
        ultimate3 = 600
    }

    private static int _waitSpawn = 7;
    public static int WaitSpawn { get { return _waitSpawn; } }

    private static float[] timeWaves = new float[7]
    {
        10, //duration of lower-ranking waves
        40, //duration of intermediate-rank waves
        60, //duration of higher-ranking waves
        120,  //duration of supreme-ranking waves
        120,  //duration of ultimate 1-ranking waves
        120,  //duration of ultimate 2-ranking waves
        0.5f  //duration of ultimate 3-ranking waves
    };

    public static float[] TimeWaves { get { return timeWaves; } }

    private static int[] minMaxNbDemon = new int[32]
    {
        // wave low
        10, 20 ,

        //wave mid
        35, 50 ,
        10, 20 ,

        //wave high
        65, 80 ,
        35, 50 ,
        10, 20 ,

        //wave highest
        95, 110 ,
        65, 80 ,
        35, 50 ,
        10, 20,

        //wave ultimate 1
        65, 80 ,
        35, 50 ,
        10, 20 ,

        //wave ultimate 2
        35, 50 ,
        10, 20 ,

        // wave ultimate 3
        10, 20 ,
    };

    public static int[] MinMaxNbDemon { get { return minMaxNbDemon; } }
}
