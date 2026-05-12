USE [erwin_evolve]
GO
/****** Object:  StoredProcedure [dbo].[SPPasarelaObtenerArbolProcedimientos]    Script Date: 12/05/2026 15:04:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--====================================================================
-- Autor:		MB57
--Procedimiento : dbo.SPPasarelaObtenerArbolProcedimientos
--Descripción  :
--    Obtiene el árbol jerárquico de Grupos de Procedimiento y
--    Procedimientos definidos en un modelo de Corporate Modeler / Erwin.

--    La jerarquía se calcula a partir de la relación visual entre
--    DIAGRAM y SHAPE:
--        - Un diagrama padre contiene una SHAPE.
--        - Esa SHAPE referencia el ANO_ID / ANO_TABNR de otro DIAGRAM.
--        - Ese DIAGRAM se considera hijo.

--Parámetros:
--    @ModelName sysname
--        Nombre del modelo a analizar. Ejemplo: N'ARTEZELI'.

--Tablas consultadas:
--    dbo.DIAGRAM
--    dbo.SHAPE
--    dbo.CW_PROP_TYPE
--    dbo.CW_LOOKUP

--Salida:
--    Devuelve una fila por cada grupo/procedimiento hijo encontrado,
--    indicando:
--        - raíz del árbol,
--        - padre,
--        - hijo,
--        - nivel jerárquico,
--        - tipo lógico,
--        - coordenadas visuales,
--        - ruta de DI_IDs.

--Notas:
--    - Solo lectura. No modifica datos.
--    - Evita ciclos mediante la columna VISITADOS.
--    - No devuelve las raíces porque se filtra NIVEL > 0.
--    - MAXRECURSION configurado a 10000.

--Ejemplo:
--    EXEC dbo.SPPasarelaObtenerArbolProcedimientos
--        @ModelName = N'ARTEZELI';
--====================================================================
ALTER   PROCEDURE [dbo].[SPPasarelaObtenerArbolProcedimientos]
    @ModelName sysname = N'ARTEZELI'
AS
BEGIN
    SET NOCOUNT ON;

    WITH Diagramas AS
    (
        SELECT
            D.MODEL_NAME,
            D.DI_ID,
            D.ANO_ID,
            D.ANO_TABNR,
            D.DI_NAME,
            D.DI_TITLE,
            D.DI_TYPE,
            L.LU_NAME AS DI_TYPE_DESC,
            CASE
                WHEN L.LU_NAME LIKE '%Grupo%Procedimiento%' THEN 'GRUPO_PROCEDIMIENTO'
                WHEN L.LU_NAME LIKE '%Procedimiento%' THEN 'PROCEDIMIENTO'
                ELSE 'OTRO'
            END AS TIPO_LOGICO
        FROM dbo.DIAGRAM D
        LEFT JOIN dbo.CW_PROP_TYPE P
            ON P.MODEL_NAME = D.MODEL_NAME
           AND P.PPT_FIELD_NAME = 'DI_TYPE'
        LEFT JOIN dbo.CW_LOOKUP L
            ON L.MODEL_NAME = D.MODEL_NAME
           AND L.PPT_ID = P.PPT_ID
           AND L.LU_ID = D.DI_TYPE
        WHERE D.MODEL_NAME = @ModelName
    ),
    Relaciones AS
    (
        SELECT
            Padre.MODEL_NAME,

            Padre.DI_ID AS PADRE_DI_ID,
            Padre.ANO_ID AS PADRE_ANO_ID,

            Hijo.DI_ID AS HIJO_DI_ID,
            Hijo.ANO_ID AS HIJO_ANO_ID,
            Hijo.ANO_TABNR AS HIJO_ANO_TABNR,

            CAST(S.SH_SEQ AS int) AS SH_SEQ,
            CAST(S.SH_Y AS int) AS SH_Y,
            CAST(S.SH_X AS int) AS SH_X
        FROM Diagramas Padre
        INNER JOIN dbo.SHAPE S
            ON S.MODEL_NAME = Padre.MODEL_NAME
           AND S.DI_ID = Padre.DI_ID
        INNER JOIN Diagramas Hijo
            ON Hijo.MODEL_NAME = S.MODEL_NAME
           AND Hijo.ANO_ID = S.ANO_ID
           AND Hijo.ANO_TABNR = S.ANO_TABNR
           AND Hijo.DI_ID <> Padre.DI_ID
        WHERE Hijo.TIPO_LOGICO IN ('GRUPO_PROCEDIMIENTO', 'PROCEDIMIENTO')
    ),
    Raices AS
    (
        SELECT D.*
        FROM Diagramas D
        WHERE D.TIPO_LOGICO = 'GRUPO_PROCEDIMIENTO'
          AND NOT EXISTS
          (
              SELECT 1
              FROM dbo.SHAPE S
              INNER JOIN dbo.DIAGRAM DP
                  ON DP.MODEL_NAME = S.MODEL_NAME
                 AND DP.DI_ID = S.DI_ID
              WHERE S.MODEL_NAME = D.MODEL_NAME
                AND S.ANO_ID = D.ANO_ID
                AND S.ANO_TABNR = D.ANO_TABNR
                AND DP.DI_ID <> D.DI_ID
          )
    ),
    Arbol
    (
        MODEL_NAME,
        RAIZ_DI_ID,
        PADRE_DI_ID,
        DI_ID,
        NIVEL,
        VISITADOS,
        RUTA_IDS,
        SH_SEQ,
        SH_Y,
        SH_X
    )
    AS
    (
        SELECT
            R.MODEL_NAME,
            CAST(R.DI_ID AS int) AS RAIZ_DI_ID,
            CAST(NULL AS int) AS PADRE_DI_ID,
            CAST(R.DI_ID AS int) AS DI_ID,
            CAST(0 AS int) AS NIVEL,
            CAST('|' + CAST(R.DI_ID AS varchar(20)) + '|' AS varchar(max)) AS VISITADOS,
            CAST(CAST(R.DI_ID AS varchar(20)) AS varchar(max)) AS RUTA_IDS,
            CAST(NULL AS int) AS SH_SEQ,
            CAST(NULL AS int) AS SH_Y,
            CAST(NULL AS int) AS SH_X
        FROM Raices R

        UNION ALL

        SELECT
            A.MODEL_NAME,
            A.RAIZ_DI_ID,
            CAST(R.PADRE_DI_ID AS int) AS PADRE_DI_ID,
            CAST(R.HIJO_DI_ID AS int) AS DI_ID,
            CAST(A.NIVEL + 1 AS int) AS NIVEL,
            CAST(A.VISITADOS + CAST(R.HIJO_DI_ID AS varchar(20)) + '|' AS varchar(max)) AS VISITADOS,
            CAST(A.RUTA_IDS + ' > ' + CAST(R.HIJO_DI_ID AS varchar(20)) AS varchar(max)) AS RUTA_IDS,
            CAST(R.SH_SEQ AS int) AS SH_SEQ,
            CAST(R.SH_Y AS int) AS SH_Y,
            CAST(R.SH_X AS int) AS SH_X
        FROM Arbol A
        INNER JOIN Relaciones R
            ON R.MODEL_NAME = A.MODEL_NAME
           AND R.PADRE_DI_ID = A.DI_ID
        WHERE CHARINDEX('|' + CAST(R.HIJO_DI_ID AS varchar(20)) + '|', A.VISITADOS) = 0
    )
    SELECT
        A.RAIZ_DI_ID,
        Raiz.DI_NAME AS RAIZ_DI_NAME,

        A.NIVEL,

        A.PADRE_DI_ID,
        Padre.ANO_ID AS PADRE_ANO_ID,
        Padre.DI_NAME AS PADRE_DI_NAME,
        Padre.DI_TITLE AS PADRE_DI_TITLE,
        Padre.DI_TYPE_DESC AS PADRE_DI_TYPE_DESC,
        Padre.TIPO_LOGICO AS PADRE_TIPO_LOGICO,

        A.DI_ID,
        Hijo.ANO_ID,
        Hijo.ANO_TABNR,
        Hijo.DI_NAME,
        Hijo.DI_TITLE,
        Hijo.DI_TYPE_DESC,
        Hijo.TIPO_LOGICO,

        A.SH_SEQ,
        A.SH_Y,
        A.SH_X,

        A.RUTA_IDS
    FROM Arbol A
    INNER JOIN Diagramas Hijo
        ON Hijo.MODEL_NAME = A.MODEL_NAME
       AND Hijo.DI_ID = A.DI_ID
    INNER JOIN Diagramas Raiz
        ON Raiz.MODEL_NAME = A.MODEL_NAME
       AND Raiz.DI_ID = A.RAIZ_DI_ID
    LEFT JOIN Diagramas Padre
        ON Padre.MODEL_NAME = A.MODEL_NAME
       AND Padre.DI_ID = A.PADRE_DI_ID
    WHERE A.NIVEL > 0
    ORDER BY
        Raiz.DI_NAME,
        A.RUTA_IDS,
        A.NIVEL,
        A.SH_Y DESC,
        A.SH_X ASC
    OPTION (MAXRECURSION 10000);
END;
