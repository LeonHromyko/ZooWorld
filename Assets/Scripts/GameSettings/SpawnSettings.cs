using System;
using UnityEngine;

namespace GameSettings
{
    [Serializable]
    public class SpawnSettings : ISpawnSettings
    {
        [SerializeField] private float _minX = -10f;
        [SerializeField] private float _maxX = 10f;
        [SerializeField] private float _minY = 2f;
        [SerializeField] private float _maxY = 5f;
        [SerializeField] private float _minZ = -10f;
        [SerializeField] private float _maxZ = 10f;
        
        public float MinX => _minX;
        public float MaxX => _maxX;
        public float MinY => _minY;
        public float MaxY => _maxY;
        public float MinZ => _minZ;
        public float MaxZ => _maxZ;
    }
}