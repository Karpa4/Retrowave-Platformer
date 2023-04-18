using UnityEngine;

public class CollectScore : MonoBehaviour
{
    [SerializeField] private int _score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameStats._gameStats.ChangeScore(_score);
        Destroy(gameObject);
    }
}
