using UnityEngine;
using System;

namespace Constantine
{
    public class PawnMovement : StateMachineMB<PawnMovement, PawnMovementState>
    {
        public PawnMovementState initialState;
        [HideInInspector]
        public float move;
        [HideInInspector]
        public int jumpCount;
        public Rigidbody2D body {get; private set;}
        public CapsuleCollider2D capsule {get; private set;}

        private void Awake() {
            body = GetComponent<Rigidbody2D>();
            capsule = GetComponent<CapsuleCollider2D>();
        }

        private void Start() {
            SetState(initialState);
        }

        public void Move(float move) {
            CurrentState?.Move(this, move);
        }

        public void Jump() {
            CurrentState?.Jump(this);
        }
    }
}