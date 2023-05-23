using System;
using UnityEngine;

public class EndLevelPortal : MonoBehaviour
{
    public event Action FinishLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerInput>(out PlayerInput input))
        {
            input.SwitchControl(false);
            FinishLevel?.Invoke();
        }
    }
}
