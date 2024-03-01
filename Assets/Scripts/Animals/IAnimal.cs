using System;
using GameSettings;
using UnityEngine;

namespace Animals
{
    public interface IAnimal
    {
        AnimalType AnimalType { get; }
        bool IsAlive { get; }
        int EatenAnimalsAmount { get; }
        GameObject GameObject { get; } 
        IAnimalSettings AnimalSettings { get; set; }

        event Action OnSpawn;
        event Action<IAnimal> OnEat;
        event Action OnDie;
        event Action<Collision> OnCollisionWithObstacle;

        void Spawn();
        void Eat(IAnimal animal1);
        void Die();
    }
}