using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PawnBehaviourAttack", menuName ="Constantine/AIState/Attack")]
    public class PawnBehaviourAttack : PawnBehaviourState 
    {
        public PawnBehaviourState nextState;
        public override void EnterState(PawnBehaviour machine)
        {

            machine.pawn.Move(0f);
            machine.pawnMovement.paused = true;
            machine.pawn.Attack((machine.pawnAI.target.centerOfMass.position - machine.firePoint.position).normalized);
        }

        public override void OnAttackDone(PawnBehaviour machine)
        {
            machine.SetState(nextState);
        }
    }
}