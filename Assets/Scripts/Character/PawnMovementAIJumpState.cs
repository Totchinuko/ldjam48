using UnityEngine;
using System;
using DG.Tweening;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PawnMovementAIJumpState", menuName ="Constantine/MovementState/AIJump")]
    public class PawnMovementAIJumpState : PawnMovementState
    {
        public float jumpForce;
        public float duration;
        public PawnMovementState moveState;


        public override void EnterState(PawnMovement machine)
        {
            PawnAI ai = machine.GetComponent<PawnAI>();
            ai.paused = true;
        }

        public override void Move(PawnMovement machine, float axis)
        {
            machine.move = axis;
        }

        public override void Jump(PawnMovement machine, Vector2 destination)
        {
            Sequence seq = DOTween.Sequence();
            seq.Append(machine.transform.DOJump(destination, jumpForce, 1, duration).SetEase(Ease.Linear));
            seq.AppendCallback(() => machine.SetState(moveState));
            seq.SetAutoKill(true);
            seq.Play();
        }

        public override void DoFixedUpdate(PawnMovement machine)
        {
            machine.body.velocity = Vector2.zero;
        }

        public override void ExitState(PawnMovement machine)
        {
            PawnAI ai = machine.GetComponent<PawnAI>();
            ai.paused = false;
        }
    }
}