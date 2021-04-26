using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Constantine
{
    public class TestPathFind : MonoBehaviour
    {

        private List<NavigationPoint> path;
        private NavigationPoint localNode;
        private NavigationPoint goalNode;

        private void Update() {
            if(GameManager.Instance.navigation == null || PlayerInputController.Instance.Pawn == null) return;

            PlateformerNavigation nav = GameManager.Instance.navigation;
            NavigationPoint pPoint = nav.GetClosestPoint(PlayerInputController.Instance.Pawn.transform.position);
            NavigationPoint lPoint = nav.GetClosestPoint(transform.position);
            if(pPoint != goalNode || lPoint != localNode) {
                Stack<NavigationPoint> sPath = new Stack<NavigationPoint>();
                nav.GetPath(lPoint, pPoint, sPath);
                goalNode = pPoint;
                localNode = lPoint;
                path = sPath.ToList();
            }
        }

        private void OnDrawGizmos() {
            if(!Application.isPlaying) return;
            if(PlayerInputController.Instance.Pawn == null || path == null || path.Count == 0) return;

            Vector3 player = PlayerInputController.Instance.Pawn.transform.position;
            Vector3 pos = transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawLine(player, goalNode.position);
            Gizmos.DrawLine(pos, path[0].position);
            Gizmos.DrawCube(path[0].position, Vector3.one * 0.2f);
            if(path.Count == 1) return;
            for (int i = 1; i < path.Count; i++)
            {
                Gizmos.DrawCube(path[i].position, Vector3.one * 0.2f);
                Gizmos.DrawLine(path[i-1].position, path[i].position);
            }
        }   
    }
}