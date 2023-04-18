using UnityEngine;

public class SpeedBonus : MonoBehaviour
{
    [SerializeField] private float _newSpeed;
    [SerializeField] private float _time;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement movement = collision.GetComponent<PlayerMovement>();
        movement.StartCoroutine(movement.ChangeSpeed(_newSpeed, _time));
    }
}
