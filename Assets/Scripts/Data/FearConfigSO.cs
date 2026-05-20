using UnityEngine;

namespace FearPark.Data
{
    [CreateAssetMenu(fileName = "ConfigMiedo", menuName = "FearPark/Config/Miedo")]
    public class FearConfigSO : ScriptableObject
    {
        // DIP: GestorMiedo usa este SO, sin depender de constantes en código.
        public float velocidadIncrementoBase;
        public float multiplicadorPorMovimiento;
        public AnimationCurve curvaEfectos;
        public float intervaloSpawnBase;
        public AnimationCurve curvaFrecuenciaSpawn;
    }
}
