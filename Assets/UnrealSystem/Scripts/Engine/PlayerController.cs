using System;
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
            
            foreach (InputAction inputAction in _input.currentActionMap.actions.Where(a => a.type == InputActionType.Button))
            {
                inputAction.performed += ctx =>
                {
                    RaiseAction(inputAction.name, ctx.ReadValue<float>());
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
                RaiseAxisUpdate(inputAction.name, new AxisValue(inputAction));
            }
        }
    }
}
