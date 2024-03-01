using UnityEngine;

namespace GameSettings
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "ZooWorld/GameSettings")]
    public class GameSettings : ScriptableObject, IGameSettings
    {
        [SerializeField] private int _animalSpawnDelayMinMilliSec = 1000;
        [SerializeField] private int _animalSpawnDelayMaxMilliSec = 2000;

        [SerializeField] private AnimalSettings[] _animalSettings;
        [SerializeField] private SpawnSettings _spawnSettings;

        [SerializeField] private int _updateStateDelayMilliSec = 1000;

        public int AnimalSpawnDelayMinMilliSec => _animalSpawnDelayMinMilliSec;
        public int AnimalSpawnDelayMaxMilliSec => _animalSpawnDelayMaxMilliSec;
        public IAnimalSettings[] AnimalSettings => _animalSettings;
        public ISpawnSettings SpawnSettings => _spawnSettings;
        public int UpdateStateDelayMilliSec => _updateStateDelayMilliSec;
    }
}