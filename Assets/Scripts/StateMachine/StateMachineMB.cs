using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constantine
{
    public abstract class StateMachineMB<M, S> : MonoBehaviour where M : StateMachineMB<M, S> where S : StateMB<M, S>
    {
        private S lastState;
        private S currentState;

        public S LastState => lastState;
        public S CurrentState => currentState;

        public float LastEnterState { get; private set; }

        public virtual void SetState(S state)
        {
            currentState?.ExitState((M)this);
            lastState = currentState;
            currentState = state;
            currentState?.EnterState((M)this);
            LastEnterState = Time.time;
        }

        protected virtual void Update()
        {
            currentState?.DoUpdate((M)this);
        }

        protected virtual void LateUpdate()
        {
            currentState?.DoLateUpdate((M)this);
        }

        protected virtual void FixedUpdate()
        {
            currentState?.DoFixedUpdate((M)this);
        }
    }
}