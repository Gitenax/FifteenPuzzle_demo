using UnityEngine;
using Zenject;

namespace Core
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private Audio _audioPrefab;
        
        public override void InstallBindings()
        {
            Container
                .Bind<Audio>()
                .FromComponentInNewPrefab(_audioPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}