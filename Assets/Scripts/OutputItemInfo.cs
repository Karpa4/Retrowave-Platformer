using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class OutputItemInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private string _itemInfo = string.Empty;
    [SerializeField] private Text _textField;

    public void ChangeText(int currentScore, int maxScore, int percent)
    {
        _itemInfo = $"Your progress is {percent}%\nScore: {currentScore}\nMax Score: {maxScore}";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _textField.text = _itemInfo;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _textField.text = "";
    }
}
