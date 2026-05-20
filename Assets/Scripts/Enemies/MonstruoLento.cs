using UnityEngine;

namespace FearPark.Enemies
{
    // LSP: puede usarse en cualquier lugar donde el sistema espere un MonstruoBase
    public class MonstruoLento : MonstruoBase
    {
        private float _timer = 0f;

        public override void Mover()
        {
            _timer += Time.deltaTime;
            // Actualiza destino cada medio segundo para simular torpeza y no saturar CPU
            if (_timer >= 0.5f)
            {
                _agent.SetDestination(_playerTransform.position);
                _timer = 0f;
            }
        }

        public override void Atacar()
        {
            Debug.Log("MonstruoLento ataca - Daño bajo.");
        }
    }
}
