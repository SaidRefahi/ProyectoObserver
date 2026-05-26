# FASE 5: Combate, Loop de Juego y Cinemachine 3D

## CONFIGURACIÓN DEL APUNTADO AUTOMÁTICO (AIM)
El apuntado y la rotación del jugador ahora son completamente automáticos:
1. **Prioridad - Enemigo en Rango:** Si hay un monstruo dentro del área de disparo (rango configurado en `DisparadorAutomatico`), el personaje rotará automáticamente en el eje Y para encarar al enemigo más cercano.
2. **Dirección de Movimiento:** Si no hay ningún enemigo en rango, el personaje apuntará/mirará en la dirección en la que se esté moviendo basándose en el Input de movimiento.
3. **Mantener Dirección:** Si no hay enemigos en rango y el personaje no se mueve, conservará su última rotación.

## CONFIGURACIÓN DEL SISTEMA DE COMBATE Y DAÑO
Aplica estos pasos en tus GameObjects:

### 1. El Jugador (Player)
* En el Inspector del GameObject del `Player`:
  * Asegúrate de tener los componentes `Player.cs`, `DisparadorAutomatico.cs` y `SistemaVidaPlayer.cs` adjuntos.
  * Crea un GameObject hijo vacío llamado **PuntoDisparo** (o similar) y posiciónalo en la parte delantera de tu modelo (donde saldrían los disparos). Asigna este transform al campo **Punto Disparo** del script `DisparadorAutomatico`.
  * Crea una Layer llamada `Enemy` y asígnasela a tus prefabs de enemigos. En el `DisparadorAutomatico`, selecciona `Enemy` en el campo **Enemy Layer**.

### 2. El Proyectil (Prefab)
* Crea una esfera u objeto visual que actúe como bala y conviértelo en un Prefab.
* Añádele un `Rigidbody` y un `Collider` (marca la casilla **Is Trigger = true**).
* Agrégale el script `Proyectil.cs` y arrastra este Prefab al campo **Prefab Proyectil** del `DisparadorAutomatico` en el Player.

### 3. Ajustes de Colisión del Enemigo
* Asegúrate de que el Prefab de tus enemigos tenga un Collider (como `CapsuleCollider` o `BoxCollider`) marcado como **Is Trigger = false** o con un Rigidbody adecuado para que `OnTriggerEnter` se ejecute correctamente cuando el proyectil choque con ellos, o cuando ellos choquen con el jugador.

---

## CONFIGURACIÓN DEL GAMEMANAGER Y UI
1. **GameManager:**
   * Crea un GameObject vacío en la escena llamado `GameManager` y añádele el script `GameManager.cs`.
2. **Panel GameOver:**
   * Crea un panel en tu UI (`Canvas > Panel`) con texto de "Game Over" y un botón para Reiniciar (que llame a `GameManager.Instance.Reiniciar()`).
   * Desactiva este panel por defecto en el Inspector y arrástralo al campo **Panel GameOver** del script `GameManager`.
3. **UIManager (Final):**
   * En tu GameObject `UIManager`, vincula los nuevos campos en el Inspector:
     * **Slider Vida:** El slider de la UI para la salud.
     * **Vida Player:** El componente `SistemaVidaPlayer` de tu personaje.
     * **Texto Puntos:** El componente TextMeshPro que mostrará el puntaje.
     * **Texto Tiempo:** El componente TextMeshPro que mostrará el tiempo transcurrido en segundos.

---

## SETUP FINAL DE CINEMACHINE 3D
Para lograr la cámara top-down fluida al estilo Shooter 3D:

### Configuración Recomendada (Framing Transposer)
1. **Follow y LookAt:** Arrastra el transform de tu `Player` a los campos **Follow** y **Look At** de tu cámara de Cinemachine (`CinemachineVirtualCamera`).
2. **Body Type:** Cambia el tipo de Body a **Framing Transposer**.
   * **Camera Distance:** Ponlo en `12` o `15` para tener una buena perspectiva aérea.
   * **Screen Y:** Establécelo en `0.5` (centrado).
   * **Damping (suavizado):** Pon `X, Y, Z` damping en valores bajos (como `0.2` o `0.5`) para que la cámara siga al jugador con un leve y agradable retraso orgánico.
3. **Cinemachine Confiner 3D:** Si quieres delimitar la cámara dentro del mapa, añade el componente `CinemachineConfiner3D` y asígnale un `BoxCollider` gigante que cubra los bordes de tu nivel.

### Configuración Alternativa Simple (Hard Lock)
Si no quieres que la cámara rote con el personaje y prefieres una perspectiva 100% rígida y vertical:
* Rota la cámara de Cinemachine en `(90, 0, 0)` en sus Transform Rotation.
* Cambia el Body de Cinemachine a **Hard Lock to Target** con el Player asignado en **Follow**. La cámara mantendrá su distancia fija y altura sin rotar jamás.

---

## 🛠️ DEBUG CHECKPOINT
Para verificar que todo el loop del juego funcione correctamente:
1. **Movimiento:** Juega en modo Play, camina con WASD/Stick y comprueba que el personaje rota mirando hacia la dirección a la que se desplaza cuando no hay enemigos cerca.
2. **Apuntado y Disparo Automático:** Deja que aparezca un enemigo y se acerque al jugador. En cuanto entre en el círculo de rango (`DisparadorAutomatico`), el personaje rotará instantáneamente mirándolo y le disparará de forma continua.
3. **Puntuación:** Al matar a un monstruo, tus puntos en la UI deben subir (+10 puntos por baja).
4. **Daño e Inmunidad:** Deja que un enemigo te toque. Tu slider de vida debe bajar, y el personaje no debe perder vida continuamente por frame debido al timer de invencibilidad (`_cooldownInvencibilidad`).
5. **Game Over:** Si tu vida llega a 0, el juego debe pausarse (`Time.timeScale = 0`), aparecerá el panel de Game Over y ya no podrás controlar al jugador.
