using UnityEngine;
using FearPark.Enemies;

namespace FearPark.Core
{
    public class SistemaVidaPlayer : MonoBehaviour
    {
        [SerializeField] private float _vidaMaxima = 100f;
        [SerializeField] private float _cooldownInvencibilidad = 1f;

        private float _vidaActual;
        private float _timerInvencibilidad;

        public float VidaNormalizada => _vidaMaxima > 0f ? Mathf.Clamp01(_vidaActual / _vidaMaxima) : 0f;

        private void Start()
        {
            _vidaActual = _vidaMaxima;
        }

        private void Update()
        {
            if (_timerInvencibilidad > 0f)
            {
                _timerInvencibilidad -= Time.deltaTime;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_timerInvencibilidad > 0f) return;

            MonstruoBase monstruo = other.GetComponent<MonstruoBase>();
            if (monstruo != null && monstruo.Datos != null)
            {
                _vidaActual -= monstruo.Datos.danio;
                _timerInvencibilidad = _cooldownInvencibilidad;

                if (_vidaActual <= 0f)
                {
                    _vidaActual = 0f;
                    if (GameManager.Instance != null)
                    {
                        GameManager.Instance.GameOver();
                    }
                }
            }
        }
    }
}
