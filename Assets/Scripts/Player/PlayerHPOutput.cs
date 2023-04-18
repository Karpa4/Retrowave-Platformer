using UnityEngine;
using UnityEngine.UI;

public class PlayerHPOutput : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private int _maxHP;
    private CharacterHealth _health;

    private void Awake()
    {
        _health = GetComponent<CharacterHealth>();
        _health.HealthChangeEvent += ChangeHP;
        _health.DeathEvent += DeathHP;
    }

    /// <summary>
    /// Изменение заполненности изображения HPBar
    /// </summary>
    /// <param name="currentHP">Кол-во HP</param>
    private void ChangeHP(int currentHP)
    {
        if (currentHP < 0)
        {
            currentHP = 0;
        }
        float result = (float)currentHP / _maxHP;
        _hpBar.fillAmount = result;
    }

    /// <summary>
    /// Отображение хп = 0
    /// </summary>
    private void DeathHP()
    {
        _hpBar.fillAmount = 0;
    }
}
