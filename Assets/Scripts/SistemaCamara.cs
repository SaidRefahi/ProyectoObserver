using UnityEngine;
using Unity.Cinemachine;
using FearPark.Core;

namespace FearPark.Observers
{
    public class SistemaCamara : MonoBehaviour, ISistemaMiedoObserver
    {
        [SerializeField] private CinemachineCamera _virtualCam;
        [SerializeField] private float _amplitudMax = 3f;
        [SerializeField] private float _frecuenciaMax = 2f;
        [SerializeField] private float _tamañoBase = 10f;
        [SerializeField] private float _tamañoMax = 15f;

        private CinemachineBasicMultiChannelPerlin _perlin;

        private void Start()
        {
            GestorMiedo.Instance.Registrar(this);

            if (_virtualCam != null)
            {
                _perlin = _virtualCam.GetComponent<CinemachineBasicMultiChannelPerlin>();
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
            if (_perlin == null || _virtualCam == null) return;

            float t = nivelActual / 100f;
            
            _perlin.AmplitudeGain = Mathf.Lerp(0f, _amplitudMax, t);
            _perlin.FrequencyGain = Mathf.Lerp(0f, _frecuenciaMax, t);
            
            _virtualCam.Lens.OrthographicSize = Mathf.Lerp(_tamañoBase, _tamañoMax, t);
        }
    }
}
