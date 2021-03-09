using AD;
using UnityEngine;
using Zenject;

public class AdvertInstaller : MonoInstaller
{
    [SerializeField] private AdvertService _adService;
    public override void InstallBindings()
    {
        Container
            .Bind<AdvertService>()
            .FromComponentInNewPrefab(_adService)
            .AsSingle()
            .NonLazy();
    }
}