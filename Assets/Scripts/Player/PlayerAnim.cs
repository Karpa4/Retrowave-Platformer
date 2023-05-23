public class PlayerAnim : BaseAnim
{
    public override void PlayAttack()
    {
        Anim.SetBool("IsAttack", true);
    }

    public void SwitchGround(bool switcher)
    {
        Anim.SetBool("IsGround", switcher);
    }
}
