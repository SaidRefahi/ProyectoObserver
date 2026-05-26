using UnityEngine;
using FearPark.Core;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FearPark.Observers
{
    public class SistemaLuces : MonoBehaviour, ISistemaMiedoObserver
    {
        [Header("Luz Ambiental (Mundo)")]
        [SerializeField] private Light _luzAmbiental;
        [SerializeField] private Color _colorAmbientalCalmo = Color.white;
        [SerializeField] private Color _colorAmbientalTerror = Color.red;

        [Header("Luz Jugador (Antorcha)")]
        [SerializeField] private Light _luzJugador;
        [SerializeField] private Color _colorJugadorCalmo = new Color(1f, 0.5f, 0f);
        [SerializeField] private Color _colorJugadorTerror = Color.red;

        [Header("Post-Procesado")]
        [SerializeField] private Volume _globalVolume;

        private Vignette _vignette;

        private void Start()
        {
            GestorMiedo.Instance.Registrar(this);
            
            if (_globalVolume != null && _globalVolume.profile != null)
            {
                _globalVolume.profile.TryGet(out _vignette);
            }
        }

        private void OnDestroy()
        {
            if (GestorMiedo.Instance != null)
            {
                GestorMiedo.Instance.Remover(this);
            }
        }

        public void OnMiedoCambiado(int nivelActual)
        {
            float t = nivelActual / 100f;
            
            if (_luzAmbiental != null)
            {
                _luzAmbiental.color = Color.Lerp(_colorAmbientalCalmo, _colorAmbientalTerror, t);
                _luzAmbiental.intensity = Mathf.Lerp(1f, 0.2f, t);
            }

            if (_luzJugador != null)
            {
                _luzJugador.color = Color.Lerp(_colorJugadorCalmo, _colorJugadorTerror, t);
            }

            if (_vignette != null)
            {
                _vignette.intensity.value = Mathf.Lerp(0f, 0.5f, t);
            }
        }
    }
}
