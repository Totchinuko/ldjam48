using UnityEngine;
using System;
using UnityEngine.Events;

namespace Constantine
{
    public class PawnHurtbox : MonoBehaviour
    {
        public UnityEvent<int> OnHit;

        public void DealDamage(int damage) {
            OnHit.Invoke(damage);
        }
    }
}