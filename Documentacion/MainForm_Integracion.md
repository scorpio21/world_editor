# Integración inicial `frmMain` → `MainForm`

## Estado

Compatibilidad estimada de esta primera superficie: **20 %**.

Se ha conectado el codec V1 a una ventana WinForms funcional sin afirmar aún
paridad visual con DirectDraw.

## Implementado

- Menú `Archivo` con `Nuevo mapa`, `Abrir mapa`, `Guardar mapa` y `Salir`.
- Panel desplazable para una superficie de 100x100 celdas.
- Selección de celda con coordenadas en la barra de estado.
- Visualización de los GRH de las capas activadas y resaltado opcional de celdas bloqueadas.
- Carga y guardado de pares `.map/.inf` V1.
- Creación de un mapa nuevo con GRH 1 en la capa 1, siguiendo `NuevoMapa`.
- Exclusión del proyecto de pruebas de la compilación recursiva del proyecto
  principal.
- `MainForm` carga `Datos\WorldEditor.ini` mediante `WorldEditorSettingsLoader`
  al arrancar. Si no existe, usa los valores predeterminados; si contiene
  rutas históricas que no existen, informa del problema en la barra de estado
  sin cerrar el editor.
- El menú `Ver` ya refleja las opciones de capas 1-4, bloqueos y cuadrícula.
  La superficie aplica esos estados y muestra los GRH disponibles de las
  capas activadas como texto mientras no exista el cargador de BMP/GRH.
- Añadido `Core/GraficosIndReader.vb`, compatible con el formato Integer de
  `Graficos.ind` del WorldEditor y con índices Long como opción explícita.
- La superficie intenta cargar el BMP, recorta `X`, `Y`, `Width` y `Height` y
  reutiliza las imágenes en caché. Para una animación usa inicialmente su
  primer frame.
- La superficie tiene un temporizador de 16 ms y contadores independientes por
  celda/capa para avanzar según `Speed` y volver al primer frame.
- El menú `Ver` incluye NPCs, objetos, translados y triggers. Se dibujan como
  marcadores de diagnóstico (`N`, `O`, `E`, `T`) usando los valores del mapa.
- `Opciones > Guardar última configuración` persiste las opciones de vista,
  rutas, última posición y último mapa en `Datos\WorldEditor.ini` al cerrar.
- El menú `Edición > Alternar bloqueo de la celda` modifica la celda
  seleccionada, actualiza su valor `Blocked`, refresca la superficie y marca
  el documento como modificado mediante `MapV1Document.Changed`.
- Si el documento tiene cambios pendientes, `Nuevo mapa`, `Abrir mapa` y
  cerrar la ventana muestran una confirmación `Sí/No/Cancelar`. `Sí` guarda
  usando el flujo normal, `No` continúa sin guardar y `Cancelar` detiene la
  operación.
- Añadido `Edición > Deshacer` para revertir la última alternancia de bloqueo.
  Esta es una adaptación inicial de un solo nivel; la pila completa de
  `Deshacer_Add`/`Deshacer_Recover` del VB6 sigue pendiente.
- Añadidos comandos `Establecer GRH en capa 1...` hasta `capa 4...`. Cada
  comando reproduce la modificación del `Graphic(layer).GrhIndex` de
  `ClickEdit`, valida el rango `0..32767`, refresca la superficie y participa
  en el deshacer de un nivel.
- Añadidos comandos `Borrar capa 1 completa` hasta `capa 4 completa`, con
  confirmación. Siguen la regla de `Quitar_Capa`: capa 1 se restablece a GRH
  `1` y capas 2–4 a `0` en las 10.000 celdas. Al ser una operación global,
  invalida el deshacer de un nivel hasta portar la pila completa.
- Añadidos `Bloquear todo el mapa` y `Quitar todos los bloqueos`, equivalentes
  a `Bloqueo_Todo(1)` y `Bloqueo_Todo(0)`, con confirmación y actualización de
  `Changed` sobre las 10.000 celdas.
- Añadidos `Rellenar capa 1...` hasta `capa 4...`, equivalentes a la rama no
  mosaico de `Superficie_Todo`: aplican un GRH a las 10.000 celdas después de
  validar y confirmar. El modo mosaico/autocompletado sigue pendiente de
  portar `frmConfigSup`.
- Añadido `SurfaceConfigForm`, equivalente a `frmConfigSup`, con controles de
  diseñador para activar mosaico, configurar ancho/largo y aplicar
  desplazamientos X/Y. `Opciones > Configuración avanzada de superficie...`
  abre la ventana y `Superficie_Todo` usa sus valores.
- Añadido `Edición > Pintar celda seleccionada...`: solicita capa y GRH base,
  aplica el patrón de mosaico usando las coordenadas reales de la celda y
  actualiza `Changed`/deshacer.
- Añadido `Edición > Activar pincel...`: configura capa y GRH base y permite
  pintar arrastrando el botón izquierdo sobre la superficie. El pincel se
  desactiva al abrir o crear otro mapa.
- `clsIniReader` registra las páginas de códigos de .NET 8 para conservar la
  lectura ANSI/CP1252 de los INI históricos.

## Diferencias pendientes

- El intervalo de refresco de 16 ms es la adaptación WinForms del bucle de
  render VB6 y todavía necesita validación visual contra el editor original.
- Si las rutas del índice o los BMP no existen, se muestran índices GRH como
  respaldo y se informa del problema en la barra de estado.
- Los NPC, objetos, translados y triggers todavía no usan sus sprites originales;
  solo tienen representación textual de diagnóstico. El radar sigue pendiente.
- Solo está migrada la edición básica explícita del bloqueo de la celda
  seleccionada y la asignación manual de GRH a las cuatro capas. Todavía
  faltan mosaicos, autocompletado, borrado de capas, NPC, objetos,
  translados, triggers y la pila completa de deshacer/rehacer.
- `MapV1Document.Changed` ya se marca al editar y se limpia al cargar o
  guardar; todavía falta usarlo para preguntar automáticamente antes de crear
  otras operaciones de edición no migradas.
- Las opciones cargadas controlan ahora las capas, los bloqueos y la cuadrícula
  de esta superficie inicial; todavía no controlan un renderer de sprites.
- No existe todavía renderer DirectDraw/GDI+ equivalente.
- V2 y V3 siguen sin implementarse.
- La vista previa avanzada y las operaciones de autocompletado por selección
  todavía no están migradas; el modo mosaico se aplica al rellenado global,
  a la pintura explícita y al pincel continuo.

## Verificación

- `dotnet build WorldEditorNet.vbproj -c Release`: 0 advertencias, 0 errores.
- `dotnet build WorldEditorNet.Tests.vbproj -c Release`: 0 advertencias, 0 errores.
- `dotnet run --project WorldEditorNet.Tests.vbproj -c Release --no-build`:
  `MapV1Codec smoke test: OK`.
