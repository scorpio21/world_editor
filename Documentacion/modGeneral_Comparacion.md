# Comparación `modGeneral.bas` → `GeneralUtilities.vb`

## Estado de esta etapa

Compatibilidad estimada del módulo completo: **25 %**.

Compatibilidad de la parte trasladada en esta etapa: **90 %** para las
funciones independientes y sin estado visual. El porcentaje global es bajo
porque `modGeneral.bas` también contiene el arranque del editor, DirectDraw,
teclado, radar, movimiento y lógica de mapas.

## Funciones portadas

| VB6 | .NET | Estado |
|---|---|---|
| `MinInt` | `GeneralUtilities.MinInt` | Portado; VB6 `Integer` se representa como `Short` |
| `MaxInt` | `GeneralUtilities.MaxInt` | Portado; VB6 `Integer` se representa como `Short` |
| `ReadField` | `GeneralUtilities.ReadField` | Portado, conserva campos vacíos finales |
| `autoCompletaPath` | `GeneralUtilities.AutoCompletaPath` | Portado como función pública reutilizable |
| `GetVar` | `GeneralUtilities.GetVar` | Portado mediante `GetPrivateProfileStringW` |
| `WriteVar` | `GeneralUtilities.WriteVar` | Portado mediante `WritePrivateProfileStringW` |
| `RandomNumber` | `GeneralUtilities.RandomNumber` | Portado con intervalo inclusivo equivalente |

## Funcionalidad aún no portada

- `typDevMODE`, `EnumDisplaySettings`, `ChangeDisplaySettings` y
  `CambioDeVideo`: requieren una traducción Win32 verificada y una política de
  resolución para WinForms.
- `CheckKeys`: depende de `GetKeyState`, `frmMain`, `MapData`, selección,
  movimiento y edición del mapa.
- `CargarMapIni`: depende de controles del formulario principal, música, rutas,
  estado global y configuración del editor.
- `Main`: depende de DirectSound, DirectDraw, `modIndices`, `modMapIO`,
  `SurfaceDB`, formularios VB6 y el bucle de render.
- `ToggleWalkMode`, `FixCoasts` y `HayAgua`: dependen de estructuras y reglas
  del mapa todavía no migradas.
- `RefreshAllChars` y `CaptionWorldEditor`: dependen de controles VB6 y del
  estado visual del editor.
- `LoadClientSetup`: lee `ao.dat` directamente en un UDT cuyo layout debe
  verificarse antes de usar `BinaryReader`.

## Diferencias conocidas

1. `GetVar` usa la longitud devuelta por Win32 y elimina espacios finales. El
   original construía un buffer VB6 de 5000 caracteres y aplicaba `RTrim` y
   `Left`; se debe comparar con un INI real si aparecen valores con espacios
   significativos.
2. `RandomNumber` usa `Random.Shared` en lugar del estado global de `Rnd` y
   `Randomize Timer`. El intervalo numérico es equivalente, pero la secuencia
   no lo es; no se debe usar para reproducir mapas o partidas deterministas.
3. `AutoCompletaPath` recibe `appPath` explícitamente porque .NET no tiene un
   equivalente directo de `App.Path`.

## Verificación

- Se leyó completo `Codigo/modGeneral.bas` antes de escribir código.
- La cobertura del grafo no registra huecos para `modGeneral.bas`; `clsIniReader`
  sigue marcado como parse-parcial, por lo que sus rangos se verificaron con
  lectura directa.
- La compilación Release se ejecutará antes de migrar otro archivo.

## Pendiente para el 100 %

Separar y migrar por dependencias: configuración/formularios, APIs de entrada,
formato `ao.dat`, editor de mapas y finalmente el bucle DirectDraw/render.
