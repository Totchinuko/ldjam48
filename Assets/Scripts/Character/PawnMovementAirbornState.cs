using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PawnMovementAirbornState", menuName ="Constantine/MovementState/Airborn")]
    public class PawnMovementAirbornState : PawnMovementState
    {
        public PawnGroundedDefinition groundedDefinition;
        public int maxJumpCount;
        public float speed;
        public float gravityScaleClimb;
        public float gravityScaleFall;
        public PawnMovementState jumpState;
        public PawnMovementState groundedState;
        public override void Move(PawnMovement machine, float axis)
        {
            machine.move = axis;
        }

        public override void Jump(PawnMovement machine)
        {
            if(machine.jumpCount < maxJumpCount) {
                machine.OnAirJump.Invoke();
                machine.SetState(jumpState);
            }
        }

        public override void EnterState(PawnMovement machine)
        {
            machine.OnAirborn.Invoke();
        }

        public override void ExitState(PawnMovement machine)
        {
            machine.body.gravityScale = 1f;
        }

        public override void DoFixedUpdate(PawnMovement machine)
        {
            Vector2 v = machine.body.velocity;  

            if(PawnMovementGroundedState.IsGrounded(machine.transform.position, groundedDefinition) && v.y <= 0) {
                machine.SetState(groundedState);
                return;
            }          

            machine.body.gravityScale = v.y < 0 ? gravityScaleFall : gravityScaleClimb;
            v.x = machine.move * speed;
            machine.body.velocity = v;
        }
    }
}