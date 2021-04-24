using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PawnGroundedDefinition", menuName ="Constantine/PawnGroundedDefinition")]
    public class PawnGroundedDefinition : ScriptableObject
    {
        public LayerMask groundMask;
        public float minDistanceToBeGrounded;
        public float radius;
    }
}