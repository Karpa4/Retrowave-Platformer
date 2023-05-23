using UnityEngine;

public class OnAndOffTrap : MonoBehaviour
{
    [SerializeField] private Collider2D _coll;

    /// <summary>
    /// Влючение и выключение коллайдера для электрической ловушки
    /// </summary>
    public void Turn()
    {
        _coll.enabled = !_coll.enabled;
    }
}
