using UnityEngine;
using System;
using System.Collections.Generic;

namespace Constantine
{
    public class NavigationPoint : MonoBehaviour
    {
        public LayerMask mask;
        public NavigationConnection[] connections;
        public Vector3 position {get; private set;}
        [NonSerialized]
        public NavigationPoint parent;

        public bool NeedJump(NavigationPoint point) {
            foreach (NavigationConnection con in connections)
            {
                if(con.destination == point)
                    return con.jump;
            }
            return false;
        }

        public void GetPoints(List<NavigationPoint> points, bool canJump = true) {
            points.Clear();
            foreach (NavigationConnection c in connections)
                if(!c.jump || canJump)
                    points.Add(c.destination);
        }

        private void Start() {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Mathf.Infinity, mask);
            if(hit.collider == null)
                throw new Exception($"No ground under {name}");
            position = hit.point;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.green;
            Gizmos.DrawCube(transform.position, Vector3.one * 0.2f);
        }

        private void OnDrawGizmosSelected() {
            if(connections == null) return;
            for (int i = 0; i < connections.Length; i++)
            {
                if(connections[i].destination == null) continue;
                Gizmos.color = connections[i].jump ? Color.blue : Color.green;
                Gizmos.DrawLine(transform.position, connections[i].destination.transform.position);
            }
        }
    }
}