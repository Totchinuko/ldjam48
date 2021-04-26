using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PawnMovementAIMove", menuName ="Constantine/MovementState/AIMove")]
    public class PawnMovementAIMove : PawnMovementState
    {
        public LayerMask mask;
        public float speed;
        public PawnMovementState jumpState;

        public override void Move(PawnMovement machine, float axis)
        {
            machine.move = axis;
        }

        public override void Jump(PawnMovement machine, Vector2 destination)
        {
            machine.SetState(jumpState);
            jumpState.Jump(machine, destination);
        }

        public override void DoFixedUpdate(PawnMovement machine)
        {

            Vector3 v = machine.body.velocity;
            v.x = machine.beingHit ? 0f : machine.move * speed;
            machine.body.velocity = v;
        }
    }
}