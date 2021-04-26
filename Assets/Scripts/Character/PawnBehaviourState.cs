using UnityEngine;
using System;

namespace Constantine
{
    public abstract class PawnBehaviourState : StateMB<PawnBehaviour, PawnBehaviourState>
    {
        public virtual void OnAttackDone(PawnBehaviour machine) {}
    }
}