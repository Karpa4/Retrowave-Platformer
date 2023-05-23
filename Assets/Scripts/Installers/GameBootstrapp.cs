using UnityEngine;
using Zenject;
using Cinemachine;
using Features.UI.Windows.Base;
using Features.Services.UI.Factory.BaseUI;

public class GameBootstrapp : MonoInstaller
{
    [SerializeField] private PlayerScore playerPrefab;
    [SerializeField] private Transform startPlayerPoint;
    [SerializeField] private EndLevelPortal endLevelPrefab;
    [SerializeField] private Transform endPortalPoint;
    [SerializeField] private CinemachineVirtualCamera vCamera;

    public override void InstallBindings()
    {
        BindPlayer();
        BindTimer();
        BindUI();
        BindFinishTrigger();
        BindEndGameService();
    }

    public override void Start()
    {
        base.Start();
        Container.Resolve<IUIFactory>();
    }

    private void BindPlayer()
    {
        PlayerScore player = Instantiate(playerPrefab, startPlayerPoint.position, Quaternion.identity);
        Container.Bind<PlayerScore>().FromInstance(player).AsSingle();
        vCamera.Follow = player.transform;
    }

    private void BindFinishTrigger()
    {
        EndLevelPortal portal = Instantiate(endLevelPrefab, endPortalPoint.position, Quaternion.identity);
        Container.Bind<EndLevelPortal>().FromInstance(portal).AsSingle();
    }

    private void BindUI()
    {
        Container.BindFactoryCustomInterface<BaseWindow, UIFactory, IUIFactory>().AsSingle();//.NonLazy();
    }

    private void BindTimer()
    {
        Container.Bind<Timer>().To<Timer>().FromNew().AsSingle().NonLazy();
    }

    private void BindEndGameService()
    {
        Container.Bind<GameEndService>().FromNew().AsSingle().NonLazy();
    }
}