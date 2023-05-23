using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionTrap : MonoBehaviour
{
    [SerializeField] private float _boomRadius;
    [SerializeField] private float _smashForce;
    [SerializeField] private int _maxDamage;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private float _delay;
    [SerializeField] private GameObject _explosionAnim;
    private Animation _animation;

    private void Awake()
    {
        _animation = GetComponent<Animation>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _animation.Play();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(_delay);
        _explosionAnim.SetActive(true);
        Explosion();
        yield return new WaitForSeconds(0.66f);
        Destroy(gameObject);
    }

    private void Explosion()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, _boomRadius, _mask);
        foreach (Collider2D item in cols)
        {
            Vector2 direction = item.transform.position - transform.position;
            float distance = direction.magnitude;
            float value = (_boomRadius - distance) / _boomRadius;
            if (value > 0)
            {
                item.GetComponent<Rigidbody2D>().AddForce(direction.normalized * value * _smashForce, ForceMode2D.Impulse);
                item.GetComponent<CharacterHealth>().TakeDamage((int)Mathf.Round(value * _maxDamage));
            }
        }
    }
}
