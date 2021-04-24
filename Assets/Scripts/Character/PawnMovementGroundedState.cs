using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PawnMovementActiveState", menuName ="Constantine/MovementState/Grounded")]
    public class PawnMovementGroundedState : PawnMovementState
    {
        public PawnGroundedDefinition groundedDefinition;
        public float speed;
        public PawnMovementState airbornState;
        public PawnMovementState jumpState;

        public override void EnterState(PawnMovement machine)
        {
            machine.jumpCount = 0;
        }
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
            if(!IsGrounded(machine.transform.position, groundedDefinition)) {
                machine.SetState(airbornState);
                machine.jumpCount = 1;
                return;
            }

            machine.body.velocity = Vector2.right * machine.move * speed;
        }

        public static bool IsGrounded(Vector2 position, PawnGroundedDefinition def) {
            RaycastHit2D hit = Physics2D.CircleCast(position + Vector2.up * def.radius, def.radius, Vector2.down, Mathf.Infinity, def.groundMask);
            if(hit.collider == null)
                Debug.LogWarning("No ground under the player feet");
            return hit.collider != null && hit.distance <= def.minDistanceToBeGrounded;
        }
    }
}