using UnityEngine;

namespace FearPark.Enemies
{
    public class MonstruoLento : MonstruoBase
    {
        private float _timer = 0f;

        public override void Mover()
        {
            _timer += Time.deltaTime;
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
