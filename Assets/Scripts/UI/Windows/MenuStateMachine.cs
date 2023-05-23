using UnityEngine;

public class MenuStateMachine : MonoBehaviour
{
    [SerializeField] private GameObject startState;
    private GameObject currentState;

    private void Start()
    {
        currentState = startState;
        if (!currentState.activeSelf)
        {
            currentState.SetActive(true);
        }
    }

    public void ChangeState(GameObject newState)
    {
        currentState.SetActive(false);
        currentState = newState;
        currentState.SetActive(true);
    }
}
