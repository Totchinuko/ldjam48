using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "PlayerInputController", menuName ="Constantine/PlayerInputController")]
    public class PlayerInputController : ScriptableObject
    {
        public PlayerInputMap map;
        public static PlayerInputController Instance {get; private set;}
        public Pawn Pawn {get; private set;}

        public void Init() {
            if(Instance != null && Instance != this) 
                throw new Exception("Only one controller authorized");
            Instance = this;

            map.move.performed += ctx => Pawn.Move(ctx.ReadValue<float>());
            map.move.canceled += ctx => Pawn.Move(ctx.ReadValue<float>());
            map.jump.started += ctx => Pawn.Jump();
            map.attack.started += ctx => Pawn.Attack();
        }

        public void PossessPawn(Pawn pawn) {
            Pawn = pawn;
            if(Pawn == null)
                map.Disable();
            else
                map.Enable();
        }
    }
}