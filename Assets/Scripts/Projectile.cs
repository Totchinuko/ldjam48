using UnityEngine;
using System;

namespace Constantine
{
    public class Projectile : MonoBehaviour
    {
        public Rigidbody2D body;
        public SpriteRenderer sprite;
        public new CircleCollider2D collider;
        public float lifeTime {get; private set;}
        public ProjectileDefinition definition {get; private set;}

        private Vector2 force;
        private bool test;

        protected virtual void Awake() {
            gameObject.SetActive(false);
        }

        public void Tick() {
            if(!gameObject.activeSelf) return;

            if(lifeTime < Time.time) {
                DestroyProjectile();
            }
        }

        public static void FireProjectile(Vector2 start, Vector2 direction, ProjectileDefinition def) {
            for (int i = 0; i < def.projectileCount; i++)
            {
                Projectile proj = ProjectilePool.Instance.RequestProjectile();
                proj.gameObject.layer = def.layer;
                Vector2 drn = GetRandomDirectionPlane(direction, Vector3.forward, def.spreadAngle);
                proj.Fire(start, drn, def);
            }
        }

        public void Fire(Vector2 start, Vector2 direction, ProjectileDefinition def) {
            definition = def;
            gameObject.SetActive(true);
            
            transform.position = start;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0,0,90) * direction);            
            body.velocity = direction.normalized * def.velocity;            

            collider.radius = def.collisionRadius;
            lifeTime = def.lifeTime + Time.time;

            def.OnProjectileFire(this);
        }

        public void DestroyProjectile() {
            ProjectilePool.Instance.RetireProjectile(this);
            gameObject.SetActive(false);
            definition.OnProjectileDestroyed(this);
        }

        public static Vector3 GetRandomDirectionPlane(Vector3 direction, Vector3 normal, float maxAngle) {
                return Quaternion.AngleAxis(UnityEngine.Random.Range(-(maxAngle / 2), maxAngle / 2), normal) * direction;
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("HurtBox"))
                definition.OnProjectileHitTarget(this, other);
            else if(other.CompareTag("Wall"))
                definition.OnProjectileHitWall(this, other);
        }

        
    }
}