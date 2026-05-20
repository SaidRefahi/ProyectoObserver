using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FearPark.Core
{
    public class GestorMiedo : MonoBehaviour
    {
        public static GestorMiedo Instance { get; private set; }

        private float _nivelMiedo;
        private int _miedoEnteroAnterior;
        
        private List<ISistemaMiedoObserver> _observers = new List<ISistemaMiedoObserver>();

        [SerializeField] private bool _aumentoAutomatico = false; // Desactivado por defecto para que pruebes con el slider
        
        [Header("Configuración (ScriptableObject)")]
        [SerializeField] private FearPark.Data.FearConfigSO _fearConfig;

        public float NivelMiedo 
        { 
            get { return _nivelMiedo; } 
            private set { _nivelMiedo = value; }
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            if (_aumentoAutomatico)
            {
                StartCoroutine(IncrementoAutomatico());
            }
        }

        public void SetearMiedo(float nivel)
        {
            _nivelMiedo = Mathf.Clamp(nivel, 0f, 100f);
            int miedoEnteroActual = (int)_nivelMiedo;
            
            if (miedoEnteroActual != _miedoEnteroAnterior)
            {
                _miedoEnteroAnterior = miedoEnteroActual;
                Notificar();
            }
        }

        public void Registrar(ISistemaMiedoObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
        }

        public void Remover(ISistemaMiedoObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
            }
        }

        private void Notificar()
        {
            for (int i = 0; i < _observers.Count; i++)
            {
                _observers[i].OnMiedoCambiado((int)_nivelMiedo);
            }
        }

        public void AumentarMiedo(float cantidad)
        {
            _nivelMiedo = Mathf.Clamp(_nivelMiedo + cantidad, 0f, 100f);
            
            int miedoEnteroActual = (int)_nivelMiedo;
            
            if (miedoEnteroActual != _miedoEnteroAnterior)
            {
                _miedoEnteroAnterior = miedoEnteroActual;
                Notificar();
            }
        }

        private IEnumerator IncrementoAutomatico()
        {
            WaitForSeconds wait = new WaitForSeconds(1f);
            while (true)
            {
                yield return wait;
                
                // DIP: Leemos la velocidad desde el SO. Si no hay SO asignado, usamos 1f como fallback.
                float incremento = _fearConfig != null ? _fearConfig.velocidadIncrementoBase : 1f;
                AumentarMiedo(incremento);
            }
        }
    }
}
