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
  seleccionada. Todavía faltan las demás herramientas de edición del VB6,
  deshacer/rehacer y los diálogos de confirmación asociados.
- `MapV1Document.Changed` ya se marca al editar y se limpia al cargar o
  guardar; todavía falta usarlo para preguntar automáticamente antes de crear
  otras operaciones de edición no migradas.
- Las opciones cargadas controlan ahora las capas, los bloqueos y la cuadrícula
  de esta superficie inicial; todavía no controlan un renderer de sprites.
- No existe todavía renderer DirectDraw/GDI+ equivalente.
- V2 y V3 siguen sin implementarse.

## Verificación

- `dotnet build WorldEditorNet.vbproj -c Release`: 0 advertencias, 0 errores.
- `dotnet build WorldEditorNet.Tests.vbproj -c Release`: 0 advertencias, 0 errores.
- `dotnet run --project WorldEditorNet.Tests.vbproj -c Release --no-build`:
  `MapV1Codec smoke test: OK`.
