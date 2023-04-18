using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SaveLoadStats : MonoBehaviour
{
    [SerializeField] private bool _isNeedToOutput;
    private List<LevelStats> _levelStats = new List<LevelStats>();
    private string _fileName = "LevelStats.json";

    private void Awake()
    {
        StartLoading();
    }

    /// <summary>
    /// ����� ���������� ��� ����������� ������. 
    /// ��������� ������ ���� ��� ����������
    /// </summary>
    /// <param name="currentScore">��������� ����</param>
    /// <param name="maxScore">������������ ���-�� ����� �� ������</param>
    /// <param name="percent">���������� �����������</param>
    public void AddNewLevelStats(int currentScore, int maxScore, int percent)
    {
        int index = SceneManager.GetActiveScene().buildIndex - 1;

        if (index == _levelStats.Count)
        {
            _levelStats.Add(new LevelStats(maxScore, currentScore, percent));
            SaveStats();
        }
        else
        {
            if (_levelStats[index].CurrentScore < currentScore)
            {
                _levelStats[index] = new LevelStats(maxScore, currentScore, percent);
                SaveStats();
            }
        }
    }

    /// <summary>
    /// ������������ � json
    /// </summary>
    private void SaveStats()
    {
        StreamWriter writer = new StreamWriter(_fileName);
        foreach (LevelStats item in _levelStats)
        {
            string json = JsonUtility.ToJson(item);
            writer.WriteLine(json);
        }
        writer.Close();
    }

    /// <summary>
    /// ��������� �������� ������
    /// </summary>
    private async void StartLoading()
    {
        await Task.Run(LoadStats);
        if (_isNeedToOutput)
        {
            GetComponent<ChooseLevelController>().UpdateChooseLevelMenu(_levelStats);
        }
    }

    private void LoadStats()
    {
        if (File.Exists(_fileName))
        {
            StreamReader reader = new StreamReader(_fileName);
            while (!reader.EndOfStream)
            {
                string json = reader.ReadLine();
                _levelStats.Add(JsonUtility.FromJson<LevelStats>(json));
            }
            reader.Close();
        }
    }


}
