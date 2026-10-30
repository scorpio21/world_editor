# Comparación de `modDeclaraciones.bas`

## Resultado

La primera migración estructural se ha realizado en
`Core\LegacyDeclarations.vb`. No se ha migrado todavía ningún comportamiento
del editor.

Compatibilidad estimada de esta primera pieza: **55% estructural**.

El porcentaje no representa compatibilidad funcional del editor completo. La
parte de datos y constantes está trasladada; las dependencias externas y la
semántica exacta de los arrays VB6 todavía requieren una capa de compatibilidad.

## Comparación

| Área VB6 | Estado .NET | Observación |
|---|---|---|
| Constantes de dirección y tamaño | Implementado | Se mantienen valores numéricos |
| `Type` de posiciones | Implementado | `Integer` VB6 se representa como `Short` |
| `Type Grh` y `GrhData` | Parcial | Los arrays requieren inicialización compatible |
| `Type MapBlock` | Parcial | Se conserva la estructura, falta validar serialización |
| Datos de cuerpos/cabezas/NPC/objetos | Parcial | Estructuras trasladadas; falta lectura real |
| Arrays globales | Parcial | Se inicializan, pero falta verificar todos los índices usados |
| `MapData_Deshacer` | Implementado estructuralmente | Se conservan límites 1..10 y 1..100 |
| APIs Win32 | Pendiente | No se han añadido P/Invoke sin analizar sus consumidores |
| DirectX 7 | Pendiente | No existe sustitución .NET equivalente directa |
| DirectSound/DirectMusic | Pendiente | Requiere backend separado |
| ADO `Connection` | Pendiente | No se ha encontrado todavía una ruta funcional que lo necesite |

## Diferencias que deben corregirse antes de declarar compatibilidad total

1. VB6 permite arrays fijos con límites explícitos como `1 To 25`; .NET usa
   arrays de cero y requiere una convención de índices o un adaptador.
2. VB6 `Type` tiene semántica de valor; las estructuras .NET deben conservarla
   sin convertirlas arbitrariamente en clases.
3. Los índices de gráficos y objetos son `Integer` VB6 (16 bits), pero antes
   de migrar los lectores hay que comprobar si los archivos actuales exceden
   ese límite.
4. Los tipos `RECT` y las superficies pertenecen a DirectX/Win32 y no deben
   sustituirse por `Rectangle` hasta analizar `modDirectDraw`.
5. La declaración VB6 `As New` tiene creación diferida. La versión .NET no
   debe crear los objetos externos automáticamente hasta conocer su ciclo de
   vida.

## Verificación realizada

- Comparación directa con las 444 líneas del archivo VB6.
- Dependencias de los tipos principales localizadas en módulos de índices,
  mapas, edición, renderer y DirectDraw.
- No se ha generado código de DirectX ni se ha supuesto el formato binario.

