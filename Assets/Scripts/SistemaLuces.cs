using UnityEngine;
using FearPark.Core;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace FearPark.Observers
{
    public class SistemaLuces : MonoBehaviour, ISistemaMiedoObserver
    {
        [SerializeField] private Light _luzPrincipal;
        [SerializeField] private Color _colorCalmo = Color.white;
        [SerializeField] private Color _colorTerror = Color.red;
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
            Debug.Log("SistemaLuces notificado: nivel=" + nivelActual);
            
            float t = nivelActual / 100f;
            
            if (_luzPrincipal != null)
            {
                _luzPrincipal.color = Color.Lerp(_colorCalmo, _colorTerror, t);
                _luzPrincipal.intensity = Mathf.Lerp(1f, 0.2f, t);
            }

            if (_vignette != null)
            {
                _vignette.intensity.value = Mathf.Lerp(0f, 0.5f, t);
            }
        }
    }
}
