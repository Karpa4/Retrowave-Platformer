using System.Collections.Generic;

namespace Features.Services
{
    public interface IPlayerStaticData
    {
        List<int> PlayerLevelScore { get; }
        List<int> MaxLevelScore { get; }
        bool IsPlayFirstTime { get; set; }
        int CurrentLevelIndex { get; set; }
        void SetNewScore(int score);
    }
}
