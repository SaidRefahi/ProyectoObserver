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
            // Agregamos el listener para cuando muevas el slider con el mouse
            _sliderMiedo.onValueChanged.AddListener(OnSliderDrag);
        }

        public void OnSliderDrag(float valor)
        {
            if (GestorMiedo.Instance != null)
            {
                // El slider va de 0 a 1, el miedo de 0 a 100
                GestorMiedo.Instance.SetearMiedo(valor * 100f);
            }
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
            // SetValueWithoutNotify evita un bucle infinito (Slider cambia Miedo -> Miedo cambia Slider...)
            _sliderMiedo.SetValueWithoutNotify(nivelActual / 100f);
            _textoNivel.text = nivelActual.ToString();
        }
    }
}
