using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Animals.Movement
{
    public class JumpAnimalMovementController : BaseAnimalMovementController
    {
        [SerializeField] private int _jumpDelayMinMilliSec = 1000;
        [SerializeField] private int _jumpDelayMaxMilliSec = 2000;
        
        protected override void OnSpawn()
        {
            base.OnSpawn();
            _ = JumpTask(CancellationTokenSource.Token);
        }

        private async UniTask JumpTask(CancellationToken cancellationToken)
        {
            var jumpDelay = Random.Range(_jumpDelayMinMilliSec, _jumpDelayMaxMilliSec);
            
            while (!cancellationToken.IsCancellationRequested)
            {
                await UniTask.Delay(jumpDelay, cancellationToken: cancellationToken);
                cancellationToken.ThrowIfCancellationRequested();
                
                Rigidbody.AddForce(MoveDirection * Speed, ForceMode.Impulse);
            }
        }
    }
}