using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    private int _damage;

    public int Damage
    {
        set
        {
            _damage = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterHealth>(out CharacterHealth health))
        {
            health.TakeDamage(_damage);
        }
        Destroy(gameObject);
    }
}
