using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutputLevelInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Text textField;
    private string itemInfo = string.Empty;

    public void ChangeText(int currentScore, int maxScore, int percent)
    {
        itemInfo = $"Your progress is {percent}%\nScore: {currentScore}\nMax Score: {maxScore}";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textField.text = itemInfo;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textField.text = string.Empty;
    }
}
