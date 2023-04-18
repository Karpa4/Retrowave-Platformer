using UnityEngine;

public class EndLevelPortal : MonoBehaviour
{
    [SerializeField] private UIStates _states;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameStats._gameStats.OutputFinishStats();
        _states.ActiveFinishScreen();
    }
}
