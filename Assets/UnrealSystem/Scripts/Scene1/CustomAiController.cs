using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnrealSystem.Engine;

namespace UnrealSystem
{
    public class CustomAiController : PawnController
    {
        private float _time = 0f;
        private float _multiplier = 1f;

        private void Update()
        {
            _time += Time.deltaTime;

            if (_time >= 1f)
            {
                RaiseAction("Fire");
                _time = 0f;
                _multiplier *= -1f;
            }

            var move = new Vector2(1f * _multiplier, 0f);
            RaiseAxisUpdate("Move", move);
        }
    }
}