using UnityEngine;

namespace FearPark.Enemies
{
    public class MonstruoRapido : MonstruoBase
    {
        public override void Mover()
        {
            _agent.SetDestination(_playerTransform.position);
        }

        public override void Atacar()
        {
            Debug.Log("MonstruoRapido ataca - Daño medio y empuje.");
            
            Rigidbody rbPlayer = _playerTransform.GetComponent<Rigidbody>();
            if (rbPlayer != null)
            {
                Vector3 direccionEmpuje = (_playerTransform.position - transform.position).normalized;
                rbPlayer.AddForce(direccionEmpuje * 10f, ForceMode.Impulse);
            }
        }
    }
}
