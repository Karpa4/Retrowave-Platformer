using UnityEngine;

public class OnAndOffTrap : MonoBehaviour
{
    [SerializeField] private Collider2D _coll;

    /// <summary>
    /// �������� � ���������� ���������� ��� ������������� �������
    /// </summary>
    public void Turn()
    {
        _coll.enabled = !_coll.enabled;
    }
}
