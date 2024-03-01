using GameSettings;
using ObjectPool;
using UnityEngine;

namespace SpawnSystem
{
    public class AnimalSpawnStrategy : ISpawnStrategy
    {
        private readonly IGameSettings _gameSettings;
        private readonly IAnimalsObjectPool _animalsObjectPool;

        public AnimalSpawnStrategy(IGameSettings gameSettings, IAnimalsObjectPool animalsObjectPool)
        {
            _gameSettings = gameSettings;
            _animalsObjectPool = animalsObjectPool;
        }

        public void Spawn()
        {
            var maxValue = 0f;

            foreach (var animalSetting in _gameSettings.AnimalSettings)
            {
                maxValue += animalSetting.SpawnProbabilityWeight;
            }
            
            var randomValue = Random.Range(0f, maxValue);
            maxValue = 0f;

            foreach (var animalSetting in _gameSettings.AnimalSettings)
            {
                maxValue += animalSetting.SpawnProbabilityWeight;

                if (maxValue >= randomValue)
                {
                    var animal = _animalsObjectPool.Get(animalSetting);
                    animal.Spawn();
                    break;
                }
            }
        }
    }
}