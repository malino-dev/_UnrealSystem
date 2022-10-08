using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

namespace UnrealSystem.Engine
{
    [RequireComponent(typeof(PlayerInput))]
    public class PlayerController : PawnController
    {
        private PlayerInput _input;

        protected virtual void Awake()
        {
            _input = GetComponent<PlayerInput>();

            // TODO: implicitly only supports 1 action map!!!
            IEnumerable<InputAction> inputActions = from a in _input.currentActionMap.actions
                                                    where a.type == InputActionType.Button
                                                    select a;
            
            foreach (var inputAction in inputActions)
            {
                inputAction.performed += ctx =>
                {
                    RaiseAction(inputAction.name, ctx.ReadValue<float>());
                };
            }
        }

        protected virtual void Update()
        {
            // TODO: implicitly only supports 1 action map!!!
            IEnumerable<InputAction> inputActions = from a in _input.currentActionMap.actions
                                                    where a.type == InputActionType.Value
                                                    select a;
            
            foreach (InputAction inputAction in inputActions)
            {
                RaiseAxisUpdate(inputAction.name, new AxisValue(inputAction));
            }
        }
    }
}
