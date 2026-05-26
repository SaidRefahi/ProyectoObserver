using UnityEngine;

namespace FearPark.Core
{
    public class DisparadorAutomatico : MonoBehaviour
    {
        [SerializeField] private float _rango = 8f;
        [SerializeField] private float _cadencia = 0.5f;
        [SerializeField] private GameObject _prefabProyectil;
        [SerializeField] private Transform _puntoDisparo;
        [SerializeField] private LayerMask _enemyLayer;

        private float _timerDisparo;
        private readonly Collider[] _collidersEnemigos = new Collider[16];

        public Transform EnemigoMasCercano { get; private set; }

        private void Update()
        {
            EnemigoMasCercano = ObtenerEnemigoMasCercano();
            _timerDisparo += Time.deltaTime;

            if (_timerDisparo >= _cadencia && EnemigoMasCercano != null)
            {
                Disparar(EnemigoMasCercano);
                _timerDisparo = 0f;
            }
        }

        private Transform ObtenerEnemigoMasCercano()
        {
            // Zero GC: Usar NonAlloc para evitar asignaciones de memoria en el loop de Update
            int cantidad = Physics.OverlapSphereNonAlloc(transform.position, _rango, _collidersEnemigos, _enemyLayer);
            
            Transform masCercano = null;
            float distanciaMinima = float.MaxValue;
            Vector3 miPosicion = transform.position;

            for (int i = 0; i < cantidad; i++)
            {
                Collider col = _collidersEnemigos[i];
                if (col == null) continue;

                float distSqr = (col.transform.position - miPosicion).sqrMagnitude;
                if (distSqr < distanciaMinima)
                {
                    distanciaMinima = distSqr;
                    masCercano = col.transform;
                }
            }

            return masCercano;
        }

        private void Disparar(Transform target)
        {
            if (_prefabProyectil == null || _puntoDisparo == null) return;

            Vector3 dir = target.position - _puntoDisparo.position;
            dir.y = 0f;
            
            if (dir.sqrMagnitude > 0.01f)
            {
                Quaternion rotacion = Quaternion.LookRotation(dir);
                Instantiate(_prefabProyectil, _puntoDisparo.position, rotacion);
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _rango);
        }
    }
}
