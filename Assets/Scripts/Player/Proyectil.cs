using UnityEngine;

namespace FearPark.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class Proyectil : MonoBehaviour
    {
        [SerializeField] private float _velocidad = 15f;
        [SerializeField] private float _tiempoVida = 3f;
        [SerializeField] private float _danio = 10f;

        private Rigidbody _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            
            // Usamos linearVelocity como es requerido en el nuevo pipeline de Unity
            _rigidbody.linearVelocity = transform.forward * _velocidad;

            Destroy(gameObject, _tiempoVida);
        }

        private void OnTriggerEnter(Collider other)
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(_danio);
                Destroy(gameObject);
            }
        }
    }
}
