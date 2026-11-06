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
- `WorldEditorNet.Tests` verifica el round-trip sintético sin modificar mapas
  reales.

## Implementado en `MainForm.vb`

- Al abrir un `.map`, se ejecuta la lógica equivalente a `Pestañas()` del
  VB6: se toma el prefijo y número del mapa abierto y se buscan los mapas
  vecinos desde `-4` hasta `+8`.
- Los controles `MapPest1` a `MapPest13` se rellenan únicamente cuando existe
  el archivo vecino; los demás quedan ocultos, deshabilitados y con el texto
  `Nada`, como en el original.
- Al guardar un mapa se vuelve a actualizar la misma lista de pestañas.
- Al abrir el `.dat` asociado se cargan nombre, música y nivel mínimo; la
  versión procede de la cabecera V1. Si no existe el `.dat`, se conservan los
  valores predeterminados.
- Al crear un mapa nuevo se limpian y ocultan las pestañas y se restauran los
  datos iniciales del mapa.

Al hacer clic sobre cada `MapPest` se construye la ruta con el mismo prefijo y
número relativo del VB6 y se abre el mapa vecino, previa confirmación de los
cambios sin guardar.

## 05/11/2026 — Navegación del visor y radar

- `MapSurfaceControl` conserva una posición de vista (`UserPos`) y dibuja la
  ventana visible centrada en ella, en lugar de pintar siempre desde la celda
  1,1.
- Las flechas desplazan la vista una celda por pulsación y el clic izquierdo en
  el radar cambia la posición visible, siguiendo el flujo de `CheckKeys` y
  `picRadar_MouseDown` del VB6.
- Las coordenadas mostradas sobre el visor se calculan con la posición de vista
  real y mantienen el color rojo en los bordes del mapa.
- El radar se reconstruye desde las celdas y el índice gráfico al crear, abrir,
  editar o desplazar el mapa.
- La posición de vista se toma de `WorldEditor.ini` y se guarda de nuevo al
  cerrar el editor.

Pendiente de verificación manual: comprobar con los mapas reales que el centro,
los límites y la escala visual del radar coinciden exactamente con DirectDraw.

## 05/11/2026 — Bloqueos y apuntador del radar

- `ShowBlocked` ahora reproduce el render del VB6: cada celda bloqueada se marca
  con un cuadrado rojo de 5x5 píxeles centrado, sin cubrir el tile completo.
- El panel `Bloqueos`, el botón lateral `CBVerBloqueosT`, el botón `Ver
  Bloqueos` y el menú `...Bloqueos` comparten el mismo estado.
- Al seleccionar el panel `Bloqueos` se activa la visualización de bloqueos,
  siguiendo la activación automática de `EstSelectPanel` del VB6.
- El indicador del minimapa se dibuja como contorno blanco sobre la imagen del
  radar; ya no se utiliza un panel relleno opaco.

## 06/11/2026 — Recursos del render

- Corregida la resolución de `Graficos.ind` y de los BMP cuando las carpetas
  bajo `bin\Debug` existen pero todavía no contienen recursos.
- La carga busca una carpeta de recursos válida en la raíz del proyecto durante
  la depuración y mantiene la ruta junto al ejecutable para instalaciones
  publicadas.
