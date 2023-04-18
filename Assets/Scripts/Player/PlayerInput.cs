using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    public bool OnPauseButton = false;
    [SerializeField] private bool _isControlled;
    [SerializeField] private float _timeBetweenShots;
    private float _nextTimeToShot = 0;
    private PlayerMovement _playerMovement;
    private float _horizontal = 0;
    private Animator _animator;
    private bool _rightMove = true;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isControlled)
        {
            _horizontal = Input.GetAxis(GlobalStringVars.HORIZONTAL_AXIS);
            if (Input.GetButtonDown(GlobalStringVars.JUMP))
            {
                _playerMovement.Jump();
            }

            if (_horizontal > 0 && _rightMove == false)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _rightMove = true;
            }
            if (_horizontal < 0 && _rightMove == true)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _rightMove = false;
            }

            if (Input.GetButtonDown(GlobalStringVars.FIRE_1) && !OnPauseButton)
            {
                if (Time.time > _nextTimeToShot)
                {
                    _nextTimeToShot = Time.time + _timeBetweenShots;
                    _animator.SetBool("IsAttack", true);
                }
            }

            _animator.SetFloat("Velocity", Mathf.Abs(_horizontal));
        }
    }

    private void FixedUpdate()
    {
        if (_isControlled)
        {
            _playerMovement.PlayerMove(_horizontal);
        }
    }

    /// <summary>
    /// Отключение контроля у игрока
    /// </summary>
    public void SetOffControl()
    {
        _isControlled = false;
    }
}
