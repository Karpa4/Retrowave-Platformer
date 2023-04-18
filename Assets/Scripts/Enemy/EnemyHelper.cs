using UnityEngine;

public class EnemyHelper : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _attackRange;
    [SerializeField] private LayerMask _mask;
    private Shooter _shooter;
    private Vector2 _direction;
    private bool _detectedPlayer = false;

    public delegate void ChangeState();
    public event ChangeState ActivateAttackState;
    public event ChangeState ActivateIdleState;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
    }

    /// <summary>
    /// �������
    /// </summary>
    public void EnemyShoot()
    {
        _shooter.Shoot(_direction);
    }

    /// <summary>
    /// ���������� ������� ������ ��� NavmeshAgent
    /// </summary>
    /// <returns>������� ������</returns>
    public Vector2 GetPlayerPosition()
    {
        return _player.position;
    }

    /// <summary>
    /// ��������� �� ����� � ���� ����� � ����� �� � ���� ����������
    /// </summary>
    public void CheckPlayer()
    {
        _direction = _player.position - transform.position;
        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, _direction, _attackRange, _mask);
        if (rayInfo)
        {
            if (rayInfo.collider.CompareTag("Player"))
            {
                if (!_detectedPlayer)
                {
                    _detectedPlayer = true;
                    ActivateAttackState?.Invoke();
                }
            }
            else
            {
                if (_detectedPlayer)
                {
                    _detectedPlayer = false;
                    ActivateIdleState?.Invoke();
                }
            }
        }
        else
        {
            if (_detectedPlayer)
            {
                _detectedPlayer = false;
                ActivateIdleState?.Invoke();
            }
        }

        if (_detectedPlayer)
        {
            CheckRotation();
        }
    }

    /// <summary>
    /// ������������ ����� ���� ��� ����������
    /// </summary>
    public void CheckRotation()
    {
        Quaternion currentRotation = transform.rotation;
        if (_player.position.x > transform.position.x && currentRotation.y == 0)
        {
            transform.rotation = new Quaternion(0, 1, 0, 0);
        }
        else if (_player.position.x < transform.position.x && currentRotation.y == 1)
        {
            transform.rotation = new Quaternion(0, 0, 0, 1);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}
