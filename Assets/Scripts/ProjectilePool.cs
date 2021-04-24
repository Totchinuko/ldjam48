using UnityEngine;
using System;
using System.Collections.Generic;

namespace Constantine
{
    [CreateAssetMenu(fileName = "ProjectilePool", menuName ="Constantine/ProjectilePool")]
    public class ProjectilePool : ScriptableObject
    {
        public int basePoolCount = 100;
        public int refillLimit = 10;
        public int refillCount = 30;
        public int refillPerFrame = 5;
        public int tickPerFrame = 50;
        public int maxForThrow = 200;
        public Projectile projectilePrefab;

        private Stack<Projectile> pool;
        private List<Projectile> total;

        public static ProjectilePool Instance {get; private set;}

        public void Init() {
            if(Instance != null && Instance != this) 
                throw new Exception("Only one projectile pool authorized");
            Instance = this;
            pool = new Stack<Projectile>(maxForThrow);
            total = new List<Projectile>(maxForThrow);
            for (int i = 0; i < basePoolCount; i++)
                SpawnOne();
            Timing.RunCoroutine(Tick(), Segment.FixedUpdate, "poolTick");
        }

        public Projectile RequestProjectile() {
            if(pool.Count <= refillLimit)
                Refill();
            return pool.Pop();
        }

        public void RetireProjectile(Projectile proj) {
            pool.Push(proj);
        }

        private void SpawnOne() {
            Projectile spawn = GameObject.Instantiate(projectilePrefab, Vector3.zero, Quaternion.identity);
            pool.Push(spawn);
            total.Add(spawn);
        }

        private void Refill() {
            if(total.Count + refillCount > maxForThrow)
                throw new Exception("Too many projectile going on. Some might have a lifetime too high");
            Timing.RunCoroutine(Refill(refillCount));
        }

        private IEnumerator<float> Refill(int count) {
            for (int i = 1; i <= count; i++)
            {   
                SpawnOne();
                if(i % refillPerFrame == 0)
                    yield return 0f;
            }
        }

        private IEnumerator<float> Tick() {
            while(true) 
            {
                int refresh = 1;
                for(int i = 0; i < total.Count; i++, refresh++) {
                    total[i].Tick();
                    if(refresh % tickPerFrame == 0)
                        yield return Timing.WaitForOneFrame;
                }

                yield return Timing.WaitForOneFrame;
            }            
        }
    }
}