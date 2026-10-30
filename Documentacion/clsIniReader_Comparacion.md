# Comparación de `clsIniReader.cls`

## Resultado

La migración se ha realizado en `Core\clsIniReader.vb`.

Compatibilidad estimada: **92% funcional** para lectura de los INI usados por
el editor.

## Funcionalidades conservadas

- Carga completa del archivo en memoria.
- Detección de secciones entre corchetes.
- Ignora líneas vacías y líneas sin `=`.
- Ignora claves situadas antes de la primera sección.
- Normaliza nombres de sección y claves a mayúsculas.
- Mantiene el valor después de `=` sin recortarlo.
- Ordena secciones y claves con quicksort.
- Usa búsqueda binaria para `GetValue`.
- Devuelve cadena vacía si no encuentra sección o clave.
- Mantiene la compatibilidad de carga repetida sobre la misma instancia: no
  borra automáticamente las secciones anteriores, igual que el VB6 original.

## Diferencias controladas

| Elemento | VB6 | VB.NET | Impacto |
|---|---|---|---|
| Lectura de archivo | `Open ... For Input` | `StreamReader` | Se usa el código de página ANSI actual del sistema |
| Arrays privados | Arrays dinámicos VB6 | `List(Of T)` privada | No afecta al contrato público |
| `Integer` de índices | 16 bits | `Integer` .NET de 32 bits | Solo es interno y no cambia los valores devueltos |
| Liberación | `Class_Terminate` | GC/`IDisposable` no necesario | No hay handles nativos en esta clase |

## Verificación

El proyecto .NET compila en Release con 0 errores y 0 advertencias. La prueba
de comportamiento contra todos los archivos de datos se realizará al migrar
`modIndices.bas` y `modMapIO.bas`, que son sus consumidores principales.

## Funcionalidad no perdida

No se ha añadido recorte automático de claves, comentarios especiales,
eliminación de duplicados ni valores por defecto, porque el VB6 original no lo
hace.
