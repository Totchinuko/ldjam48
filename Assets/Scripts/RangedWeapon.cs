using UnityEngine;
using System;

namespace Constantine
{
    public class RangedWeapon : MonoBehaviour
    {
        public Transform startPoint;
        public ProjectileDefinition projectile;

        private Pawn pawn;
        private int shot;

        private void Awake() {
            pawn = GetComponent<Pawn>();
        }

        public void Fire() {
            Vector3 direction = pawn.animator.forward;
            Vector3 start = startPoint.position;
            Projectile.FireProjectile(start, direction, projectile);
        }
    }
}