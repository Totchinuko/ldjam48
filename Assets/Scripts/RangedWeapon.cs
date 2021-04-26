using UnityEngine;
using System;
using UnityEngine.Events;

namespace Constantine
{
    public class RangedWeapon : MonoBehaviour
    {
        public Transform startPoint;
        public ProjectileDefinition projectile;

        private Pawn pawn;
        private int shot;

        public UnityEvent OnAttack;

        private void Awake() {
            pawn = GetComponent<Pawn>();
        }

        public void Fire(Vector3 direction) {
            Vector3 start = startPoint.position;
            Projectile.FireProjectile(start, direction, projectile);
            OnAttack.Invoke();
        }
    }
}