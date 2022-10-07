using UnityEngine;
using UnrealSystem.Engine;

namespace UnrealSystem
{
    public class PlayerScript : MonoBehaviour
    {
        private InputComponent _input;
        
        private void Awake()
        {
            _input = GetComponent<InputComponent>();
        }

        private void Start()
        {
            _input.BindAction("Fire", OnFire);
            _input.BindAxis("Move", OnMove);
        }

        private void OnFire(float value)
        {
            Debug.Log("[fire]");
        }

        private void OnMove(AxisValue value)
        {
            transform.position += (Vector3)value.Get<Vector2>() * (6f * Time.deltaTime);
        }
    }
}