using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Constantine
{
    public class PawnAI : MonoBehaviour
    {
        [NonSerialized]
        public bool paused;
        public bool canJump;
        public float stoppingDistance;
        public float nextDistance;
        public NavigationPoint targetPoint {get; private set;}
        public Pawn target {get; private set;}
        public NavigationPoint nextPoint {get; private set;}
        public NavigationPoint previousPoint {get; private set;}
        public NavigationPoint localPoint {get; private set;}
        public Stack<NavigationPoint> path {get; private set;}

        private PlateformerNavigation nav => GameManager.Instance.navigation;

        public UnityEvent<Vector2> OnJump;
        public UnityEvent<float> OnMove;


        public void SetTarget(Pawn target) {
            this.target = target;
            if(nav == null) return;
            localPoint = nav.GetClosestPoint(this.transform.position);
            if(canJump)
                targetPoint = nav.GetClosestPoint(this.target.transform.position);
            else
                targetPoint = nav.GetClosestWithoutJump(localPoint, this.target.transform.position);
            if(targetPoint == localPoint) {
                path.Clear();
                path.Push(targetPoint);
            }
            else if(nav.GetPath(localPoint, targetPoint, path)) {
                nextPoint = path.Pop();
                previousPoint = localPoint;
            }
            else {
                nextPoint = localPoint;
                previousPoint = localPoint;
            }
        }

        public void Jump(Vector3 destination) {
            paused = true;
            OnJump.Invoke(destination);
        }

        protected virtual void Awake() {
            path = new Stack<NavigationPoint>(10);
        }

        protected virtual void Update() {
            if(nav == null || target == null || paused) {
                return;
            }

            localPoint = nav.GetClosestPoint(transform.position);
            NavigationPoint tp = nav.GetClosestPoint(target.transform.position);
            if(tp != targetPoint || nextPoint == null || (previousPoint != localPoint && nextPoint != localPoint)) {
                SetTarget(target);
            }

            float tdist = (transform.position - target.transform.position).sqrMagnitude;
            float ndist = (transform.position - nextPoint.position).sqrMagnitude;
            float ldist = (transform.position - localPoint.position).sqrMagnitude;

            float move = 0f;
            if(tdist < ndist && tdist > stoppingDistance * stoppingDistance) {
                move = Mathf.Sign((target.transform.position - transform.position).x);
            }
            else if(tdist >= ndist && ndist > nextDistance * nextDistance) {
                move = Mathf.Sign((nextPoint.position - transform.position).x);
            }
            else if(ndist <= nextDistance * nextDistance && path != null && path.Count > 0) {
                previousPoint = nextPoint;
                nextPoint = path.Pop();
            }

            if(localPoint.NeedJump(nextPoint) && canJump && ldist <= nextDistance * nextDistance) {
                Jump(nextPoint.position);
                move = Mathf.Sign((nextPoint.position - transform.position).x);
            }
            else if(localPoint.NeedJump(nextPoint) && canJump) {
                move = Mathf.Sign((localPoint.position - transform.position).x);
            }
            else if(localPoint.NeedJump(nextPoint) && !canJump) {
                move = 0f;
            }

            OnMove.Invoke(move);
        }

        // private void OnDrawGizmos() {
        //     if(!Application.isPlaying) return;
        //     if(PlayerInputController.Instance.Pawn == null || target == null) return;

        //     Vector3 ptarget = target.transform.position;
        //     Vector3 pos = transform.position;

        //     Gizmos.DrawCube(localPoint.position, Vector3.one * 0.2f);

        //     if(this.path == null || this.path.Count == 0) {
        //         Gizmos.DrawLine(ptarget, targetPoint.position);
        //         Gizmos.DrawLine(pos, nextPoint.position);
        //         return;
        //     }
        //     NavigationPoint[] path = this.path.ToArray();
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawLine(ptarget, targetPoint.position);
        //     Gizmos.DrawLine(pos, path[0].position);
        //     Gizmos.DrawCube(path[0].position, Vector3.one * 0.2f);
        //     if(path.Length == 1) return;
        //     for (int i = 1; i < path.Length; i++)
        //     {
        //         Gizmos.DrawCube(path[i].position, Vector3.one * 0.2f);
        //         Gizmos.DrawLine(path[i-1].position, path[i].position);
        //     }
        // }

    }
}