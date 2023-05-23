using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] 
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpOffset;
    [SerializeField] private AnimationCurve moveCurve;
    [SerializeField] private Transform groundColTransform;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private PlayerAnim playerAnim;
    [SerializeField] private Rigidbody2D rigidBody;

    private bool isGround = false;
    private bool secondJumpAvailable = true;

    private void Start()
    {
        playerInput.JumpEvent += Jump;
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundColTransform.position, jumpOffset, groundMask);
        if (isGround && !secondJumpAvailable)
        {
            secondJumpAvailable = true;
        }
        playerAnim.SwitchGround(isGround);
        PlayerMove();
    }

    public void PlayerMove()
    {
        float abs = Mathf.Abs(playerInput.Horizontal);
        if (abs > 0.01f)
        {
            rigidBody.velocity = new Vector2(moveCurve.Evaluate(playerInput.Horizontal) * speed, rigidBody.velocity.y);
        }
        playerAnim.SetVelocity(abs);
    }

    public void Jump()
    {
        if (isGround)
        {
            secondJumpAvailable = true;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
        else if (secondJumpAvailable)
        {
            secondJumpAvailable = false;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpForce);
        }
    }

    public void Cleanup()
    {
        rigidBody.constraints = RigidbodyConstraints2D.FreezePositionX;
        playerInput.JumpEvent -= Jump;
    }
}
