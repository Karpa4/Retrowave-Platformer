using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private void Start()
    {
        LoadSettings();
    }

    /// <summary>
    /// Изменение значения слайдера музыки
    /// </summary>
    /// <param name="sliderValue">Значение слайдера</param>
    public void SetMusicVolume(float sliderValue)
    {
        mainMixer.SetFloat(GlobalConstants.MusicKey, sliderValue);
    }

    /// <summary>
    /// Изменение значения слайдера звуков
    /// </summary>
    /// <param name="sliderValue">Значение слайдера</param>
    public void SetSoundsVolume(float sliderValue)
    {
        mainMixer.SetFloat(GlobalConstants.SoundKey, sliderValue);
    }

    /// <summary>
    /// Настройки сохраняются при выходе из меню настроек
    /// </summary>
    public void SaveSettings()
    {
        PlayerPrefs.SetFloat(GlobalConstants.MusicKey, musicSlider.value);
        PlayerPrefs.SetFloat(GlobalConstants.SoundKey, soundSlider.value);
    }

    private void LoadSettings()
    {
        float tempValue = PlayerPrefs.GetFloat(GlobalConstants.MusicKey, 0);
        musicSlider.value = tempValue;
        mainMixer.SetFloat(GlobalConstants.MusicKey, tempValue);

        tempValue = PlayerPrefs.GetFloat(GlobalConstants.SoundKey, 0);
        soundSlider.value = tempValue;
        mainMixer.SetFloat(GlobalConstants.SoundKey, tempValue);
    }
}
