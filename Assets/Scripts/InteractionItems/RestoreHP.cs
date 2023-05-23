using UnityEngine;

public class RestoreHP : CollectScore
{
    [SerializeField] private int _hpToRestore;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<CharacterHealth>(out CharacterHealth health))
        {
            health.HealByMed(_hpToRestore);
        }
        base.OnTriggerEnter2D(collision);
    }
}
