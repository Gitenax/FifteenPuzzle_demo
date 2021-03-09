using UnityEngine;
using Zenject;

namespace Core
{
	public class ServiceInstaller : MonoInstaller
	{
		[SerializeField] private FifteenGame _gameManager;
		
		public override void InstallBindings()
		{
			BindGameManager();
		}

		private void BindGameManager()
		{
			Container
				.Bind<FifteenGame>()
				.FromComponentInNewPrefab(_gameManager)
				.AsSingle()
				.NonLazy();
		}
	}
}