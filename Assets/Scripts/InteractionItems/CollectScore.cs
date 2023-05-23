using UnityEngine;

public class CollectScore : MonoBehaviour, IScore
{
    [SerializeField] private int score;

    public int Score { get => score; set => score = value; }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerScore>(out PlayerScore playerScore))
        {
            playerScore.AddScore(score);
            Destroy(gameObject);
        }
    }
}
