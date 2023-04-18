using UnityEngine;

public class ShowStartText : MonoBehaviour
{
    [SerializeField] private GameObject _firstLaunchTextObject;

    private void Awake()
    {
        int number = PlayerPrefs.GetInt("FirstShow");
        if (number == 0)
        {
            Time.timeScale = 0;
            _firstLaunchTextObject.SetActive(true);
            PlayerPrefs.SetInt("FirstShow", 1);
        }
    }
}
