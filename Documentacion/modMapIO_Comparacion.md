# Comparación `modMapIO.bas` → `MapInfoFile.vb`

## Estado

Compatibilidad estimada del módulo completo: **40 %**.

Compatibilidad de la parte textual migrada: **85 %**.

El formato V1 binario también está verificado y disponible en
`Core/MapV1Codec.vb`.

## Portado

- `FileSize` mediante `FileInfo.Length`, devolviendo `-1` ante error como el
  original.
- Comprobación explícita de archivo/directorio (`FileExist`).
- `MapInfo_Guardar` mediante `MapInfoFile.Save`.
- `MapInfo_Cargar` mediante `MapInfoFile.Load`.
- Claves, conversiones y valores de `MapInfo` conservados.
- `MapaV1_Cargar` mediante `MapV1Codec.Load`.
- `MapaV1_Guardar` mediante `MapV1Codec.Save`.
- Cabecera V1: versión `Int16`, descripción ANSI de 255 bytes, CRC y palabra
  mágica `Int32` en little-endian.
- `.map`: 4 campos reservados y 10.000 celdas de 13 bytes.
- `.inf`: 5 campos reservados y 10.000 celdas de 16 bytes.

## Diferencia detectada y conservada

El VB6 escribe la clave `SeCaenItems`, pero al cargar consulta `SeCaeItems`.
La migración mantiene esa diferencia para no ocultar un comportamiento del
original. Debe decidirse en una futura fase si se corrige en ambos proyectos o
si se soportan las dos variantes por compatibilidad histórica.

## Pendiente

No se ha implementado todavía:

- `MapaV2_Cargar` y `MapaV2_Guardar`.
- `MapaV3_Cargar` y `MapaV3_Guardar`.
- `AbrirMapa`, `GuardarMapa`, `NuevoMapa` y `DeseaGuardarMapa`.
- `Pestañas` y actualización de formularios.
- Serialización binaria de `MiCabecera`, `MapData`, `.map` y `.inf`.

La razón es que esas rutinas usan `Put/Get` de VB6 sobre UDTs con arrays,
dependen de `MiCabecera`, `MapData`, personajes, objetos, DirectDraw y controles
VB6. La compatibilidad binaria debe verificarse con archivos `.map`, `.inf` y
`.map2` reales antes de escribir un codec .NET.

## Verificación V1

- `Mapa1.map` mide 130.273 bytes, exactamente `273 + 10.000 * 13`.
- `Mapa1.inf` mide 160.010 bytes, exactamente `10 + 10.000 * 16`.
- La descripción comienza en el byte 2 y la primera celda comienza en el byte
  273, confirmando el layout de `MapaV1_Guardar`.
- La lectura y escritura se mantienen en un codec aislado y todavía no están
  conectadas a `MainForm`.
