using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [SerializeField] private PlayerFinder playerFinder;
    [SerializeField] private Shooter shooter;
    [SerializeField] private float timeBetweenShots;

    private float nextTimeToShot;
    private Transform currentTarget;

    public PlayerFinder Finder { get => playerFinder; }
    public Transform CurrentTarget { get => currentTarget; }

    protected virtual void Awake()
    {
        nextTimeToShot = 0;
        playerFinder.ChangeTarget += GetNewTarget;
    }

    public virtual void BaseShoot()
    {
        Vector2 direction = currentTarget.position - transform.position;
        shooter.Shoot(direction);
    }

    public virtual void EnemyIsDead()
    {
        playerFinder.ChangeTarget -= GetNewTarget;
    }

    protected virtual void GetNewTarget(Transform target)
    {
        if (target == null)
        {
            AttackModeOff();
        }
        else
        {
            currentTarget = target;
            AttackModeOn();
        }
    }

    protected virtual void AttackModeOn() { }

    protected virtual void AttackModeOff() { }

    public void CheckAttackTiming()
    {
        if (Time.time > nextTimeToShot)
        {
            BaseAttack();
            nextTimeToShot = Time.time + timeBetweenShots;
        }
    }

    protected virtual void BaseAttack()
    {
        BaseShoot();
    }
}
