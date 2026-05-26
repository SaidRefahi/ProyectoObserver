using UnityEngine;
using UnityEngine.AI;
using FearPark.Data;
using FearPark.Core;
using FearPark.Factory;

namespace FearPark.Enemies
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class MonstruoBase : MonoBehaviour, IDamageable
    {
        protected MonsterDataSO _datos;
        protected float _vida;
        protected NavMeshAgent _agent;
        
        public MonsterDataSO Datos => _datos;
        
        // Estático para no tener que buscarlo en cada instancia (eficiente)
        protected static Transform _playerTransform;

        protected virtual void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        protected virtual void Start()
        {
            if (_playerTransform == null)
            {
                Player player = FindAnyObjectByType<Player>();
                if (player != null)
                {
                    _playerTransform = player.transform;
                }
            }

            if (_datos == null)
            {
                FabricaMonstruos fabrica = FindAnyObjectByType<FabricaMonstruos>();
                if (fabrica != null && fabrica.TablaSpawn != null && fabrica.TablaSpawn.MonstruosDisponibles != null)
                {
                    foreach (MonsterDataSO data in fabrica.TablaSpawn.MonstruosDisponibles)
                    {
                        if (data != null && data.prefab != null && gameObject.name.StartsWith(data.prefab.name))
                        {
                            Inicializar(data, _playerTransform);
                            break;
                        }
                    }
                }
            }
            else if (_vida <= 0f)
            {
                _vida = _datos.vida;
            }
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
            string nombre = _datos != null ? _datos.nombreMonstruo : gameObject.name;
            Debug.Log($"{nombre} está atacando al jugador.");
        }

        public virtual void TakeDamage(float damage)
        {
            _vida -= damage;
            if (_vida <= 0f)
            {
                Morir();
            }
        }

        public virtual void Morir()
        {
            if (_datos != null && _datos.sonidoMuerte != null)
            {
                AudioSource.PlayClipAtPoint(_datos.sonidoMuerte, transform.position);
            }
            
            if (FearPark.UI.UIManager.Instance != null)
            {
                FearPark.UI.UIManager.Instance.AgregarPuntos(10);
            }
            
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
