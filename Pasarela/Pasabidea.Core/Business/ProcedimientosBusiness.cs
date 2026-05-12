using Lantik.Pasabidea.Core.Data;
using Lantik.Pasabidea.Core.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Lantik.Pasabidea.Core.Business
{
    public sealed class ProcedimientosBusiness
    {
        public List<ProcedimientoDTO> ObtenerArbolProcedimientos(string modelName)
        {
            if (string.IsNullOrWhiteSpace(modelName))
            {
                throw new ArgumentException("El modelo no puede estar vacío.", nameof(modelName));
            }

            using (ProcedimientosData data = new ProcedimientosData())
            {
                DataTable dt = data.ObtenerArbolProcedimientos(modelName.Trim());

                List<ProcedimientoDTO> lista = dt
                    .AsEnumerable()
                    .Select(ProcedimientosMapper.FromDataRow)
                    .ToList();

                return ProcedimientosTreeBuilder.Build(lista);
            }
        }
    }


    internal static class ProcedimientosMapper
    {
        public static ProcedimientoDTO FromDataRow(DataRow row)
        {
            if (row == null)
                throw new ArgumentNullException(nameof(row));

            return new ProcedimientoDTO
            {
                RaizDiId = GetInt(row, "RAIZ_DI_ID"),
                RaizDiName = GetString(row, "RAIZ_DI_NAME"),

                DiId = GetInt(row, "DI_ID"),
                AnoId = GetInt(row, "ANO_ID"),
                AnoTabnr = GetInt(row, "ANO_TABNR"),

                DiName = GetString(row, "DI_NAME"),
                DiTitle = GetString(row, "DI_TITLE"),

                TipoLogico = GetString(row, "TIPO_LOGICO"),
                DiTypeDesc = GetString(row, "DI_TYPE_DESC"),

                Nivel = GetInt(row, "NIVEL"),
                RutaIds = GetString(row, "RUTA_IDS")
            };
        }

        private static int GetInt(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName))
                return 0;

            object value = row[columnName];

            if (value == null || value == DBNull.Value)
                return 0;

            int result;
            return int.TryParse(value.ToString(), out result)
                ? result
                : 0;
        }

        private static string GetString(DataRow row, string columnName)
        {
            if (!row.Table.Columns.Contains(columnName))
                return string.Empty;

            object value = row[columnName];

            return value == null || value == DBNull.Value
                ? string.Empty
                : value.ToString().Trim();
        }
    }

    internal static class ProcedimientosTreeBuilder
    {
        public static List<ProcedimientoDTO> Build(IEnumerable<ProcedimientoDTO> items)
        {
            var raices = new List<ProcedimientoDTO>();
            var nodosPorRuta = new Dictionary<string, ProcedimientoDTO>();

            foreach (var item in items)
            {
                if (item == null || string.IsNullOrWhiteSpace(item.RutaIds))
                    continue;

                item.RutaIds = NormalizarRuta(item.RutaIds);

                string rutaRaiz = item.RaizDiId.ToString();

                if (!nodosPorRuta.ContainsKey(rutaRaiz))
                {
                    var raiz = new ProcedimientoDTO
                    {
                        RaizDiId = item.RaizDiId,
                        RaizDiName = item.RaizDiName,
                        DiId = item.RaizDiId,
                        DiName = item.RaizDiName,
                        DiTitle = item.RaizDiName,
                        TipoLogico = "GRUPO_PROCEDIMIENTO",
                        DiTypeDesc = "Grupo de Procedimientos",
                        Nivel = 0,
                        RutaIds = rutaRaiz
                    };

                    nodosPorRuta.Add(rutaRaiz, raiz);
                    raices.Add(raiz);
                }

                if (nodosPorRuta.ContainsKey(item.RutaIds))
                    continue;

                string rutaPadre = ObtenerRutaPadre(item.RutaIds);

                ProcedimientoDTO padre;
                if (!nodosPorRuta.TryGetValue(rutaPadre, out padre))
                    padre = nodosPorRuta[rutaRaiz];

                padre.Hijos.Add(item);
                nodosPorRuta.Add(item.RutaIds, item);
            }

            return raices;
        }

        private static string ObtenerRutaPadre(string rutaIds)
        {
            string ruta = NormalizarRuta(rutaIds);

            int pos = ruta.LastIndexOf(">");
            if (pos < 0)
                return string.Empty;

            return ruta.Substring(0, pos).Trim();
        }

        private static string NormalizarRuta(string ruta)
        {
            return string.IsNullOrWhiteSpace(ruta)
                ? string.Empty
                : ruta.Replace("  ", " ").Trim();
        }
    }
}