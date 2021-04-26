using UnityEngine;
using System;
using UnityEngine.Events;

namespace Constantine
{
    public class PawnAnimator : MonoBehaviour
    {
        public bool defaultLeft;
        public float lowerGunCooldown;
        public float thresholdFireUp = 0.8f;
        protected Pawn pawn;
        protected PawnAI pawnAI;
        protected bool fliped;
        protected float lowerGunTime;
        private Vector2 direction;

        protected readonly int shootID = Animator.StringToHash("shoot");
        protected readonly int enemyShootID = Animator.StringToHash("enemyshoot");
        protected readonly int enemyShootUpID = Animator.StringToHash("enemyshootup");
        protected readonly int airbornID = Animator.StringToHash("airborn");
        protected readonly int runID = Animator.StringToHash("run");
        protected readonly int verticalID = Animator.StringToHash("vertical");
        protected readonly int doubleJumpID = Animator.StringToHash("doubleJump");
        protected readonly int hitID = Animator.StringToHash("hit");
        protected readonly int deadID = Animator.StringToHash("dead");

        public Vector2 forward => defaultLeft ? -transform.right : transform.right;
        public Vector2 down => -transform.up;

        public Animator animator {get; private set;}

        public UnityEvent<Vector3> OnAirJumpShoot;
        public UnityEvent<Vector3> OnEnemyShoot;
        public UnityEvent OnHitStatusBegin;
        public UnityEvent OnHitStatusEnd;

        public void Attack() {
            lowerGunTime = Time.time + lowerGunCooldown;
            animator.SetBool(shootID, true);
        }

        public void Attack(Vector3 direction) {
            float d = Mathf.Clamp01(Vector2.Dot(Vector2.up, direction.normalized));
            this.direction = direction;
            if(thresholdFireUp <= d)
                animator.SetTrigger(enemyShootUpID);
            else
                animator.SetTrigger(enemyShootID);
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
            pawnAI = GetComponentInParent<PawnAI>();      
            animator = GetComponent<Animator>();     
        }

        private void LateUpdate() {
            float x = pawn.movement.move;            
            if(!Mathf.Approximately(x, 0)) {
                fliped = x < 0;
            }
            else if(pawnAI != null) {
                Vector2 dir = pawnAI.target.transform.position - transform.position;
                fliped = dir.x < 0;
            }
            transform.rotation = Quaternion.Euler(0, fliped == defaultLeft ? 0 : 180, 0);

            if(animator == null) return;
            if(lowerGunTime < Time.time && animator.GetBool(shootID))
                animator.SetBool(shootID, false);
            animator.SetBool(runID, !Mathf.Approximately(x, 0));
            animator.SetFloat(verticalID, pawn.movement.body.velocity.y);
        }

        protected virtual void EventEnemyShoot() {
            OnEnemyShoot.Invoke(direction);
        }

        protected virtual void EventAirJumpShoot() {
            OnAirJumpShoot.Invoke(down);
        }

        protected virtual void EventHitEnded() {
            OnHitStatusEnd.Invoke();
        }
    }
}