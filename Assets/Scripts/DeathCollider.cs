using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<CharacterHealth>(out CharacterHealth health))
        {
            health.TakeDamage(300);
        }
        
    }
}
