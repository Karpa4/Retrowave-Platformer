using UnityEngine;
using UnityEngine.UI;

public class GameStats : MonoBehaviour
{
    public static GameStats _gameStats;
    private int _scoreCount;
    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _finishStatsText;
    [SerializeField] private Text _deathStatsText;
    [SerializeField] private Timer _timer;
    [SerializeField] private int _maxLevelScore;
    private SaveLoadStats _saveStats;

    private void Start()
    {
        _scoreCount = 0;
        if (_gameStats == null)
        {
            _gameStats = this;
        }
        _saveStats = GetComponent<SaveLoadStats>();
    }

    /// <summary>
    /// Вывод статистики при прохождения уровня
    /// </summary>
    public void OutputFinishStats()
    {
        float temp = (float)_scoreCount / _maxLevelScore;
        int percent = Mathf.RoundToInt(temp * 100);
        float time = _timer.GetTime();
        _saveStats.AddNewLevelStats(_scoreCount, _maxLevelScore, percent);
        _finishStatsText.text = $"Your progress is {percent}%\nScore: {_scoreCount}\nTime: {time.ToString("F2")}";
    }

    /// <summary>
    /// Вывод статистики при проигрыше
    /// </summary>
    public void OutputDeathStats()
    {
        float time = _timer.GetTime();
        _deathStatsText.text = $"Score: {_scoreCount}\nTime: {time.ToString("F2")}";
    }

    /// <summary>
    /// Увеличение кол-ва очков
    /// </summary>
    /// <param name="ScoreValue">Кол-во очков</param>
    public void ChangeScore(int ScoreValue)
    {
        _scoreCount += ScoreValue;
        _scoreText.text = _scoreCount.ToString();
    }
}
