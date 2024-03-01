using System;
using Animals;
using UnityEngine;

namespace GameSettings
{
    [Serializable]
    public class AnimalSettings : IAnimalSettings
    {
        [SerializeField] private AnimalController _prefab;
        [SerializeField] private float _spawnProbabilityWeight = 1f;

        public IAnimal Prefab => _prefab;
        public float SpawnProbabilityWeight => _spawnProbabilityWeight;
    }
}