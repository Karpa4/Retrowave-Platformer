using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _dataRefreshDelay;
    private EnemyHelper _helper;
    private bool _isAlive = true;
    private float _nextTimeToShot = 0;
    private bool _isAttack = false;
    private Animator _animator;
    private CharacterHealth _health;

    private void Awake()
    {
        _helper = GetComponent<EnemyHelper>();
        _health = GetComponent<CharacterHealth>();
        _health.DeathEvent += TurretIsDead;
        _animator = GetComponent<Animator>();
        _helper.ActivateAttackState += AttackOn;
        _helper.ActivateIdleState += AttackOff;
        StartCoroutine(TurretMainCoroutine());
    }

    private void AttackOn()
    {
        _isAttack = true;
    }

    private void AttackOff()
    {
        _isAttack = false;
    }

    private void TurretIsDead()
    {
        _isAlive = false;
    }

    private IEnumerator TurretMainCoroutine()
    {
        while (_isAlive)
        {
            yield return new WaitForSeconds(_dataRefreshDelay);
            _helper.CheckPlayer();
            if (_isAttack)
            {
                if (Time.time > _nextTimeToShot)
                {
                    _nextTimeToShot = Time.time + _timeBetweenShots;
                    _animator.SetTrigger("IsAttack");
                }
            }
        }
    }
}
