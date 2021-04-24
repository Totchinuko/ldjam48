using UnityEngine;
using System;

namespace Constantine
{
    public class HitAreaTest : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag("HurtBox")) {
                PawnHurtbox hb = other.GetComponent<PawnHurtbox>();
                hb.DealDamage(0);
            }
        }
    }
}