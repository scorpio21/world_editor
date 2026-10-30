# Comparación `modGameIni.bas` → `GameIni.vb`

## Resultado

Compatibilidad estimada: **45 %**.

La migración cubre las declaraciones y la inicialización de la cabecera. La
serialización de `Inicio.con` permanece pendiente de especificación binaria y
no se ha sustituido por una interpretación inventada.

## Elementos trasladados

| VB6 | VB.NET | Estado |
|---|---|---|
| `tCabecera` | `GameIni.tCabecera` | Portado estructuralmente |
| `tGameIni` | `GameIni.tGameIni` | Portado estructuralmente |
| `MiCabecera` | `GameIni.MiCabecera` | Portado |
| `Config_Inicio` | `GameIni.Config_Inicio` | Portado |
| `IniciarCabecera` | `GameIni.IniciarCabecera` | Portado |
| `LeerGameIni` | `GameIni.LeerGameIni` | Bloqueado explícitamente |
| `EscribirGameIni` | `GameIni.EscribirGameIni` | Bloqueado explícitamente |

## Diferencias y motivo

1. En VB6 `tCabecera.Desc` es `String * 255`. En .NET se conserva como
   `String`, pero todavía no se escribe en disco porque falta confirmar la
   codificación y el tamaño exacto usado por los archivos existentes.
2. `tGameIni` contiene varios `String` de longitud variable. El original usa
   `Get #n, , GameIni` y `Put #n, , GameIni`; ese comportamiento no define por sí
   solo un formato interoperable con `BinaryReader`/`BinaryWriter` de .NET.
3. Las funciones de lectura y escritura lanzan `NotSupportedException` para
   evitar generar archivos `Inicio.con` incompatibles. Actualmente no hay
   consumidores de esas dos funciones en el código VB6 localizado.
4. Los `Long` de VB6 se representan como `Integer` de 32 bits en .NET y el
   `Integer` de VB6 (`NumeroMapas`) como `Short` de 16 bits.

## Verificación realizada

- Se leyó completo `Codigo/modGameIni.bas`.
- Se buscó el uso de `LeerGameIni`, `EscribirGameIni`, `IniciarCabecera`,
  `MiCabecera` y `Config_Inicio` en el proyecto.
- `MiCabecera` sí es usado por `modMapIO.bas` y `modIndices.bas`; por ello se
  conserva como estado público.
- `LeerGameIni` y `EscribirGameIni` no tienen consumidores detectados.
- La compilación Release del proyecto .NET se ejecutará antes de continuar con
  el siguiente archivo.

## Pendiente para alcanzar el 100 %

1. Inspeccionar archivos reales `Inicio.con` y confirmar su formato byte a byte.
2. Confirmar si las cadenas VB6 se almacenan como ANSI fijo, BSTR/descriptores u
   otro formato producido por la aplicación.
3. Implementar un codec con pruebas de lectura y escritura contra archivos
   reales.
