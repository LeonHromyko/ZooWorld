using System;
using GameSettings;
using ObjectPool;
using UnityEngine;
using VContainer;
using World;

namespace Animals
{
    public class AnimalController : MonoBehaviour, IAnimal
    {
        private const string GroundTag = "Ground";
        
        [SerializeField] private AnimalType _animalType;

        private IWorldController _worldController;
        private IAnimalsObjectPool _animalsObjectPool;
            
        public AnimalType AnimalType => _animalType;
        public bool IsAlive { get; private set; }
        public int EatenAnimalsAmount { get; private set; }
        public GameObject GameObject => gameObject;
        public IAnimalSettings AnimalSettings { get; set; }

        public event Action OnSpawn;
        public event Action<IAnimal> OnEat;
        public event Action OnDie;
        public event Action<Collision> OnCollisionWithObstacle;

        [Inject]
        public void Construct(IWorldController worldController, IAnimalsObjectPool animalsObjectPool)
        {
            _worldController = worldController;
            _animalsObjectPool = animalsObjectPool;
        }
        
        public void Spawn()
        {
            IsAlive = true;
            EatenAnimalsAmount = 0;
            transform.position = _worldController.GetSpawnPosition();
            gameObject.SetActive(true);
            OnSpawn?.Invoke();
        }
        
        public void Eat(IAnimal animal)
        {
            EatenAnimalsAmount += animal.EatenAnimalsAmount + 1;
            animal.Die();
            OnEat?.Invoke(animal);
        }

        public void Die()
        {
            IsAlive = false;
            gameObject.SetActive(false);
            _animalsObjectPool.Release(this);
            _worldController.Die(this);
            OnDie?.Invoke();
        }

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        private void OnCollisionEnter(Collision other)
        {
            var otherAnimal = other.collider.GetComponent<IAnimal>();

            if (otherAnimal != null)
            {
                _worldController.Fight(this, otherAnimal);
            }
            else if (!other.collider.CompareTag(GroundTag))
            {
                OnCollisionWithObstacle?.Invoke(other);
            }
        }
    }
}