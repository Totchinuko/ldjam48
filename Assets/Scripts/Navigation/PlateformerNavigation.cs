using UnityEngine;
using System;
using System.Collections.Generic;

namespace Constantine
{
    public class PlateformerNavigation : MonoBehaviour
    {
        protected NavigationPoint[] points;

        private void Awake() {
            points = GetComponentsInChildren<NavigationPoint>();
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

        public Stack<NavigationPoint> GetPath(Vector3 start, Vector3 goal) {
            NavigationPoint begin = GetClosestPoint(start);
            NavigationPoint end = GetClosestPoint(goal);

            Stack<NavigationPoint> path = new Stack<NavigationPoint>();
            Queue<NavigationPoint> openList = new Queue<NavigationPoint>();
            List<NavigationPoint> closedList = new List<NavigationPoint>();
            List<NavigationPoint> points = new List<NavigationPoint>(10);
            NavigationPoint current = null;

            openList.Enqueue(begin);

            while(openList.Count != 0 && !closedList.Contains(end)) {
                current = openList.Dequeue();
                closedList.Add(current);
                current.GetPoints(points);                

                foreach(NavigationPoint p in points) {
                    if(!closedList.Contains(p) && !openList.Contains(p)) {
                        p.parent = current;
                        openList.Enqueue(p);
                    }
                }
            }

            if(current == null || !closedList.Contains(current))
                return null;
            NavigationPoint final = current;
            do {
                path.Push(final);
                final = final.parent;
            } while(final != begin && final != null);
            
            return path;
        }
    }
}