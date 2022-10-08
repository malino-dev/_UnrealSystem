using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnrealSystem.Engine;

namespace UnrealSystem
{
    public class WaypointController : PawnController
    {
        public float moveSpeed = 6f;
        public Transform[] waypoints;

        private int[] _current;
        private MoveTo[] _myPawns;

        protected override void Start()
        {
            base.Start();

            Pawn[] pawns = GetPawns().ToArray();

            _current = new int[pawns.Length];
            _myPawns = new MoveTo[pawns.Length];
            int i = 0;
            
            foreach (Pawn pawn in pawns)
            {
                _myPawns[i] = pawn.GetComponent<MoveTo>();
                _myPawns[i].Target = waypoints[_current[i]];
                i++;
            }
        }

        private void Update()
        {
            int i = 0;
            
            foreach (MoveTo pawn in _myPawns)
            {
                HandlePawn(i, pawn);
                i++;
            }
        }

        private void HandlePawn(int i, MoveTo pawn)
        {
            Transform target = waypoints[_current[i]];

            if (pawn.transform.position == target.position)
            {
                _current[i] = (_current[i] + 1) % waypoints.Length;
                target = waypoints[_current[i]];
            }
            
            pawn.Target = target;
        }
    }
}