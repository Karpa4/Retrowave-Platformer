using System.Collections;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    [SerializeField] private int _damage;
    [SerializeField] private float _nextDamageDelay;
    private bool _insideTrap;
    private CharacterHealth _health = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_health == null)
        {
            _health = collision.GetComponent<CharacterHealth>();
        }
        _insideTrap = true;
        StartCoroutine(TakeDamageAndDelay());
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _insideTrap = false;
    }

    private IEnumerator TakeDamageAndDelay()
    {
        _health.TakeDamage(_damage);
        yield return new WaitForSeconds(_nextDamageDelay);
        if (_insideTrap)
        {
            StartCoroutine(TakeDamageAndDelay());
        }
    }
}
