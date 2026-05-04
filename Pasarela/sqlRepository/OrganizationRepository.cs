using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly DBContext DB;

        public OrganizationRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "ORGANIZATION";
            public const string OU_ID = "OU_ID";
            public const string OU_NAME = "OU_NAME";
            public const string OU_TYPE = "OU_TYPE";

            //Querys
            public const string GetByModelName = "SELECT O.OU_ID, O.OU_NAME, O.OU_TYPE FROM [erwin_evolve].[dbo].[ORGANIZATION] O WHERE O.MODEL_NAME = {0}";
        }

        private Organization Parse_DataRow_To_POCO(DataRow dr)
        {
            Organization ret = new Organization
            {
                OU_ID = (int)dr[Table.OU_ID],
                OU_NAME = dr[Table.OU_NAME].ToString(),
                OU_TYPE = (int)dr[Table.OU_TYPE],
            };
            return ret;
        }

        public ResponseBase<IList<Organization>> GetByModelName(string ModelName)
        {
            ResponseBase<IList<Organization>> response = new ResponseBase<IList<Organization>>();
            IList<Organization> retList = new List<Organization>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = string.Format(Table.GetByModelName, ModelName);

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Organization organization;

                foreach (DataRow dr in listDT.Rows)
                {
                    organization = Parse_DataRow_To_POCO(dr);
                    retList.Add(organization);
                }
                response.Data = retList;

                if (response.Query_Result.Query_HasError())
                    Logger.Error(
                        TraceHelper.StackTracePath(new StackTrace()),
                        response.Query_Result.SQLMessage);

            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                throw;
            }
            finally
            {
                this.DB.CloseConnection();
            }
            return response;
        }
    }
}
