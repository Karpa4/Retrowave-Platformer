using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    private int currentHealth;

    //Событие изменения здоровья
    public delegate void HealthHandler(float ratio);
    public event HealthHandler HealthChangeEvent;

    //Событие смерти
    public delegate void DeathHandler();
    public event DeathHandler DeathEvent;

    private void Awake()
    {
        currentHealth = _maxHealth;
    }

    /// <summary>
    /// Метод принятия урона
    /// </summary>
    /// <param name="damage">Кол-во урона</param>
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                DeathEvent?.Invoke();
            }
            else
            {
                CalcRatioForEvent();
            }
        }
    }

    /// <summary>
    /// Метод для взаимодействия с аптечкой
    /// </summary>
    public void HealByMed(int hpRestore)
    {
        currentHealth += hpRestore;
        if (currentHealth > _maxHealth)
        {
            currentHealth = _maxHealth;
        }
        CalcRatioForEvent();
    }

    private void CalcRatioForEvent()
    {
        float ratio = (float)currentHealth / _maxHealth;
        HealthChangeEvent?.Invoke(ratio);
    }

    /// <summary>
    /// Немедленное убийство сущности
    /// </summary>
    public void InstantlyDeath()
    {
        DeathEvent?.Invoke();
    }
}