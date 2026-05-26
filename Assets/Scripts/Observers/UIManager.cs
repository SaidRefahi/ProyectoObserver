using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FearPark.Core;

namespace FearPark.UI
{
    public class UIManager : MonoBehaviour, ISistemaMiedoObserver
    {
        public static UIManager Instance { get; private set; }

        [Header("Miedo")]
        [SerializeField] private Slider _sliderMiedo;
        [SerializeField] private TextMeshProUGUI _textoNivel;

        [Header("Vida y Estado")]
        [SerializeField] private Slider _sliderVida;
        [SerializeField] private SistemaVidaPlayer _vidaPlayer;

        [Header("Estadísticas")]
        [SerializeField] private TextMeshProUGUI _textoPuntos;
        [SerializeField] private TextMeshProUGUI _textoTiempo;

        private int _puntos = 0;
        private float _tiempoJugando = 0f;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (GestorMiedo.Instance != null)
            {
                GestorMiedo.Instance.Registrar(this);
            }

            if (_sliderMiedo != null)
            {
                _sliderMiedo.onValueChanged.AddListener(OnSliderDrag);
            }
        }

        private void Update()
        {
            if (_sliderVida != null && _vidaPlayer != null)
            {
                _sliderVida.value = _vidaPlayer.VidaNormalizada;
            }

            _tiempoJugando += Time.deltaTime;
            if (_textoTiempo != null)
            {
                _textoTiempo.text = ((int)_tiempoJugando).ToString() + "s";
            }
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
            if (_sliderMiedo != null)
            {
                // SetValueWithoutNotify evita un bucle infinito (Slider cambia Miedo -> Miedo cambia Slider...)
                _sliderMiedo.SetValueWithoutNotify(nivelActual / 100f);
                
                Image fillImage = _sliderMiedo.fillRect.GetComponent<Image>();
                if (fillImage != null)
                {
                    fillImage.color = Color.Lerp(Color.green, Color.red, nivelActual / 100f);
                }
            }

            if (_textoNivel != null)
            {
                _textoNivel.text = nivelActual.ToString();
            }
        }

        public void AgregarPuntos(int cantidad)
        {
            _puntos += cantidad;
            if (_textoPuntos != null)
            {
                _textoPuntos.text = _puntos.ToString();
            }
        }
    }
}
