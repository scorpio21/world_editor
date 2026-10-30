# Integración inicial `frmMain` → `MainForm`

## Estado

Compatibilidad estimada de esta primera superficie: **20 %**.

Se ha conectado el codec V1 a una ventana WinForms funcional sin afirmar aún
paridad visual con DirectDraw.

## Implementado

- Menú `Archivo` con `Nuevo mapa`, `Abrir mapa`, `Guardar mapa` y `Salir`.
- Panel desplazable para una superficie de 100x100 celdas.
- Selección de celda con coordenadas en la barra de estado.
- Visualización del GRH de capa 1 y resaltado de celdas bloqueadas.
- Carga y guardado de pares `.map/.inf` V1.
- Creación de un mapa nuevo con GRH 1 en la capa 1, siguiendo `NuevoMapa`.
- Exclusión del proyecto de pruebas de la compilación recursiva del proyecto
  principal.

## Diferencias pendientes

- No se cargan BMP ni `Graficos.ind`; por tanto, se muestran índices GRH y no
  sprites.
- No están migradas las capas 2, 3 y 4 en la superficie visual.
- No están migrados NPC, objetos, triggers, translados ni radar.
- No están migrados los menús de edición ni el estado `MapInfo.Changed`.
- No está conectado todavía `WorldEditorSettingsLoader` a `MainForm`.
- No existe todavía renderer DirectDraw/GDI+ equivalente.
- V2 y V3 siguen sin implementarse.

## Verificación

- `dotnet build WorldEditorNet.vbproj -c Release`: 0 advertencias, 0 errores.
- `dotnet build WorldEditorNet.Tests.vbproj -c Release`: 0 advertencias, 0 errores.
- `dotnet run --project WorldEditorNet.Tests.vbproj -c Release --no-build`:
  `MapV1Codec smoke test: OK`.
