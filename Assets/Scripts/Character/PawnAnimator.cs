using UnityEngine;
using System;
using UnityEngine.Events;

namespace Constantine
{
    public class PawnAnimator : MonoBehaviour
    {
        public bool defaultLeft;
        protected Pawn pawn;
        protected bool fliped;
        protected float lastAttack;

        protected readonly int shootID = Animator.StringToHash("shoot");
        protected readonly int airbornID = Animator.StringToHash("airborn");
        protected readonly int runID = Animator.StringToHash("run");
        protected readonly int verticalID = Animator.StringToHash("vertical");
        protected readonly int doubleJumpID = Animator.StringToHash("doubleJump");
        protected readonly int hitID = Animator.StringToHash("hit");
        protected readonly int deadID = Animator.StringToHash("dead");

        public Vector2 forward => defaultLeft ? -transform.right : transform.right;
        public Vector2 down => -transform.up;

        public Animator animator {get; private set;}

        public UnityEvent OnAirJumpShoot;
        public UnityEvent OnHitStatusBegin;
        public UnityEvent OnHitStatusEnd;

        public void Attack() {
            lastAttack = Time.time;
        }

        public void ToggleDead(bool toggle) {
            animator.SetBool(deadID, toggle);
        }

        public void Hit() {
            animator.SetTrigger(hitID);
            OnHitStatusBegin.Invoke();
        }

        public void ToggleAirborn(bool toggle) {
            animator.SetBool(airbornID, toggle);
        }

        public void AirJump() { 
            if(animator.GetBool(airbornID))
                animator.SetTrigger(doubleJumpID);
        }

        private void Awake() {
            pawn = GetComponentInParent<Pawn>();      
            animator = GetComponent<Animator>();     
        }

        private void LateUpdate() {
            float x = pawn.movement.move;            
            if(!Mathf.Approximately(x, 0)) {
                fliped = x < 0;
            }
            transform.rotation = Quaternion.Euler(0, fliped == defaultLeft ? 0 : 180, 0);

            if(animator == null) return;
            animator.SetBool(runID, !Mathf.Approximately(x, 0));
            animator.SetFloat(verticalID, pawn.movement.body.velocity.y);
        }

        protected virtual void EventAirJumpShoot() {
            OnAirJumpShoot.Invoke();
        }

        protected virtual void EventHitEnded() {
            OnHitStatusEnd.Invoke();
        }
    }
}