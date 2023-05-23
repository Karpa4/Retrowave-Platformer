using UnityEngine;

public class EnemyController : BaseEnemy
{
    [SerializeField] private BaseAnim baseAnim;
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeToRevert;

    private float currentTimeToRevert;
    private Quaternion previousRotation;

    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;
    private const float ATTACK_STATE = 3;
    private float currentState;
    private float previousState;

    protected override void Awake()
    {
        base.Awake();
        currentState = WALK_STATE;
        currentTimeToRevert = 0;
        previousState = 0;
        moveSpeed = -moveSpeed;
    }

    protected override void AttackModeOff()
    {
        currentState = previousState;
        transform.rotation = previousRotation;
    }

    protected override void AttackModeOn()
    {
        previousState = currentState;
        previousRotation = transform.rotation;
        currentState = ATTACK_STATE;
        rigidBody.velocity = Vector2.zero;
    }

    private void Update()
    {
        if (currentTimeToRevert >= timeToRevert)
        {
            currentTimeToRevert = 0;
            currentState = REVERT_STATE;
        }

        switch (currentState)
        {
            case IDLE_STATE:
                currentTimeToRevert += Time.deltaTime;
                break;

            case WALK_STATE:
                rigidBody.velocity = Vector2.right * moveSpeed;
                break;

            case REVERT_STATE:
                if (transform.rotation == new Quaternion(0, 1, 0, 0))
                {
                    transform.rotation = new Quaternion(0, 0, 0, 1);
                }
                else
                {
                    transform.rotation = new Quaternion(0, 1, 0, 0);
                }
                moveSpeed *= -1;
                currentState = WALK_STATE;
                break;

            case ATTACK_STATE:
                CheckAttackTiming();
                break;
        }
        baseAnim.SetVelocity(rigidBody.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyStopper"))
        {
            currentState = IDLE_STATE;
        }
    }

    public override void EnemyIsDead()
    {
        rigidBody.velocity = Vector2.zero;
        rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
        base.EnemyIsDead();
    }
}
