# FASE 3: Implementación de ScriptableObjects (Arquitectura Guiada por Datos)

## 1. Algoritmo de Selección Ponderada Aleatoria (Weighted Random) en `SpawnTableSO`

**¿Cómo funciona paso a paso?**
Si tenemos un Vampiro (peso 10) y un Zombi (peso 90):
1. **Filtro**: Primero revisamos si el monstruo tiene permitido aparecer según el nivel de miedo actual. (Ej. miedo = 50, descartamos monstruos de nivel 60+).
2. **Suma de Pesos**: Sumamos los pesos de los que pasaron el filtro (10 + 90 = 100).
3. **Random**: Tiramos los dados y obtenemos un número al azar entre 0 y 100 (Supongamos que salió `75`).
4. **Iteración Acumulativa**:
   - Pasamos por el Vampiro (peso 10). Acumulamos: `10`. ¿Es 75 menor o igual a 10? **No**. Seguimos.
   - Pasamos por el Zombi (peso 90). Acumulamos: `10 + 90 = 100`. ¿Es 75 menor o igual a 100? **Sí**. Aparece el Zombi.

Esto garantiza estadísticamente que los objetos con mayor "peso" abarquen una franja más grande del rango del número aleatorio.

---

## 2. Consumo de ScriptableObjects en los Observers

Aquí mostramos cómo se debe modificar el código de los sistemas para **consumir los ScriptableObjects (DIP)** sin alterar la arquitectura Observer existente (sin usar Actions).

### `GestorMiedo.cs`
**Antes (Hardcodeado):**
```csharp
private float velocidadAumento = 5f;
```
**Después (Inyección de Dependencia usando SO):**
```csharp
[SerializeField] private FearPark.Data.FearConfigSO _fearConfig;

private void Update()
{
    // Aumentar usando el config SO
    float multiplicador = isMoving ? _fearConfig.multiplicadorPorMovimiento : 1f;
    _nivelMiedo += _fearConfig.velocidadIncrementoBase * multiplicador * Time.deltaTime;
}
```

### `SistemaCamara.cs`
**Antes:**
```csharp
_perlin.m_AmplitudeGain = Mathf.Lerp(0f, 5f, nivelMiedo / 100f);
```
**Después (Usando AnimationCurve para comportamiento NO-LINEAL):**
```csharp
[SerializeField] private FearPark.Data.CameraConfigSO _cameraConfig;

public void OnMiedoCambiado(int nuevoMiedo)
{
    float t = nuevoMiedo / 100f; // 0.0 a 1.0
    
    // Evaluate(t) permite que el temblor no crezca hasta cierto punto de la curva
    _perlin.m_AmplitudeGain = Mathf.Lerp(_cameraConfig.amplitudShakeMin, _cameraConfig.amplitudShakeMax, _cameraConfig.curvaShake.Evaluate(t));
    _perlin.m_FrequencyGain = Mathf.Lerp(_cameraConfig.frecuenciaShakeMin, _cameraConfig.frecuenciaShakeMax, _cameraConfig.curvaShake.Evaluate(t));
}
```

### `SistemaSonido.cs`
**Después (Usando arreglos desde el SO):**
```csharp
[SerializeField] private FearPark.Data.AudioConfigSO _audioConfig;

public void OnMiedoCambiado(int nuevoMiedo)
{
    // Mapeamos el miedo 0-100 a índices 0-3
    int indice = Mathf.Clamp(nuevoMiedo / 25, 0, 3);
    
    // Asignar recursos desde el SO basado en el rango
    _audioSource.clip = _audioConfig.clipsPorRango[indice];
    float volumenObjetivo = _audioConfig.volumenesPorRango[indice];
    
    // Iniciar corrutina de Fade usando _audioConfig.tiempoFade ...
}
```

---

## 3. CHECKPOINT (Prueba de Diseño de Datos)
1. Ve a Unity, da click derecho y crea dos archivos `FearConfigSO` usando el menú (`FearPark/Config/Miedo`).
2. Llámales `ConfigTranquilo` y `ConfigHardcore`.
3. Al primero ponle `velocidadIncrementoBase = 1` y al segundo `velocidadIncrementoBase = 10`.
4. Arrastra `GestorMiedo` en el Inspector.
5. Dale al **Play**.
6. Mientras el juego corre, arrastra uno u otro archivo SO en la casilla del `GestorMiedo`. ¡Notarás que el ritmo de miedo del juego cambia instantáneamente sin compilar código! Esto es el poder de la inyección de dependencias basada en datos.
