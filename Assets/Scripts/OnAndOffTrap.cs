using UnityEngine;

public class OnAndOffTrap : MonoBehaviour
{
    private Collider2D _coll;

    private void Awake()
    {
        _coll = GetComponent<Collider2D>();
    }

    /// <summary>
    /// Влючение и выключение коллайдера для электрической ловушки
    /// </summary>
    public void Turn()
    {
        _coll.enabled = !_coll.enabled;
    }
}
