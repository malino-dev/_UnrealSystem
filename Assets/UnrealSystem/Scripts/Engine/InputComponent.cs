#nullable enable

using System;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable CS8618

namespace UnrealSystem.Engine
{
    [RequireComponent(typeof(Pawn))]
    public class InputComponent : MonoBehaviour
    {
        private Pawn _pawn;

        private readonly Dictionary<string, AxisValue> _axisValues = new();
        
        private readonly Dictionary<string, List<Action<AxisValue>>> _axisActions = new();
        private readonly Dictionary<string, List<Action<float>>> _actionActions = new();
        
        private void Awake()
        {
            _pawn = GetComponent<Pawn>();
        }

        private void Start()
        {
            /*_pawn.AxisUpdated += OnAxisUpdated;
            _pawn.ActionTriggered += OnActionTriggered;*/
            _pawn.Possessed += OnPossessed;
            _pawn.Unpossessed += OnUnpossessed;
        }

        private void OnPossessed(PawnController controller)
        {
            controller.AxisUpdated += OnAxisUpdated;
            controller.ActionTriggered += OnActionTriggered;
        }

        private void OnUnpossessed(PawnController controller)
        {
            controller.AxisUpdated -= OnAxisUpdated;
            controller.ActionTriggered -= OnActionTriggered;
        }

        public T ReadAxis<T>(string key) where T : struct
        {
            if (!_axisValues.ContainsKey(key)) return default;
            return _axisValues[key].Get<T>();
        }

        private void SetAxisValue(string key, AxisValue value)
        {
            _axisValues[key] = value;
        }

        public void Reset()
        {
            _axisValues.Clear();
        }

        public void BindAction(string key, Action<float> action)
        {
            if (!_actionActions.ContainsKey(key)) _actionActions[key] = new();

            _actionActions[key].Add(action);
        }
        
        public void BindAxis(string key, Action<AxisValue> action)
        {
            if (!_axisActions.ContainsKey(key)) _axisActions[key] = new();

            _axisActions[key].Add(action);
        }

        private void OnAxisUpdated(string key, AxisValue value)
        {
            SetAxisValue(key, value);
            
            if (!_axisActions.ContainsKey(key)) return;

            foreach (Action<AxisValue> action in _axisActions[key])
            {
                action.Invoke(value);
            }
        }

        private void OnActionTriggered(string key, float value)
        {
            if (!_actionActions.ContainsKey(key)) return;

            foreach (var action in _actionActions[key])
            {
                action.Invoke(value);
            }
        }
    }
}