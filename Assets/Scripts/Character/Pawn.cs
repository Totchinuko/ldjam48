using UnityEngine;
using System;
using UnityEngine.Events;

namespace Constantine
{
    public class Pawn : MonoBehaviour
    {
        public Transform centerOfMass;
        public PawnAnimator animator {get; private set;}
        public PawnMovement movement {get; private set;}

        public UnityEvent<Vector3> OnAttack;

        private void Awake() {
            movement = GetComponent<PawnMovement>();
            animator = GetComponentInChildren<PawnAnimator>();
        }

        public void Move(float axis) {
            movement.Move(axis);
        }

        public void Jump() {
            movement.Jump();
        }

        public void Jump(Vector2 destination) {
            movement.Jump(destination);
        }

        public void Attack() {
            OnAttack.Invoke(animator.forward);
        }

        public void Attack(Vector3 direction) {
            OnAttack.Invoke(direction);
        }

    }
}