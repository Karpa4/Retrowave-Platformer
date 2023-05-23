using UnityEngine;

public class BaseDeath : MonoBehaviour
{
    [SerializeField] private CharacterHealth health;
    [SerializeField] private BaseAnim baseAnim;

    private void Start()
    {
        health.DeathEvent += IsDead;
    }

    protected virtual void IsDead()
    {
        health.DeathEvent -= IsDead;
        baseAnim.PlayDeath();
    }
}
