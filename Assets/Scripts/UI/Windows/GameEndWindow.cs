using UnityEngine;
using Features.GameStates;
using UnityEngine.UI;
using Features.UI.Windows.Base;
using Features.Services;
using Zenject;
using Features.GameStates.States;

public class GameEndWindow : BaseWindow
{
    [SerializeField] private Text endText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private IGameStateMachine gameStateMachine;
    private int currentIndex;

    [Inject]
    public void Construct(Timer timer, IGameStateMachine gameStateMachine, PlayerScore playerScore,
        IPlayerStaticData playerStaticData)
    {
        currentIndex = playerStaticData.CurrentLevelIndex;
        this.gameStateMachine = gameStateMachine;
        endText.text = $"Score: {playerScore.GetScore()}\nTime: {timer.GetTime().ToString("F2")}";
    }

    protected override void Subscribe()
    {
        restartButton.onClick.AddListener(RestartLevel);
        menuButton.onClick.AddListener(ToMainMenu);
    }

    protected override void Cleanup()
    {
        Time.timeScale = 1;
        restartButton.onClick.RemoveListener(RestartLevel);
        menuButton.onClick.RemoveListener(ToMainMenu);
    }

    private void RestartLevel()
    {
        Cleanup();
        gameStateMachine.Enter<GameLoadState, int>(currentIndex);
    }

    private void ToMainMenu()
    {
        Cleanup();
        gameStateMachine.Enter<MainMenuState>();
    }
}
