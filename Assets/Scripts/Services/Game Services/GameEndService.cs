using Features.Services.UI.Windows;
using UnityEngine;
using Features.Services.UI.Factory;
using Zenject;
using Features.Services.Coroutine;
using System;
using System.Collections;

public class GameEndService
{
    private readonly PlayerScore playerScore;
    private readonly EndLevelPortal endLevelPortal;
    private readonly IWindowsService windowsService;
    private readonly CharacterHealth playerHealth;
    private readonly ICoroutineRunner coroutineRunner;

    private const float DelayAfterDeath = 1;

    [Inject]
    public GameEndService(PlayerScore playerScore, EndLevelPortal endLevelPortal, 
        IWindowsService windowsService, ICoroutineRunner coroutineRunner)
    {
        this.coroutineRunner = coroutineRunner;
        this.playerScore = playerScore;
        this.endLevelPortal = endLevelPortal;
        this.windowsService = windowsService;
        playerHealth = playerScore.GetComponent<CharacterHealth>();
        Subscribe();
    }

    private void Subscribe()
    {
        endLevelPortal.FinishLevel += ShowFinishScreen;
        playerHealth.DeathEvent += ShowGameOverScreen;
    }

    private void ShowFinishScreen()
    {
        windowsService.Open(WindowId.GameFinish);
        PauseGame();
        Cleanup();
    }

    private void ShowGameOverScreen()
    {
        coroutineRunner.StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(DelayAfterDeath);
        windowsService.Open(WindowId.GameOver);
        PauseGame();
        Cleanup();
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void Cleanup()
    {
        endLevelPortal.FinishLevel += ShowFinishScreen;
        playerHealth.DeathEvent += ShowGameOverScreen;
    }
}
