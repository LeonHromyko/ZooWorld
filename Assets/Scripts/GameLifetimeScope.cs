using Animals;
using Animals.Fight;
using Camera;
using GameSettings;
using ObjectPool;
using SpawnSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using World;

public class GameLifetimeScope : LifetimeScope
{
    [SerializeField] private GameSettings.GameSettings _gameSettings;
    [SerializeField] private CameraController _cameraController;
    
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance<IGameSettings>(_gameSettings);

        builder.RegisterComponent<ICameraController>(_cameraController);

        builder.Register<ISpawnStrategy, AnimalSpawnStrategy>(Lifetime.Singleton);
        builder.Register<IAnimalsFightStrategy, AnimalsFightStrategy>(Lifetime.Singleton);
        
        builder.Register<IAnimalsObjectPool, AnimalsObjectPool>(Lifetime.Singleton);
        builder.Register<IWorldController, WorldController>(Lifetime.Singleton);

        builder.RegisterEntryPoint<AnimalSpawnSystem>();
    }
}