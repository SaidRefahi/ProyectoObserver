# FASE 4: Factory de Enemigos y NavMesh

## SETUP DEL NAVMESH
Antes de hacer pruebas en la escena, necesitas configurar el suelo para que los enemigos sepan por dónde caminar.
1. **Marcar Suelo:** Selecciona el suelo o los planos/cubos de tu escenario en la jerarquía.
2. Ve al panel de la derecha (Inspector) y en la esquina superior derecha activa el checkbox que dice **"Navigation Static"** (o "Static" y luego elige Navigation). Esto le dice a Unity que ese objeto no se moverá y es seguro caminar sobre él.
3. **Hornear (Bake):** Ve al menú principal de arriba: `Window > AI > Navigation`. Se abrirá una pestaña al lado del Inspector.
4. En esa pestaña ve a la opción **Bake** y dale al botón `Bake` de abajo. Verás que tu suelo se cubre de una malla azul. ¡Listo!

## CONFIGURACIÓN DEL NAVMESH AGENT Y RIGIDBODY EN PREFABS
A tus Prefabs de enemigos (que tendrán `MonstruoLento.cs`, etc.):
1. Al agregar el script `Monstruo...`, automáticamente se añadirá el componente `NavMeshAgent`. 
2. Sus propiedades como **Speed** y **Radius** se sobreescribirán desde tu ScriptableObject (MonsterDataSO) al inicializarse. Aun así, puedes configurar `Acceleration` (cuánto tarda en llegar a la vel. máxima) y `Stopping Distance` (distancia a la que frenan antes de llegar al jugador) a tu gusto.
3. **IMPORTANTE: Rigidbody Kinematic.** Añade un `Rigidbody` para las colisiones/físicas si lo necesitas, pero **márcalo como `Is Kinematic = true`**. ¿Por qué? Porque si dejas que el motor de físicas controle al enemigo al mismo tiempo que el `NavMeshAgent` trata de moverlo, entrarán en conflicto y el personaje temblará o se quedará atrapado en el piso.

---

## 🛠️ DEBUG CHECKPOINT

Prueba el flujo completo en Play Mode jugando con la barra de Miedo:

1. **Miedo = 10:** Sólo el `MonstruoLento` aparecerá (según lo que configuraste en tus SO y Tabla de Spawn). Lo verás caminar torpemente por el NavMesh hacia el Player.
2. **Miedo = 50:** Empieza a haber una mezcla entre `MonstruoLento` y `MonstruoRapido` compitiendo (dependiendo del Weighted Random configurado en tus datos).
3. **Miedo = 90:** Empiezan a aparecer los monstruos Élite que predecirán los movimientos y atajarán al jugador antes de que llegue a su destino.
4. **Prueba OCP (Open/Closed Principle):** 
   - Crea un archivo nuevo de `MonsterDataSO` (ej: "ZombiGigante").
   - Créale un modelo nuevo como Prefab.
   - Asígnalo en tu `SpawnTableSO`.
   - Aparecerá en el juego automáticamente sin que hayas tocado una sola línea de C# en el Spawner (`FabricaMonstruos.cs`). ¡Ese es el poder del Patrón Factory orientado a datos!
