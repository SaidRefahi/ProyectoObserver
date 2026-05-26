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
        private DisparadorAutomatico _disparador;

        public Vector3 AimWorldPosition { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _actions = new FearParkInputActions();
            _disparador = GetComponent<DisparadorAutomatico>();
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
            // INPUT: Lectura del movimiento
            Vector2 input = _actions.Player.Move.ReadValue<Vector2>();
            
            _moveDirection.Set(input.x, 0f, input.y);
            
            if (_moveDirection.sqrMagnitude > 0.01f && GestorMiedo.Instance != null)
            {
                float fearIncrease = _moveDirection.magnitude * _multiplicadorMiedo * Time.deltaTime;
                GestorMiedo.Instance.AumentarMiedo(fearIncrease);
            }

            // APUNTADO AUTOMÁTICO: Apuntar al enemigo más cercano si existe; de lo contrario, mirar a la dirección de movimiento.
            Transform target = (_disparador != null) ? _disparador.EnemigoMasCercano : null;
            if (target != null)
            {
                Vector3 dir = target.position - transform.position;
                dir.y = 0f; // Ignorar diferencia de altura
                
                if (dir.sqrMagnitude > 0.01f)
                {
                    transform.rotation = Quaternion.LookRotation(dir);
                }
                
                AimWorldPosition = target.position;
            }
            else if (_moveDirection.sqrMagnitude > 0.01f)
            {
                // Si no hay enemigos pero se mueve, rota en la dirección del movimiento
                transform.rotation = Quaternion.LookRotation(_moveDirection);
            }
        }

        private void FixedUpdate()
        {
            Vector3 targetPos = _rigidbody.position + _moveDirection * (_velocidad * Time.fixedDeltaTime);
            _rigidbody.MovePosition(targetPos);
        }
    }
}
