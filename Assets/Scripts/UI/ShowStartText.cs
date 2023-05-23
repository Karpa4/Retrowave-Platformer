using UnityEngine;
using Zenject;
using Features.Services;
using UnityEngine.UI;

public class ShowStartText : MonoBehaviour
{
    [SerializeField] private GameObject firstPlayScreen;
    [SerializeField] private Button playButton;

    [Inject]
    public void Construct(IPlayerStaticData playerStaticData)
    {
        if (playerStaticData.IsPlayFirstTime)
        {
            Time.timeScale = 0;
            firstPlayScreen.SetActive(true);
            playerStaticData.IsPlayFirstTime = false;
            playButton.onClick.AddListener(StartPlay);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartPlay()
    {
        playButton.onClick.RemoveListener(StartPlay);
        Time.timeScale = 1;
        Destroy(gameObject);
    }
}
