# Comparación `modPaneles.bas` → `MainForm`

## Estado

Compatibilidad estimada: **25 %**.

El módulo VB6 no administra solamente menús: controla la visibilidad de los
controles de edición de `frmMain`, el estado visual de los paneles superiores y
la opción `Control Automáticamente`. La versión .NET actual no tiene todavía
los paneles equivalentes de translados, NPCs, objetos y triggers.

## Comportamiento verificado en VB6

- `EstSelectPanel` cambia el estado visual del panel seleccionado y, cuando
  `mnuVerAutomatico` está activo, fuerza temporalmente la capa, los bloqueos o
  los triggers visibles.
- `VerFuncion(0)` muestra u oculta los controles de superficies, capas,
  selección, quitar y la previsualización.
- `VerFuncion(1)` muestra u oculta los controles de translados.
- `VerFuncion(2)` muestra u oculta los controles de bloqueos.
- `VerFuncion(3)` y `VerFuncion(4)` muestran u ocultan NPCs y NPCs hostiles.
- `VerFuncion(5)` muestra u oculta objetos.
- `VerFuncion(6)` muestra u oculta triggers.
- `Filtrar` rellena las listas de superficies, NPCs y objetos a partir de sus
  estructuras de datos.
- `VistaPreviaDeSup` dibuja el GRH seleccionado, incluyendo el modo mosaico.

## Equivalencia actual

| VB6 | .NET | Estado |
|---|---|---|
| `VerFuncion` para capas | `Ver > ...Capas` y `MapSurfaceControl.ShowLayer*` | Parcialmente implementado |
| `VerFuncion` para bloqueos | `Ver > ...Bloqueos` | Parcialmente implementado |
| `VerFuncion` para translados | `Ver > ...Translados` | Marcador diagnóstico |
| `VerFuncion` para NPCs | `Ver > ...NPC's` | Marcador diagnóstico |
| `VerFuncion` para objetos | `Ver > ...Objetos` | Marcador diagnóstico |
| `VerFuncion` para triggers | `Ver > ...Trigger's` | Marcador diagnóstico |
| paneles de edición F5–F12 | no existen controles WinForms equivalentes | Pendiente |
| `Filtrar` | no existe aún una lista de recursos equivalente | Pendiente |
| `VistaPreviaDeSup` | recorte BMP básico en la superficie | Parcialmente implementado |

## Decisión de migración

Las opciones `Paneles` permanecen visibles pero deshabilitadas hasta migrar sus
controles y datos asociados. Activarlas ahora produciría una acción vacía y no
sería fiel al comportamiento original. El siguiente trabajo debe crear los
paneles WinForms en el diseñador y conectarlos después a `VerFuncion` y
`EstSelectPanel`; no se sustituirán por cuadros de diálogo improvisados.
