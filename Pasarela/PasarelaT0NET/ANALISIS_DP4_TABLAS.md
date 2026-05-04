# Análisis tablas afectadas en paso CorporateModeler (DP4)

Este documento resume las tablas de Pasarela impactadas durante el bloque de lectura/carga desde CorporateModeler (conexión `conDP4`) en `msPasarProcedimientos`.

## Tablas indicadas (confirmación)

### Grafo + metadatos
- `DIAGRAMA`: **Sí**, se limpia y se vuelve a poblar desde información de DP4.
- `PROPIEDADES_DI`: **Sí**, se limpia y se inserta por trámite/diagrama.
- `CONECTORES_DI`: **Sí**, se limpia, se inserta y se actualiza con metadatos de conector.

### Acciones y parametrización
- `ACCIONES_DI`: **Sí**, se limpia y se inserta en varias fases de generación/corrección.
- `CONECTOR_ACC`: **Sí**, se limpia y se inserta/actualiza en lectura de conectores entre acciones.
- `PARAM_ACC`: **Sí**, se limpia y se inserta/actualiza al parametrizar acciones.
- `VARIABLES_PA`: **Sí**, se inserta/actualiza como diccionario de variables no existentes o con descripción ajustada.
- `VAR_ALTA_PA`: **Sí**, se limpia por procedimiento y se inserta para variables de alta.
- `VALIDACIONES`: **Sí**, se limpia por procedimiento y se inserta con mensajes de validación.

## Otras tablas importantes (no T0 Infraestructura)

- `ERRORES` y `ERRORES_HISTORICO`: se migran errores previos al histórico y se registran nuevos durante el proceso.
- `PROCESOS_PENDIENTES`: se actualiza el estado de ejecución (`P`/`S`) y fechas.
- `WFFLOWS` y `WFFLOWACTIONS`: se limpian y regeneran cuando no hay errores tras el bloque DP4.

> Nota: se excluyen de este listado las tablas explícitamente de infraestructura T0 (`TBPFIN*`, `TBDCELTA_PA`, etc.).
