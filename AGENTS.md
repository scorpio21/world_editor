# AGENTS.md

# WorldEditor .NET

Migración progresiva de AomaniaWorldEditor VB6 a .NET 8 WinForms.

## Objetivo principal

Mantener la compatibilidad funcional y visual con el editor VB6 original.

No introducir comportamientos nuevos sin verificar previamente el comportamiento
del editor VB6. La fidelidad funcional tiene prioridad sobre la modernización.

## Core Principles

- Pensar antes de programar.
- Reutilizar antes de crear.
- Mantener el código pequeño y comprensible.
- Aplicar la mínima solución completa que resuelva el problema.

## Skills Workflow

### Ponytail (obligatorio)

Antes de escribir código:

1. Buscar si la funcionalidad ya existe.
2. Comprobar si .NET ya resuelve el problema.
3. Buscar clases o servicios similares.
4. Reutilizar los patrones existentes.
5. Escribir la mínima cantidad de código necesaria.

Evitar sobreingeniería, abstracciones innecesarias, dependencias nuevas,
clases gigantes y código duplicado.

### Superpowers mindset

Antes de implementar:

1. Analizar el problema.
2. Crear un plan breve.
3. Dividir el trabajo en tareas pequeñas.
4. Identificar riesgos.
5. Implementar por fases.
6. Revisar antes de finalizar.

No saltar directamente a la implementación.

### Codebase Memory

Antes de modificar un sistema importante:

- analizar las implementaciones existentes;
- respetar las convenciones del proyecto;
- evitar duplicar funcionalidades;
- consultar el grafo del código cuando esté disponible.

### Review

Después de cambios importantes, revisar duplicados, dependencias innecesarias,
deuda técnica y posibles regresiones.

### Review Security

Realizar una revisión de seguridad cuando se modifique lectura o escritura de
archivos, serialización, configuración o recursos externos.

## Arquitectura

- La UI WinForms muestra datos, recibe eventos y delega la lógica.
- La lógica migrada desde VB6 debe residir en `Core/`, `Services/` o
  `Infrastructure/`.
- No mover lógica de negocio compleja a `MainForm`.
- Evitar clases God Object y dependencias circulares.
- Aplicar la separación progresivamente; no hacer refactorizaciones masivas que
  alteren el comportamiento observable.

### Límites de código propio

- Máximo recomendado: 300 líneas por archivo.
- Máximo recomendado: 15 métodos por clase.
- Métodos preferiblemente menores de 50 líneas.

Estos límites no se aplican de forma estricta a código generado, formularios
migrados pendientes de separar ni archivos de recursos.

## Diseñador de Windows Forms

- Crear y colocar controles desde el diseñador de Visual Studio.
- Mantener los nombres, tamaños, posiciones, colores, jerarquía y visibilidad
  del formulario VB6 cuando estén documentados.
- No crear controles dinámicamente desde código para sustituir controles del
  diseñador.
- No editar manualmente `*.Designer.vb`; cualquier cambio visual debe hacerse
  desde el diseñador.
- No editar manualmente `*.resx` ni `*.Resources.vb` salvo que sea necesario
  para reparar un recurso; preferir el editor de recursos de Visual Studio.
- No cambiar tamaños de controles ya ajustados sin indicarlo previamente.
- Documentar cada control añadido o confirmado en
  `Documentacion/Inventario_frmMain_Controles.md`.

## UI Design Workflow

Cuando se cree una interfaz nueva o se rediseñe una existente:

1. Usar `ui-ux-pro-max` cuando se cree una ventana, herramienta o panel
   complejo, para generar o validar el sistema de diseño.
2. Definir colores, espaciado y tipografía antes de implementar cambios
   visuales.
3. Definir la jerarquía de componentes y su relación con el diseño VB6.
4. Implementar la interfaz mediante el diseñador de Windows Forms.

Después de la implementación:

5. Ejecutar una crítica visual y de UX con `impeccable` cuando se modifique una
   interfaz existente.
6. Ejecutar una pasada de pulido con `impeccable` usando, según corresponda,
   `critique`, `layout`, `clarify` o `polish`.
7. Corregir los problemas detectados y volver a comprobar el resultado en el
   diseñador y en ejecución.

En una migración, este flujo debe respetar siempre el diseño y el
comportamiento verificables del formulario VB6 original. No se debe reemplazar
la interfaz original por un diseño inventado sin autorización explícita.

## Reglas de migración VB6

Antes de migrar un módulo, formulario o clase:

1. Analizar el archivo VB6 original.
2. Documentar sus responsabilidades.
3. Identificar variables globales, tipos, enums, clases, APIs y dependencias.
4. Implementar la equivalencia .NET.
5. Comparar el comportamiento con el original.
6. Indicar el porcentaje estimado de compatibilidad.

Nunca asumir comportamientos ambiguos. Si una rutina VB6 no está clara:

- marcarla como pendiente;
- documentar la duda y el código original;
- no inventar lógica.

## Compatibilidad

Prioridades:

1. Compatibilidad funcional.
2. Compatibilidad de datos y formatos.
3. Compatibilidad visual.
4. Refactorización.

La refactorización nunca debe alterar el resultado observable.

## Formatos de archivo

Antes de implementar o modificar formatos, verificar tamaño binario, orden de
campos, tipos originales, endianess y valores por defecto.

Formatos relevantes del proyecto:

- VB6: `.vbp`, `.frm`, `.frx`, `.bas`, `.cls`.
- Recursos y configuración: `.resx`, `.ini`.
- Datos del editor: `.map`, `.dat`, `Inicio.con`, `.inf`.

No utilizar aproximaciones para formatos binarios.

## Revisión obligatoria

Antes de entregar cambios importantes:

- buscar código duplicado;
- buscar dependencias circulares;
- verificar compatibilidad con .NET 8;
- comprobar que no se rompe la migración;
- compilar el proyecto;
- actualizar la documentación correspondiente.

Para cambios importantes, realizar una revisión de código. Para cambios de
lectura de archivos, recursos externos o serialización, realizar una revisión de
seguridad.

## Completion Rules

Antes de finalizar un cambio:

- el código debe compilar;
- no debe haber errores nuevos;
- no debe haber duplicados evidentes;
- los archivos deben mantener un tamaño razonable;
- las responsabilidades deben estar separadas;
- la compatibilidad existente debe estar preservada;
- la documentación debe reflejar lo implementado y lo pendiente.

## Documentación y control de cambios

- Actualizar los `.md` relacionados cuando se complete una parte de la
  migración.
- Registrar limitaciones, decisiones y compatibilidad estimada.
- No cerrar una tarea como completa si aún falta verificar su comportamiento.
- Preservar los cambios existentes del usuario y no ejecutar operaciones
  destructivas sin autorización explícita.

## Prohibido

- Crear archivos gigantes sin justificación.
- Añadir funcionalidades que no existan en VB6 sin documentarlo y confirmarlo.
- Eliminar compatibilidad existente.
- Mover lógica de negocio al formulario principal.
- Mezclar UI y lógica de negocio.
- Sobrescribir cambios del usuario.
