using UnityEngine;
using Unity.Cinemachine;
using FearPark.Core;

namespace FearPark.Observers
{
    public class SistemaCamara : MonoBehaviour, ISistemaMiedoObserver
    {
        [SerializeField] private CinemachineCamera _virtualCam;
        
        [Header("Configuración (ScriptableObject)")]
        [SerializeField] private FearPark.Data.CameraConfigSO _cameraConfig;

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
            if (_perlin == null || _virtualCam == null || _cameraConfig == null) return;

            float t = nivelActual / 100f;
            
            _perlin.AmplitudeGain = Mathf.Lerp(_cameraConfig.amplitudShakeMin, _cameraConfig.amplitudShakeMax, _cameraConfig.curvaShake.Evaluate(t));
            _perlin.FrequencyGain = Mathf.Lerp(_cameraConfig.frecuenciaShakeMin, _cameraConfig.frecuenciaShakeMax, _cameraConfig.curvaShake.Evaluate(t));
            
            _virtualCam.Lens.OrthographicSize = Mathf.Lerp(_cameraConfig.distanciaCamaraBase, _cameraConfig.distanciaCamaraMaxima, _cameraConfig.curvaZoom.Evaluate(t));
        }
    }
}
