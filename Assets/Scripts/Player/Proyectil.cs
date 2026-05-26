using UnityEngine;
using FearPark.Enemies;

namespace FearPark.Core
{
    [RequireComponent(typeof(Rigidbody))]
    public class Proyectil : MonoBehaviour
    {
        [SerializeField] private float _velocidad = 15f;
        [SerializeField] private float _tiempoVida = 3f;

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
            MonstruoBase monstruo = other.GetComponent<MonstruoBase>();
            if (monstruo != null)
            {
                monstruo.Morir();
                Destroy(gameObject);
            }
        }
    }
}
