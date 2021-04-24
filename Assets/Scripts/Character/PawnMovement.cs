using UnityEngine;
using System;

namespace Constantine
{
    public class PawnMovement : StateMachineMB<PawnMovement, PawnMovementState>
    {
        public float move;
        public Rigidbody2D body {get; private set;}
        public CapsuleCollider2D capsule {get; private set;}

        private void Awake() {
            body = GetComponent<Rigidbody2D>();
            capsule = GetComponent<CapsuleCollider2D>();
        }
    }
}