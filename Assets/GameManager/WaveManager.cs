public static class WaveManager
{
    public enum Phase : int
    {
        low = 400,
        mid = 1800,
        high = 6075,
        highest = 15975,
        ultimate1 = 20000,
        ultimate2 = 50000,
        //ultimate3 = 60000
    }

    private static int _waitSpawn = 7;
    public static int WaitSpawn { get { return _waitSpawn; } }

    private static float[] timeWaves = new float[4]
    {
        10, //duration of lower-ranking waves
        40, //duration of intermediate-rank waves
        60, //duration of higher-ranking waves
        120  //duration of supreme-ranking waves
    };

    public static float[] TimeWaves { get { return timeWaves; } }

    private static int[] minMaxNbDemon = new int[20]
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
        10, 20
    };

    public static int[] MinMaxNbDemon { get { return minMaxNbDemon; } }



    // add higher ranked demons
    public static readonly int MaxScoreWave1 =   400;
    public static readonly int MaxScoreWave2 =  1800;
    public static readonly int MaxScoreWave3 =  6075;
    public static readonly int MaxScoreWave4 = 15975;

    // remove lower ranked demons
    /*public static readonly int MaxScoreWave5 = 400;
    public static readonly int MaxScoreWave6 = 1800;
    public static readonly int MaxScoreWave7 = 6075;
    public static readonly int MaxScoreWave8 = 15975;*/
}
