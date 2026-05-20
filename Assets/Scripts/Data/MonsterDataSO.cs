using UnityEngine;

namespace FearPark.Data
{
    [CreateAssetMenu(fileName = "NuevoMonstruo", menuName = "FearPark/Datos/Monstruo")]
    public class MonsterDataSO : ScriptableObject
    {
        // OCP: Agregar un nuevo monstruo solo implica crear un nuevo asset con estos datos, sin tocar una sola línea de código.
        
        public string nombreMonstruo;
        
        [Header("Estadísticas Base")]
        public float vida;
        public float velocidad;
        public float danio;
        
        [Header("Navegación (NavMesh)")]
        public float radioColision;
        public float alturaAgente;
        
        [Header("Multimedia")]
        public AudioClip sonidoMuerte;
        public AudioClip sonidoAtaque;
        public Sprite iconoUI;
        public GameObject prefab;
        
        [Header("Reglas de Spawn")]
        public int nivelMiedoMinimo;
        public float pesoDeAparicion;
    }
}
