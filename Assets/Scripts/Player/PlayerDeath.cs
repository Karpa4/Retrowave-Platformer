using System.Collections;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private UIStates _states;
    [SerializeField] private float _delayBeforeUI;
    private PlayerInput _input;
    private CharacterHealth _health;
    private Animator _animator;

    private void Awake()
    {
        _health = GetComponent<CharacterHealth>();
        _input = GetComponent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _health.DeathEvent += PlayerIsDead;
    }

    /// <summary>
    /// Отключение контроля у игрока и анимация смерти
    /// </summary>
    private void PlayerIsDead()
    {
        _animator.SetBool("IsAlive", false);
        _input.SetOffControl();
        StartCoroutine(DelayBeforeUI());
    }

    /// <summary>
    /// Вывод статистики и активация экрана смерти
    /// </summary>
    /// <returns></returns>
    private IEnumerator DelayBeforeUI()
    {
        GameStats._gameStats.OutputDeathStats();
        yield return new WaitForSeconds(_delayBeforeUI);
        _states.ActiveDeathScreen();
    }
}
