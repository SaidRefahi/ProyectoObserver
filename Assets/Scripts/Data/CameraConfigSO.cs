using UnityEngine;

namespace FearPark.Data
{
    [CreateAssetMenu(fileName = "ConfigCamara", menuName = "FearPark/Config/Camara")]
    public class CameraConfigSO : ScriptableObject
    {
        public float amplitudShakeMin;
        public float amplitudShakeMax;
        
        public float frecuenciaShakeMin;
        public float frecuenciaShakeMax;
        
        public float distanciaCamaraBase;
        public float distanciaCamaraMaxima;
        
        public AnimationCurve curvaShake;
        public AnimationCurve curvaZoom;
    }
}
