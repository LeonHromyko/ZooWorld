using System.Threading;
using Cysharp.Threading.Tasks;
using GameSettings;
using UnityEngine;
using VContainer;
using World;
using Random = UnityEngine.Random;

namespace Animals.Movement
{
    [RequireComponent(typeof(IAnimal))]
    public abstract class BaseAnimalMovementController : MonoBehaviour
    {
        [SerializeField] private float _speedMin = 1f;
        [SerializeField] private float _speedMax = 2f;

        [SerializeField] private Vector3 _newMoveDirectionMin = new Vector3(-1, 0, -1);
        [SerializeField] private Vector3 _newMoveDirectionMax = new Vector3(1, 0, 1);

        protected IGameSettings GameSettings;
        protected IWorldController WorldController;
        protected CancellationTokenSource CancellationTokenSource;
        
        protected IAnimal Animal;
        protected Rigidbody Rigidbody;

        protected float Speed;
        protected Vector3 MoveDirection;

        [Inject]
        public void Construct(IGameSettings gameSettings, IWorldController worldController)
        {
            GameSettings = gameSettings;
            WorldController = worldController;
        }
        
        protected virtual void Awake()
        {
            Animal = GetComponent<IAnimal>();
            Rigidbody = GetComponent<Rigidbody>();

            Animal.OnSpawn += OnSpawn;
            Animal.OnDie += OnDie;
            Animal.OnCollisionWithObstacle += OnCollisionWithObstacle;
        }

        protected virtual void OnDestroy()
        {
            Animal.OnSpawn -= OnSpawn;
            Animal.OnDie -= OnDie;
            Animal.OnCollisionWithObstacle -= OnCollisionWithObstacle;
            
            CancellationTokenSource?.Cancel();
        }

        protected virtual Vector3 GetNewMoveDirection()
        {
            return new Vector3(
                Random.Range(_newMoveDirectionMin.x, _newMoveDirectionMax.x),
                Random.Range(_newMoveDirectionMin.y, _newMoveDirectionMax.y),
                Random.Range(_newMoveDirectionMin.z, _newMoveDirectionMax.z))
                .normalized;
        }

        protected virtual void OnSpawn()
        {
            Speed = Random.Range(_speedMin, _speedMax);
            MoveDirection = GetNewMoveDirection();
            CancellationTokenSource = new CancellationTokenSource();
            _ = UpdateStateTask(CancellationTokenSource.Token);
        }

        protected virtual void OnDie()
        {
            CancellationTokenSource?.Cancel();
        }

        protected virtual void OnCollisionWithObstacle(Collision collision)
        {
            var normal = collision.GetContact(0).normal;
            
            if (Mathf.Abs(normal.x) > float.Epsilon || Mathf.Abs(normal.z) > float.Epsilon)
            {
                MoveDirection = new Vector3(normal.x, MoveDirection.y, normal.z).normalized;
            }
        }

        private async UniTask UpdateStateTask(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                UpdateState();
                await UniTask.Delay(GameSettings.UpdateStateDelayMilliSec, cancellationToken: cancellationToken);
            }
        }

        protected void UpdateState()
        {
            if (!WorldController.IsInWorldBorders(transform.position))
            {
                var direction = WorldController.GetCenterPoint() - transform.position;
                MoveDirection = new Vector3(direction.x, MoveDirection.y, direction.z);
            }
        }
    }
}