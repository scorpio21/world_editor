# Memoria WorldEditor .NET

## Objetivo

Migrar progresivamente `AomaniaWorldEditor` VB6 a VB.NET WinForms .NET 8 en
`K:\AO_Aospain\utilidades\WorldEditor\WorldEditorNet`, manteniendo el
comportamiento comprobado y sin inventar equivalencias.

## Estado actual

- ✅ Codec V1 `.map/.inf`, mapa inicial, carga/guardado y edición básica.
- ✅ Render WinForms inicial con capas, bloqueos, cuadrícula y marcadores.
- ✅ Barra de menús con la jerarquía declarada en `Codigo/frmMain.frm`.
- ✅ Auditoría inicial de `modPaneles.bas`.
- ⚠️ Paneles F5–F12 y controles de translados/NPCs/objetos/triggers aún no
  tienen equivalentes WinForms; sus entradas se mantienen deshabilitadas.
- ⚠️ DirectDraw, V2/V3, radar y sprites completos siguen pendientes.

## Verificación

```powershell
dotnet build .\WorldEditorNet.vbproj --configuration Release
dotnet build .\WorldEditorNet.Tests\WorldEditorNet.Tests.vbproj --configuration Release
dotnet run --project .\WorldEditorNet.Tests\WorldEditorNet.Tests.vbproj --configuration Release --no-build
```

Resultado verificado: ambos builds sin errores ni advertencias y
`MapV1Codec smoke test: OK`.

## Historial reciente

- `73ceaec` — corrección de la jerarquía de menús VB6.
- `75bd229` — auditoría de `modPaneles.bas` y documentación de pendientes.

## Siguiente paso

Diseñar en WinForms los paneles equivalentes a F5–F12 a partir de los
controles declarados en `Codigo/frmMain.frm`; después conectar su visibilidad
con una adaptación verificable de `VerFuncion` y `EstSelectPanel`.
