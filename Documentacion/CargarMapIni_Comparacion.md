# Comparación `CargarMapIni` → `WorldEditorSettingsLoader`

## Estado

Compatibilidad estimada: **75 %** para la carga de configuración aislada.

La rutina ya no depende de controles VB6 ni muestra mensajes durante la carga:
devuelve un modelo `WorldEditorSettings` que la futura `MainForm` podrá aplicar.

## Comportamiento trasladado

- Lee `Datos\WorldEditor.ini` con `clsIniReader`.
- Conserva los cuatro grupos de configuración: `CONFIGURACION`, `INDEX`,
  `PATH` y `MOSTRAR`.
- Conserva el valor por defecto `MaxGrhs=32200`.
- Resuelve rutas relativas y rutas que empiezan por `\` contra el directorio de
  la aplicación, y añade la barra final.
- Valida `Graficos`, `MIDI`, `INIT` y `DATS` como directorios existentes.
- Conserva la normalización de `LastPos`, `PantallaX`, `PantallaY`,
  `ClienteHeight` y `ClienteWidth`.
- Expone las opciones de visualización y configuración como propiedades
  booleanas para que WinForms las aplique al menú y al render.
- Si falta el archivo, conserva los directorios y valores base del original y
  marca `ConfigurationMissing=True`.

## Diferencias controladas

1. El VB6 actualiza directamente `frmMain`, `frmMusica` y variables globales;
   .NET devuelve un modelo para evitar acoplar la carga a un formulario que aún
   no existe.
2. El VB6 muestra `MsgBox` y ejecuta `End` cuando una ruta es inválida. .NET
   lanza `DirectoryNotFoundException`; la UI decidirá cómo mostrar el error sin
   terminar el proceso abruptamente.
3. `GetVar` se mantiene mediante la API Win32 en `GeneralUtilities`, pero la
   comparación final con INI que contengan espacios significativos queda
   pendiente de una prueba con archivos históricos.

## Pendiente

- Aplicar este modelo a los controles y menús de `MainForm`.
- Migrar el guardado de opciones (`WriteVar`) cuando exista el formulario.
- Integrar `DirMidi` con el selector de música.
- Verificar rutas históricas y archivos `WorldEditor.ini` con codificaciones
  ANSI distintas.
