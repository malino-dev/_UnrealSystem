using UnityEngine;
using UnityEngine.InputSystem;

namespace UnrealSystem.Engine
{
    public class AxisValue
    {
        private readonly object _value;
        
        public AxisValue(InputAction action)
        {
            object value = action.ReadValueAsObject();
                
            if (value == null)
            {
                if (action.expectedControlType == "Vector2") value = Vector2.zero;
                else value = 0f;
            }

            _value = value;
        }

        public AxisValue(object value)
        {
            if (value == null) value = 0f;
            _value = value;
        }

        public T Get<T>() where T : struct => (T) _value;

        public object Get() => _value;
    }
}