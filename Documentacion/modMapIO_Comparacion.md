# Comparación `modMapIO.bas` → `MapInfoFile.vb`

## Estado

Compatibilidad estimada del módulo completo: **20 %**.

Compatibilidad de la parte textual migrada: **85 %**.

## Portado

- `FileSize` mediante `FileInfo.Length`, devolviendo `-1` ante error como el
  original.
- Comprobación explícita de archivo/directorio (`FileExist`).
- `MapInfo_Guardar` mediante `MapInfoFile.Save`.
- `MapInfo_Cargar` mediante `MapInfoFile.Load`.
- Claves, conversiones y valores de `MapInfo` conservados.

## Diferencia detectada y conservada

El VB6 escribe la clave `SeCaenItems`, pero al cargar consulta `SeCaeItems`.
La migración mantiene esa diferencia para no ocultar un comportamiento del
original. Debe decidirse en una futura fase si se corrige en ambos proyectos o
si se soportan las dos variantes por compatibilidad histórica.

## Pendiente

No se ha implementado todavía:

- `MapaV1_Cargar` y `MapaV1_Guardar`.
- `MapaV2_Cargar` y `MapaV2_Guardar`.
- `MapaV3_Cargar` y `MapaV3_Guardar`.
- `AbrirMapa`, `GuardarMapa`, `NuevoMapa` y `DeseaGuardarMapa`.
- `Pestañas` y actualización de formularios.
- Serialización binaria de `MiCabecera`, `MapData`, `.map` y `.inf`.

La razón es que esas rutinas usan `Put/Get` de VB6 sobre UDTs con arrays,
dependen de `MiCabecera`, `MapData`, personajes, objetos, DirectDraw y controles
VB6. La compatibilidad binaria debe verificarse con archivos `.map`, `.inf` y
`.map2` reales antes de escribir un codec .NET.
