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
  primer frame; la reproducción temporal todavía no está migrada.

## Diferencias pendientes

- La reproducción de animaciones no está migrada: se muestra el primer frame.
- Si las rutas del índice o los BMP no existen, se muestran índices GRH como
  respaldo y se informa del problema en la barra de estado.
- No están migrados NPC, objetos, triggers, translados ni radar.
- No están migrados los menús de edición ni el estado `MapInfo.Changed`.
- Las opciones cargadas controlan ahora las capas, los bloqueos y la cuadrícula
  de esta superficie inicial; todavía no controlan un renderer de sprites.
- No existe todavía renderer DirectDraw/GDI+ equivalente.
- V2 y V3 siguen sin implementarse.

## Verificación

- `dotnet build WorldEditorNet.vbproj -c Release`: 0 advertencias, 0 errores.
- `dotnet build WorldEditorNet.Tests.vbproj -c Release`: 0 advertencias, 0 errores.
- `dotnet run --project WorldEditorNet.Tests.vbproj -c Release --no-build`:
  `MapV1Codec smoke test: OK`.
