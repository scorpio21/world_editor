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

## Diferencias pendientes

- No se cargan BMP ni `Graficos.ind`; por tanto, se muestran índices GRH y no
  sprites.
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
