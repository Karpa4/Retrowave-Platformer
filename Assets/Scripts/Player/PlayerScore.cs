using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private int score = 0;

    public event Action<int> NewScore;

    public void AddScore(int scoreCount)
    {
        score += scoreCount;
        NewScore?.Invoke(score);
    }

    public int GetScore()
    {
        return score;
    }
}
