# WorldEditor .NET

Migración progresiva del editor de mapas VB6 `AomaniaWorldEditor` a VB.NET
WinForms sobre .NET 8.

## Estado actual

- Proyecto SDK-style creado y compilable con .NET 8.
- `modDeclaraciones.bas` trasladado parcialmente a
  `Core/LegacyDeclarations.vb`.
- `clsIniReader.cls` trasladado a `Core/clsIniReader.vb` con ordenación y
  búsqueda binaria.
- `modGameIni.bas` trasladado parcialmente a `Core/GameIni.vb`: estructuras,
  estado global e inicialización de cabecera portados; la serialización de
  `Inicio.con` queda pendiente hasta confirmar su formato binario.
- La parte independiente de `modGeneral.bas` está en
  `Core/GeneralUtilities.vb`: campos, rutas, INI Win32 y utilidades numéricas.
  El arranque, editor, DirectDraw y estado visual siguen separados y
  pendientes de sus dependencias.
- `CargarMapIni` está aislado en `Core/WorldEditorSettings.vb`; carga y valida
  las cuatro secciones de `Datos/WorldEditor.ini` y `MainForm` lo ejecuta al
  arrancar. Las opciones todavía no controlan el renderer ni los menús.
- La serialización textual `.dat` de `modMapIO.bas` está aislada en
  `Core/MapInfoFile.vb`. Los formatos binarios `.map/.inf` siguen pendientes de
  verificación byte a byte.
- El formato V1 `.map/.inf` ya está verificado y aislado en
  `Core/MapV1Codec.vb`; V2/V3 y la integración con el editor siguen pendientes.
- La prueba `WorldEditorNet.Tests` ejecuta un round-trip V1 en un directorio
  temporal y comprueba tamaños, cabecera y datos de una celda.
- `MainForm` ya tiene una superficie WinForms inicial para abrir, guardar,
  seleccionar y visualizar celdas V1; todavía no es el renderer gráfico final.
- El menú `Ver` aplica las opciones de capas, bloqueos y cuadrícula cargadas
  desde `WorldEditor.ini`; si falta un recurso se muestra su índice GRH.
- `GraficosIndReader` ya lee `Graficos.ind` Integer y Long, y la superficie
  puede recortar los BMP asociados y avanzar sus animaciones con el valor
  `Speed`; el refresco WinForms usa un temporizador de 16 ms.
- Estructuras, constantes y estado de datos documentados.
- DirectX 7, DirectSound, DirectMusic, ADO y el renderer todavía no están
  implementados.
- La ventana principal es únicamente un shell de migración.

## Compilar

```powershell
dotnet restore WorldEditorNet.vbproj
dotnet build WorldEditorNet.vbproj --configuration Release
```

## Criterio de migración

Cada archivo VB6 se analiza antes de convertirse. Después de cada conversión
se compara con el original, se documentan las funciones pendientes y se asigna
un porcentaje de compatibilidad. El código ambiguo no se implementa hasta
verificar su comportamiento en el editor original.

La documentación de inventario y comparación se encuentra en la carpeta
`Documentacion` del proyecto VB6 padre.
