using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Lantik.Pasarela.sqlRepository
{
    /// <summary>
    /// Transformador inicial ERWIN/Evolve -> PASARELA_ARTEZ.
    ///
    /// Objetivo:
    ///   - Partir de un DI_ID de Erwin/Evolve.
    ///   - Leer la estructura jerárquica real del procedimiento.
    ///   - Generar registros intermedios de Pasarela:
    ///       DIAGRAMA
    ///       CONECTORES_DI
    ///       PROPIEDADES_DI
    ///       ACCIONES_DI
    ///       CONECTOR_ACC
    ///       PARAM_ACC
    ///
    /// Regla importante de PasarelaT0:
    ///   - JOINER sirve para conectores/saltos de flujo.
    ///   - DIAGRAM.ANO_ID sirve para bajar jerarquía/explotar diagramas.
    ///
    /// Por tanto:
    ///   - ORDEN_N1..ORDEN_N5 se calculan bajando por diagramas hijos.
    ///   - Las acciones internas del trámite van a ACCIONES_DI con ORDEN_ACC.
    ///   - Los subtrámites/ramificaciones internas van a DIAGRAMA con ORDEN_N2..N5.
    /// </summary>
    public class ErwinPasarelaArtezTransformer
    {
        private readonly DBContext DBErwin;
        private readonly DBContext DBPasarela;

        public ErwinPasarelaArtezTransformer()
        {
            DBErwin = new DBContext(Settings.BD_DP4);
            DBPasarela = new DBContext(Settings.BD_PASARELA);
        }

        public static string ObtenerPropiedadObjetoDiagrama(
            string connectionString,
            string modelName,
            int diId,
            string tipoObjeto,
            string scriptNamePropiedad)
        {
            const string sql = @"
SELECT TOP (1)
    LTRIM(RTRIM(
        COALESCE(
            NULLIF(P.N.value('(value/text())[1]', 'nvarchar(4000)'), N''),
            NULLIF(P.N.value('(text())[1]', 'nvarchar(4000)'), N''),
            NULLIF(P.N.value('(*/@value)[1]', 'nvarchar(4000)'), N''),
            NULLIF(P.N.value('(*/@id)[1]', 'nvarchar(4000)'), N''),
            NULLIF(P.N.value('(*/@name)[1]', 'nvarchar(4000)'), N'')
        )
    )) AS VALOR
FROM dbo.SHAPE S
JOIN dbo.CW_OBJECT_TYPE OT
    ON OT.MODEL_NAME = S.MODEL_NAME
   AND OT.OT_ID = S.ANO_TABNR
JOIN dbo.CW_OBJECT CO
    ON CO.MODEL_NAME = S.MODEL_NAME
   AND CO.OT_ID = S.ANO_TABNR
   AND CO.GO_ID = S.ANO_ID
CROSS APPLY
(
    SELECT TRY_CAST(CONVERT(nvarchar(max), CO.USERDEFINED) AS xml) AS XML_DATA
) X
CROSS APPLY X.XML_DATA.nodes('//*[local-name()=""property""]') P(N)
WHERE S.MODEL_NAME = @MODEL_NAME
  AND S.DI_ID = @DI_ID
  AND OT.OT_NAME = @TIPO_OBJETO
  AND P.N.value('@scriptname', 'nvarchar(500)') COLLATE DATABASE_DEFAULT
      = @SCRIPTNAME_PROPIEDAD COLLATE DATABASE_DEFAULT
ORDER BY S.SH_SEQ;";

            using (var cn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(sql, cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 60;

                cmd.Parameters.Add("@MODEL_NAME", SqlDbType.NVarChar, 100).Value = modelName;
                cmd.Parameters.Add("@DI_ID", SqlDbType.Int).Value = diId;
                cmd.Parameters.Add("@TIPO_OBJETO", SqlDbType.NVarChar, 200).Value = tipoObjeto;
                cmd.Parameters.Add("@SCRIPTNAME_PROPIEDAD", SqlDbType.NVarChar, 500).Value = scriptNamePropiedad;

                cn.Open();

                object result = cmd.ExecuteScalar();

                return result == null || result == DBNull.Value
                    ? null
                    : Convert.ToString(result).Trim();
            }
        }

        public static string ObtenerCodigoProcedimiento(
            string connectionString,
            string modelName,
            int diId)
        {
            return ObtenerPropiedadObjetoDiagrama(
                connectionString,
                modelName,
                diId,
                "Procedimiento",
                "CÓDIGOPROCEDIMIENTO"
            );
        }

        public ResponseBase<int> GenerarDesdeDiId(int diId, string procedimiento, string modelName = "ARTEZELI")
        {
            var response = new ResponseBase<int>();

            try
            {
                DeleteByProcedimiento(procedimiento);

                var diagramas = LeerDiagramas(diId, procedimiento, modelName);
                foreach (DataRow dr in diagramas.Rows)
                    InsertDiagrama(dr);

                var conectoresDi = LeerConectoresDi(procedimiento, modelName);
                foreach (DataRow dr in conectoresDi.Rows)
                    InsertConectorDi(dr);

                var propiedades = LeerPropiedadesDi(procedimiento, modelName);
                foreach (DataRow dr in propiedades.Rows)
                    InsertPropiedadDi(dr);

                var acciones = LeerAccionesDi(procedimiento, modelName);
                foreach (DataRow dr in acciones.Rows)
                    InsertAccionDi(dr);

                var conectoresAcc = LeerConectoresAcc(procedimiento, modelName);
                foreach (DataRow dr in conectoresAcc.Rows)
                    InsertConectorAcc(dr);

                var parametros = LeerParamAcc(procedimiento, modelName);
                foreach (DataRow dr in parametros.Rows)
                    InsertParamAcc(dr);

                response.Data = 1;
            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                response.Data = int.MinValue;
                throw;
            }
            finally
            {
                DBErwin.CloseConnection();
                DBPasarela.CloseConnection();
            }

            return response;
        }

        private void DeleteByProcedimiento(string procedimiento)
        {
            ExecutePasarela("DELETE FROM PARAM_ACC WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.AddApply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM CONECTOR_ACC WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.AddApply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM ACCIONES_DI WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.AddApply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM PROPIEDADES_DI WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.AddApply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM CONECTORES_DI WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.AddApply("@PROCEDIMIENTO", procedimiento));
            ExecutePasarela("DELETE FROM DIAGRAMA WHERE PROCEDIMIENTO = @PROCEDIMIENTO", p => p.AddApply("@PROCEDIMIENTO", procedimiento));
        }

        /// <summary>
        /// Genera DIAGRAMA respetando la jerarquía de PasarelaT0:
        ///   - Nivel 1: shapes de tipo proceso/trámite dentro del diagrama raíz.
        ///   - Nivel 2..5: shapes de tipo trámite/ramificación dentro del diagrama hijo del padre.
        ///
        /// No usa JOINER para bajar niveles; JOINER solo representa conexión de flujo.
        /// </summary>
        private DataTable LeerDiagramas(int diId, string procedimiento, string modelName)
        {
            string sql = @"
DECLARE @ModelName sysname = @P_MODEL_NAME;
DECLARE @DiID int = @P_DI_ID;
DECLARE @Procedimiento nvarchar(255) = @P_PROCEDIMIENTO;
DECLARE @DiAnoID int;

SELECT @DiAnoID = D.ANO_ID
FROM dbo.DIAGRAM D
WHERE D.MODEL_NAME = @ModelName
  AND D.DI_ID = @DiID;

IF @DiAnoID IS NULL
BEGIN
    RAISERROR('No existe DIAGRAM para MODEL_NAME=%s y DI_ID=%d', 16, 1, @ModelName, @DiID);
    RETURN;
END;

;WITH TipoProceso AS
(
    SELECT
        P.PR_ID,
        P.PR_NAME,
        P.PR_ALT_NAME,
        P.PR_TYPE,
        P.USERDEFINED,
        LU.LU_NAME AS TIPO_PROCESO,
        CASE
            WHEN LU.LU_NAME IN
            (
                N'Trámite de Inicio',
                N'Trámite',
                N'Trámite automático',
                N'Trámite general',
                N'Trámite General Automático',
                N'Ramificación',
                N'Alta Expediente',
                N'Inicio Automático Expediente',
                N'Bloque de Tramitación Común'
            )
            THEN 1
            ELSE 0
        END AS ES_DIAGRAMA_PASARELA
    FROM dbo.PROCESS P
    LEFT JOIN dbo.CW_PROP_TYPE PT
      ON PT.MODEL_NAME = P.MODEL_NAME
     AND PT.PPT_FIELD_NAME = 'PR_TYPE'
    LEFT JOIN dbo.CW_LOOKUP LU
      ON LU.MODEL_NAME = P.MODEL_NAME
     AND LU.PPT_ID = PT.PPT_ID
     AND LU.LU_ID = P.PR_TYPE
    WHERE P.MODEL_NAME = @ModelName
),
Nivel1 AS
(
    SELECT
        S.DI_ID,
        S.ANO_ID AS ID_DIAGRAMA,
        @DiAnoID AS ID_PADRE,
        S.SH_SEQ AS NUM_SEQ,
        S.SH_Y,
        S.SH_X,
        TP.PR_NAME,
        TP.PR_ALT_NAME,
        TP.PR_TYPE,
        TP.TIPO_PROCESO,
        TP.USERDEFINED,
        ORDEN_N1 = ROW_NUMBER() OVER (ORDER BY S.SH_Y DESC, S.SH_X ASC, S.SH_SEQ),
        ORDEN_N2 = CAST(0 AS int),
        ORDEN_N3 = CAST(0 AS int),
        ORDEN_N4 = CAST(0 AS int),
        ORDEN_N5 = CAST(0 AS int),
        NIVEL = 1,
        ARBOL = CAST(RIGHT('000000' + CAST(S.SH_SEQ AS varchar(6)), 6) AS varchar(1024))
    FROM dbo.SHAPE S
    JOIN TipoProceso TP
      ON TP.PR_ID = S.ANO_ID
    WHERE S.MODEL_NAME = @ModelName
      AND S.DI_ID = @DiID
      AND S.ANO_TABNR = 8953
      AND TP.ES_DIAGRAMA_PASARELA = 1
),
Nivel2 AS
(
    SELECT
        S2.DI_ID,
        S2.ANO_ID AS ID_DIAGRAMA,
        N1.ID_DIAGRAMA AS ID_PADRE,
        S2.SH_SEQ AS NUM_SEQ,
        S2.SH_Y,
        S2.SH_X,
        TP.PR_NAME,
        TP.PR_ALT_NAME,
        TP.PR_TYPE,
        TP.TIPO_PROCESO,
        TP.USERDEFINED,
        N1.ORDEN_N1,
        ORDEN_N2 = ROW_NUMBER() OVER (PARTITION BY N1.ORDEN_N1 ORDER BY S2.SH_Y DESC, S2.SH_X ASC, S2.SH_SEQ),
        ORDEN_N3 = CAST(0 AS int),
        ORDEN_N4 = CAST(0 AS int),
        ORDEN_N5 = CAST(0 AS int),
        NIVEL = 2,
        ARBOL = CAST(N1.ARBOL + '.' + RIGHT('000000' + CAST(S2.SH_SEQ AS varchar(6)), 6) AS varchar(1024))
    FROM Nivel1 N1
    JOIN dbo.DIAGRAM DH
      ON DH.MODEL_NAME = @ModelName
     AND DH.ANO_ID = N1.ID_DIAGRAMA
     AND DH.DI_TYPE <> 1
    JOIN dbo.SHAPE S2
      ON S2.MODEL_NAME = @ModelName
     AND S2.DI_ID = DH.DI_ID
     AND S2.ANO_TABNR = 8953
    JOIN TipoProceso TP
      ON TP.PR_ID = S2.ANO_ID
     AND TP.ES_DIAGRAMA_PASARELA = 1
),
Nivel3 AS
(
    SELECT
        S3.DI_ID,
        S3.ANO_ID AS ID_DIAGRAMA,
        N2.ID_DIAGRAMA AS ID_PADRE,
        S3.SH_SEQ AS NUM_SEQ,
        S3.SH_Y,
        S3.SH_X,
        TP.PR_NAME,
        TP.PR_ALT_NAME,
        TP.PR_TYPE,
        TP.TIPO_PROCESO,
        N2.USERDEFINED,
        N2.ORDEN_N1,
        N2.ORDEN_N2,
        ORDEN_N3 = ROW_NUMBER() OVER (PARTITION BY N2.ORDEN_N1, N2.ORDEN_N2 ORDER BY S3.SH_Y DESC, S3.SH_X ASC, S3.SH_SEQ),
        ORDEN_N4 = CAST(0 AS int),
        ORDEN_N5 = CAST(0 AS int),
        NIVEL = 3,
        ARBOL = CAST(N2.ARBOL + '.' + RIGHT('000000' + CAST(S3.SH_SEQ AS varchar(6)), 6) AS varchar(1024))
    FROM Nivel2 N2
    JOIN dbo.DIAGRAM DH
      ON DH.MODEL_NAME = @ModelName
     AND DH.ANO_ID = N2.ID_DIAGRAMA
     AND DH.DI_TYPE <> 1
    JOIN dbo.SHAPE S3
      ON S3.MODEL_NAME = @ModelName
     AND S3.DI_ID = DH.DI_ID
     AND S3.ANO_TABNR = 8953
    JOIN TipoProceso TP
      ON TP.PR_ID = S3.ANO_ID
     AND TP.ES_DIAGRAMA_PASARELA = 1
),
Nivel4 AS
(
    SELECT
        S4.DI_ID,
        S4.ANO_ID AS ID_DIAGRAMA,
        N3.ID_DIAGRAMA AS ID_PADRE,
        S4.SH_SEQ AS NUM_SEQ,
        S4.SH_Y,
        S4.SH_X,
        TP.PR_NAME,
        TP.PR_ALT_NAME,
        TP.PR_TYPE,
        TP.TIPO_PROCESO,
        TP.USERDEFINED,
        N3.ORDEN_N1,
        N3.ORDEN_N2,
        N3.ORDEN_N3,
        ORDEN_N4 = ROW_NUMBER() OVER (PARTITION BY N3.ORDEN_N1, N3.ORDEN_N2, N3.ORDEN_N3 ORDER BY S4.SH_Y DESC, S4.SH_X ASC, S4.SH_SEQ),
        ORDEN_N5 = CAST(0 AS int),
        NIVEL = 4,
        ARBOL = CAST(N3.ARBOL + '.' + RIGHT('000000' + CAST(S4.SH_SEQ AS varchar(6)), 6) AS varchar(1024))
    FROM Nivel3 N3
    JOIN dbo.DIAGRAM DH
      ON DH.MODEL_NAME = @ModelName
     AND DH.ANO_ID = N3.ID_DIAGRAMA
     AND DH.DI_TYPE <> 1
    JOIN dbo.SHAPE S4
      ON S4.MODEL_NAME = @ModelName
     AND S4.DI_ID = DH.DI_ID
     AND S4.ANO_TABNR = 8953
    JOIN TipoProceso TP
      ON TP.PR_ID = S4.ANO_ID
     AND TP.ES_DIAGRAMA_PASARELA = 1
),
Nivel5 AS
(
    SELECT
        S5.DI_ID,
        S5.ANO_ID AS ID_DIAGRAMA,
        N4.ID_DIAGRAMA AS ID_PADRE,
        S5.SH_SEQ AS NUM_SEQ,
        S5.SH_Y,
        S5.SH_X,
        TP.PR_NAME,
        TP.PR_ALT_NAME,
        TP.PR_TYPE,
        TP.TIPO_PROCESO,
        TP.USERDEFINED,
        N4.ORDEN_N1,
        N4.ORDEN_N2,
        N4.ORDEN_N3,
        N4.ORDEN_N4,
        ORDEN_N5 = ROW_NUMBER() OVER (PARTITION BY N4.ORDEN_N1, N4.ORDEN_N2, N4.ORDEN_N3, N4.ORDEN_N4 ORDER BY S5.SH_Y DESC, S5.SH_X ASC, S5.SH_SEQ),
        NIVEL = 5,
        ARBOL = CAST(N4.ARBOL + '.' + RIGHT('000000' + CAST(S5.SH_SEQ AS varchar(6)), 6) AS varchar(1024))
    FROM Nivel4 N4
    JOIN dbo.DIAGRAM DH
      ON DH.MODEL_NAME = @ModelName
     AND DH.ANO_ID = N4.ID_DIAGRAMA
     AND DH.DI_TYPE <> 1
    JOIN dbo.SHAPE S5
      ON S5.MODEL_NAME = @ModelName
     AND S5.DI_ID = DH.DI_ID
     AND S5.ANO_TABNR = 8953
    JOIN TipoProceso TP
      ON TP.PR_ID = S5.ANO_ID
     AND TP.ES_DIAGRAMA_PASARELA = 1
),
Arbol AS
(
    SELECT * FROM Nivel1
    UNION ALL SELECT * FROM Nivel2
    UNION ALL SELECT * FROM Nivel3
    UNION ALL SELECT * FROM Nivel4
    UNION ALL SELECT * FROM Nivel5
),
ConXml AS
(
    SELECT
        A.*,
        TRY_CAST(CONVERT(nvarchar(max), A.USERDEFINED) AS xml) AS UDxml
    FROM Arbol A
),
Flags AS
(
    SELECT
        C.*,
        CASE WHEN C.UDxml IS NULL THEN NULL ELSE C.UDxml.value('(/UD/PlazoTipo1/text())[1]', 'varchar(20)') END AS PLAZOTIPO1,
        CASE WHEN C.UDxml IS NULL THEN NULL ELSE C.UDxml.value('(/UD/PlazoTipo2/text())[1]', 'varchar(20)') END AS PLAZOTIPO2,
        CASE WHEN C.UDxml IS NULL THEN NULL ELSE C.UDxml.value('(/UD/NivTramit/text())[1]',  'varchar(10)') END AS NIV_TRAMIT,
        CASE WHEN C.UDxml IS NULL THEN NULL ELSE C.UDxml.value('(/UD/BloqueoExp/text())[1]', 'varchar(5)') END AS BLOQUEO_EXP,
        CASE WHEN C.UDxml IS NULL THEN NULL ELSE C.UDxml.value('(/UD/UnionRamas/text())[1]', 'varchar(5)') END AS UNION_RAMAS,
        CASE WHEN C.UDxml IS NULL THEN NULL ELSE C.UDxml.value('(/UD/TramitSimul/text())[1]','varchar(5)') END AS TRAMIT_SIMUL,
        CASE WHEN C.UDxml IS NULL THEN NULL ELSE C.UDxml.value('(/UD/TramOculto/text())[1]', 'varchar(5)') END AS TRAM_OCULTO,
        CASE WHEN C.UDxml IS NULL THEN NULL ELSE C.UDxml.value('(/UD/IndValorVar/text())[1]','varchar(5)') END AS IND_VALORVAR,
        CASE WHEN C.UDxml IS NULL THEN NULL ELSE C.UDxml.value('(/UD/VueltaAtras/text())[1]','varchar(5)') END AS VUELTA_ATRAS
    FROM ConXml C
)
SELECT
    @Procedimiento AS PROCEDIMIENTO,
    ORDEN_N1,
    ORDEN_N2,
    ORDEN_N3,
    ORDEN_N4,
    ORDEN_N5,
    ID_DIAGRAMA,
    ID_PADRE,
    NUM_SEQ,
    DI_ID,
    TIPO_PROCESO AS CAT_DIAGRAMA,
    PR_NAME AS NOMBRE,
    CONVERT(varchar(max), USERDEFINED) AS USERDEFINED,
    NIVEL,
    ARBOL,
    PLAZOTIPO1,
    PLAZOTIPO2,
    NIV_TRAMIT,
    BLOQUEO_EXP,
    UNION_RAMAS,
    TRAMIT_SIMUL,
    TRAM_OCULTO,
    IND_VALORVAR,
    VUELTA_ATRAS,
    PR_NAME AS NOMBRE_TRAM
FROM Flags
ORDER BY ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_DI_ID", diId);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        /// <summary>
        /// Lee conectores del flujo principal y de los diagramas ya cargados en DIAGRAMA.
        /// El orden se obtiene de PASARELA.DIAGRAMA para no recalcularlo.
        /// </summary>
        private DataTable LeerConectoresDi(string procedimiento, string modelName)
        {
            string sql = @"
SELECT
    D.PROCEDIMIENTO,
    J.ANO_ID AS ID_CONECTOR,
    J.DI_ID AS DIAGRAMA,
    J.JO_SEQ AS NUM,
    J.SH_SEQ_FROM AS NUM_SEC_DESDE,
    J.SH_SEQ_TO AS NUM_SEC_HASTA,
    ISNULL(LU.LU_NAME, 'Normal') AS CAT_CONECTOR,
    J.DI_ID AS DI_ID,
    D.ORDEN_N1,
    D.ORDEN_N2,
    D.ORDEN_N3,
    D.ORDEN_N4,
    C.CO_TYPE AS TIPO_CONECTOR,
    'N' AS SALIDA
FROM dbo.JOINER J
JOIN dbo.SHAPE SF
  ON SF.MODEL_NAME = J.MODEL_NAME
 AND SF.DI_ID = J.DI_ID
 AND SF.SH_SEQ = J.SH_SEQ_FROM
LEFT JOIN dbo.CONNECTOR C
  ON C.MODEL_NAME = J.MODEL_NAME
 AND C.CO_ID = J.ANO_ID
LEFT JOIN dbo.CW_LOOKUP LU
  ON LU.MODEL_NAME = J.MODEL_NAME
 AND LU.LU_ID = C.CO_TYPE
JOIN DIAGRAMA D
  ON D.PROCEDIMIENTO = @P_PROCEDIMIENTO
 AND D.DI_ID = J.DI_ID
 AND D.NUM_SEQ = J.SH_SEQ_FROM
WHERE J.MODEL_NAME = @P_MODEL_NAME
ORDER BY D.ORDEN_N1, D.ORDEN_N2, D.ORDEN_N3, D.ORDEN_N4, D.ORDEN_N5, J.JO_SEQ;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        /// <summary>
        /// PROPIEDADES_DI debe usar exactamente los mismos ORDEN_N1..N5 que DIAGRAMA.
        /// No recalcula ROW_NUMBER().
        /// </summary>
        private DataTable LeerPropiedadesDi(string procedimiento, string modelName)
        {
            string sql = @"
;WITH Base AS
(
    SELECT
        D.PROCEDIMIENTO,
        D.ORDEN_N1,
        D.ORDEN_N2,
        D.ORDEN_N3,
        D.ORDEN_N4,
        D.ORDEN_N5,
        D.ID_DIAGRAMA,
        D.NOMBRE AS NOM_DIAGRAMA,
        D.CAT_DIAGRAMA AS TIPO_DIAGRAMA,
        TRY_CAST(CONVERT(nvarchar(max), P.USERDEFINED) AS xml) AS UDxml
    FROM " + PasarelaSchemaPrefix() + @"DIAGRAMA D
    JOIN dbo.PROCESS P
      ON P.MODEL_NAME = @P_MODEL_NAME
     AND P.PR_ID = D.ID_DIAGRAMA
    WHERE D.PROCEDIMIENTO = @P_PROCEDIMIENTO
)
SELECT
    PROCEDIMIENTO,
    ORDEN_N1,
    ORDEN_N2,
    ORDEN_N3,
    ORDEN_N4,
    ORDEN_N5,
    ID_DIAGRAMA,
    NOM_DIAGRAMA,
    TIPO_DIAGRAMA,
    CASE WHEN UDxml IS NULL THEN NULL ELSE UDxml.value('(/UD/PlazoTipo1/text())[1]', 'varchar(250)') END AS PLAZTIP1_DI,
    CASE WHEN UDxml IS NULL THEN NULL ELSE UDxml.value('(/UD/PlazoTipo2/text())[1]', 'varchar(250)') END AS PLAZTIP2_DI,
    CASE WHEN UDxml IS NULL THEN NULL ELSE UDxml.value('(/UD/NivTramit/text())[1]',  'varchar(250)') END AS NIVELTRAM_DI,
    CASE WHEN UDxml IS NULL THEN NULL ELSE UDxml.value('(/UD/BloqueoExp/text())[1]', 'varchar(250)') END AS INDBLOQ_DI,
    CASE WHEN UDxml IS NULL THEN NULL ELSE UDxml.value('(/UD/UnionRamas/text())[1]', 'varchar(250)') END AS INDRAM_DI,
    'N' AS INDPERSINT
FROM Base
ORDER BY ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        /// <summary>
        /// Lee acciones funcionales dentro del diagrama hijo de cada trámite.
        /// Excluye trámites/ramificaciones/bloques, que ya van a DIAGRAMA.
        /// </summary>
        private DataTable LeerAccionesDi(string procedimiento, string modelName)
        {
            string sql = @"
;WITH Tipos AS
(
    SELECT
        P.PR_ID,
        P.PR_NAME,
        P.PR_ALT_NAME,
        P.PR_TYPE,
        LU.LU_NAME AS TIPO_PROCESO,
        CASE
            WHEN LU.LU_NAME IN
            (
                N'Trámite de Inicio',
                N'Trámite',
                N'Trámite automático',
                N'Trámite general',
                N'Trámite General Automático',
                N'Ramificación',
                N'Alta Expediente',
                N'Inicio Automático Expediente',
                N'Bloque de Tramitación Común'
            )
            THEN 1
            ELSE 0
        END AS ES_DIAGRAMA_PASARELA
    FROM dbo.PROCESS P
    LEFT JOIN dbo.CW_PROP_TYPE PT
      ON PT.MODEL_NAME = P.MODEL_NAME
     AND PT.PPT_FIELD_NAME = 'PR_TYPE'
    LEFT JOIN dbo.CW_LOOKUP LU
      ON LU.MODEL_NAME = P.MODEL_NAME
     AND LU.PPT_ID = PT.PPT_ID
     AND LU.LU_ID = P.PR_TYPE
    WHERE P.MODEL_NAME = @P_MODEL_NAME
),
Acciones AS
(
    SELECT
        D.PROCEDIMIENTO,
        D.ORDEN_N1,
        D.ORDEN_N2,
        D.ORDEN_N3,
        D.ORDEN_N4,
        D.ORDEN_N5,
        ORDEN_ACC = ROW_NUMBER() OVER
        (
            PARTITION BY D.PROCEDIMIENTO, D.ORDEN_N1, D.ORDEN_N2, D.ORDEN_N3, D.ORDEN_N4, D.ORDEN_N5
            ORDER BY SA.SH_Y DESC, SA.SH_X ASC, SA.SH_SEQ
        ),
        PA.PR_ID AS ID_ACCION,
        PA.PR_NAME AS NOM_ACCION,
        PA.PR_ID AS NUM_ACCION,
        'T' AS TIPO_ACCION,
        ISNULL(NULLIF(LTRIM(RTRIM(PA.PR_ALT_NAME)), ''), PA.PR_NAME) AS PATH_HIDRA,
        SA.SH_SEQ AS NUM_SEQ,
        SA.DI_ID
    FROM " + PasarelaSchemaPrefix() + @"DIAGRAMA D
    JOIN dbo.DIAGRAM DH
      ON DH.MODEL_NAME = @P_MODEL_NAME
     AND DH.ANO_ID = D.ID_DIAGRAMA
     AND DH.DI_TYPE <> 1
    JOIN dbo.SHAPE SA
      ON SA.MODEL_NAME = @P_MODEL_NAME
     AND SA.DI_ID = DH.DI_ID
     AND SA.ANO_TABNR = 8953
    JOIN dbo.PROCESS PA
      ON PA.MODEL_NAME = @P_MODEL_NAME
     AND PA.PR_ID = SA.ANO_ID
    JOIN Tipos T
      ON T.PR_ID = PA.PR_ID
    WHERE D.PROCEDIMIENTO = @P_PROCEDIMIENTO
      AND ISNULL(T.ES_DIAGRAMA_PASARELA, 0) = 0
)
SELECT
    PROCEDIMIENTO,
    ORDEN_N1,
    ORDEN_N2,
    ORDEN_N3,
    ORDEN_N4,
    ORDEN_N5,
    ORDEN_ACC,
    ID_ACCION,
    NOM_ACCION,
    NUM_ACCION,
    TIPO_ACCION,
    PATH_HIDRA,
    NUM_SEQ,
    DI_ID
FROM Acciones
ORDER BY ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5, ORDEN_ACC;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        /// <summary>
        /// Conectores entre acciones internas de cada diagrama de trámite.
        /// Usa ACCIONES_DI ya insertada para localizar ORDEN_N1..N5.
        /// </summary>
        private DataTable LeerConectoresAcc(string procedimiento, string modelName)
        {
            string sql = @"
SELECT
    A.PROCEDIMIENTO,
    J.ANO_ID AS ID_CONECTOR,
    A.ID_ACCION AS ID_DIAGRAMA,
    J.JO_SEQ AS NUM_CONECTOR,
    J.SH_SEQ_FROM AS NUM_SEQ_DESDE,
    J.SH_SEQ_TO AS NUM_SEQ_HASTA,
    ISNULL(LU.LU_NAME, 'Normal') AS CAT_CONECTOR,
    'N' AS IND_SALIDA_TRAM,
    J.DI_ID
FROM " + PasarelaSchemaPrefix() + @"ACCIONES_DI A
JOIN dbo.JOINER J
  ON J.MODEL_NAME = @P_MODEL_NAME
 AND J.DI_ID = A.DI_ID
 AND J.SH_SEQ_FROM = A.NUM_SEQ
LEFT JOIN dbo.CONNECTOR C
  ON C.MODEL_NAME = J.MODEL_NAME
 AND C.CO_ID = J.ANO_ID
LEFT JOIN dbo.CW_LOOKUP LU
  ON LU.MODEL_NAME = J.MODEL_NAME
 AND LU.LU_ID = C.CO_TYPE
WHERE A.PROCEDIMIENTO = @P_PROCEDIMIENTO
ORDER BY A.ORDEN_N1, A.ORDEN_N2, A.ORDEN_N3, A.ORDEN_N4, A.ORDEN_N5, A.ORDEN_ACC, J.JO_SEQ;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        /// <summary>
        /// Parámetros de acciones. Usa ACCIONES_DI ya insertada para no recalcular órdenes.
        /// De momento mete los atributos en orden DM_SEQ como base.
        /// </summary>
        private DataTable LeerParamAcc(string procedimiento, string modelName)
        {
            string sql = @"
;WITH Params AS
(
    SELECT
        A.PROCEDIMIENTO,
        A.ORDEN_N1,
        A.ORDEN_N2,
        A.ORDEN_N3,
        A.ORDEN_N4,
        A.ORDEN_N5,
        A.ORDEN_ACC,
        A.ID_ACCION,
        ORDEN_PA = ROW_NUMBER() OVER
        (
            PARTITION BY A.PROCEDIMIENTO, A.ORDEN_N1, A.ORDEN_N2, A.ORDEN_N3, A.ORDEN_N4, A.ORDEN_N5, A.ORDEN_ACC
            ORDER BY DU.DM_SEQ
        ),
        PARAMETRO = '@' + AT.AT_NAME,
        VALOR = AT.AT_NAME
    FROM " + PasarelaSchemaPrefix() + @"ACCIONES_DI A
    JOIN dbo.CW_DATA_USAGE DU
      ON DU.MODEL_NAME = @P_MODEL_NAME
     AND DU.PR_ID = A.ID_ACCION
    JOIN dbo.ATTRIBUTE AT
      ON AT.MODEL_NAME = @P_MODEL_NAME
     AND AT.AT_ID = DU.AT_ID
    WHERE A.PROCEDIMIENTO = @P_PROCEDIMIENTO
)
SELECT
    PROCEDIMIENTO,
    ORDEN_N1,
    ORDEN_N2,
    ORDEN_N3,
    ORDEN_N4,
    ORDEN_N5,
    ORDEN_ACC,
    ID_ACCION,
    PARAMETRO,
    VALOR,
    ORDEN_PA
FROM Params
ORDER BY ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5, ORDEN_ACC, ORDEN_PA;";

            return QueryErwin(sql, p =>
            {
                p.Add("@P_MODEL_NAME", modelName);
                p.Add("@P_PROCEDIMIENTO", procedimiento);
                p.Apply();
            });
        }

        /// <summary>
        /// Ajusta aquí si las tablas intermedias están en otro esquema/base.
        /// Si DBErwin y DBPasarela apuntan a servidores/bases diferentes y no hay linked server,
        /// estas SELECT que leen PASARELA desde ERWIN no funcionarán.
        ///
        /// En ese caso, conviene cambiar el flujo:
        ///   - leer DIAGRAMA desde DBPasarela a DataTable;
        ///   - construir queries parametrizadas contra ERWIN con listas de IDs;
        ///   - o usar una tabla temporal/staging común.
        /// </summary>
        private static string PasarelaSchemaPrefix()
        {
            return "dbo.";
        }

        private void InsertDiagrama(DataRow dr)
        {
            string sql = @"
INSERT INTO DIAGRAMA
(
    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
    ID_DIAGRAMA, ID_PADRE, NUM_SEQ, DI_ID,
    CAT_DIAGRAMA, NOMBRE, USERDEFINED, NIVEL, ARBOL,
    PLAZOTIPO1, PLAZOTIPO2, NIV_TRAMIT, BLOQUEO_EXP, UNION_RAMAS,
    TRAMIT_SIMUL, TRAM_OCULTO, IND_VALORVAR, VUELTA_ATRAS, NOMBRE_TRAM
)
VALUES
(
    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
    @ID_DIAGRAMA, @ID_PADRE, @NUM_SEQ, @DI_ID,
    @CAT_DIAGRAMA, @NOMBRE, CONVERT(varchar(max), @USERDEFINED), @NIVEL, @ARBOL,
    @PLAZOTIPO1, @PLAZOTIPO2, @NIV_TRAMIT, @BLOQUEO_EXP, @UNION_RAMAS,
    @TRAMIT_SIMUL, @TRAM_OCULTO, @IND_VALORVAR, @VUELTA_ATRAS, @NOMBRE_TRAM
);";

            ExecutePasarela(sql, p =>
            {
                p.Set("@PROCEDIMIENTO", GetStringOrDbNull(dr, "PROCEDIMIENTO"));

                p.Set("@ORDEN_N1", GetIntOrDbNull(dr, "ORDEN_N1"));
                p.Set("@ORDEN_N2", GetIntOrDbNull(dr, "ORDEN_N2"));
                p.Set("@ORDEN_N3", GetIntOrDbNull(dr, "ORDEN_N3"));
                p.Set("@ORDEN_N4", GetIntOrDbNull(dr, "ORDEN_N4"));
                p.Set("@ORDEN_N5", GetIntOrDbNull(dr, "ORDEN_N5"));

                p.Set("@ID_DIAGRAMA", GetIntOrDbNull(dr, "ID_DIAGRAMA"));
                p.Set("@ID_PADRE", GetIntOrDbNull(dr, "ID_PADRE"));
                p.Set("@NUM_SEQ", GetIntOrDbNull(dr, "NUM_SEQ"));
                p.Set("@DI_ID", GetIntOrDbNull(dr, "DI_ID"));

                p.Set("@CAT_DIAGRAMA", GetStringOrDbNull(dr, "CAT_DIAGRAMA"));
                p.Set("@NOMBRE", GetStringOrDbNull(dr, "NOMBRE"));
                p.Set("@USERDEFINED", GetStringOrDbNull(dr, "USERDEFINED"));
                p.Set("@NIVEL", GetIntOrDbNull(dr, "NIVEL"));
                p.Set("@ARBOL", GetStringOrDbNull(dr, "ARBOL"));

                p.Set("@PLAZOTIPO1", GetStringOrDbNull(dr, "PLAZOTIPO1"));
                p.Set("@PLAZOTIPO2", GetStringOrDbNull(dr, "PLAZOTIPO2"));
                p.Set("@NIV_TRAMIT", GetStringOrDbNull(dr, "NIV_TRAMIT"));
                p.Set("@BLOQUEO_EXP", GetStringOrDbNull(dr, "BLOQUEO_EXP"));
                p.Set("@UNION_RAMAS", GetStringOrDbNull(dr, "UNION_RAMAS"));

                p.Set("@TRAMIT_SIMUL", GetStringOrDbNull(dr, "TRAMIT_SIMUL"));
                p.Set("@TRAM_OCULTO", GetStringOrDbNull(dr, "TRAM_OCULTO"));
                p.Set("@IND_VALORVAR", GetStringOrDbNull(dr, "IND_VALORVAR"));
                p.Set("@VUELTA_ATRAS", GetStringOrDbNull(dr, "VUELTA_ATRAS"));
                p.Set("@NOMBRE_TRAM", GetStringOrDbNull(dr, "NOMBRE_TRAM"));
                p.Apply();
            });
        }

        private void InsertConectorDi(DataRow dr)
        {
            string sql = @"
INSERT INTO CONECTORES_DI
(
    PROCEDIMIENTO, ID_CONECTOR, DIAGRAMA, NUM, NUM_SEC_DESDE, NUM_SEC_HASTA,
    CAT_CONECTOR, DI_ID, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, TIPO_CONECTOR, SALIDA
)
VALUES
(
    @PROCEDIMIENTO, @ID_CONECTOR, @DIAGRAMA, @NUM, @NUM_SEC_DESDE, @NUM_SEC_HASTA,
    @CAT_CONECTOR, @DI_ID, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @TIPO_CONECTOR, @SALIDA
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ID_CONECTOR", dr["ID_CONECTOR"]);
                Add(p, "@DIAGRAMA", dr["DIAGRAMA"]);
                Add(p, "@NUM", dr["NUM"]);
                Add(p, "@NUM_SEC_DESDE", dr["NUM_SEC_DESDE"]);
                Add(p, "@NUM_SEC_HASTA", dr["NUM_SEC_HASTA"]);
                Add(p, "@CAT_CONECTOR", dr["CAT_CONECTOR"]);
                Add(p, "@DI_ID", dr["DI_ID"]);
                Add(p, "@ORDEN_N1", dr["ORDEN_N1"]);
                Add(p, "@ORDEN_N2", dr["ORDEN_N2"]);
                Add(p, "@ORDEN_N3", dr["ORDEN_N3"]);
                Add(p, "@ORDEN_N4", dr["ORDEN_N4"]);
                Add(p, "@TIPO_CONECTOR", dr["TIPO_CONECTOR"]);
                Add(p, "@SALIDA", dr["SALIDA"]);
                p.Apply();
            });
        }

        private void InsertPropiedadDi(DataRow dr)
        {
            string sql = @"
INSERT INTO PROPIEDADES_DI
(
    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
    ID_DIAGRAMA, NOM_DIAGRAMA, TIPO_DIAGRAMA,
    PLAZTIP1_DI, PLAZTIP2_DI, NIVELTRAM_DI,
    INDBLOQ_DI, INDRAM_DI, INDPERSINT
)
VALUES
(
    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
    @ID_DIAGRAMA, @NOM_DIAGRAMA, @TIPO_DIAGRAMA,
    @PLAZTIP1_DI, @PLAZTIP2_DI, @NIVELTRAM_DI,
    @INDBLOQ_DI, @INDRAM_DI, @INDPERSINT
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ORDEN_N1", dr["ORDEN_N1"]);
                Add(p, "@ORDEN_N2", dr["ORDEN_N2"]);
                Add(p, "@ORDEN_N3", dr["ORDEN_N3"]);
                Add(p, "@ORDEN_N4", dr["ORDEN_N4"]);
                Add(p, "@ORDEN_N5", dr["ORDEN_N5"]);

                Add(p, "@ID_DIAGRAMA", dr["ID_DIAGRAMA"]);
                Add(p, "@NOM_DIAGRAMA", dr["NOM_DIAGRAMA"]);
                Add(p, "@TIPO_DIAGRAMA", dr["TIPO_DIAGRAMA"]);

                Add(p, "@PLAZTIP1_DI", dr["PLAZTIP1_DI"]);
                Add(p, "@PLAZTIP2_DI", dr["PLAZTIP2_DI"]);
                Add(p, "@NIVELTRAM_DI", dr["NIVELTRAM_DI"]);
                Add(p, "@INDBLOQ_DI", dr["INDBLOQ_DI"]);
                Add(p, "@INDRAM_DI", dr["INDRAM_DI"]);
                Add(p, "@INDPERSINT", dr["INDPERSINT"]);

                p.Apply();
            });
        }

        private void InsertAccionDi(DataRow dr)
        {
            string sql = @"
INSERT INTO ACCIONES_DI
(
    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
    ORDEN_ACC, ID_ACCION, NOM_ACCION, NUM_ACCION, TIPO_ACCION, PATH_HIDRA, NUM_SEQ, DI_ID
)
VALUES
(
    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
    @ORDEN_ACC, @ID_ACCION, @NOM_ACCION, @NUM_ACCION, @TIPO_ACCION, @PATH_HIDRA, @NUM_SEQ, @DI_ID
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ORDEN_N1", dr["ORDEN_N1"]);
                Add(p, "@ORDEN_N2", dr["ORDEN_N2"]);
                Add(p, "@ORDEN_N3", dr["ORDEN_N3"]);
                Add(p, "@ORDEN_N4", dr["ORDEN_N4"]);
                Add(p, "@ORDEN_N5", dr["ORDEN_N5"]);
                Add(p, "@ORDEN_ACC", dr["ORDEN_ACC"]);
                Add(p, "@ID_ACCION", dr["ID_ACCION"]);
                Add(p, "@NOM_ACCION", dr["NOM_ACCION"]);
                Add(p, "@NUM_ACCION", dr["NUM_ACCION"]);
                Add(p, "@TIPO_ACCION", dr["TIPO_ACCION"]);
                Add(p, "@PATH_HIDRA", dr["PATH_HIDRA"]);
                Add(p, "@NUM_SEQ", dr["NUM_SEQ"]);
                Add(p, "@DI_ID", dr["DI_ID"]);
                p.Apply();
            });
        }

        private void InsertConectorAcc(DataRow dr)
        {
            string sql = @"
INSERT INTO CONECTOR_ACC
(
    PROCEDIMIENTO, ID_CONECTOR, ID_DIAGRAMA, NUM_CONECTOR,
    NUM_SEQ_DESDE, NUM_SEQ_HASTA, CAT_CONECTOR, IND_SALIDA_TRAM, DI_ID
)
VALUES
(
    @PROCEDIMIENTO, @ID_CONECTOR, @ID_DIAGRAMA, @NUM_CONECTOR,
    @NUM_SEQ_DESDE, @NUM_SEQ_HASTA, @CAT_CONECTOR, @IND_SALIDA_TRAM, @DI_ID
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ID_CONECTOR", dr["ID_CONECTOR"]);
                Add(p, "@ID_DIAGRAMA", dr["ID_DIAGRAMA"]);
                Add(p, "@NUM_CONECTOR", dr["NUM_CONECTOR"]);
                Add(p, "@NUM_SEQ_DESDE", dr["NUM_SEQ_DESDE"]);
                Add(p, "@NUM_SEQ_HASTA", dr["NUM_SEQ_HASTA"]);
                Add(p, "@CAT_CONECTOR", dr["CAT_CONECTOR"]);
                Add(p, "@IND_SALIDA_TRAM", dr["IND_SALIDA_TRAM"]);
                Add(p, "@DI_ID", dr["DI_ID"]);
                p.Apply();
            });
        }

        private void InsertParamAcc(DataRow dr)
        {
            string sql = @"
INSERT INTO PARAM_ACC
(
    PROCEDIMIENTO, ORDEN_N1, ORDEN_N2, ORDEN_N3, ORDEN_N4, ORDEN_N5,
    ORDEN_ACC, ID_ACCION, PARAMETRO, VALOR, ORDEN_PA
)
VALUES
(
    @PROCEDIMIENTO, @ORDEN_N1, @ORDEN_N2, @ORDEN_N3, @ORDEN_N4, @ORDEN_N5,
    @ORDEN_ACC, @ID_ACCION, @PARAMETRO, @VALOR, @ORDEN_PA
);";

            ExecutePasarela(sql, p =>
            {
                Add(p, "@PROCEDIMIENTO", dr["PROCEDIMIENTO"]);
                Add(p, "@ORDEN_N1", dr["ORDEN_N1"]);
                Add(p, "@ORDEN_N2", dr["ORDEN_N2"]);
                Add(p, "@ORDEN_N3", dr["ORDEN_N3"]);
                Add(p, "@ORDEN_N4", dr["ORDEN_N4"]);
                Add(p, "@ORDEN_N5", dr["ORDEN_N5"]);
                Add(p, "@ORDEN_ACC", dr["ORDEN_ACC"]);
                Add(p, "@ID_ACCION", dr["ID_ACCION"]);
                Add(p, "@PARAMETRO", dr["PARAMETRO"]);
                Add(p, "@VALOR", dr["VALOR"]);
                Add(p, "@ORDEN_PA", dr["ORDEN_PA"]);
                p.Apply();
            });
        }

        private DataTable QueryErwin(string sql, Action<ParameterBag> configureParameters)
        {
            DBErwin.ClearParameters();
            DBErwin.Type = CommandType.Text;
            DBErwin.SqlStatment = sql;

            var bag = new ParameterBag(DBErwin);
            configureParameters?.Invoke(bag);

            Logger.Info("Ejecutamos ERWIN query: " + sql);
            return DBErwin.GetDataTable();
        }

        private void ExecutePasarela(string sql, Action<ParameterBag> configureParameters)
        {
            DBPasarela.ClearParameters();
            DBPasarela.Type = CommandType.Text;
            DBPasarela.SqlStatment = sql;

            var bag = new ParameterBag(DBPasarela);
            configureParameters?.Invoke(bag);

            Logger.Info("Ejecutamos PASARELA query: " + sql);
            DBPasarela.ExecuteNonQuery();
        }

        private static object GetIntOrDbNull(DataRow dr, string columnName)
        {
            if (!dr.Table.Columns.Contains(columnName))
                return DBNull.Value;

            object value = dr[columnName];

            if (value == null || value == DBNull.Value)
                return DBNull.Value;

            return Convert.ToInt32(value);
        }

        private static object GetStringOrDbNull(DataRow dr, string columnName)
        {
            if (!dr.Table.Columns.Contains(columnName))
                return DBNull.Value;

            object value = dr[columnName];

            return value == null || value == DBNull.Value
                ? (object)DBNull.Value
                : value.ToString();
        }

        private static void Add(ParameterBag p, string name, object value)
        {
            p.Add(name, value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()) ? DBNull.Value : value);
        }

        /// <summary>
        /// Adaptador mínimo para no depender del nombre exacto del método de parámetros de DBContext.
        /// </summary>
        private sealed class ParameterBag
        {
            private readonly DBContext _db;

            private readonly Dictionary<string, object> _values =
                new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

            public ParameterBag(DBContext db)
            {
                _db = db;
            }

            public void Add(string name, object value)
            {
                Set(name, value);
            }

            public void AddApply(string name, object value)
            {
                Set(name, value);
                Apply();
            }

            public void Set(string name, object value)
            {
                _values[NormalizeParameterName(name)] = value ?? DBNull.Value;
            }

            public void Apply()
            {
                foreach (var kv in _values)
                {
                    _db.AddParameter(
                        kv.Key,
                        ParameterDirection.Input,
                        GuessSqlDbType(kv.Value),
                        kv.Value
                    );
                }
            }

            private static string NormalizeParameterName(string name)
            {
                return name.StartsWith("@") ? name : "@" + name;
            }

            private static SqlDbType GuessSqlDbType(object value)
            {
                if (value == null || value == DBNull.Value)
                    return SqlDbType.NVarChar;

                Type type = value.GetType();

                if (type == typeof(int)) return SqlDbType.Int;
                if (type == typeof(long)) return SqlDbType.BigInt;
                if (type == typeof(short)) return SqlDbType.SmallInt;
                if (type == typeof(bool)) return SqlDbType.Bit;
                if (type == typeof(DateTime)) return SqlDbType.DateTime;
                if (type == typeof(decimal)) return SqlDbType.Decimal;
                if (type == typeof(double)) return SqlDbType.Float;
                if (type == typeof(float)) return SqlDbType.Real;
                if (type == typeof(byte[])) return SqlDbType.VarBinary;

                return SqlDbType.NVarChar;
            }
        }
    }
}
