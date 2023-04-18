using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelController : MonoBehaviour
{
    [SerializeField] private List<Button> _levelButtons;

    private void Awake()
    {
        foreach (Button item in _levelButtons)
        {
            item.gameObject.GetComponent<OutputItemInfo>().enabled = false;
        }
    }

    /// <summary>
    /// Включает необходимое кол-во кнопок и компонентов OutputItemInfo
    /// (в зависимости от кол-ва пройденных уровней)
    /// </summary>
    /// <param name="stats">Список статистики об уровнях</param>
    public void UpdateChooseLevelMenu(List<LevelStats> stats)
    {
        for (int i = 0; i < stats.Count; i++)
        {
            _levelButtons[i].interactable = true;
            OutputItemInfo itemInfo = _levelButtons[i].gameObject.GetComponent<OutputItemInfo>();
            itemInfo.enabled = true;
            itemInfo.ChangeText(stats[i].CurrentScore, stats[i].MaxLevelScore, stats[i].Percent);
        }
        if (stats.Count < 3)
        {
            _levelButtons[stats.Count].interactable = true;
        }
    }
}
