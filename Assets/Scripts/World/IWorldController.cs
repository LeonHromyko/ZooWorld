using System;
using Animals;
using UnityEngine;

namespace World
{
    public interface IWorldController
    {
        event Action<int> OnPreyDie;
        event Action<int> OnPredatorDie;

        void Fight(IAnimal animal1, IAnimal animal2);
        void Die(IAnimal animal);
        Vector3 GetSpawnPosition();
        Vector3 GetCenterPoint();
        bool IsInWorldBorders(Vector3 position);
    }
}