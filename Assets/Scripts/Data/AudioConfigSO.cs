using UnityEngine;

namespace FearPark.Data
{
    [CreateAssetMenu(fileName = "ConfigAudio", menuName = "FearPark/Config/Audio")]
    public class AudioConfigSO : ScriptableObject
    {
        // DIP: SistemaSonido lee estos arreglos para decidir cómo reaccionar a niveles de miedo.
        public AudioClip[] clipsPorRango = new AudioClip[4];
        public float[] volumenesPorRango = new float[4];
        public float tiempoFade;
    }
}
