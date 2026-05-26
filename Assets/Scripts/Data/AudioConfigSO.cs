using UnityEngine;

namespace FearPark.Data
{
    [CreateAssetMenu(fileName = "ConfigAudio", menuName = "FearPark/Config/Audio")]
    public class AudioConfigSO : ScriptableObject
    {
        public AudioClip[] clipsPorRango = new AudioClip[4];
        public float[] volumenesPorRango = new float[4];
        public float tiempoFade;
    }
}
