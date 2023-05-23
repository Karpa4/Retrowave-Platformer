using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
    [SerializeField] private bool isControlled;
    private float horizontal = 0;
    private bool rightMove = true;

    public float Horizontal => horizontal;
    public event Action AttackEvent;
    public event Action JumpEvent;

    void Update()
    {
        if (isControlled)
        {
            horizontal = Input.GetAxis(GlobalConstants.HORIZONTAL_AXIS);
            if (Input.GetButtonDown(GlobalConstants.Jump))
            {
                JumpEvent?.Invoke();
            }

            if (horizontal > 0 && rightMove == false)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                rightMove = true;
            }
            if (horizontal < 0 && rightMove == true)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                rightMove = false;
            }

            if (Input.GetButtonDown(GlobalConstants.Fire_1))
            {
                if (EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }
                AttackEvent?.Invoke();
            }
        }
    }

    /// <summary>
    /// Отключение контроля у игрока
    /// </summary>
    public void SwitchControl(bool switcher)
    {
        isControlled = switcher;
    }
}
