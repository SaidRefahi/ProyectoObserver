using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FearPark.Data
{
    [CreateAssetMenu(fileName = "TablaSpawn", menuName = "FearPark/Datos/TablaSpawn")]
    public class SpawnTableSO : ScriptableObject
    {
        [SerializeField] private List<MonsterDataSO> monstruosDisponibles;

        public IReadOnlyList<MonsterDataSO> MonstruosDisponibles => monstruosDisponibles;

        public MonsterDataSO ObtenerMonstruoAleatorio(int nivelMiedo)
        {
            var filtrados = monstruosDisponibles.Where(m => m.nivelMiedoMinimo <= nivelMiedo).ToList();

            if (filtrados.Count == 0) return null;

            float pesoTotal = filtrados.Sum(m => m.pesoDeAparicion);

            float valorAleatorio = Random.Range(0f, pesoTotal);

            float pesoAcumulado = 0f;
            foreach (var monstruo in filtrados)
            {
                pesoAcumulado += monstruo.pesoDeAparicion;
                if (valorAleatorio <= pesoAcumulado)
                {
                    return monstruo;
                }
            }

            return filtrados.Last();
        }
    }
}
