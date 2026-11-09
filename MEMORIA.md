# Memoria WorldEditor .NET

## Objetivo

Migrar progresivamente `AomaniaWorldEditor` VB6 a VB.NET WinForms .NET 8 en
`K:\AO_Aospain\utilidades\WorldEditor\WorldEditorNet`, manteniendo el
comportamiento comprobado y sin inventar equivalencias.

## Estado actual

- ✅ Codec V1 `.map/.inf`, mapa inicial, carga/guardado y edición básica.
- ✅ Render WinForms inicial con capas, bloqueos, cuadrícula y marcadores.
- ✅ Barra de menús con la jerarquía declarada en `Codigo/frmMain.frm`.
- ✅ Auditoría inicial de `modPaneles.bas`.
- ✅ Control WinForms `LegacyPanelButton` y barra superior con siete paneles
  diagonales, gradiente, hover, selección y clic.
- ✅ Estructura visual del formulario alineada con VB6: lateral izquierdo con
  información del mapa, botón de información, botón de quitar funciones y
  área de ayuda; la barra F5–F12 queda a la derecha.
- ✅ Barra F5–F12 ajustada con solapamiento diagonal y segunda fila de 14
  estados `Nada`, según la distribución de `frmMain.frm`.
- ✅ Primera ampliación del diseño: radar izquierdo y panel inferior de
  composición/información del tile añadidos al `MainForm.Designer.vb`.
- ✅ Atajos `F4`, `F5`–`F9`, `F11` y `F12` conectados a la selección de paneles.
- ⚠️ El clic de F5–F12 ya cambia la selección y el estado. Los grupos visuales
  de `VerFuncion` ya están colocados en el diseñador: superficies,
  translados, bloqueos, NPCs, NPCs hostiles, objetos y triggers. Falta conectar
  la matriz exacta de visibilidad/carga de datos de `modPaneles.bas`.
- ✅ Los controles equivalentes de listas, filtros, capas, GRH, translados,
  acciones de superficie/bloqueo/NPC/objeto/trigger y temporizador de
  auto-guardado están serializados en
  `MainForm.Designer.vb`; sus nombres originales están documentados en
  `Documentacion/Inventario_frmMain_Controles.md`.
- ✅ El área de información del tile ahora está serializada con el nombre
  original `StatTxt`.
- ✅ `Picture2` se colocó superpuesto con `StatTxt` en `leftSidebar`, en la
  posición equivalente al VB6 `(4,416)`, con `297×364 px`. `PreviewGrh` quedó
  dentro de ese contenedor en `(0,8)` con `291×252 px`, según `frmMain.frm`.
  El visor usa únicamente el `Picture2` del diseñador y su hijo `PreviewGrh`;
  no hay un panel auxiliar duplicado.
- ✅ Se corrigió el tamaño del área visible del mapa: `mapSurface` ahora usa
  `892×567 px`, igual que `MainViewShp` convertido desde el VB6, y
  `mapScrollPanel` usa `908×583 px` incluyendo el padding.
- ✅ Se corrigió la posición vertical de `tileCompositionPanel`: ahora usa
  `Top=688 px`, equivalente a `FrameComposicionTile.Top=10320 twips`, y queda
  debajo de `mapScrollPanel` sin solaparlo.
- ⏳ Tarea a largo plazo: integrar el renderizado directamente en `MainViewShp`
  y eliminar la duplicidad conceptual con `mapSurface`, después de completar
  y validar todo el diseño y el comportamiento actual.
- ✅ Se añadieron al diseñador los controles originales `COPIAR_GRH(0..3)` y
  `MemoriaAuxiliar`, con sus dimensiones convertidas desde `frmMain.frm`.
  La lógica de copia queda pendiente.
- ✅ Se corrigió `functionBarPanel`: dejó de ocupar todo el ancho y sus siete
  paneles ahora usan las posiciones y anchos convertidos de `SelectPanel(0..6)`;
  la fila `MapPest` queda debajo sin solaparse.
- ✅ Se ajustó `panelStateRow` para que mida `681×17 px`, igual que la fila
  de `MapPest(0..12)`, en lugar de estirarse a todo el ancho del formulario.
- ✅ Se declaró `MainForm.vb` como formulario y `MainForm.Designer.vb` como
  archivo dependiente en `WorldEditorNet.vbproj`, para que Visual Studio vuelva
  a ofrecer y cargar correctamente el diseñador.
- ✅ Se añadieron al diseñador `POSX`, `FPS`, `Shape1`, `ApuntadorRadar`,
  `lXhor`, `lYver`, `Separacion1`, `Separacion2`, `Image` y `MainViewShp`.
  Las formas VB6 están representadas mediante `Panel`, ya que WinForms no
  tiene un control `Shape` nativo; su dibujo dinámico sigue pendiente.
- ✅ Se eliminaron del diseñador las etiquetas provisionales duplicadas del
  radar; permanecen solo `radarPictureBox`, `Shape1`, `ApuntadorRadar`,
  `POSX` y `FPS` dentro de su contenedor equivalente.
- ✅ Se eliminaron del diseñador todos los auxiliares provisionales detectados:
  duplicados del radar, visor GRH auxiliar, etiquetas antiguas del tile,
  etiquetas de ayuda y etiquetas de copia GRH/memoria sin correspondencia
  funcional.
- ✅ Se sustituyeron las etiquetas agrupadas provisionales del bloque inferior
  por los controles originales `LabelPosX`, `LabelPosY`, `LabelBloqueado`,
  `LabelCapa1..4`, `LabelTrasMapa`, `LabelTrasX`, `LabelTrasY`,
  `LabelTipoNPC`, `LabelNPCIndex`, `LabelNPCName`, `LabelOBJIndex`,
  `LabelOBJName` y `LabelOBJCantidad`. Los grupos se ajustaron a las
  posiciones convertidas de `FrameComposicionTile`.
- ✅ `MapSurfaceControl` ya no inicia su temporizador al ser instanciado por
  el diseñador de Visual Studio; en ejecución normal mantiene la animación.
- ✅ Se ajustaron en el diseñador los nombres VB6 de radar, composición del
  tile, capas, coordenadas y estado (`picRadar`, `POSX`, `LabelCapa0..3`,
  `LabelBloqueado`, `LabelTrasMapa`, `LabelNPCName`, `LabelOBJName`, `FPS`).
- ✅ Se añadieron al diseñador los cuatro combos que faltaban para las
  funciones de NPCs hostiles y objetos: `cNumFunc1`, `cCantFunc1`,
  `cNumFunc2` y `cCantFunc2`. La documentación distingue ahora los controles
  físicos del comportamiento todavía no conectado.
- ✅ Se añadieron al diseñador las etiquetas físicas de filtros y funciones
  (`lbFiltrar0..3`, `lNumFunc0..2`, `lCantFunc0..2`, `lbCapas`, `lbGrh`) y
  las etiquetas de traslado `lMapN`, `LabelTrasX` y `LabelTrasY`.
- ✅ Se añadieron al área de composición del tile las etiquetas exactas
  `LabelNPCIndex`, `LabelTipoNPC`, `LabelOBJIndex` y `LabelOBJCantidad`.
- ✅ Se ajustó el lateral izquierdo a las posiciones base del VB6: `picRadar`
  en `(8,8)` con tamaño `107×106`, `Frame1` de información en `(120,8)`
  con tamaño `189×82` y el botón de quitar funciones en `(120,94)`.
- ✅ Se identificó correctamente `pPaneles`: su equivalente funcional es
  `functionContentPanel`, no el `PictureBox pPaneles` oculto. El contenedor
  funcional se ajustó a `(8,120)` y `293×293 px`, equivalentes a los 4395
  twips del VB6.
- ✅ Se ajustó la barra superior a las posiciones/tamaños convertidos de
  `SelectPanel(0..6)` y la fila `MapPest(0..12)` a 50×17 px, con la separación
  original del `.frm`.
- ✅ Se corrigió visualmente la distribución equivalente a `pPaneles`: los
  controles de translados usan las posiciones del VB6 y los botones inferiores
  de quitar/insertar superficie son hijos directos de `functionContentPanel`,
  no de `surfaceFunctionPanel`.
- ⚠️ En VB6 `pPaneles` es el `PictureBox` contenedor real de esos controles;
  en .NET `pPaneles` conserva la imagen de fondo `imagenfondo` y
  `functionContentPanel` actúa como capa funcional superpuesta. La jerarquía
  no es idéntica por la limitación del diseñador WinForms con `PictureBox`.
- ✅ Se ajustaron los paneles de Bloqueos, NPC, NPC hostil, Objetos y Triggers
  al área base de `pPaneles` y se trasladaron sus listas, filtros y botones a
  las coordenadas convertidas del `.frm`.
- ✅ Auditoría del radar: el VB6 usa `picRadar` con `Shape1`,
  `ApuntadorRadar`, `POSX` y `FPS` como hijos. La estructura .NET conserva
  esos controles y no mantiene etiquetas auxiliares duplicadas.
- ✅ `Frame1` se reconstruyó en el diseñador con las etiquetas de fondo
  `lblFNombreMapa`, `lblFMusica`, `lblFVersion`, `Label1`, los valores
  `lblMapNombre`, `lblMapMusica`, `lblMapVersion`, `lblMapNivelMin` y el botón
  `cmdInformacionDelMapa` equivalente (`mapInfoButton`). Se eliminaron las
  etiquetas duplicadas provisionales del bloque.
- ⚠️ Las matrices de controles indexados de VB6 todavía son controles .NET
  separados por grupo; falta unificar su comportamiento indexado en la lógica.
  `lMapN`, `lXhor` y `lYver` siguen pendientes como controles físicos exactos.
- ⚠️ `lXhor` y `lYver` siguen pendientes como controles exactos del visor
  original; no se han sustituido por equivalentes inventados.
- ⚠️ DirectDraw, V2/V3, radar y sprites completos siguen pendientes.

## Verificación

```powershell
dotnet build .\WorldEditorNet.vbproj --configuration Release
dotnet build .\WorldEditorNet.Tests\WorldEditorNet.Tests.vbproj --configuration Release
dotnet run --project .\WorldEditorNet.Tests\WorldEditorNet.Tests.vbproj --configuration Release --no-build
```

Resultado verificado: ambos builds sin errores ni advertencias y
`MapV1Codec smoke test: OK`.

## Historial reciente

- `73ceaec` — corrección de la jerarquía de menús VB6.
- `75bd229` — auditoría de `modPaneles.bas` y documentación de pendientes.
- Sin commit todavía — ampliación del inventario visual y verificación del
  diseño/control serializado; build Release y smoke test correctos.

## Última actualización

- ✅ Se añadieron al diseñador los controles laterales originales
  `CBVerCuadricula`, `CBVerBloqueosT`, `CBVerBloqueosA`, `CBVerTriggers` y
  `CBVerTraslados`, con tamaño `33×33 px` y posiciones convertidas desde
  `frmMain.frm`.
- ✅ `Image` quedó visible en el diseñador con su tamaño y posición
  convertidos desde VB6, y su GIF está integrado en `Properties/EditorResources.resx`.
- ✅ Se conectaron los eventos confirmados de `CBVerBloqueosT`,
  `CBVerTraslados` y `CBVerTriggers` con sus menús equivalentes.
- ✅ Se corrigió el tipo visual de los cinco controles laterales: ahora son
  `Button` estándar cuadrados de `33×33 px`, no `LegacyPanelButton`, porque
  el `lvButtons_H` lateral del VB6 no utiliza la geometría diagonal de la
  barra superior.
- ⚠️ `CBVerCuadricula` y `CBVerBloqueosA` no tienen procedimiento `Click` en
  el `frmMain.frm` original; se mantienen sin comportamiento inventado.
- ✅ Se extrajeron desde `frmMain.frx` los seis recursos GIF de los controles
  laterales, se guardaron en `Assets\LegacyButtons` y se integraron en
  `Properties/EditorResources.resx`; el diseñador los asigna a las propiedades
  `Image` de los controles.
- ✅ Se eliminó el recurso duplicado `My Project\Resources.resx` y su archivo
  generado `Resources.Designer.vb`. Se conservó `My Project\Application.myapp`.
- ✅ `WorldEditorNet.vbproj` usa ahora únicamente `Properties\EditorResources.resx`
  para estos seis recursos.
- ✅ Compilación Release verificada sin errores ni advertencias.
- ✅ Se añadió `imagenfondo` a `EditorResources.resx` y se expuso mediante el
  módulo de compatibilidad de recursos para que el diseñador VB compile.
- ✅ El estado inicial ya no selecciona `Superficie`: todos los paneles de
  funciones y `functionContentPanel` empiezan ocultos, dejando visible el
  fondo de `pPaneles`.

## Siguiente paso

Completar el resto del diseño visual de `frmMain.frm` desde el diseñador:
visor de GRH, pestañas de mapas, nombres indexados exactos y formas/separadores.
Después decidir si se incorporan los siete iconos originales desde el diseñador;
por ahora los botones permanecen deliberadamente solo con texto. Finalmente
migrar por grupos los controles de `VerFuncion`
y conectar su comportamiento con `EstSelectPanel`, empezando por Superficie y
Translados.

### Fuentes disponibles

Se han localizado en `Assets/fuentes` las fuentes `Caribbean.ttf` y `Ringbearer
Medium.TTF` (además de sus variantes `.eot`). No se han aplicado todavía al
formulario porque `frmMain.frm` utiliza `Arial`, `MS Serif`, `MS Sans Serif` y
`Tahoma`; sustituirlas ahora modificaría la fidelidad visual del original.
Podrán incorporarse posteriormente mediante el diseñador si se decide usar
una apariencia personalizada.

### Última verificación

- ✅ `LegacyPanelButton` conserva la propiedad de compatibilidad `ButtonImage`,
  pero los siete botones de la barra superior quedan actualmente sin imagen
  asignada: se muestran únicamente con su texto, como se solicitó.
- ✅ Se eliminaron las asignaciones de imágenes de los siete botones superiores;
  el diseñador y la aplicación los muestran solo con texto.
- ✅ Build Release correcto: 0 advertencias, 0 errores.
- ✅ Build Debug verificado el 03/11/2026: 0 advertencias, 0 errores.
- ✅ Issue #1: título, `WindowState`, `MaximizeBox`, `StartPosition` y
  `KeyPreview` verificados en `MainForm.Designer.vb` después de guardarlos
  desde el diseñador.
- ✅ `BackColor` del formulario verificado desde el diseñador:
  `Color.FromArgb(224,224,224)`.
- ✅ Issue #1 cerrado en el apartado visual: las posiciones finales se
  verificaron y guardaron desde el diseñador. `rightContentPanel=(308,24)`,
  `mapScrollPanel=(0,82)` con `Padding=0`, y tanto `mapSurface` como
  `MainViewShp` quedan en `(0,0)` dentro del área de mapa. Ambos visores
  conservan el tamaño `892x567`.
- ✅ `functionContentPanel.Visible` quedó establecido como `False` en el
  diseñador para que el estado inicial muestre `pPaneles`, igual que VB6.
- ✅ `Documentacion/Inventario_frmMain_Controles.md` refleja esta verificación
  y deja como pendiente solo revisar el orden Z al activar cada panel.
- ⚠️ Visual Studio puede seguir dibujando el panel oculto cuando está
  seleccionado en el Document Outline; esa representación es del diseñador,
  no implica que esté visible al ejecutar.

### 03/11/2026 — Diseñador y orden Z

- ✅ Se resolvió la referencia heredada del diseñador a
  `menuStrip1_ItemClicked`; `MainForm` vuelve a abrirse correctamente en modo
  Diseño.
- ✅ Se ajustó exclusivamente mediante el diseñador el orden Z de la interfaz:
  `pPaneles` queda como fondo y `functionContentPanel` por encima al activarse.
- ✅ No se modificaron tamaños ni posiciones de los controles ajustados por el
  usuario.
- ✅ Build Debug verificado: 0 advertencias y 0 errores.

### Pendiente estructural — superficie del mapa

- ⚠️ En el VB6 original el mapa se presenta mediante `MainViewShp`.
- ⚠️ La migración actual usa `mapSurface` como renderer y mantiene
  `MainViewShp` oculto dentro de `mapScrollPanel`; esto no debe considerarse
  equivalente al original.
- ⏳ Antes de conectar más funciones del editor hay que decidir y adaptar el
  render para que `MainViewShp` sea la superficie visual principal, respetando
  su tamaño, posición y orden Z. `mapSurface` y `mapScrollPanel` solo deben
  permanecer como soporte interno si no alteran ese comportamiento.

### Issues de la fase de diseño — 03/11/2026

La fase visual queda dividida en estas tareas del repositorio
`scorpio21/world_editor`:

- #1 Auditoría final de tamaños, posiciones y anclajes.
- #2 Alinear `MainViewShp`, `mapSurface` y `mapScrollPanel`.
- #3 Corregir el lateral izquierdo y el radar.
- #4 Completar orden Z y visibilidad inicial.
- #5 Ajustar la barra superior y la fila `MapPest`.
- #6 Completar los paneles funcionales del diseñador.
- #7 Revisar recursos, fuentes e imágenes.
- #8 Validar y congelar el diseño antes de conectar funcionalidad.

No se debe comenzar la conexión funcional completa hasta cerrar la issue #8.

### 04/11/2026 — Issue #2: entradas de render y datos de ejecución

- ✅ Se verificó que `MapSurfaceControl` es el control que actualmente dibuja
  las celdas y busca los BMP mediante `GraphicsIndex` y `GraphicsDirectory`.
- ✅ `WorldEditorNet.vbproj` copia al resultado `Datos/WorldEditor.ini`,
  `INIT/Graficos.ind`, `MIDI/*` y `Maps/*`, conservando sus carpetas.
- ✅ Se comprobó en `bin/Debug/net8.0-windows` la presencia de `MIDI/1.MID` y
  `INIT/Graficos.ind` (180693 bytes).
- ✅ La configuración original carga ahora las rutas de gráficos e índice sin
  producir el mensaje de directorio `Graficos` inexistente.
- ✅ Build Debug y prueba smoke de `WorldEditorNet.Tests`: correctos, 0
  advertencias y 0 errores.
- ⚠️ El arranque en mapa nuevo sigue mostrando la cuadrícula; es esperado
  porque todavía no se ha abierto un `.map` con superficies dibujables.
- ⏳ Falta validar con `Mapa1.map` que cada GRH se dibuje desde el índice y
  completar la equivalencia funcional entre `MainViewShp` del VB6 y
  `mapSurface` del .NET. El issue #2 no se cierra todavía.

- ✅ Se añadió también `Maps/*` al contenido copiado por el proyecto para que
  los mapas de prueba acompañen a `INIT` y `MIDI` en `bin/Debug` y en futuras
  publicaciones.
- ✅ Corrección: las tres carpetas se toman de `WorldEditorNet/INIT`,
  `WorldEditorNet/MIDI` y `WorldEditorNet/Maps`, que son las carpetas que el
  usuario incorporó al proyecto; no de rutas `..` externas.
- ✅ Tras recompilar y arrancar una instancia limpia, la composición de
  `leftSidebar`, `rightContentPanel`, `functionBarPanel` y `mapScrollPanel`
  mantiene la separación prevista; el solapamiento mostrado corresponde a una
  instancia/posición anterior y no se reproduce en la ejecución recién
  compilada.

### 04/11/2026 — Recursos locales del editor

- ✅ La configuración se toma del `Datos/WorldEditor.ini` original ubicado en
  `WorldEditor/Datos`, no de `utilidades/Datos/Conf.ini`.
- ✅ Se añadieron los gráficos locales de `WorldEditorNet/Graficos` al
  resultado de compilación.
- ✅ El cargador prioriza las carpetas locales junto al ejecutable (`Graficos`,
  `INIT`, `MIDI` y `DATS`) sobre las rutas históricas absolutas del INI. Esto
  conserva el INI de VB6 y mantiene el ejecutable portable.
- ✅ Debug compila sin advertencias ni errores y el resultado contiene el INI,
  `Graficos.ind`, BMP, MIDI, DATS y mapas de prueba.
- ⏳ Sigue pendiente abrir `Mapa1.map` desde la aplicación y verificar
  visualmente que las superficies se dibujan con esos recursos.

### 04/11/2026 — Corrección de la carpeta DATS

- ✅ Se confirmó que los datos de VB6 están en `WorldEditorNet/DATS`.
- ✅ `WorldEditor.ini` usa ahora `DirDats=DATS`.
- ✅ El cargador prioriza `DATS` junto al ejecutable y conserva `Dat` solo como
  fallback de compatibilidad.
- ✅ El proyecto copia `DATS/*.dat` al directorio de salida.
- ✅ Build Debug verificado: 0 advertencias y 0 errores.

### 04/11/2026 — Primera conexión funcional del panel Superficie

- ✅ Al seleccionar una superficie, el editor actualiza el GRH y la capa como
  `lListado(0)_Click` del VB6.
- ✅ El estado informa también si la superficie está marcada como bloqueada.
- ✅ Build Debug verificado: 0 advertencias y 0 errores.
- ⏳ Pendiente: conectar las acciones de insertar/quitar superficie y el
  preview gráfico con el comportamiento completo de `modPaneles.bas`.

### 04/11/2026 — Edición de superficies sobre el mapa

- ✅ `Insertar Superficie` activa el modo de colocación y aplica el GRH/capa
  seleccionados al hacer clic en una celda.
- ✅ `Quitar en esta Capa` elimina la superficie de la celda seleccionada.
- ✅ `Quitar en Capas 2 y 3` reproduce la operación documentada de VB6 sobre
  las capas 2 y 3.
- ✅ Los modos se excluyen entre sí y deshabilitan los controles que el VB6
  deshabilitaba durante cada operación.
- ✅ Build Debug verificado: 0 advertencias y 0 errores.
- ⏳ Pendiente: preview visual del GRH y edición de translados, bloqueos,
  NPCs, objetos y triggers.

### 04/11/2026 — Preview de GRH y translados

- ✅ `PreviewGrh` ya pinta el GRH seleccionado usando `Graficos.ind`, el BMP
  asociado y el rectángulo `X/Y/Width/Height` del índice.
- ✅ El preview conserva el control creado en el diseñador y muestra un aviso
  dentro del propio visor cuando el GRH o el BMP no están disponibles.
- ✅ `insertTransferButton` escribe `ExitMap`, `ExitX` y `ExitY` en la celda
  seleccionada, equivalentes a `TileExit.Map/X/Y` del VB6.
- ✅ `removeTransferButton` elimina el translado de la celda seleccionada.
- ✅ `unionManualButton` reproduce las coordenadas de borde del VB6 para los
  bordes X<=11, X>=90, Y<=10 e Y>=91.
- ✅ `LegacyIndicesLoader.LoadObjects` carga `DATS/OBJ.dat` con la misma
  estructura que `CargarIndicesOBJ` y rellena `objectListBox`.
- ✅ `insertTransferObjectButton` usa `ObjTranslado` de `WorldEditor.ini`,
  valida `ObjType=19` y coloca el objeto con cantidad 1 junto al translado,
  como `modEdicion.bas`.
- ⚠️ `unionAutoButton` queda pendiente porque requiere migrar `frmUnionAdyacente`.

### 04/11/2026 — Lógica base de unión automática

- ✅ Se añadió `Core/AdjacentTransferService.vb` con la lógica verificable de
  `frmUnionAdyacente.cmdAplicar_Click`.
- ✅ Respeta los cuatro bordes, omite celdas bloqueadas y pone las coordenadas
  destino a cero cuando el mapa destino es 0.
- ✅ Se añadieron los perfiles de límites `Básica` y `Ulla` documentados en el
  VB6, además de lectura de translados existentes por borde.
- ✅ La lógica está cubierta por la prueba de humo del proyecto.
- ⏳ Falta crear en el diseñador la ventana equivalente a `frmUnionAdyacente`
  y enlazar sus controles al servicio; no se han inventado controles ni
  valores en `MainForm`.
- ✅ Build Debug y prueba de humo verificados correctamente.

### 04/11/2026 — Edición de objetos

- ✅ `objectListBox` selecciona el índice real de `OBJ.dat`.
- ✅ `insertObjectFunctionButton` escribe `ObjIndex` y `ObjAmount` en la
  celda seleccionada.
- ✅ `removeObjectFunctionButton` limpia ambos campos, reproduciendo
  `cQuitarFunc(2)` del VB6.
- ✅ Los modos de insertar y quitar son excluyentes y se desactivan al cambiar
  de panel o pulsar `Quitar Funciones`.
- ✅ Build Debug y prueba de humo verificados correctamente.
- ✅ `PreviewGrh` reproduce los GRH animados respetando sus frames y velocidad
  del índice.
- ✅ Build Debug verificado: 0 advertencias y 0 errores.
## 04/11/2026 — Edición básica de NPCs y triggers

- `LegacyIndicesLoader.LoadNpcs` carga `NPCs.dat` y `NPCs-HOSTILES.dat` con el
  mismo desplazamiento `+500` usado por `CargarIndicesNPC` en VB6.
- Las listas del panel NPC y NPC hostil se rellenan desde esos archivos.
- `MapSurfaceControl` permite insertar y quitar `NpcIndex` y `Trigger` en la
  celda seleccionada, con deshacer y marca de cambios.
- Los botones `Insertar función`, `Quitar función`, `Insertar Trigger` y
  `Quitar Trigger` ya tienen flujo funcional.
- Los botones `Agregar función al azar` también ejecutan la selección aleatoria
  sobre celdas no bloqueadas, respetando el límite de 200 del VB6.
- La migración no crea personajes visuales del motor VB6; escribe el
  `NpcIndex` del mapa, que es el dato persistido por el formato `.map`.

## 04/11/2026 — Unión automática verificada

- `AdjacentTransferService` queda verificado para los cuatro bordes, mapas de
  destino, límites básicos/Ulla y preservación de celdas bloqueadas.
- La prueba de humo cubre aplicación completa y limpieza con mapa `0`.
- Pendiente de UI: crear en el diseñador el formulario equivalente a
  `frmUnionAdyacente` (`Mapa(0..3)`, `Aplicar(0..3)`, `PosLim(0..7)`, perfiles
  de límites y botones aplicar/cancelar/default) y conectarlo al botón
  `unionAutoButton`. La lógica no se ejecuta desde el botón mientras esa UI no
  exista, para no inventar valores.

## 05/11/2026 — Vista de mapa y contador FPS

- `MainViewShp` se muestra y se pinta en negro al abrir un mapa; permanece
  oculto al crear un mapa nuevo, igual que el flujo visual del VB6.
- El `mapSurface` sigue siendo el renderizador contenido dentro de
  `MainViewShp`, sin modificar posiciones ni tamaños del diseñador.
- `FPS` muestra el número de repintados del visor durante el último segundo;
  al iniciar sin mapa muestra `FPS: 0` en lugar de `FPS: ??`.
- Build Debug verificado: 0 advertencias y 0 errores.

## 05/11/2026 — Unión adyacente y pestañas de mapas

- ✅ `UnionAdyacenteForm` aplica los cuatro bordes con los límites básicos del
  VB6, lee los translados existentes y habilita `Aplicar` solo con un borde
  seleccionado.
- ✅ Se validan mapas `0..1024` y límites `1..99`; las celdas bloqueadas se
  conservan sin cambios.
- ✅ `MapPest1..MapPest13` se rellenan al abrir/guardar y ahora abren el mapa
  vecino correspondiente al hacer clic, respetando `MapPest_Click` del VB6.
- ✅ Documentación actualizada en `Documentacion/modMapIO_Comparacion.md` y
  `Documentacion/Inventario_frmMain_Controles.md`.
- ✅ Build verificado:
  `dotnet build K:\AO_Aospain\utilidades\WorldEditor\WorldEditorNet\WorldEditorNet.vbproj --configuration Debug --no-restore`
  — 0 advertencias, 0 errores.
- ⚠️ Los perfiles VB6 `Legal` y `Ulla` aún no tienen menú en el diseñador;
  no se han inventado controles para ellos.

## 05/11/2026 — Navegación del mapa

- ✅ Vista centrada en `UserPos` y navegación con las cuatro flechas.
- ✅ Clic en el minimapa reposiciona el visor y actualiza el apuntador.
- ✅ Coordenadas del ratón usan la celda real del mapa y el contador FPS sigue
  funcionando mediante los repintados del visor.
- ✅ Radar reconstruido desde el documento cargado.
- ⚠️ Falta comparar visualmente la escala final del radar y el encuadre contra
  DirectDraw usando un mapa real.

## 05/11/2026 — Bloqueos y apuntador del radar

- ✅ Corregido el render de bloqueos: marca roja de 5x5 centrada por celda,
  equivalente a `BMPSurface.DrawBox` del VB6.
- ✅ Sincronizados `Bloqueos`, `CBVerBloqueosT`, `Ver Bloqueos` y el menú
  `...Bloqueos`.
- ✅ El apuntador del minimapa se pinta como contorno transparente sobre el
  bitmap del radar.
- ✅ Build Debug: 0 advertencias y 0 errores.

## 06/11/2026 — Resolución de recursos gráficos

- ✅ Corregida la selección de carpetas de `Graficos` e `INIT` cuando las
  carpetas creadas bajo `bin\Debug` existen pero están vacías.
- ✅ Durante la depuración se buscan los recursos en las carpetas ascendientes
  del proyecto, sin fijar rutas absolutas.
- ✅ `Graficos.ind` y los BMP del mapa vuelven a resolverse desde la misma
  raíz de recursos; se evita el texto de reserva con números cuando hay
  gráficos disponibles.
- ✅ Build Debug y pruebas smoke: correctos.
- ✅ El proyecto copia íntegramente `Datos`, `INIT` y `DATS` al directorio de
  salida en cada compilación.

## 06/11/2026 — Configuración inicial de rutas

- ✅ Si `Graficos`, `DATS`, `INIT` o `MIDI` no son válidos, el arranque solicita
  cada carpeta mediante el selector estándar de Windows.
- ✅ Las rutas elegidas se guardan en `Datos\WorldEditor.ini`, sección `[PATH]`,
  usando las claves `DirGraficos`, `DirDats`, `DirIndex` y `DirMidi`.
- ✅ Si el INI no existe pero las carpetas incluidas junto al ejecutable son
  válidas, se crea automáticamente con esas rutas.
- ⚠️ Si el usuario cancela la selección o elige una carpeta sin los recursos
  mínimos, el editor continúa abierto y muestra configuración pendiente; no se
  inventan rutas absolutas.

## 06/11/2026 — Escala de GRH en el render

- ✅ `MapSurfaceControl.DrawGraphic` deja de forzar todos los GRH a una celda
  de 32×32.
- ✅ Las superficies de la primera capa conservan su tamaño real
  (`pixelWidth`/`pixelHeight`), como el `BltFast` del VB6.
- ✅ Las capas superiores aplican el centrado vertical y horizontal del VB6,
  equivalente a `DDrawTransGrhtoSurface(..., Center=1)`.
- ✅ Build Debug: 0 advertencias y 0 errores.
- ✅ Los objetos del mapa se reconstruyen desde `OBJ.dat` usando su
  `GrhIndex` y se dibujan con el tamaño real del GRH, centrados como en
  `DDrawTransGrhtoSurface(..., Center=1)` del VB6.
- ✅ `Personajes.ind` y `Cabezas.ind` se cargan con el formato binario exacto
  del VB6: cabecera de 263 bytes, contador y registros de 12/8 bytes.
- ✅ Los NPCs se reconstruyen desde `NPCs.dat`, `Personajes.ind` y
  `Cabezas.ind`, respetando dirección, GRH del cuerpo, GRH de cabeza y
  `HeadOffset`.
- ✅ El marcador textual `N123` solo se muestra cuando el NPC no puede
  renderizarse por datos incompletos o inválidos.
- ⚠️ Falta comprobar visualmente en ejecución la escala y posición de algunos
  NPCs altos sobre un mapa real frente al DirectDraw original.

## 06/11/2026 — Render de NPCs

- ✅ Añadidos `LoadBodies` y `LoadHeads` en `LegacyIndicesLoader`.
- ✅ Se validan tamaños binarios para evitar leer registros desalineados.
- ✅ `MapSurfaceControl` dibuja primero el cuerpo y después la cabeza con el
  desplazamiento definido por `Personajes.ind`.
- ✅ Debug y Release compilan sin advertencias ni errores.
- ✅ Smoke test de `MapV1Codec`: correcto.

## 06/11/2026 — Transparencia y orden de objetos

- ✅ Aplicada la clave de color heredada del VB6: el negro de los BMP se
  convierte en transparente al cargar las superficies, equivalente a
  `SetColorKey(DDCKEY_SRCBLT)` y `DDBLTFAST_SRCCOLORKEY`.
- ✅ Corregido el orden de composición del mapa: capas 1-2, objetos, NPCs,
  capa 3 y capa 4, igual que `RenderScreen` del original.
- ✅ Los objetos siguen usando el `GrhIndex` de `OBJ.dat`, tamaño real del GRH
  y centrado compatible con `DDrawTransGrhtoSurface`.
- ✅ Debug y Release compilados sin advertencias ni errores.
- ⚠️ Falta una comprobación visual del mapa real en ejecución para validar los
  BMP concretos del usuario después de esta corrección.

## 06/11/2026 — Escala de baldosa y cuadrícula

- ✅ Corregida la celda del visor de 24×24 a 32×32 píxeles, que es la medida
  usada por `RenderScreen` en VB6. Esto corrige la separación y el tamaño
  aparente de casas y objetos.
- ✅ `CBVerCuadricula` ahora alterna la cuadrícula del render, igual que
  `DDrawSpecialToSurface(..., 17, ...)` cuando `CBVerCuadricula.Value = True`.
- ✅ El menú `Ver → Grilla` y el botón lateral mantienen el mismo estado.
- ✅ Debug y Release compilados sin advertencias ni errores.

## 09/11/2026 — Etiquetas de mapas vecinos

- ✅ Las pestañas `MapPest` conservan la fuente Arial 8,25 del VB6 para mostrar
  correctamente `Mapa10`, `Mapa11`, etc., sin recortar el último dígito.
- ✅ Se mantienen las posiciones y el tamaño original de 50×17 píxeles.
- ✅ Compilación Debug correcta, sin advertencias ni errores.

