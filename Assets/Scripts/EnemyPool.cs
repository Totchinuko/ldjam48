using UnityEngine;
using System;
using System.Collections.Generic;

namespace Constantine
{
    [CreateAssetMenu(fileName = "EnemyPool", menuName ="Constantine/EnemyPool")]
    public class EnemyPool : ScriptableObject
    {
        public int basePoolCount = 50;
        public int refillLimit = 5;
        public int refillCount = 20;
        public int refillPerFrame = 5;
        public PawnAI[] aiToPool;

        [NonSerialized]
        private List<PawnAI> allPawns;
        [NonSerialized]
        private Stack<PawnAI>[] availablePawns;
        [NonSerialized]
        private bool running = false;

        public EnemyPool Instance {get; private set;}
        

        public void Init() {
            if(Instance != null && Instance != this)
                throw new Exception("Can't use multiple pools");

            Instance = this;
            allPawns = new List<PawnAI>(100);
            running = true;
            availablePawns = new Stack<PawnAI>[aiToPool.Length];
            for (int i = 0; i < availablePawns.Length; i++) {
                availablePawns[i] = new Stack<PawnAI>(50);
                InitialRefill(i);
            }
        }

        public void ClearPool() {
            Timing.RunCoroutine(DestroyAll());
        }

        public PawnAI RequestPawn(int type) {
            if(availablePawns[type].Count <= refillLimit)
                Refill(type);
            return availablePawns[type].Pop();
        }

        public void ReleasePawn(PawnAI pawn) {
            availablePawns[pawn.type].Push(pawn);
            pawn.gameObject.SetActive(false);
        }

        private void SpawnOne(int type) {
            PawnAI ai = Instantiate(aiToPool[type], Vector3.zero, Quaternion.identity);
            ai.gameObject.SetActive(false);
            ai.type = type;
            availablePawns[type].Push(ai);
            allPawns.Add(ai);
        }

        private void Refill(int type) {
            Timing.RunCoroutine(Refill(type, refillCount));
        }

        private void InitialRefill(int type) {
            Timing.RunCoroutine(Refill(type, 50));
        }

        private IEnumerator<float> Refill(int type, int count) {
            for (int i = 1; i <= count; i++)
            {   
                SpawnOne(type);
                if(i % refillPerFrame == 0)
                    yield return 0f;
            }
        }

        private IEnumerator<float> DestroyAll() {
            if(allPawns == null) yield break;
            for (int i = 0; i < allPawns.Count; i++)
            {
                Destroy(allPawns[i].gameObject);
                if(i % 10 == 0)
                    yield return Timing.WaitForOneFrame;
            }
            allPawns.Clear();
            availablePawns = null;
            running = false;
        }
    }
}