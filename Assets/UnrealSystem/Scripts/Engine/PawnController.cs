using System;
using UnityEngine;

namespace UnrealSystem.Engine
{
    public abstract class PawnController : MonoBehaviour
    {
        [SerializeField] protected int controllerIndex = -1;
        
        public abstract event Action<string, AxisValue> AxisUpdated;
        public abstract event Action<string, float> ActionTriggered;
    }
}