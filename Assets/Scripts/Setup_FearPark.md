# Setup de la Escena - Fear Park (Fase 1)

A continuación te explico paso a paso cómo configurar la escena en Unity una vez que tienes los scripts generados en tu carpeta `Assets/Scripts`.

## 1. Configuración del Jugador (Player)

1. En la ventana **Hierarchy**, crea un Cilindro o Cápsula 3D (`Click derecho -> 3D Object -> Capsule`). Nómbralo **`Player`**.
2. **Componentes necesarios:**
   * Al agregar el script `Player.cs` al GameObject, automáticamente se añadirá un **Rigidbody** (gracias al atributo `[RequireComponent]`).
   * Mantén el **Capsule Collider** predeterminado.
3. **Configuración del Rigidbody:**
   * Ve al Inspector del `Player` y despliega la sección **Constraints** dentro del Rigidbody.
   * Selecciona las casillas **Freeze Rotation: X, Y, y Z**. Esto es crucial para que el modelo no se caiga ni empiece a rodar físicamente cuando apliques movimiento o choques contra paredes.
4. **Script Player:**
   * Define la `Velocidad` y `Multiplicador Miedo` a gusto.

*(Nota sobre el PlayerInput component: Como estamos instanciando la clase generada `FearParkInputActions` directamente en el código vía `new` en Awake por diseño Zero GC, NO necesitas agregar el componente visual `Player Input` en el inspector. El script ya maneja la conexión con el asset de forma automática).*

## 2. Configuración del Gestor de Miedo

1. Crea un GameObject vacío en la Hierarchy (`Click derecho -> Create Empty`) y nómbralo **`GestorMiedo`** o **`GameManager`**.
2. Arrástrale el script `GestorMiedo.cs`. 
3. Como es un Singleton, el script se auto-asignará en el método `Awake()`. No requiere mayor configuración.

## 3. Configuración de la UI (UIManager)

1. En la Hierarchy, crea un **Slider** (`Click derecho -> UI -> Slider`). Esto creará automáticamente un **Canvas** y un **EventSystem** si no existen.
2. Crea un **Text - TextMeshPro** (`Click derecho -> UI -> Text - TextMeshPro`) dentro del mismo Canvas. Ubícalo cerca del Slider.
3. Crea un GameObject vacío llamado **`UIManager`** (o añade el script directamente al Canvas).
4. Arrástrale el script `UIManager.cs`.
5. En el Inspector del script, asigna:
   * **Slider Miedo**: arrastra el GameObject del Slider.
   * **Texto Nivel**: arrastra el GameObject de TextMeshPro.

## 4. Setup del Suelo (Ground) para la Fase 5

1. Crea un Plano o Cubo grande para el suelo (`Click derecho -> 3D Object -> Plane`).
2. Ve arriba a la derecha en el Inspector, en la pestaña **Layer**, y haz click en `Add Layer...`.
3. Crea una capa llamada **`Ground`**.
4. Vuelve a seleccionar tu Plano del suelo y asígnale la capa `Ground` en el menú desplegable. 
*(Esto será fundamental para el Raycast de la puntería en la Fase 5).*

## 5. Debug Checkpoint

Dale Play a la escena y verifica lo siguiente:
* [ ] El slider empieza a subir un poco cada segundo (gracias a la corrutina en `GestorMiedo`).
* [ ] Al presionar WASD, el Player se mueve sobre el plano (XZ) y no atraviesa el suelo ni sale volando.
* [ ] Al mover el Player, la barra de miedo se llena más rápido, reflejándose en el número de la UI en tiempo real.
* [ ] En el Inspector, revisa que el componente `Rigidbody` del Player mantenga su rotación bloqueada y no caiga infinitamente.
