using UnityEngine;
using System;

namespace Constantine
{
    public class TestMovement : MonoBehaviour
    {
        public Transform spawn;
        public Pawn playerPrefab;
        public PawnAI aiTest;

        public void StartScene() {
            Pawn pawn = Instantiate(playerPrefab, spawn.position, Quaternion.identity);
            PlayerInputController.Instance.PossessPawn(pawn);
            aiTest.SetTarget(pawn.transform);
        }
    }
}