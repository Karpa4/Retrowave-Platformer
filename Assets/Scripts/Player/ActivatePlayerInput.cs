using System.Collections;
using UnityEngine;

public class ActivatePlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerInput _input;

    /// <summary>
    /// Если игрок нажал на кнопку паузы, то сбрасываем этим методом при возвращении в игру
    /// </summary>
    public void SetActiveControl()
    {
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.05f);
        _input.OnPauseButton = false;
    }
}
