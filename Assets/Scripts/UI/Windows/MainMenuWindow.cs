using Features.GameStates;
using UnityEngine;
using UnityEngine.UI;
using Features.UI.Windows.Base;
using Features.Services;
using Zenject;
using Features.GameStates.States;

namespace Features.UI.Windows
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private ChooseLevel chooseLevel;
        [SerializeField] private Button exitButton;

        private IGameStateMachine gameStateMachine;

        [Inject]
        public void Construct(IGameStateMachine gameStateMachine, IPlayerStaticData playerStaticData)
        {
            this.gameStateMachine = gameStateMachine;
            chooseLevel.Construct(playerStaticData.PlayerLevelScore, playerStaticData.MaxLevelScore);
        }

        protected override void Subscribe()
        {
            exitButton.onClick.AddListener(ExitGame);
        }

        protected override void Cleanup()
        {
            exitButton.onClick.RemoveListener(ExitGame);
        }

        public void StartGame(int levelNumber)
        {
            int index = levelNumber + 1;
            gameStateMachine.Enter<GameLoadState, int>(index);
        }

        private void ExitGame()
        {
            Application.Quit();
        }
    }
}
