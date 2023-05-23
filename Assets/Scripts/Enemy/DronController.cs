using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class DronController : BaseEnemy
{
    [SerializeField] private float dataRefreshDelay;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform startPointToMove;

    private bool isAlive;
    private bool isAttack;

    protected override void Awake()
    {
        base.Awake();

        isAlive = true;
        isAttack = false;
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.SetDestination(startPointToMove.position);
        Finder.CheckRotation(startPointToMove);
        StartCoroutine(DronMainCoroutine());
    }

    protected override void AttackModeOn()
    {
        agent.isStopped = true;
        isAttack = true;
    }

    protected override void AttackModeOff()
    {
        agent.isStopped = false;
        isAttack = false;
    }

    public override void EnemyIsDead()
    {
        isAlive = false;
        base.EnemyIsDead();
    }

    private IEnumerator DronMainCoroutine()
    {
        while (isAlive)
        {
            if (!isAttack && CurrentTarget != null)
            {
                agent.SetDestination(CurrentTarget.position);
            }

            yield return new WaitForSeconds(dataRefreshDelay);
            if (isAttack)
            {
                CheckAttackTiming();
            }
        }
    }
}
