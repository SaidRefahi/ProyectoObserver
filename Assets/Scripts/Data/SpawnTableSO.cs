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

        // OCP: Si agregamos nuevos monstruos en el Inspector, el algoritmo los considera sin necesidad de tocar este código.
        public MonsterDataSO ObtenerMonstruoAleatorio(int nivelMiedo)
        {
            // 1. Filtrar
            var filtrados = monstruosDisponibles.Where(m => m.nivelMiedoMinimo <= nivelMiedo).ToList();

            if (filtrados.Count == 0) return null;

            // 2. Calcular peso total
            float pesoTotal = filtrados.Sum(m => m.pesoDeAparicion);

            // 3. Random entre 0 y el peso total
            float valorAleatorio = Random.Range(0f, pesoTotal);

            // 4. Iterar acumulando
            float pesoAcumulado = 0f;
            foreach (var monstruo in filtrados)
            {
                pesoAcumulado += monstruo.pesoDeAparicion;
                if (valorAleatorio <= pesoAcumulado)
                {
                    return monstruo; // Seleccionado!
                }
            }

            return filtrados.Last(); // Fallback de seguridad
        }
    }
}
