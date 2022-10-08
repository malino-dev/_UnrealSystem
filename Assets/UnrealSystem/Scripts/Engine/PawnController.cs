using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UnrealSystem.Engine
{
    public abstract class PawnController : MonoBehaviour
    {
        [SerializeField] protected int controllerIndex = -1;
        
        public event Action<string, AxisValue> AxisUpdated;
        public event Action<string, float> ActionTriggered;

        private IEnumerable<Pawn> _pawns;

        protected virtual void Start()
        {
            PossessPawns();
        }
        
        protected void PossessPawns()
        {
            IEnumerable<Pawn> pawns = GetPawns();
            
            foreach (Pawn pawn in pawns)
            {
                pawn.Possess(this);
            }
        }
        
        protected IEnumerable<Pawn> GetPawns()
        {
            if (_pawns != null) return _pawns;
            
            _pawns = from p in FindObjectsOfType<Pawn>() 
                     where p.autoPossessedBy == controllerIndex && p.autoPossessedBy != -1
                     select p;

            return _pawns;
        }
        
        protected void RaiseAxisUpdate(string key, AxisValue value)
        {
            AxisUpdated?.Invoke(key, value);
        }
        
        protected void RaiseAction(string key, float value = 1f)
        {
            ActionTriggered?.Invoke(key, value);
        }

    }
}