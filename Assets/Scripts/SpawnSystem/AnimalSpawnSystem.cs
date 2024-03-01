using System.Threading;
using Cysharp.Threading.Tasks;
using GameSettings;
using UnityEngine;
using VContainer.Unity;

namespace SpawnSystem
{
    public class AnimalSpawnSystem : IAsyncStartable
    {
        private readonly IGameSettings _gameSettings;
        private readonly ISpawnStrategy _spawnStrategy;

        public AnimalSpawnSystem(IGameSettings gameSettings, ISpawnStrategy spawnStrategy)
        {
            _gameSettings = gameSettings;
            _spawnStrategy = spawnStrategy;
        }

        public async UniTask StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var delay = Random.Range(
                    _gameSettings.AnimalSpawnDelayMinMilliSec,
                    _gameSettings.AnimalSpawnDelayMaxMilliSec);

                await UniTask.Delay(delay, cancellationToken: cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                
                _spawnStrategy.Spawn();
            }
        }
    }
}