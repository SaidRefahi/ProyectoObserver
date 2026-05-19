using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FearPark.Core;

namespace FearPark.UI
{
    public class UIManager : MonoBehaviour, ISistemaMiedoObserver
    {
        [SerializeField] private Slider _sliderMiedo;
        [SerializeField] private TextMeshProUGUI _textoNivel;

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
            _sliderMiedo.value = nivelActual / 100f;
            _textoNivel.text = nivelActual.ToString();
        }
    }
}
