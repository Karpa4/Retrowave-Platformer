using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;

    public Animator Anim { get => anim; }

    public virtual void PlayAttack()
    {
        anim.SetTrigger("IsAttack");
    }

    public void PlayDeath()
    {
        anim.SetBool("IsDead", true);
    }

    public void SetVelocity(float velocity)
    {
        anim.SetFloat("Velocity", velocity);
    }
}
