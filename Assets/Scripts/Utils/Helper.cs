using System;

public static class Helper
{
    private static int RANDOM_SEED = 123;

    private static Random m_Random = null;

    private static void InitializeRandom()
    {
        if (m_Random == null)
        {
            m_Random = new Random(RANDOM_SEED);
        }
    }

    public static float Random()
    {
        InitializeRandom();
        return (float)m_Random.NextDouble();
    }

    public static float Random(float max)
    {
        InitializeRandom();
        return (float)m_Random.NextDouble() * max;
    }

    public static float Random(float min, float max)
    {
        InitializeRandom();
        return min + (float)m_Random.NextDouble() * (max - min);
    }

    public static int Random(int max)
    {
        InitializeRandom();
        return m_Random.Next(max);
    }

    public static int Random(int min, int max)
    {
        InitializeRandom();
        return m_Random.Next(min, max);
    }
}