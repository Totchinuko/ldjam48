using UnityEngine;
using System;

namespace Constantine
{
    [CreateAssetMenu(fileName = "GameManager", menuName ="Constantine/GameManager")]
    public class GameManager : ScriptableObject
    {
        public PlayerInputController playerController;

        public static GameManager Instance {get; private set;}
        
        public void Init() {
            if(Instance != null && Instance != this) 
                throw new Exception("Only one controller authorized");
            Instance = this;
            playerController.Init();
        }
    }
}