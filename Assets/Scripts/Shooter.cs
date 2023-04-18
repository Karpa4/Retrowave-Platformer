using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private int _damage;

    /// <summary>
    /// Стрельба по прямой для игрока
    /// </summary>
    public void Shoot()
    {
        GameObject CurrentBullet = Instantiate(_bullet, _shootPoint.position, transform.rotation);
        CurrentBullet.GetComponent<DamageDealer>().Damage = _damage;
        Rigidbody2D CurrentBulVel = CurrentBullet.GetComponent<Rigidbody2D>();
        CurrentBulVel.velocity = transform.right * _bulletSpeed;
    }

    /// <summary>
    /// Стрельба в определенном направлении для врагов
    /// </summary>
    /// <param name="direction">Направление выстрела</param>
    public void Shoot(Vector2 direction)
    {
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        GameObject CurrentBullet = Instantiate(_bullet, _shootPoint.position, Quaternion.Euler(0, 0, rotZ));
        CurrentBullet.GetComponent<DamageDealer>().Damage = _damage;
        Rigidbody2D CurrentBulVel = CurrentBullet.GetComponent<Rigidbody2D>();
        CurrentBulVel.velocity = direction.normalized * _bulletSpeed;
    }
}
