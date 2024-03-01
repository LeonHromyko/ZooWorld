using UnityEngine;

namespace Animals.Movement
{
    public class LinearAnimalMovementController : BaseAnimalMovementController
    {
        private void FixedUpdate()
        {
            Rigidbody.velocity = MoveDirection * Speed * Time.fixedDeltaTime;
        }
    }
}