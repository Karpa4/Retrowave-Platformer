using System.Collections;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private int _score;
    [SerializeField] private float _timeToDestroy;
    private CharacterHealth _health;
    private Animator _animator;
    private Collider2D _col;

    private void Awake()
    {
        _health = GetComponent<CharacterHealth>();
        _health.DeathEvent += EnemyIsDead;
        _animator = GetComponent<Animator>();
        _col = GetComponent<Collider2D>();
    }

    private void EnemyIsDead()
    {
        GameStats._gameStats.ChangeScore(_score);
        _animator.SetBool("IsDead", true);
        _col.enabled = false;
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(_timeToDestroy);
        Destroy(gameObject);
    }
}
