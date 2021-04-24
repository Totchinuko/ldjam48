using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "ProjectileDefinition", menuName ="Constantine/ProjectileDefinition")]
    public class ProjectileDefinition : ScriptableObject
    {
        public Sprite sprite;
        public Color color;
        public float collisionRadius;
        public float velocity;
        public int layer;
        public int projectileCount;
        public float spreadAngle;
        public float lifeTime;

        public virtual void OnProjectileFire(Projectile projectile) {
            projectile.sprite.sprite = sprite;
            projectile.sprite.color = color;
        }

        public virtual void OnProjectileHitTarget(Projectile projectile, Collider2D target) {
            // todo target damage
            projectile.DestroyProjectile();
        }

        public virtual void OnProjectileHitWall(Projectile projectile, Collider2D wall) {
            projectile.DestroyProjectile();
        }

        public virtual void OnProjectileDestroyed(Projectile projectile) {
            	
        }
    }
}