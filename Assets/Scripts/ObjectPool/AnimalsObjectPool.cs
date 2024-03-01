using System.Collections.Generic;
using Animals;
using GameSettings;
using VContainer;
using VContainer.Unity;

namespace ObjectPool
{
    public class AnimalsObjectPool : IAnimalsObjectPool
    {
        private readonly IObjectResolver _container;
        
        private readonly Dictionary<IAnimalSettings, Queue<IAnimal>> _animalsObjectPool = new();

        public AnimalsObjectPool(IObjectResolver container, IGameSettings gameSettings)
        {
            _container = container;
            
            foreach (var animalSettings in gameSettings.AnimalSettings)
            {
                _animalsObjectPool[animalSettings] = new Queue<IAnimal>();
            }
        }

        public IAnimal Get(IAnimalSettings animalSettings)
        {
            var pool = _animalsObjectPool[animalSettings];

            return pool.Count > 0 
                ? pool.Dequeue() 
                : CreateAnimal(animalSettings);
        }

        public void Release(IAnimal animal)
        {
            var pool = _animalsObjectPool[animal.AnimalSettings];
            pool.Enqueue(animal);
        }

        private IAnimal CreateAnimal(IAnimalSettings animalSettings)
        {
            var animalGameObject = _container.Instantiate(animalSettings.Prefab.GameObject);
            animalGameObject.SetActive(false);

            var animal = animalGameObject.GetComponent<IAnimal>();
            animal.AnimalSettings = animalSettings;
                
            return animal;
        }
    }
}