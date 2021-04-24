using UnityEngine;
using System;

namespace Constantine
{
    public class PawnAnimator : MonoBehaviour
    {
        protected Pawn pawn;
        protected bool fliped;

        public Vector2 forward => transform.right;

        private void Awake() {
            pawn = GetComponentInParent<Pawn>();            
        }

        private void LateUpdate() {
            float x = pawn.movement.body.velocity.x;
            if(!Mathf.Approximately(x, 0)) {
                fliped = x < 0;
            }
            transform.rotation = Quaternion.Euler(0, fliped ? 180 : 0, 0);
        }
    }
}