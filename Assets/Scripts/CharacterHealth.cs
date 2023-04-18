using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int _currentHealth;

    //Событие изменения здоровья
    public delegate void HealthHandler(int currentHealth);
    public event HealthHandler HealthChangeEvent;

    //Событие смерти
    public delegate void DeathHandler();
    public event DeathHandler DeathEvent;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    /// <summary>
    /// Метод принятия урона
    /// </summary>
    /// <param name="damage">Кол-во урона</param>
    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;
            if (_currentHealth < 0)
            {
                _currentHealth = 0;
            }
            else if (_currentHealth > 0)
            {
                HealthChangeEvent?.Invoke(_currentHealth);
            }

            if (_currentHealth == 0)
            {
                DeathEvent?.Invoke();
            }
        }
    }

    /// <summary>
    /// Метод для взаимодействия с аптечкой
    /// </summary>
    /// <returns>true - аптечка была использована , false - у игрока полное здоровье</returns>
    public void HealByMed(int hpRestore)
    {
        _currentHealth += hpRestore;
        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        HealthChangeEvent?.Invoke(_currentHealth);
    }
}
