using UnityEngine;
using System;
using UnityEngine.InputSystem;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PlayerInputMap", menuName ="Constantine/PlayerInputMap")]
    public class PlayerInputMap : ScriptableObject
    {
        public InputAction move;
        public InputAction jump;
        public InputAction attack;

        public void Enable() {
            move.Enable();
            jump.Enable();
            attack.Enable();
        }

        public void Disable() {
            move.Disable();
            jump.Disable();
            attack.Disable();
        }
    }
}