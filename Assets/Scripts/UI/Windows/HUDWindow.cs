using Features.UI.Windows.Base;
using Features.Services.UI.Factory;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Features.Services.UI.Windows;

public class HUDWindow : BaseWindow
{
    [SerializeField] private Button pauseButton;
    [SerializeField] private Text scoreText;
    [SerializeField] private Image hpBar;
    [SerializeField] private Text timeText;

    private PlayerScore playerScore;
    private CharacterHealth playerHealth;
    private Timer timer;
    private IWindowsService windowsService;

    [Inject]
    public void Construct(PlayerScore playerScore, Timer timer, IWindowsService windowsService)
    {
        this.windowsService = windowsService;
        this.timer = timer;
        this.playerScore = playerScore;
        playerHealth = playerScore.GetComponent<CharacterHealth>();
    }

    protected override void Subscribe()
    {
        pauseButton.onClick.AddListener(ActivePause);
        timer.RefreshTime += DisplayTime;
        playerHealth.HealthChangeEvent += DisplayHP;
        playerHealth.DeathEvent += DisplayZeroHP;
        playerScore.NewScore += DisplayScore;
    }

    private void ActivePause()
    {
        Time.timeScale = 0;
        windowsService.Open(WindowId.Pause);
    }

    private void DisplayHP(float ratio)
    {
        hpBar.fillAmount = ratio;
    }

    private void DisplayZeroHP()
    {
        hpBar.fillAmount = 0;
    }

    private void DisplayScore(int score)
    {
        scoreText.text = score.ToString();
    }

    private void DisplayTime(float time)
    {
        timeText.text = time.ToString("F2");
    }

    protected override void Cleanup()
    {
        pauseButton.onClick.RemoveListener(ActivePause);
        timer.RefreshTime -= DisplayTime;
        playerHealth.HealthChangeEvent -= DisplayHP;
        playerHealth.DeathEvent -= DisplayZeroHP;
        playerScore.NewScore -= DisplayScore;
    }
}
