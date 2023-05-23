using UnityEngine;
using Features.GameStates;
using UnityEngine.UI;
using Features.UI.Windows.Base;
using Features.Services;
using Zenject;
using Features.GameStates.States;
using UnityEngine.SceneManagement;

public class GameFinishWindow : BaseWindow
{
    [SerializeField] private Text endText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button menuButton;

    private IGameStateMachine gameStateMachine;
    private IPlayerStaticData playerStaticData;

    [Inject]
    public void Construct(Timer timer, IGameStateMachine gameStateMachine, PlayerScore playerScore,
        IPlayerStaticData playerStaticData)
    {
        this.playerStaticData = playerStaticData;
        this.gameStateMachine = gameStateMachine;
        int index = playerStaticData.CurrentLevelIndex - 2;

        CheckNextLevel();
        CalcFinishStats(playerScore.GetScore(), playerStaticData.MaxLevelScore[index], timer.GetTime());

        if (index < playerStaticData.PlayerLevelScore.Count)
        {
            CompareScore(playerScore.GetScore(), playerStaticData.PlayerLevelScore[index]);
        }
        else
        {
            playerStaticData.SetNewScore(playerScore.GetScore());
        }
    }

    private void CompareScore(int currentScore, int oldScore)
    {
        if (currentScore > oldScore)
        {
            playerStaticData.SetNewScore(currentScore);
        }
    }

    private void CalcFinishStats(int currentScore, int maxScore, float time)
    {
        float temp = (float)currentScore / maxScore;
        int percent = Mathf.RoundToInt(temp * 100);
        ShowFinishStat(percent, currentScore, time);
    }

    private void ShowFinishStat(int percent, int score, float time)
    {
        endText.text = $"Your progress is { percent } %\nScore: { score}\nTime: { time.ToString("F2")}";
    }

    private void CheckNextLevel()
    {
        if (playerStaticData.CurrentLevelIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            nextButton.gameObject.SetActive(false);
        }
    }

    protected override void Subscribe()
    {
        nextButton.onClick.AddListener(NextLevel);
        menuButton.onClick.AddListener(ToMainMenu);
    }

    protected override void Cleanup()
    {
        Time.timeScale = 1;
        nextButton.onClick.RemoveListener(NextLevel);
        menuButton.onClick.RemoveListener(ToMainMenu);
    }

    private void NextLevel()
    {
        Cleanup();
        gameStateMachine.Enter<GameLoadState, int>(playerStaticData.CurrentLevelIndex + 1);
    }

    private void ToMainMenu()
    {
        Cleanup();
        gameStateMachine.Enter<MainMenuState>();
    }
}
