using System;
using System.Linq;
using UnityEngine;
using UnrealSystem.Engine;

namespace UnrealSystem
{
    public class AiController : PawnController
    {
        public override event Action<string, AxisValue> AxisUpdated;
        public override event Action<string, float> ActionTriggered;

        private float _time = 0f;
        private float _multiplier = 1f;

        private void Start()
        {
            
            foreach (var pawn in FindObjectsOfType<Pawn>().Where(p => p.autoPossessedBy == controllerIndex && p.autoPossessedBy != -1))
            {
                pawn.Possess(this);
            }

        }

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time >= 1f)
            {
                ActionTriggered?.Invoke("Fire", 1f);
                _time = 0f;
                _multiplier *= -1f;
            }
            
            AxisUpdated?.Invoke("Move", new AxisValue(new Vector2(_multiplier, 0f)));
        }
    }
}