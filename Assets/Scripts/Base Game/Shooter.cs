using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private int damage;

    /// <summary>
    /// Стрельба в определенном направлении для врагов
    /// </summary>
    /// <param name="direction">Направление выстрела</param>
    public void Shoot(Vector2 direction)
    {
        Bullet curBullet = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        curBullet.Damage = damage;
        Rigidbody2D CurrentBulVel = curBullet.GetComponent<Rigidbody2D>();
        CurrentBulVel.velocity = direction.normalized * bulletSpeed;
    }
}
