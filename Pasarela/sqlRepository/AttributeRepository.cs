using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Lantik.Pasarela.Entities.POCOs;
using Lantik.Pasarela.Helpers;
using Lantik.Pasarela.Interfaces;

namespace Lantik.Pasarela.sqlRepository
{
    public class AttributeRepository: IAttributeRepository
    {
        private readonly DBContext DB;

        public AttributeRepository()
        {
            this.DB = new DBContext(Settings.BD_DP4);
        }
        public struct Table
        {
            public const string _Name = "ATTRIBUTE";
            public const string AT_ID = "AT_ID";
            public const string AT_NAME = "AT_NAME";

            //Querys
            public const string GetAll = "SELECT TOP  10 AT_ID, AT_NAME  FROM [erwin_evolve].[dbo].[ATTRIBUTE]";
        }

        private Entities.POCOs.Attribute Parse_DataRow_To_POCO(DataRow dr)
        {
            Entities.POCOs.Attribute ret = new Entities.POCOs.Attribute
            {
                AT_ID = (int)dr[Table.AT_ID],
                AT_NAME = dr[Table.AT_NAME].ToString(),
            };
            return ret;
        }

        public ResponseBase<IList<Entities.POCOs.Attribute>> GetAll()
        {
            ResponseBase<IList<Entities.POCOs.Attribute>> response = new ResponseBase<IList<Entities.POCOs.Attribute>>();
            IList<Entities.POCOs.Attribute> retList = new List<Entities.POCOs.Attribute>();
            this.DB.ClearParameters();

            this.DB.SqlStatment = Table.GetAll;

            try
            {
                Logger.Info("Ejecutamos la query: " + this.DB.SqlStatment);
                DataTable listDT = this.DB.GetDataTable();

                Entities.POCOs.Attribute attribute;

                foreach (DataRow dr in listDT.Rows)
                {
                    attribute = Parse_DataRow_To_POCO(dr);
                    retList.Add(attribute);
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
