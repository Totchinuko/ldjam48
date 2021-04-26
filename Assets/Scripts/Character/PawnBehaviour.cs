using UnityEngine;
using System;

namespace Constantine
{
    public class PawnBehaviour : StateMachineMB<PawnBehaviour, PawnBehaviourState>
    {
        public PawnBehaviourState initialState;
        public Transform firePoint;
        public PawnAI pawnAI {get; private set;}
        public Pawn pawn {get; private set;}
        public PawnMovement pawnMovement {get; private set;}

        private void Awake() {
            pawn = GetComponent<Pawn>();
            pawnMovement = GetComponent<PawnMovement>();
            pawnAI = GetComponent<PawnAI>();
        }

        private void Start() {
            SetState(initialState);
        }

        public void OnAttackDone() {
            CurrentState?.OnAttackDone(this);
        }

    }
}