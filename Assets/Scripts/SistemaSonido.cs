using UnityEngine;
using FearPark.Core;

namespace FearPark.Observers
{
    public class SistemaSonido : MonoBehaviour, ISistemaMiedoObserver
    {
        [SerializeField] private AudioSource _fuenteAudio;
        [SerializeField] private AudioClip[] _clipsPorRango = new AudioClip[4];
        [SerializeField] private float[] _volumenesPorRango = new float[4];

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
            
            int nuevoIndice = ObtenerIndicePorNivel(nivelActual);
            
            if (nuevoIndice != _indiceActual)
            {
                _indiceActual = nuevoIndice;
                if (_indiceActual < _clipsPorRango.Length && _fuenteAudio != null)
                {
                    _fuenteAudio.clip = _clipsPorRango[_indiceActual];
                    _fuenteAudio.volume = _volumenesPorRango[_indiceActual];
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
