using UnityEngine;

public class RestoreHP : MonoBehaviour
{
    [SerializeField] private int _hpToRestore;
    [SerializeField] private int _score;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<CharacterHealth>().HealByMed(_hpToRestore);
        GameStats._gameStats.ChangeScore(_score);
        Destroy(gameObject);
    }
}
