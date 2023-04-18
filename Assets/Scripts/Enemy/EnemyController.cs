using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _timeToRevert;
    private EnemyHelper _helper;
    private Animator _anim;
    private Rigidbody2D _rigidBody;
    private float _nextTimeToShot = 0;
    private float _currentTimeToRevert;
    private Quaternion _previousRotation;
    private CharacterHealth _health;

    private const float IDLE_STATE = 0;
    private const float WALK_STATE = 1;
    private const float REVERT_STATE = 2;
    private const float ATTACK_STATE = 3;
    private float _currentState;
    private float _previousState;

    private void Awake()
    {
        _helper = GetComponent<EnemyHelper>();
        _helper.ActivateAttackState += AttackOn;
        _helper.ActivateIdleState += AttackOff;
        _health = GetComponent<CharacterHealth>();
        _health.DeathEvent += EnemyIsDead;
        _moveSpeed = -_moveSpeed;
        _currentState = WALK_STATE;
        _currentTimeToRevert = 0;
        _previousState = 0;
        _anim = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void AttackOff()
    {
        _currentState = _previousState;
        transform.rotation = _previousRotation;
    }

    private void AttackOn()
    {
        _previousState = _currentState;
        _previousRotation = transform.rotation;
        _currentState = ATTACK_STATE;
        _rigidBody.velocity = Vector2.zero;
    }

    private void Update()
    {
        _helper.CheckPlayer();
        if (_currentTimeToRevert >= _timeToRevert)
        {
            _currentTimeToRevert = 0;
            _currentState = REVERT_STATE;
        }

        switch (_currentState)
        {
            case IDLE_STATE:
                _currentTimeToRevert += Time.deltaTime;
                break;

            case WALK_STATE:
                _rigidBody.velocity = Vector2.right * _moveSpeed;
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
                _moveSpeed *= -1;
                _currentState = WALK_STATE;
                break;

            case ATTACK_STATE:
                if (Time.time > _nextTimeToShot)
                {
                    _helper.EnemyShoot();
                    _nextTimeToShot = Time.time + _timeBetweenShots;
                }
                break;
        }
        _anim.SetFloat("Velocity", _rigidBody.velocity.magnitude);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyStopper"))
        {
            _currentState = IDLE_STATE;
        }
        
    }

    private void EnemyIsDead()
    {
        _rigidBody.velocity = Vector2.zero;
        _rigidBody.constraints = RigidbodyConstraints2D.FreezeAll;
        enabled = false;
    }
}
