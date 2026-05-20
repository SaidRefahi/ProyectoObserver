using UnityEngine;

namespace FearPark.Enemies
{
    public class MonstruoElite : MonstruoBase
    {
        public override void Inicializar(FearPark.Data.MonsterDataSO data, Transform player)
        {
            base.Inicializar(data, player);
            _vida *= 2f; // El doble de resistente
        }

        public override void Mover()
        {
            Rigidbody rbPlayer = _playerTransform.GetComponent<Rigidbody>();
            if (rbPlayer != null)
            {
                // Predicción del movimiento del jugador basándose en su velocidad
                Vector3 prediccion = _playerTransform.position + rbPlayer.linearVelocity * 0.5f;
                _agent.SetDestination(prediccion);
            }
            else
            {
                _agent.SetDestination(_playerTransform.position);
            }
        }

        public override void Atacar()
        {
            Debug.Log("MonstruoElite ataca - Daño alto (AOE).");
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    Debug.Log("El jugador recibió el ataque de área del Élite.");
                }
            }
        }
    }
}
