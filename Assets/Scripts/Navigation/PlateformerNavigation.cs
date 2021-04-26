using UnityEngine;
using System;
using System.Collections.Generic;

namespace Constantine
{
    public class PlateformerNavigation : MonoBehaviour
    {
        protected NavigationPoint[] points;
        Queue<NavigationPoint> openList = new Queue<NavigationPoint>(10);
        List<NavigationPoint> closedList = new List<NavigationPoint>(10);
        List<NavigationPoint> samplePoint = new List<NavigationPoint>(10);

        private void Awake() {
            points = GetComponentsInChildren<NavigationPoint>();
        }

        private void Start() {
            GameManager.Instance.navigation = this;
        }

        private void OnDestroy() {
            GameManager.Instance.navigation = null;
        }

        public NavigationPoint GetClosestPoint(Vector3 position) {
            float sdist = Mathf.Infinity;
            NavigationPoint selected = null;
            foreach(NavigationPoint p in points) {
                float d = (position - p.position).sqrMagnitude;
                if(d < sdist) {
                    selected = p;
                    sdist = d;
                }
            }
            return selected;
        }

        public NavigationPoint GetClosestWithoutJump(NavigationPoint origin, Vector3 position) {
            float sdist = Mathf.Infinity;
            NavigationPoint selected = null;
            NavigationPoint current = null;
            openList.Clear();
            closedList.Clear();
            samplePoint.Clear();

            openList.Enqueue(origin);
            while(openList.Count != 0) {
                current = openList.Dequeue();
                closedList.Add(current);
                current.GetPoints(samplePoint, false);
                foreach(NavigationPoint p in samplePoint) {
                    if(!openList.Contains(p) && !closedList.Contains(p)) {
                        openList.Enqueue(p);                        
                    }
                }
            }

            foreach(NavigationPoint p in closedList) {
                float d = (position - p.position).sqrMagnitude;
                if(d < sdist) {
                    selected = p;
                    sdist = d;
                }
            }
            return selected;
        }
        
        public bool GetPath(Vector3 start, Vector3 goal, Stack<NavigationPoint> path)  {
            NavigationPoint begin = GetClosestPoint(start);
            NavigationPoint end = GetClosestPoint(goal);
            return GetPath(begin, end, path);
        }

        public bool GetPath(NavigationPoint begin, NavigationPoint end, Stack<NavigationPoint> path) {

            path.Clear();

            NavigationPoint current = null;
            openList.Clear();
            closedList.Clear();
            samplePoint.Clear();
            openList.Enqueue(begin);

            while(openList.Count != 0 && !closedList.Contains(end)) {
                current = openList.Dequeue();
                closedList.Add(current);
                current.GetPoints(samplePoint);                

                foreach(NavigationPoint p in samplePoint) {
                    if(!closedList.Contains(p) && !openList.Contains(p)) {
                        p.parent = current;
                        openList.Enqueue(p);
                    }
                }
            }

            if(current == null || !closedList.Contains(current)) {
                return false;
            }
                
            NavigationPoint final = current;
            do {
                path.Push(final);
                final = final.parent;
            } while(final != begin && final != null);

            return true;
        }
    }
}