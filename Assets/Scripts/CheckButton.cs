using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Проверяем, находится ли курсор мыши на кнопке паузы. Чтобы игрок не стрелял при нажатии на кнопку
/// </summary>
public class CheckButton : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler
{
    [SerializeField] private PlayerInput _input;

    public void OnPointerEnter(PointerEventData eventData)
    {
        _input.OnPauseButton = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _input.OnPauseButton = false;
    }
}