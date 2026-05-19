# Setup de la Escena - Fear Park (Fase 2: Observer Puro)

Todos los scripts de la Fase 2 han sido generados. Aquí te explico cómo armar la escena y los conceptos clave detrás del código.

## Conceptos Clave de la Fase 2

### 1. ¿Por qué esto es más eficiente que el `Update()` de la UI?
En la Fase 1, el `UIManager` preguntaba **cada frame (60+ veces por segundo)**: *"¿Gestor, cuál es tu nivel de miedo?"*. A esto se le llama **Polling**. En la Fase 2 (Observer PUSH), el UIManager simplemente espera tranquilamente. El Gestor de Miedo solo le avisa *"¡Actualizate!"* cuando el valor entero cambia. Pasamos de 60 verificaciones inútiles por segundo a unas pocas actualizaciones exactas. ¡Es una optimización enorme para el procesador!

### 2. Diferencia de Cinemachine en 3D Top-Down vs 2D
En 2D, una cámara ortográfica se maneja en el plano XY y escala con `OrthographicSize` o acercando la cámara en el eje Z. En 3D Top-Down, puedes usar una cámara *Perspectiva* (donde el "zoom" se hace cambiando el FOV o alejando la cámara físicamente en el eje Y) o una cámara *Ortográfica* (donde el zoom sigue siendo `OrthographicSize`, dando un look isométrico perfecto sin distorsión). El script `SistemaCamara` usa `OrthographicSize`, asumiendo un Top-Down isométrico estricto, tal como se configura usualmente en juegos de este género.

---

## Instrucciones de Setup en Unity

### 1. Actualización de UIManager
1. Tu objeto `UIManager` de la escena se actualizará automáticamente con el nuevo código. 
2. Ya no usará `Update()`. Revisa en el Inspector que las referencias al **Slider Miedo** y **Texto Nivel** sigan conectadas correctamente.

### 2. Setup del Sistema de Sonido
1. Crea un GameObject vacío y llámalo **`SistemaSonido`**.
2. Arrástrale el script `SistemaSonido.cs`. Automáticamente te pedirá asignar una *Fuente Audio*. Agrégale un componente **AudioSource** al mismo objeto.
3. Configura el `AudioSource` para que haga **Loop** y asegúrate de marcar **Play On Awake**.
4. En el inspector del script `SistemaSonido`:
   * **Fuente Audio:** Asigna el componente AudioSource que acabas de crear.
   * **Clips Por Rango:** Pon el tamaño en **4** y asigna 4 pistas de música distintas (ej. desde una pista de calma hasta una de pánico extremo).
   * **Volumenes Por Rango:** Pon el tamaño en **4** y asigna los volúmenes para cada pista (ej. 0.5, 0.7, 0.9, 1.0).

### 3. Setup del Sistema de Luces
1. Crea un GameObject vacío y llámalo **`SistemaLuces`**.
2. Agrégale el script `SistemaLuces.cs`.
3. Busca tu **Directional Light** principal en la escena y arrástrala al campo `Luz Principal`.
4. Define un **Color Calmo** (ej. blanco o amarillo pálido) y un **Color Terror** (ej. rojo oscuro o verde tóxico).
5. (Opcional URP): Si tienes un `Global Volume` en tu escena con un override de Vignette, arrastra ese objeto de la jerarquía al campo `Global Volume`. El script controlará la intensidad del oscurecimiento en los bordes.

### 4. Setup del Sistema de Cámara
1. Asegúrate de tener una **Virtual Camera** de Cinemachine apuntando a tu jugador. (En el Inspector de la cámara, asegúrate de que en la sección *Lens* esté configurada en modo **Orthographic**).
2. Crea un GameObject vacío llamado **`SistemaCamara`** (o si prefieres, arrastra el script directamente al objeto de la Virtual Camera).
3. Agrégale el script `SistemaCamara.cs`.
4. Asigna la **Virtual Camera** en el inspector.
5. Selecciona la Virtual Camera en la jerarquía, baja a la sección **Extensions**, haz click en "Add Extension" y agrega `CinemachineBasicMultiChannelPerlin` (Noise). 
6. Asigna un *Noise Profile* de la lista desplegable (ej. *6D Wobble*). El script se encargará de encender, apagar y modificar la fuerza de este ruido en tiempo real según el miedo.

### 5. Debug Checkpoint Final
1. Dale **Play**.
2. **Consola:** Deberías ver inmediatamente los mensajes: *"SistemaSonido notificado: nivel=0"*, *"SistemaLuces notificado: nivel=0"*, etc.
3. Muévete con WASD para acelerar el miedo (o espera a que suba solo).
4. **Verificación visual y sonora:**
   * Al llegar a nivel 26, el clip de audio de fondo debería cambiar al segundo clip.
   * La luz direccional debería ir tiñéndose poco a poco hacia el "Color Terror" y bajando su intensidad.
   * La cámara debería empezar a temblar cada vez más fuerte y hacer un ligero zoom out.
5. **Test Crítico:** Mientras juegas, selecciona el objeto `SistemaSonido` en la Hierarchy y **bórralo (Presiona Supr/Delete)**. Sigue moviendo al jugador. Si la consola **NO tira errores rojos** de *NullReferenceException*, ¡Felicidades! Significa que el método `Remover()` en `OnDestroy` funcionó perfectamente y tu patrón Observer es a prueba de balas.
