# Inventario visual de `frmMain`

Fuente revisada:

- `Codigo/frmMain.frm`
- `Codigo/modPaneles.bas`, procedimiento `VerFuncion`

## Resumen

El formulario VB6 contiene 227 declaraciones visuales, incluyendo elementos de menú, controles indexados y separadores. Además, `modPaneles.bas` utiliza controles de función que deben formar parte del diseño aunque no aparezcan como bloques `Begin VB.*` dentro de `frmMain.frm`.

### Leyenda de estado

- ✅ Control físico existente en `MainForm.Designer.vb`.
- ⚠️ Equivalente visual colocado, pero no es el mismo control, tipo o indexación VB6, o falta su comportamiento.
- ❌ Control todavía no colocado de forma física en el diseñador.

## Controles declarados en `frmMain.frm`

### Contenedores

- ⚠️ `FrameComposicionTile` → equivalente visual `tileCompositionPanel` (no es un `Frame` VB6)
- ⚠️ `Frame3` → equivalente visual `tileLayersGroup`
- ⚠️ `FramePosicion` → equivalente visual `tilePositionGroup`
- ⚠️ `Frame4` → equivalente visual `tileTransferGroup`
- ⚠️ `Frame5` → equivalente visual `tileNpcGroup`
- ⚠️ `Frame6` → equivalente visual `tileObjectGroup`
- ✅ `Frame1` → equivalente visual `mapInfoPanel`, con sus etiquetas de fondo y valores separados

### Imágenes y visores

- ⚠️ `picRadar` está representado por `radarPanel`; contiene `Shape1`,
  `ApuntadorRadar`, `POSX` y `FPS`. Las formas VB6 se representan mediante
  `Panel`, porque WinForms no tiene un control `Shape` nativo.
- ✅ `pPaneles`: en VB6 es un `PictureBox` real de `293×293 px` (4395 twips), situado en `(8,120)` y usado como fondo/contenedor visual. En WinForms se mantiene como `PictureBox` dentro de `leftSidebar`; `functionContentPanel` permanece como control hermano superpuesto, porque el diseñador no permite usar un `PictureBox` como contenedor visual de controles.
- ✅ Distribución de translados de `pPaneles`: `transferMapTextBox`, `transferXTextBox` y `transferYTextBox` ocupan las posiciones equivalentes a `tTMapa`, `tTX` y `tTY`; los botones `insertTransferButton`, `insertTransferObjectButton`, `unionManualButton`, `unionAutoButton` y `removeTransferButton` están colocados con la separación del `.frm` original.
- ✅ Controles inferiores directos de `pPaneles`: `removeSurfaceLayerButton`, `removeSurfaceAllLayersButton` y `selectSurfaceButton` están fuera de `surfaceFunctionPanel`, como en el VB6, y conservan la distribución inferior izquierda/derecha.
- ✅ Los grupos de funciones `Bloqueos`, `NPC`, `NPC hostil`, `Objetos` y `Triggers` usan ahora el área base de `pPaneles` (`293×293 px`). Sus listas, filtros y botones inferiores se han colocado con la conversión de las coordenadas VB6 (15 twips ≈ 1 px).
- ⚠️ Los controles VB6 indexados (`lListado()`, `cFiltro()`, `cNumFunc()`, `cCantFunc()`, `lNumFunc()`, `lCantFunc()`, `lbFiltrar()`, `cInsertarFunc()`, `cQuitarFunc()` y `cAgregarFuncalAzar()`) están representados por controles .NET separados por grupo, no por matrices VB6. La posición visual está adaptada; la indexación común todavía debe completarse en la lógica.
- ✅ `lMapN`, `lXhor` y
  `lYver` ya están colocados como etiquetas físicas ocultas, pero falta
  conectar su visibilidad a la lógica original.
- ✅ `Picture2`
- ✅ `Picture5`
- ✅ `Picture6`
- ✅ `Picture7`
- ✅ `Picture8`
- ✅ `Picture9`
- ✅ `Picture11`
- ✅ `PreviewGrh`
- ✅ `Image` — `PictureBox` colocado con la posición y tamaño del `.frm`, con `Image.gif` integrado en `Properties/EditorResources.resx`.
- ⚠️ `CBVerCuadricula` — `Button` estándar de 33×33 px, con imagen integrada; el VB6 no contiene un procedimiento `Click` para este control.
- ✅ `CBVerBloqueosT` — `Button` estándar de 33×33 px, con imagen integrada y conectado a `mnuVerBloqueos_Click`.
- ⚠️ `CBVerBloqueosA` — `Button` estándar de 33×33 px, con imagen integrada; el VB6 no contiene un procedimiento `Click` para este control.
- ✅ `CBVerTriggers` — `Button` estándar de 33×33 px, con imagen integrada y conectado a `mnuVerTriggers_Click`.
- ✅ `CBVerTraslados` — `Button` estándar de 33×33 px, con imagen integrada y conectado a `mnuVerTranslados_Click`.

Los cinco controles `CBVer*` están en la columna derecha del área principal,
con posición convertida desde `frmMain.frm`: `x=1208` y `y=184, 216, 248,
280, 312`. Los tres eventos existentes en el VB6 ya están conectados; los dos
controles sin procedimiento `Click` permanecen sin acción hasta encontrar una
referencia adicional en el código original.

### Recursos extraídos de `frmMain.frx`

Se han extraído los recursos GIF originales, sin asignarlos por código:

- ✅ `Assets/LegacyButtons/CBVerCuadricula.gif` — 20×20 px
- ✅ `Assets/LegacyButtons/CBVerBloqueosT.gif` — 20×20 px
- ✅ `Assets/LegacyButtons/CBVerBloqueosA.gif` — 20×20 px
- ✅ `Assets/LegacyButtons/CBVerTriggers.gif` — 24×24 px
- ✅ `Assets/LegacyButtons/CBVerTraslados.gif` — 24×24 px
- ✅ `Assets/LegacyButtons/Image.gif` — 31×17 px

Las imágenes están integradas en `Properties/EditorResources.resx` y asignadas a
las propiedades visuales serializadas del diseñador, sin cargar rutas desde
el código en tiempo de ejecución.

### Listas

- ✅ `lListado(0)`: superficies → `surfaceListBox` (`Name = lListado0`)
- ✅ `lListado(1)`: NPCs → `npcListBox` (`Name = lListado1`)
- ✅ `lListado(2)`: NPCs hostiles → `hostileNpcListBox` (`Name = lListado2`)
- ✅ `lListado(3)`: objetos → `objectListBox` (`Name = lListado3`)
- ✅ `lListado(4)`: triggers → `triggerListBox` (`Name = lListado4`)

### Combos

- ✅ `cCapas`: capa seleccionada
- ✅ `cGrh`: GRH de superficie
- ✅ `cFiltro(0)`: filtro de superficies
- ✅ `cFiltro(1)`: filtro de NPCs
- ✅ `cFiltro(2)`: filtro de NPCs hostiles
- ✅ `cFiltro(3)`: filtro de objetos
- ✅ `cNumFunc(0)`: número de función de NPC
- ✅ `cNumFunc(1)`: combo físico `hostileNpcNumberCombo` (`Name = cNumFunc1`)
- ✅ `cNumFunc(2)`: combo físico `objectNumberCombo` (`Name = cNumFunc2`)
- ✅ `cCantFunc(0)`: cantidad de NPC
- ✅ `cCantFunc(1)`: combo físico `hostileNpcAmountCombo` (`Name = cCantFunc1`)
- ✅ `cCantFunc(2)`: combo físico `objectAmountCombo` (`Name = cCantFunc2`)

### TextBox

- ✅ `StatTxt`: información y estado del tile
- ✅ `tTX`: coordenada X del traslado
- ✅ `tTY`: coordenada Y del traslado
- ✅ `tTMapa`: mapa destino del traslado

### Etiquetas

- ✅ `FPS` → etiqueta física independiente dentro del equivalente `radarPanel`.
- ✅ `POSX` → etiqueta física independiente dentro del equivalente `radarPanel`.
- ✅ `lMapN`, `LabelTrasX`, `LabelTrasY`: etiquetas físicas del panel de translados
- ✅ `lXhor`, `lYver`: etiquetas físicas colocadas y ocultas como en VB6; falta conectar su visibilidad.
- ✅ `lbFiltrar(0..3)`: etiquetas físicas `lbFiltrar0..3`
- ✅ `lbCapas`, `lbGrh`: etiquetas físicas del panel de superficies
- ✅ `lblMapNombre`, `lblMapMusica`, `lblMapVersion`, `lblMapNivelMin` → valores del mapa
- ✅ `lblFNombreMapa`, `lblFMusica`, `lblFVersion`, `Label1` → etiquetas de fondo del bloque de información
- ✅ `LabelPosX`, `LabelPosY`, `LabelBloqueado`
- ⚠️ `LabelCapa(1..4)` → cuatro etiquetas físicas `LabelCapa1..4`; la
  indexación VB6 aún no está reproducida.
- ✅ `LabelTrasMapa`, `LabelTrasX`, `LabelTrasY`
- ✅ `LabelNPCIndex`, `LabelTipoNPC`, `LabelNPCName`: etiquetas físicas del bloque NPC del tile
- ✅ `LabelOBJIndex`, `LabelOBJCantidad`, `LabelOBJName`: etiquetas físicas del bloque de objeto del tile
- ✅ `COPIAR_GRH(0..3)` y `MemoriaAuxiliar` están colocados como etiquetas
  físicas del diseñador; su comportamiento de copia queda pendiente.
- ✅ `MapPest(1..13)` — colocados como `Label` `MapPest1` a `MapPest13`.
- `Label1`

### Otros controles

- ✅ `Separacion1`, `Separacion2`: separadores físicos colocados como paneles de 1 píxel
- ⚠️ `ApuntadorRadar`, `Shape1`, `MainViewShp`: equivalentes físicos colocados
  como paneles; WinForms no dispone del control `Shape` de VB6.
- ✅ `TimAutoGuardarMapa` → `autoSaveTimer`
- ✅ `Dialog` → `openMapDialog` y `saveMapDialog` (`OpenFileDialog`/`SaveFileDialog`)

## Controles de función usados por `modPaneles.bas`

Estos nombres deben existir también en el diseño .NET, aunque algunos no aparecen como `Begin VB.*` en el formulario porque el proyecto VB6 los crea/configura mediante código o mediante controles externos:

- ✅ Superficies: `cQuitarEnEstaCapa`, `cQuitarEnTodasLasCapas`, `cSeleccionarSuperficie`
- ✅ Translados: `cInsertarTrans`, `cInsertarTransOBJ`, `cUnionManual`, `cUnionAuto`, `cQuitarTrans`
- ✅ Bloqueos: `cInsertarBloqueo`, `cQuitarBloqueo`, `cVerBloqueos`
- ✅ NPC/NPC hostil/objetos: controles visuales `cInsertarFunc(0..2)`, `cQuitarFunc(0..2)`, `cAgregarFuncalAzar(0..2)`
- ✅ Triggers: `cInsertarTrigger`, `cQuitarTrigger`, `cVerTriggers`
- ✅ Selector superior equivalente: siete `LegacyPanelButton` para `SelectPanel(0..6)`

## Qué se muestra en cada panel

### Panel 0: Superficie

Controles visualmente colocados; la visibilidad todavía no está conectada a `VerFuncion`:

- ✅ `lListado(0)`
- ✅ `cFiltro(0)`
- ✅ `cCapas`
- ✅ `cGrh`
- ✅ `cQuitarEnEstaCapa`
- ✅ `cQuitarEnTodasLasCapas`
- ✅ `cSeleccionarSuperficie`
- ✅ `lbFiltrar(0)` → `surfaceFilterLabel`
- ✅ `lbCapas` → `layerLabel`
- ✅ `lbGrh` → `grhLabel`
- ✅ `PreviewGrh`

`StatTxt` está colocado directamente en `leftSidebar`, fuera de los paneles
de funciones, con posición `(16,418)` y tamaño `290×285 px`, igual que el
VB6 (`Left=16`, `Top=418`, `Width=290`, `Height=285`).

`Picture2` se colocó directamente en `leftSidebar`, superpuesto con `StatTxt`
en la posición equivalente al VB6 `(4,416)`, con tamaño `297×364 px`.
`PreviewGrh` es hijo de `Picture2`, en `(0,8)` y con tamaño `291×252 px`,
respetando las dimensiones del bloque original.
El visor usa únicamente `Picture2` y su hijo `PreviewGrh`; no se añade un
panel auxiliar duplicado.

`FrameComposicionTile` (`tileCompositionPanel`) conserva la conversión del
 VB6: posición `(312,688)` y tamaño `697×97 px`. Se coloca debajo de
 `mapScrollPanel`, cuyo borde inferior queda en `686 px`, evitando el
 solapamiento que existía al usar `Top=664`.

### Panel 1: Translados

Controles visualmente colocados; la visibilidad todavía no está conectada a `VerFuncion`:

- ✅ `lMapN`; ✅ `lXhor` y `lYver` son etiquetas físicas ocultas como en VB6.
- ✅ `tTMapa`, `tTX`, `tTY`
- ✅ `cInsertarTrans`, `cInsertarTransOBJ`
- ✅ `cUnionManual`, `cUnionAuto`
- ✅ `cQuitarTrans`

### Panel 2: Bloqueos

Controles visualmente colocados; la visibilidad todavía no está conectada a `VerFuncion`:

- ✅ `cQuitarBloqueo`
- ✅ `cInsertarBloqueo`
- ✅ `cVerBloqueos`

### Panel 3: NPCs

Controles visualmente colocados; la visibilidad todavía no está conectada a `VerFuncion`:

- ✅ `lListado(1)`
- ✅ `cFiltro(1)`
- ✅ `lbFiltrar(1)`
- ✅ `lNumFunc(0)`, ✅ `cNumFunc(0)`
- ✅ `cInsertarFunc(0)`, `cQuitarFunc(0)`, `cAgregarFuncalAzar(0)`
- ✅ `lCantFunc(0)`, ✅ `cCantFunc(0)`

### Panel 4: NPCs hostiles

Controles visualmente colocados; la visibilidad todavía no está conectada a `VerFuncion`:

- ✅ `lListado(2)`
- ✅ `cFiltro(2)`
- ✅ `lbFiltrar(2)`
- ✅ `lNumFunc(1)`, ✅ `cNumFunc(1)`
- ✅ `cInsertarFunc(1)`, `cQuitarFunc(1)`, `cAgregarFuncalAzar(1)`
- ✅ `lCantFunc(1)`, ✅ `cCantFunc(1)`

### Panel 5: Objetos

Controles visualmente colocados; la visibilidad todavía no está conectada a `VerFuncion`:

- ✅ `lListado(3)`
- ✅ `cFiltro(3)`
- ✅ `lbFiltrar(3)`
- ✅ `lNumFunc(2)`, ✅ `cNumFunc(2)`
- ✅ `cInsertarFunc(2)`, `cQuitarFunc(2)`, `cAgregarFuncalAzar(2)`
- ✅ `lCantFunc(2)`, ✅ `cCantFunc(2)`

### Panel 6: Triggers

Controles visualmente colocados; la visibilidad todavía no está conectada a `VerFuncion`:

- ✅ `cQuitarTrigger`
- ✅ `cInsertarTrigger`
- ✅ `cVerTriggers`
- ✅ `lListado(4)`

## Regla de implementación para WinForms

Todos estos controles deben existir físicamente en el diseñador de `MainForm` o en controles de usuario diseñados visualmente. La lógica de `VerFuncion` solo debe cambiar `Visible`, `Enabled`, texto, color y selección. No se deben crear controles desde el constructor ni desde `MainForm.vb`.

## Estado actual del diseño .NET

### Ya colocados en `MainForm.Designer.vb`

- ✅ Formulario principal `MainForm`.
- ✅ `menuStrip1` y estructura principal de menús.
- ✅ Panel lateral `leftSidebar`.
- ✅ Panel de información del mapa `mapInfoPanel`, con tamaño equivalente a `Frame1` (`189×82 px`).
- ✅ Etiquetas de fondo `lblFNombreMapa`, `lblFMusica`, `lblFVersion`, `Label1` y valores `lblMapNombre`, `lblMapMusica`, `lblMapVersion`, `lblMapNivelMin`, colocados según el `.frm`.
- ✅ Botón `mapInfoButton`.
- ✅ Botón `clearFunctionsButton`.
- ✅ Barra superior `functionBarPanel`.
- ✅ Botones diagonales `surfacePanelButton`, `transfersPanelButton`, `blockedPanelButton`, `npcsPanelButton`, `hostileNpcsPanelButton`, `objectsPanelButton` y `triggersPanelButton`.
- ✅ Segunda fila `panelStateRow` con 13 etiquetas `MapPest` de estado.
- ✅ Área de mapa `mapScrollPanel` y `mapSurface`.
- ✅ Barra de estado `statusStrip1` y `statusLabel`.
- ✅ Diálogos `openMapDialog` y `saveMapDialog`.
- ⚠️ `radarPanel` es el contenedor WinForms equivalente de `picRadar` y ya
  contiene `Shape1`, `ApuntadorRadar`, `POSX` y `FPS`. Las formas son paneles
  WinForms y su dibujo dinámico todavía debe conectarse.
- ✅ Panel inferior equivalente `tileCompositionPanel`.
- ✅ Grupos visuales del tile con sus etiquetas originales equivalentes.
- ⚠️ Panel visual `surfaceFunctionPanel` con filtro, capa, GRH y lista; actualmente está dentro de `functionContentPanel`, no directamente dentro del `pPaneles` original.
- ✅ Panel de Translados con campos y botones principales.
- ✅ Panel de Bloqueos con insertar, quitar y ver.
- ✅ Panel de NPCs con lista, filtro, número y cantidad.
- ✅ Paneles de NPCs hostiles, Objetos y Triggers con sus listas, filtros y combos físicos de número/cantidad; la lógica todavía está pendiente.
- ✅ Controles visuales de insertar, quitar y agregar funciones al azar para NPCs, NPCs hostiles y Objetos.
- ✅ Acciones visuales de Superficie: quitar en capa, quitar en todas las capas y seleccionar superficie.
- ✅ Acciones visuales de Triggers: insertar, quitar y ver triggers.
- ✅ `POSX` y `FPS` están colocados dentro del radar. No existen etiquetas
  auxiliares duplicadas en la jerarquía de `radarPanel`.
- ✅ Contenedores visuales equivalentes a los grupos de composición del tile.
- ✅ Temporizador visual `autoSaveTimer` preparado para el guardado automático.
- ✅ `PreviewGrh` añadido como `PictureBox` del diseñador.
- ✅ `pPaneles`, `Picture2`, `Picture5`, `Picture6`, `Picture7`, `Picture8`, `Picture9` y `Picture11` están declarados como `PictureBox` en el diseñador. `pPaneles` está dentro de `leftSidebar`; `functionContentPanel` se superpone como capa funcional. `Picture2` está superpuesto con `StatTxt` y contiene `PreviewGrh`.
- ✅ Los controles físicos `COPIAR_GRH0..3` y `MemoriaAuxiliar` están colocados
  en el diseñador con las dimensiones convertidas desde `frmMain.frm`.
- ✅ `StatTxt` como área visual de estado del tile, con el nombre original del VB6.
- ⚠️ Faltan la indexación VB6 real de controles repetidos y conectar los controles con la lógica de `VerFuncion`.

### Colocados como equivalentes visuales; comportamiento pendiente

- ⚠️ `radarPanel` es el contenedor equivalente a `picRadar`.
  `Shape1`, `ApuntadorRadar`, `POSX` y `FPS` están colocados dentro, junto
  con el visor `radarPictureBox`.
- ⚠️ `tileCompositionPanel` contiene los grupos visuales y las etiquetas de capas/traslado/NPC/objeto/bloqueo; faltan algunos campos originales y sus datos dinámicos.
- ⚠️ `functionContentPanel` es la capa funcional superpuesta a `pPaneles`; contiene los siete paneles de función (`surfaceFunctionPanel`, `transferFunctionPanel`, `blockingFunctionPanel`, `npcFunctionPanel`, `hostileNpcFunctionPanel`, `objectFunctionPanel` y `triggerFunctionPanel`) y usa el tamaño base del contenedor VB6 (`293×293 px`). Falta completar el cambio de visibilidad y carga de datos equivalente a `VerFuncion`.
- ⚠️ Los siete `LegacyPanelButton` sustituyen visualmente a `SelectPanel(0..6)`, pero todavía falta completar la lógica equivalente de `VerFuncion`.

### Colocados en el diseñador, pendientes de fidelidad funcional

- ⚠️ `lListado(0..4)`: equivalentes `surfaceListBox`, `npcListBox`, `hostileNpcListBox`, `objectListBox` y `triggerListBox`, con sus nombres `lListado0..4`.
- ⚠️ `cCapas`, `cGrh` y `cFiltro(0..3)`: equivalentes colocados y renombrados en el diseñador.
- ⚠️ `cNumFunc(0..2)` y `cCantFunc(0..2)`: los seis controles físicos están colocados y nombrados en el diseñador; falta completar la indexación lógica y su carga de datos.
- ⚠️ `StatTxt`, `tTX`, `tTY` y `tTMapa`: controles equivalentes colocados; falta conectar su lectura/escritura con las operaciones del mapa.
- ⚠️ `lbFiltrar(0..3)`, `lbCapas`, `lbGrh`, `lNumFunc(0..2)` y `lCantFunc(0..2)`: sus etiquetas visuales equivalentes están colocadas; falta ajustar todos los nombres indexados y textos dinámicos.
- ⚠️ Acciones de Superficie: `cQuitarEnEstaCapa`, `cQuitarEnTodasLasCapas`, `cSeleccionarSuperficie`.
- ⚠️ Acciones de Translados: `cInsertarTrans`, `cInsertarTransOBJ`, `cUnionManual`, `cUnionAuto`, `cQuitarTrans`.
- ⚠️ Acciones de Bloqueos: `cInsertarBloqueo`, `cQuitarBloqueo`, `cVerBloqueos`.
- ⚠️ Acciones de NPC, NPC hostil y objetos: `cInsertarFunc(0..2)`, `cQuitarFunc(0..2)`, `cAgregarFuncalAzar(0..2)`.
- ⚠️ Acciones de Triggers: `cInsertarTrigger`, `cQuitarTrigger`, `cVerTriggers`.
- ⚠️ `COPIAR_GRH(0..3)` y `MemoriaAuxiliar`: equivalentes visuales colocados como etiquetas.
- ⚠️ `TimAutoGuardarMapa`: equivalente colocado como `autoSaveTimer`.
- ⚠️ `Separacion1`, `Separacion2`, `Shape1` y `MainViewShp`: están representados mediante paneles WinForms; falta conectar el dibujo dinámico equivalente a las líneas y formas VB6.
- ⚠️ `FrameComposicionTile`: contenedor y grupos visuales colocados con las
  posiciones convertidas del `.frm`; falta enlazar los datos de cada celda.

### Pendiente antes de empezar la lógica completa

- ⏳ Verificar visualmente en el diseñador la posición, tamaño y orden Z de todos los paneles de función.
- ✅ Mantener `pPaneles` como fondo `PictureBox` y `functionContentPanel` como capa hermana superpuesta; WinForms no permite reproducir mediante el diseñador la relación de contenedor del `PictureBox` VB6.
- ⏳ Implementar después la matriz de visibilidad de `modPaneles.bas` (`VerFuncion`) sin crear controles desde `MainForm.vb`.

### Resultado de la auditoría del inventario

- ✅ No quedan controles concretos marcados como no colocados.
- ⚠️ Los controles repetidos VB6 están representados físicamente, pero aún no
  tienen una indexación común equivalente.
- ⚠️ La lógica de visibilidad y actualización de datos todavía no está
  conectada al comportamiento de `VerFuncion`.
