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
- ⚠️ Corrección estructural pendiente: en VB6 `pPaneles` es el `PictureBox`
  contenedor real de esos controles; en .NET `pPaneles` sigue siendo un
  `PictureBox` oculto/vacío y `functionContentPanel` es el contenedor visible.
  El área visual sí usa la posición `(8,120)` y el tamaño `293×293 px`, pero
  la jerarquía todavía no es idéntica.
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

## Siguiente paso

Completar el resto del diseño visual de `frmMain.frm` desde el diseñador:
visor de GRH, pestañas de mapas, nombres indexados exactos y formas/separadores.
Después extraer de `frmMain.frx` los siete iconos originales y asignarlos
desde el diseñador; finalmente migrar por grupos los controles de `VerFuncion`
y conectar su comportamiento con `EstSelectPanel`, empezando por Superficie y
Translados.
