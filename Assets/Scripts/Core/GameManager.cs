using UnityEngine;
using UnityEngine.SceneManagement;

namespace FearPark.Core
{
    public class GameManager : MonoBehaviour
    {
        public enum GameState { Jugando, GameOver }

        public static GameManager Instance { get; private set; }

        [SerializeField] private GameObject _panelGameOver;

        private GameState _estado = GameState.Jugando;

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

        public void GameOver()
        {
            if (_estado == GameState.GameOver) return; // Evitar doble llamada

            _estado = GameState.GameOver;
            Time.timeScale = 0f;

            if (_panelGameOver != null)
            {
                _panelGameOver.SetActive(true);
            }

            // Deshabilitar input del player de forma segura
            Player player = FindAnyObjectByType<Player>();
            if (player != null)
            {
                // Si usa el componente PlayerInput nativo
                var playerInput = player.GetComponent<UnityEngine.InputSystem.PlayerInput>();
                if (playerInput != null)
                {
                    playerInput.DeactivateInput();
                }
                
                // Además desactivamos el script Player para detener movimientos de física
                player.enabled = false;
            }
        }

        public void Reiniciar()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
