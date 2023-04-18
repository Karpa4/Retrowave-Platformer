using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    private float _currentTime;

    // Update is called once per frame
    void Update()
    {
        _currentTime += Time.deltaTime;
        _timerText.text = _currentTime.ToString("F2");
    }

    public float GetTime()
    {
        return _currentTime;
    }
}
