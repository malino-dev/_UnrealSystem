using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnrealSystem.Engine
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : PawnController
    {
        public override event Action<string, AxisValue> AxisUpdated;
        public override event Action<string, float> ActionTriggered;
        
        private PlayerInput _input;

        private void Awake()
        {
            _input = GetComponent<PlayerInput>();
            
            foreach (InputAction inputAction in _input.currentActionMap.actions.Where(a => a.type == InputActionType.Button))
            {
                inputAction.performed += ctx =>
                {
                    ActionTriggered?.Invoke(inputAction.name, ctx.ReadValue<float>());
                };
            }
        }

        protected virtual void Start()
        {
            foreach (var pawn in FindObjectsOfType<Pawn>().Where(p => p.autoPossessedBy == controllerIndex && p.autoPossessedBy != -1))
            {
                pawn.Possess(this);
            }
        }

        protected virtual void Update()
        {
            foreach (InputAction inputAction in _input.currentActionMap.actions.Where(a => a.type == InputActionType.Value))
            {
                AxisUpdated?.Invoke(inputAction.name, new AxisValue(inputAction));
            }
        }
    }
}
