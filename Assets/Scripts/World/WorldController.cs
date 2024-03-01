using System;
using Animals;
using Animals.Fight;
using Camera;
using GameSettings;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World
{
    public class WorldController : IWorldController
    {
        private readonly IGameSettings _gameSettings;
        private readonly IAnimalsFightStrategy _animalsFightStrategy;
        private readonly ICameraController _cameraController;

        private int _deadPreysAmount;
        private int _deadPredatorsAmount;

        public event Action<int> OnPreyDie;
        public event Action<int> OnPredatorDie;

        public WorldController(IGameSettings gameSettings, IAnimalsFightStrategy animalsFightStrategy, ICameraController cameraController)
        {
            _gameSettings = gameSettings;
            _animalsFightStrategy = animalsFightStrategy;
            _cameraController = cameraController;
        }

        public void Fight(IAnimal animal1, IAnimal animal2)
        {
            _animalsFightStrategy.Fight(animal1, animal2);
        }
        
        public void Die(IAnimal animal)
        {
            switch (animal.AnimalType)
            {
                case AnimalType.Prey:
                    _deadPreysAmount++;
                    OnPreyDie?.Invoke(_deadPreysAmount);
                    break;
                
                case AnimalType.Predator:
                    _deadPredatorsAmount++;
                    OnPredatorDie?.Invoke(_deadPredatorsAmount);
                    break;
                
                default:
                    throw new NotImplementedException($"{nameof(WorldController)}.{nameof(Die)} is not implemented for {animal.AnimalType}");
            }
        }

        public Vector3 GetSpawnPosition()
        {
            return new Vector3(
                Random.Range(_gameSettings.SpawnSettings.MinX, _gameSettings.SpawnSettings.MaxX),
                Random.Range(_gameSettings.SpawnSettings.MinY, _gameSettings.SpawnSettings.MaxY),
                Random.Range(_gameSettings.SpawnSettings.MinZ, _gameSettings.SpawnSettings.MaxZ));
        }

        public Vector3 GetCenterPoint()
        {
            return Vector3.zero;
        }

        public bool IsInWorldBorders(Vector3 position)
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(_cameraController.Camera);
            return GeometryUtility.TestPlanesAABB(planes, new Bounds(position, Vector3.zero));
        }
    }
}