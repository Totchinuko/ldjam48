using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PawnMovementActiveState", menuName ="Constantine/MovementState/ActiveState")]
    public class PawnMovementGroundedState : PawnMovementState
    {
        public LayerMask groundMask;
        public float minDistanceToBeGrounded;
        public float speed;
        public PawnMovementState airbornState;
        public PawnMovementState jumpState;
        public override void Move(PawnMovement machine, float axis)
        {
            machine.move = axis;
        }

        public override void Jump(PawnMovement machine)
        {
            machine.SetState(jumpState);
        }

        public override void DoFixedUpdate(PawnMovement machine)
        {
            RaycastHit2D hit = Physics2D.Raycast(machine.transform.position, Vector2.down, Mathf.Infinity, groundMask);
            if(hit.collider == null)
                Debug.LogWarning("No ground under the player feet");
            if(hit.collider != null && hit.distance > minDistanceToBeGrounded) {
                machine.SetState(airbornState);
                return;
            }

            machine.body.velocity = Vector2.left * machine.move * speed;
        }
    }
}