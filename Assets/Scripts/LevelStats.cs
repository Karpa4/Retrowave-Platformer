using System;

[Serializable]
public class LevelStats
{
    public int MaxLevelScore;
    public int CurrentScore;
    public int Percent;

    public LevelStats(int maxScore, int currentScore, int percent)
    {
        MaxLevelScore = maxScore;
        CurrentScore = currentScore;
        Percent = percent;
    }
}
