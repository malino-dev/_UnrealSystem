using System;
using UnityEngine;

namespace UnrealSystem.Engine
{
    public abstract class PawnController : MonoBehaviour
    {
        [SerializeField] protected int controllerIndex = -1;
        
        public event Action<string, AxisValue> AxisUpdated;
        public event Action<string, float> ActionTriggered;

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