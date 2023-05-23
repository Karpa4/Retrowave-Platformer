using UnityEngine;
using System.Collections;

public class Turret : BaseEnemy
{
    [SerializeField] private float dataRefreshDelay;
    [SerializeField] private BaseAnim baseAnim;

    private bool isAttack;
    private bool coroutineIsActive;

    protected override void Awake()
    {
        base.Awake();
        coroutineIsActive = false;
        isAttack = false;
    }

    protected override void AttackModeOff()
    {
        isAttack = false;
    }

    protected override void AttackModeOn()
    {
        isAttack = true;
        if (!coroutineIsActive)
        {
            StartCoroutine(TurretAttack());
        }
    }

    protected override void BaseAttack()
    {
        baseAnim.PlayAttack();
    }

    public override void EnemyIsDead()
    {
        isAttack = false;
        base.EnemyIsDead();
    }

    private IEnumerator TurretAttack()
    {
        coroutineIsActive = true;
        while (isAttack)
        {
            yield return new WaitForSeconds(dataRefreshDelay);
            CheckAttackTiming();
        }
        coroutineIsActive = false;
    }
}
