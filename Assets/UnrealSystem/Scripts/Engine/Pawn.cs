using System;
using UnityEngine;

namespace UnrealSystem.Engine
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] public int autoPossessedBy = -1;
        
        private InputComponent _input;
        private PawnController _controller;

        public event Action<string, AxisValue> AxisUpdated;
        public event Action<string, float> ActionTriggered;

        private void Awake()
        {
            _input = GetComponent<InputComponent>();
        }

        public void Possess(PawnController controller)
        {
            _input.Reset();
            
            _controller = controller;
            _controller.AxisUpdated += OnAxisUpdated;
            _controller.ActionTriggered += OnActionTriggered;
        }

        public void Unpossess()
        {
            _controller.AxisUpdated -= OnAxisUpdated;
            _controller.ActionTriggered -= OnActionTriggered;
            _controller = null;

            _input.Reset();
        }
        
        private void OnAxisUpdated(string key, AxisValue value)
        {
            AxisUpdated?.Invoke(key, value);
        }

        private void OnActionTriggered(string key, float value)
        {
            ActionTriggered?.Invoke(key, value);
        }

    }
}