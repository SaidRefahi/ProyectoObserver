using UnityEngine;

namespace FearPark.Core
{
    public class ZonaMuerte : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.GameOver();
                }
            }
        }
    }
}
