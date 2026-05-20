using UnityEngine;
using FearPark.Core;
using FearPark.Data;
using FearPark.Enemies;

namespace FearPark.Factory
{
    public class FabricaMonstruos : MonoBehaviour, IMonstruoFactory, ISistemaMiedoObserver
    {
        [Header("Configuración de Factory")]
        [SerializeField] private SpawnTableSO tablaSpawn;
        [SerializeField] private FearConfigSO configMiedo;
        
        [Header("Referencias de Escena")]
        [SerializeField] private Transform[] puntosDeSpawn;
        [SerializeField] private Transform playerTransform;

        private float _tiempoDesdeUltimoSpawn = 0f;
        private int _nivelActual = 0;

        private void Start()
        {
            // OBSERVER: Nos suscribimos al GestorMiedo al iniciar
            if (GestorMiedo.Instance != null)
            {
                GestorMiedo.Instance.Registrar(this);
            }
        }

        private void OnDestroy()
        {
            // OBSERVER: Nos desuscribimos de forma segura
            if (GestorMiedo.Instance != null)
            {
                GestorMiedo.Instance.Remover(this);
            }
        }

        // OBSERVER: Recibimos la notificación de cambio por interfaz, SIN usar delegates/events.
        public void OnMiedoCambiado(int nivelActual)
        {
            _nivelActual = nivelActual;
        }

        private void Update()
        {
            if (tablaSpawn == null || configMiedo == null || puntosDeSpawn.Length == 0) return;

            _tiempoDesdeUltimoSpawn += Time.deltaTime;
            
            // Calculamos cada cuánto debe spawnear basándonos en la curva de frecuencia y el miedo actual
            float factor = configMiedo.curvaFrecuenciaSpawn.Evaluate(_nivelActual / 100f);
            float intervalo = configMiedo.intervaloSpawnBase * (1f - factor);
            
            intervalo = Mathf.Max(0.5f, intervalo); // Limite mínimo para no saturar

            if (_tiempoDesdeUltimoSpawn >= intervalo)
            {
                MonsterDataSO dataAInstanciar = tablaSpawn.ObtenerMonstruoAleatorio(_nivelActual);
                if (dataAInstanciar != null && dataAInstanciar.prefab != null)
                {
                    Vector3 pos = puntosDeSpawn[Random.Range(0, puntosDeSpawn.Length)].position;
                    CrearMonstruo(dataAInstanciar, pos);
                }
                
                _tiempoDesdeUltimoSpawn = 0f;
            }
        }

        // FACTORY: Devolvemos siempre MonstruoBase (la abstracción). Ocultamos cómo se fabrica.
        // OCP: Si añadimos 10 monstruos más al SpawnTableSO, este código nunca se modifica.
        public MonstruoBase CrearMonstruo(MonsterDataSO data, Vector3 posicion)
        {
            GameObject go = Instantiate(data.prefab, posicion, Quaternion.identity);
            MonstruoBase monstruo = go.GetComponent<MonstruoBase>();
            
            if (monstruo != null)
            {
                monstruo.Inicializar(data, playerTransform);
            }
            
            return monstruo;
        }
    }
}
