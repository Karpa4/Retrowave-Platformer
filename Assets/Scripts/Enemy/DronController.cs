using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class DronController : MonoBehaviour
{
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _dataRefreshDelay;
    private EnemyHelper _helper;
    private NavMeshAgent _agent;
    private bool _isAlive = true;
    private float _nextTimeToShot = 0;
    private bool _isAttack = false;
    private CharacterHealth _health;

    private void Awake()
    {
        _helper = GetComponent<EnemyHelper>();
        _helper.ActivateAttackState += AttackOn;
        _helper.ActivateIdleState += AttackOff;
        _helper.CheckRotation();
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _health = GetComponent<CharacterHealth>();
        _health.DeathEvent += DronIsDead;
        StartCoroutine(DronMainCoroutine());
    }

    private void AttackOn()
    {
        _agent.isStopped = true;
        _isAttack = true;
    }

    private void AttackOff()
    {
        _agent.isStopped = false;
        _isAttack = false;
    }

    private void DronIsDead()
    {
        _isAlive = false;
    }

    private IEnumerator DronMainCoroutine()
    {
        while (_isAlive)
        {
            if (!_isAttack)
            {
                _agent.SetDestination(_helper.GetPlayerPosition());
                _helper.CheckRotation();
            }

            yield return new WaitForSeconds(_dataRefreshDelay);
            _helper.CheckPlayer();
            if (_isAttack)
            {
                if (Time.time > _nextTimeToShot)
                {
                    _nextTimeToShot = Time.time + _timeBetweenShots;
                    _helper.EnemyShoot();
                }
            }
        }
    }
}
