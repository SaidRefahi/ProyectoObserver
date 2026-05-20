using UnityEngine;
using UnityEngine.AI;
using FearPark.Data;

namespace FearPark.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class MonstruoBase : MonoBehaviour
    {
        protected float _vida;
        protected NavMeshAgent _agent;
        protected MonsterDataSO _datos;
        
        // Estático para no tener que buscarlo en cada instancia (eficiente)
        protected static Transform _playerTransform;

        protected virtual void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        public virtual void Inicializar(MonsterDataSO data, Transform player)
        {
            _datos = data;
            _playerTransform = player;
            
            _vida = data.vida;
            _agent.speed = data.velocidad;
            _agent.radius = data.radioColision;
        }

        protected virtual void Update()
        {
            if (_playerTransform != null && _agent.isOnNavMesh)
            {
                Mover();
            }
        }

        public abstract void Mover();

        public virtual void Atacar()
        {
            Debug.Log($"{_datos.nombreMonstruo} está atacando al jugador.");
        }

        public virtual void Morir()
        {
            if (_datos.sonidoMuerte != null)
            {
                AudioSource.PlayClipAtPoint(_datos.sonidoMuerte, transform.position);
            }
            
            // Aquí llamaríamos al GameManager o UIManager para dar puntos
            // UIManager.Instance.AgregarPuntos(10);
            
            Destroy(gameObject);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Atacar();
            }
        }
    }
}
