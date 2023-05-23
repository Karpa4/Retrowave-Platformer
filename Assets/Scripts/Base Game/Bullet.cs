using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage;

    public int Damage
    {
        set
        {
            damage = value;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterHealth>(out CharacterHealth health))
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}
