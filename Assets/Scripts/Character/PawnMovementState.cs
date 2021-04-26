using UnityEngine;
using System;

namespace Constantine
{
    public abstract class PawnMovementState : StateMB<PawnMovement, PawnMovementState>
    {
        public virtual void Move(PawnMovement machine, float axis) {}
        public virtual void Jump(PawnMovement machine) {}
        public virtual void Jump(PawnMovement machine, Vector2 destination) {}
    }
}