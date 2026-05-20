using UnityEngine;

namespace FearPark.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _velocidad = 5f;
        [SerializeField] private float _multiplicadorMiedo = 2f;
        
        private FearParkInputActions _actions;
        private Rigidbody _rigidbody;
        private Vector3 _moveDirection;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _actions = new FearParkInputActions();
        }

        private void OnEnable()
        {
            _actions.Player.Enable();
        }

        private void OnDisable()
        {
            _actions.Player.Disable();
        }

        private void Update()
        {
            Vector2 input = _actions.Player.Move.ReadValue<Vector2>();
            
            _moveDirection.Set(input.x, 0f, input.y);
            
            if (_moveDirection.sqrMagnitude > 0.01f && GestorMiedo.Instance != null)
            {
                float fearIncrease = _moveDirection.magnitude * _multiplicadorMiedo * Time.deltaTime;
                GestorMiedo.Instance.AumentarMiedo(fearIncrease);
            }
        }

        private void FixedUpdate()
        {
            Vector3 targetPos = _rigidbody.position + _moveDirection * (_velocidad * Time.fixedDeltaTime);
            _rigidbody.MovePosition(targetPos);
        }
    }
}
