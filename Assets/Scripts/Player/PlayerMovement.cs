using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))] 
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement vars")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    [Header("Settings")]
    [SerializeField] private Transform _groundColTransform;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private AnimationCurve _curve;
    [SerializeField] private float _jumpOffset;

    private bool _isGround = false;
    private bool _secondJumpAvailable = true;
    private Rigidbody2D _rigidBody;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _isGround = Physics2D.OverlapCircle(_groundColTransform.position, _jumpOffset, _groundMask);
        if (_isGround && !_secondJumpAvailable)
        {
            _secondJumpAvailable = true;
        }
        _animator.SetBool("IsGround", _isGround);
    }

    public void PlayerMove(float direction)
    {
        if (Mathf.Abs(direction) > 0.01f)
        {
            _rigidBody.velocity = new Vector2(_curve.Evaluate(direction) * _speed, _rigidBody.velocity.y);
        }
    }

    /// <summary>
    /// Увеличение скорости после поднятия бонуса
    /// </summary>
    /// <param name="newSpeed">Новая скорость</param>
    /// <param name="time">Длительность бонуса</param>
    /// <returns></returns>
    public IEnumerator ChangeSpeed(float newSpeed, float time)
    {
        float temp = _speed;
        _speed = newSpeed;
        yield return new WaitForSeconds(time);
        _speed = temp;
    }

    public void Jump()
    {
        if (_isGround)
        {
            _secondJumpAvailable = true;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
        }
        else if (_secondJumpAvailable)
        {
            _secondJumpAvailable = false;
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _jumpForce);
        }
    }
}
