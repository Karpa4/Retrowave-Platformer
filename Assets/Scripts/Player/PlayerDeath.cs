using UnityEngine;

public class PlayerDeath : BaseDeath
{
    [SerializeField] private PlayerInput input;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private PlayerMovement movement;

    protected override void IsDead()
    {
        movement.Cleanup();
        playerAttack.Cleanup();
        input.SwitchControl(false);
        base.IsDead();
    }
}
