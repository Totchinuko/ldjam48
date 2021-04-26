using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PawnBehaviourChase", menuName ="Constantine/AIState/Chase")]
    public class PawnBehaviourChase : PawnBehaviourState
    {
        public float duration;
        public PawnBehaviourState nextState;

        public override void EnterState(PawnBehaviour machine)
        {
            machine.pawnMovement.paused = false;
        }
        public override void DoUpdate(PawnBehaviour machine)
        {
            if(machine.pawnAI.target == null)
                machine.pawnAI.SetTarget(PlayerInputController.Instance.Pawn);

            if(machine.LastEnterState + duration < Time.time)
                machine.SetState(nextState);
        }
    }
}