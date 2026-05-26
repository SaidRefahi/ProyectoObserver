using UnityEngine;
using FearPark.Core;

namespace FearPark.Observers
{
    public class SistemaSonido : MonoBehaviour, ISistemaMiedoObserver
    {
        [SerializeField] private AudioSource _fuenteAudio;
        
        [Header("Configuración (ScriptableObject)")]
        [SerializeField] private FearPark.Data.AudioConfigSO _audioConfig;

        private int _indiceActual = -1;

        private void Start()
        {
            GestorMiedo.Instance.Registrar(this);
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
            Debug.Log("SistemaSonido notificado: nivel=" + nivelActual);
            
            if (_audioConfig == null) return;
            
            int nuevoIndice = ObtenerIndicePorNivel(nivelActual);
            
            if (nuevoIndice != _indiceActual)
            {
                _indiceActual = nuevoIndice;
                if (_indiceActual < _audioConfig.clipsPorRango.Length && _fuenteAudio != null)
                {
                    _fuenteAudio.clip = _audioConfig.clipsPorRango[_indiceActual];
                    _fuenteAudio.volume = _audioConfig.volumenesPorRango[_indiceActual];
                    _fuenteAudio.Play();
                }
            }
        }

        private int ObtenerIndicePorNivel(int nivel)
        {
            if (nivel <= 25) return 0;
            if (nivel <= 50) return 1;
            if (nivel <= 75) return 2;
            return 3;
        }
    }
}
