using System;
using UnityEngine;

namespace UnrealSystem
{
    public class WaypointAiStandalone : MonoBehaviour
    {
        public Transform[] waypoints;
        public float speed = 6f;
        public bool isAiActive = true;

        private int _current = 0;

        public void StartAi()
        {
            isAiActive = true;
        }

        public void StopAi()
        {
            isAiActive = false;
        }
        
        private void Update()
        {
            if (!isAiActive) return;
            
            transform.position = Vector3.MoveTowards(
                transform.position, 
                waypoints[_current].position, 
                speed * Time.deltaTime);

            if (transform.position == waypoints[_current].position)
            {
                _current = (_current + 1) % waypoints.Length;
            }
        }
    }
}