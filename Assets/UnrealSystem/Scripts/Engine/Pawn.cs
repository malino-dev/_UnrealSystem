using System;
using UnityEngine;

namespace UnrealSystem.Engine
{
    public class Pawn : MonoBehaviour
    {
        [SerializeField] public int autoPossessedBy = -1;
        
        private InputComponent _input;
        private PawnController _controller;

        public event Action<PawnController> Possessed;
        public event Action<PawnController> Unpossessed;

        private void Awake()
        {
            _input = GetComponent<InputComponent>();
        }

        public void Possess(PawnController controller)
        {
            _controller = controller;
            Possessed?.Invoke(_controller);
        }

        public void Unpossess(PawnController controller)
        {
            if (controller != _controller)
                throw new InvalidOperationException("can't unpossess if you're not the possessing");
            
            Unpossessed?.Invoke(_controller);
            _controller = null;
        }
    }
}