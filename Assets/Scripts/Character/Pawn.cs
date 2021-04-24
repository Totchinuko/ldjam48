using UnityEngine;
using System;

namespace Constantine
{
    public class Pawn : MonoBehaviour
    {
        public PawnMovement movement {get; private set;}

        private void Awake() {
            movement = GetComponent<PawnMovement>();
        }

        public void Move(float axis) {
            movement.Move(axis);
        }

        public void Jump() {
            movement.Jump();
        }

        public void Attack() {

        }

    }
}