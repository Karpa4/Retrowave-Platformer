using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Features.Services
{
    public class PlayerStaticData : IPlayerStaticData
    {
        public bool IsPlayFirstTime 
        { 
            get => isPlayFirstTime;
            set
            {
                if (value == false)
                {
                    PlayerPrefs.SetInt(GlobalConstants.FirstPlayKey, 1);
                }
                isPlayFirstTime = value; 
            }
        }
        public int CurrentLevelIndex { get => currentLevelIndex; set => currentLevelIndex = value; }
        public List<int> PlayerLevelScore => playerLevelScore;
        public List<int> MaxLevelScore => maxLevelScore;

        private List<int> playerLevelScore;
        private List<int> maxLevelScore;
        private int currentLevelIndex;
        private bool isPlayFirstTime;

        public PlayerStaticData()
        {
            playerLevelScore = new List<int>();
            maxLevelScore = new List<int>() { 1015, 1840, 2985 };
            currentLevelIndex = 0;
            LoadIsPlayFirstTime();
            LoadStats();
        }

        private void LoadIsPlayFirstTime()
        {
            int res = PlayerPrefs.GetInt(GlobalConstants.FirstPlayKey, 0);
            if (res == 0)
            {
                isPlayFirstTime = true;
            }
            else
            {
                isPlayFirstTime = false;
            }
        }

        private void SaveStats()
        {
            StreamWriter writer = new StreamWriter(GlobalConstants.FileName);
            foreach (int item in playerLevelScore)
            {
                string json = JsonUtility.ToJson(new JsonInt { Score = item });
                writer.WriteLine(json);
            }
            writer.Close();
        }

        private void LoadStats()
        {
            if (File.Exists(GlobalConstants.FileName))
            {
                StreamReader reader = new StreamReader(GlobalConstants.FileName);
                while (!reader.EndOfStream)
                {
                    string json = reader.ReadLine();
                    playerLevelScore.Add(JsonUtility.FromJson<JsonInt>(json).Score);
                }
                reader.Close();
            }
        }

        public void SetNewScore(int score)
        {
            int statsIndex = currentLevelIndex - 2;
            if (statsIndex < playerLevelScore.Count)
            {
                playerLevelScore[statsIndex] = score;
            }
            else
            {
                playerLevelScore.Add(score);
            }
            SaveStats();
        }
    }
}
