using System;
using UnityEngine;
using UnrealSystem.Engine;

namespace UnrealSystem
{
    public class MoveTo : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;
        [SerializeField] private Transform target;
        
        private InputComponent _input;

        public Transform Target
        {
            get => target;
            set => target = value;
        }

        public float MoveSpeed => moveSpeed;

        private void Awake() => TryGetComponent(out _input);

        private void Update()
        {
            if (Target == null) return;
            
            transform.position = Vector3.MoveTowards(
                transform.position,
                Target.position, 
                MoveSpeed * Time.deltaTime);
        }
    }
}