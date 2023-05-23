using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevel : MonoBehaviour
{
    [SerializeField] private List<OutputLevelInfo> levelInfos;
    [SerializeField] private List<Button> levelButtons;

    public void Construct(List<int> playerScore, List<int> maxScore)
    {
        for (int i = 0; i < playerScore.Count; i++)
        {
            float temp = (float)playerScore[i] / maxScore[i];
            int percent = Mathf.RoundToInt(temp * 100);
            levelInfos[i].ChangeText(playerScore[i], maxScore[i], percent);
            int increment = i + 1;
            if (increment < maxScore.Count)
            {
                levelButtons[increment].interactable = true;
            }
        }
    }
}
