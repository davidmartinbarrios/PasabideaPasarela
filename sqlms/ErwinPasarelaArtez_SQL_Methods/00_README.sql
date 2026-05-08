/*

Orden lógico del método GenerarDesdeDiId:
  01_DeleteByProcedimiento.sql
  10_LeerDiagramas.sql
  20_InsertDiagrama.sql               -- plantilla por fila
  11_LeerConectoresDi.sql
  21_InsertConectorDi.sql              -- plantilla por fila
  12_LeerPropiedadesDi.sql
  22_InsertPropiedadDi.sql             -- plantilla por fila
  13_LeerAccionesDi.sql
  23_InsertAccionDi.sql                -- plantilla por fila
  14_LeerConectoresAcc.sql
  24_InsertConectorAcc.sql             -- plantilla por fila
  15_LeerParamAcc.sql
  25_InsertParamAcc.sql                -- plantilla por fila

Scripts de utilidad:
  02_ObtenerPropiedadObjetoDiagrama.sql
  03_ObtenerCodigoProcedimiento.sql

Valores de ejemplo incluidos:
  @P_MODEL_NAME    = N'ARTEZELI'
  @P_DI_ID         = 3750
  @P_PROCEDIMIENTO = N'TA999900'

Notas:
  - Los Leer*.sql se lanzan sobre la BD ERWIN/Corporate Modeler, normalmente erwin_evolve o Corporate_Modeler.
  - Los Insert*.sql y Delete*.sql se lanzan sobre PASARELA_ARTEZ/PASARELA.
  - Si las BDs están en servidores distintos, no mezcles INSERT...SELECT cross-db sin linked server.
  - Los Insert*.sql son plantillas para probar una fila. En la aplicación se alimentan desde cada DataRow.
*/
