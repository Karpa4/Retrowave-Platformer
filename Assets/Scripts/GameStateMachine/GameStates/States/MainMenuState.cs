using Features.GameStates.States.Interfaces;
using Features.SceneLoading.Scripts;
using Features.Services.UI.Factory;
using Features.Services.UI.Windows;

namespace Features.GameStates.States
{
    public class MainMenuState : IState
    {
        private readonly ISceneLoader sceneLoader;
        private readonly IWindowsService windowsService;

        public MainMenuState(IGameStateMachine gameStateMachine, ISceneLoader sceneLoader, IWindowsService windowsService)
        {
            this.sceneLoader = sceneLoader;
            this.windowsService = windowsService;
            gameStateMachine.Register(this);
        }

        public void Enter() =>
          sceneLoader.Load(GlobalConstants.MenuIndex, OnLoaded);

        public void Exit() { }

        private void OnLoaded() =>
          windowsService.Open(WindowId.MainMenu);
    }
}