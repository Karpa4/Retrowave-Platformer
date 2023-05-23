using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Shooter shooter;
    [SerializeField] private float timeBetweenShots;
    [SerializeField] private PlayerAnim playerAnim;
    [SerializeField] private PlayerInput playerInput;

    private float nextTimeToShot = 0;

    private void Start()
    {
        playerInput.AttackEvent += TryAttack;
    }

    private void TryAttack()
    {
        if (Time.time > nextTimeToShot)
        {
            nextTimeToShot = Time.time + timeBetweenShots;
            playerAnim.PlayAttack();
        }
    }

    public void PlayerShoot()
    {
        shooter.Shoot(transform.right);
    }

    public void Cleanup()
    {
        playerInput.AttackEvent -= TryAttack;
    }
}
