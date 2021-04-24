using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PawnMovementJumpState", menuName ="Constantine/MovementState/Jump")]
    public class PawnMovementJumpState : PawnMovementState
    {
        public float jumpForce;
        public PawnMovementState airbornState;

        public override void EnterState(PawnMovement machine)
        {
            machine.jumpCount++;
        }

        public override void Move(PawnMovement machine, float axis)
        {
            machine.move = axis;
        }

        public override void DoFixedUpdate(PawnMovement machine)
        {
            Vector2 v = machine.body.velocity;
            v.y = jumpForce;
            machine.body.velocity = v;
            machine.SetState(airbornState);
        }
    }
}