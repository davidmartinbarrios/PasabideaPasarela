using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Lantik.Pasarela.Helpers;

namespace Lantik.Pasarela.sqlRepository
{
    public class DBContext
    {
        #region variables
        private string _prv_ConnectionString = string.Empty;
        private string _prv_SqlStatment = string.Empty;
        private CommandType _prv_CommandType;

        private SqlConnection _prv_Connection;
        private SqlTransaction _prv_Trans;

        private Int32 _prv_CommandTimeOut = Int32.MinValue;
        private List<SqlParameter> _prv_ParameterArrayList = new List<SqlParameter>();

        private bool _prv_bError = false;
        private string _prv_sMsError = string.Empty;
        private string _prv_sNomTransaccion = string.Empty;
        private bool _prv_cerrarConexion = true;
        //Pruebas
        //private string cnnBDPrograma;
        #endregion

        #region propiedades
        public string TransactionName
        {
            get { return _prv_sNomTransaccion; }
            set { _prv_sNomTransaccion = value; }
        }
        public bool CerrarConexionAlFinalizar
        {
            get { return _prv_cerrarConexion; }
            set { _prv_cerrarConexion = value; }
        }
        public SqlConnection Connection
        {
            get { return _prv_Connection; }
            set { _prv_Connection = value; }
        }
        public CommandType Type
        {
            get { return _prv_CommandType; }
            set { _prv_CommandType = value; }
        }
        public string ConnectionString
        {
            get { return _prv_ConnectionString; }
            set
            {
                if (_prv_Connection.State == ConnectionState.Open)
                    _prv_Connection.Close();
                _prv_ConnectionString = value;
                _prv_Connection.ConnectionString = _prv_ConnectionString;
            }
        }
        public void RemoveParameter(string name)
        {
            DeleteParameter(name);
        }

        public bool HayError
        {
            get { return _prv_bError; }
        }
        public string DescripcionError
        {
            get { return _prv_sMsError; }
        }
        public string SqlStatment
        {
            get { return _prv_SqlStatment; }
            set { _prv_SqlStatment = value; }
        }
        public Boolean IsOpen
        {
            get
            {
                if (_prv_Connection != null)
                {
                    if (_prv_Connection.State == ConnectionState.Open)
                        return true;
                    else
                        return false;
                }
                else
                {
                    return false;
                }
            }
        }
        public List<SqlParameter> ParameterLists
        {
            get { return _prv_ParameterArrayList; }
        }
        public Int32 CommandTimeOut
        {
            get { return _prv_CommandTimeOut; }
            set { _prv_CommandTimeOut = value; }
        }
        public SqlTransaction Transaction
        {
            get { return _prv_Trans; }
            set { _prv_Trans = value; }
        }
        #endregion

        #region constructor
        public DBContext()
        {
            LocateConnection();
            _prv_CommandType = CommandType.Text;
            _prv_CommandTimeOut = -1;
            _prv_cerrarConexion = true;
            _prv_sNomTransaccion = string.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString">Cadena de conexión con la base de datos SQL SERVER</param>
        /// <param name="sNomTransaccion"></param>
        public DBContext(string connectionString, string sNomTransaccion = "") : base()
        {
            LocateConnection();
            if (connectionString != null)
            {
                _prv_ConnectionString = connectionString;
                _prv_Connection.ConnectionString = _prv_ConnectionString;
            }

            _prv_CommandTimeOut = -1;

            if (sNomTransaccion != null)
                _prv_sNomTransaccion = sNomTransaccion;
            else
                _prv_sNomTransaccion = string.Empty;

            _prv_cerrarConexion = true;
        }
        public DBContext(SqlConnection cnn, SqlTransaction tran, string tranName, bool cerrarConexion) : base()
        {
            _prv_Connection = cnn;
            _prv_bError = false;
            _prv_CommandTimeOut = -1;
            _prv_Trans = tran;
            _prv_cerrarConexion = cerrarConexion;
            _prv_sNomTransaccion = tranName;
        }
        #endregion

        #region destructor
#pragma warning disable CS0465 // Introducir un método 'Finalize' afectar a la invocación del destructor
        public virtual void Finalize()
        {
            GC.SuppressFinalize(this);
        }
#pragma warning restore CS0465 // Introducir un método 'Finalize' afectar a la invocación del destructor
        #endregion

        #region metodos privados
        private void LocateConnection()
        {
            _prv_Connection = new SqlConnection();
            _prv_bError = false;
        }
        #endregion

        #region metodos publicos
        public void CloseConnection()
        {
            if (_prv_Trans == null && _prv_Connection.State == ConnectionState.Open)
                _prv_Connection.Close();
        }
        public SqlDataReader GetSqlDataReader()
        {
            SqlCommand cmdExec = new SqlCommand(_prv_SqlStatment, _prv_Connection);
            SqlDataReader sqlDrReturn = null;
            try
            {
                if (_prv_Connection.State == ConnectionState.Closed)
                    _prv_Connection.Open();

                var aux = cmdExec;
                {
                    aux.Parameters.Clear();
                    if (_prv_CommandTimeOut != 1)
                        aux.CommandTimeout = _prv_CommandTimeOut;
                    if (_prv_Trans != null)
                        aux.Transaction = _prv_Trans;

                    aux.CommandType = _prv_CommandType;

                    foreach (SqlParameter param in _prv_ParameterArrayList)
                    {
                        aux.Parameters.Add(param);
                    }

                    sqlDrReturn = aux.ExecuteReader(CommandBehavior.CloseConnection);
                    return sqlDrReturn;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                SetError(ex.ToString());
                sqlDrReturn = null;
                return sqlDrReturn;
            }

        }
        public DataSet GetDataSet()
        {
            SqlDataAdapter internalAdapter = new SqlDataAdapter(_prv_SqlStatment, _prv_Connection);
            DataSet ds = new DataSet();

            internalAdapter.SelectCommand.Parameters.Clear();
            if (_prv_CommandTimeOut != -1)
                internalAdapter.SelectCommand.CommandTimeout = _prv_CommandTimeOut;

            if (_prv_Trans != null)
                internalAdapter.SelectCommand.Transaction = _prv_Trans;

            internalAdapter.SelectCommand.CommandType = _prv_CommandType;

            foreach (SqlParameter param in _prv_ParameterArrayList)
            {
                internalAdapter.SelectCommand.Parameters.Add(param);
            }

            internalAdapter.Fill(ds);
            return ds;
        }
        public DataSet GetDataSet(string name)
        {
            SqlDataAdapter internalAdapter = new SqlDataAdapter(_prv_SqlStatment, _prv_Connection);
            DataSet ds = new DataSet();

            internalAdapter.SelectCommand.Parameters.Clear();

            if (_prv_CommandTimeOut != -1)
                internalAdapter.SelectCommand.CommandTimeout = _prv_CommandTimeOut;

            if (_prv_Trans != null)
                internalAdapter.SelectCommand.Transaction = _prv_Trans;
            internalAdapter.SelectCommand.CommandType = _prv_CommandType;

            foreach (SqlParameter param in _prv_ParameterArrayList)
            {
                internalAdapter.SelectCommand.Parameters.Add(param);
            }
            internalAdapter.Fill(ds, name);
            return ds;
        }
        public DataTable GetDataTable()
        {
            //SqlDataAdapter internalAdapter = new SqlDataAdapter("SELECT * FROM DIAGRAM;", _prv_Connection);

            SqlDataAdapter internalAdapter = new SqlDataAdapter(_prv_SqlStatment, _prv_Connection);
            DataTable dtSql = new DataTable();

            if (_prv_Connection.State == ConnectionState.Closed)
                _prv_Connection.Open();

            internalAdapter.SelectCommand.Parameters.Clear();

            if (_prv_CommandTimeOut != -1)
                internalAdapter.SelectCommand.CommandTimeout = _prv_CommandTimeOut;

            if (_prv_Trans != null)
                internalAdapter.SelectCommand.Transaction = _prv_Trans;

            internalAdapter.SelectCommand.CommandType = _prv_CommandType;

            foreach (SqlParameter param in _prv_ParameterArrayList)
            {
                internalAdapter.SelectCommand.Parameters.Add(param);
            }

            internalAdapter.Fill(dtSql);
            internalAdapter.SelectCommand.Parameters.Clear();

            return dtSql;
        }
        public void AddTableToDataSet(string name, ref DataSet ds)
        {
            SqlDataAdapter internalAdapter = new SqlDataAdapter(_prv_SqlStatment, _prv_Connection);

            try
            {
                internalAdapter.SelectCommand.Parameters.Clear();
                if (_prv_CommandTimeOut != -1)
                    internalAdapter.SelectCommand.CommandTimeout = _prv_CommandTimeOut;
                if (_prv_Trans != null)
                    internalAdapter.SelectCommand.CommandType = _prv_CommandType;

                internalAdapter.SelectCommand.CommandType = _prv_CommandType;

                foreach (SqlParameter param in _prv_ParameterArrayList)
                {
                    internalAdapter.SelectCommand.Parameters.Add(param);
                }

                internalAdapter.Fill(ds, name);
            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                SetError(ex.ToString());
            }
        }
        public void SetError()
        {
            _prv_bError = true;
        }
        public void SetError(string sMsgError)
        {
            _prv_bError = true;
            _prv_sMsError = sMsgError.Trim();
        }
        public void ClearError()
        {
            _prv_bError = false;
            _prv_sMsError = string.Empty;
        }
        public void ClearParameters()
        {
            _prv_ParameterArrayList.Clear();
        }
        public void DeleteParameter(Int32 index)
        {
            if (index < _prv_ParameterArrayList.Count)
                _prv_ParameterArrayList.RemoveAt(index);
        }
        public void DeleteParameter(string name)
        {
            Int32 i = 0;
            foreach (SqlParameter param in _prv_ParameterArrayList)
            {
                if (param.ParameterName.ToLower() == name.ToLower())
                    _prv_ParameterArrayList.RemoveAt(i);

                i++;
            }
        }
        public SqlParameterCollection Parameters()
        {
            SqlCommand cmd = new SqlCommand();
            SqlParameterCollection prms;

            foreach (SqlParameter prm in _prv_ParameterArrayList)
            {
                cmd.Parameters.Add(prm);
            }
            prms = cmd.Parameters;
            cmd.Dispose();
            return prms;
        }
        public SqlParameter Parameters(Int32 index)
        {
            if (index < _prv_ParameterArrayList.Count)
                return _prv_ParameterArrayList[index];
            else
                return null;
        }
        public SqlParameter Parameters(string name)
        {
            Int32 i = 0;
            SqlParameter result = null;
            foreach (SqlParameter param in _prv_ParameterArrayList)
            {
                if (param.ParameterName.ToLower() == name.ToLower())
                {
                    result = param;
                    break;
                }
            }

            if (i >= _prv_ParameterArrayList.Count)
                result = null;

            return result;
        }
        public void AddParameter(string name, ParameterDirection direction, SqlDbType type)
        {
            SqlParameter param = new SqlParameter(name, type);
            param.Direction = direction;
            _prv_ParameterArrayList.Add(param);
        }
        public void AddParameter(string name, ParameterDirection direction, SqlDbType type, Int32 size, Object value)
        {
            SqlParameter param = new SqlParameter(name, type);

            param.Direction = direction;
            param.Size = size;
            param.Value = value;

            _prv_ParameterArrayList.Add(param);
        }
        public void AddParameter(string name, ParameterDirection direction, SqlDbType type, object value)
        {
            SqlParameter param = new SqlParameter(name, type);

            param.Direction = direction;
            param.Value = value;

            _prv_ParameterArrayList.Add(param);
        }
        public Int32 ExecuteNonQuery()
        {
            SqlCommand cmdExec = new SqlCommand(_prv_SqlStatment, _prv_Connection);
            cmdExec.Parameters.Clear();

            if (_prv_CommandTimeOut != -1)
                cmdExec.CommandTimeout = _prv_CommandTimeOut;

            if (_prv_Connection.State == ConnectionState.Closed)
                _prv_Connection.Open();

            cmdExec.CommandType = _prv_CommandType;

            foreach (SqlParameter param in _prv_ParameterArrayList)
                cmdExec.Parameters.Add(param);

            if (_prv_Trans != null)
                cmdExec.Transaction = _prv_Trans;

            return cmdExec.ExecuteNonQuery();
        }
        public SqlDataReader ExecuteReader()
        {
            SqlCommand cmdExec = new SqlCommand(_prv_SqlStatment, _prv_Connection);
            cmdExec.Parameters.Clear();

            if (_prv_CommandTimeOut != -1)
                cmdExec.CommandTimeout = _prv_CommandTimeOut;

            if (_prv_Connection.State == ConnectionState.Closed)
                _prv_Connection.Open();

            if (_prv_Trans != null)
                cmdExec.Transaction = _prv_Trans;

            cmdExec.CommandType = _prv_CommandType;

            foreach (SqlParameter param in _prv_ParameterArrayList)
                cmdExec.Transaction = _prv_Trans;

            if (_prv_Trans != null)
                cmdExec.Transaction = _prv_Trans;

            return cmdExec.ExecuteReader();


        }
        public object ExecuteScalar()
        {
            SqlCommand cmdExec = new SqlCommand(_prv_SqlStatment, _prv_Connection);
            cmdExec.Parameters.Clear();

            if (_prv_CommandTimeOut != -1)
                cmdExec.CommandTimeout = _prv_CommandTimeOut;

            if (_prv_Connection.State == ConnectionState.Closed)
                _prv_Connection.Open();

            cmdExec.CommandType = _prv_CommandType;

            foreach (SqlParameter param in _prv_ParameterArrayList)
                cmdExec.Parameters.Add(param);

            if (_prv_Trans != null)
                cmdExec.Transaction = _prv_Trans;

            return cmdExec.ExecuteScalar();
        }
        public void BeginTRAN(IsolationLevel iso, string TransactionName)
        {
            if (_prv_Connection.State != ConnectionState.Open)
                _prv_Connection.Open();

            _prv_Trans = _prv_Connection.BeginTransaction(iso, TransactionName);
            _prv_sNomTransaccion = TransactionName;
        }
        public void CommitTRAN()
        {
            if (_prv_Trans != null)
                _prv_Trans.Commit();

            _prv_Trans = null;
        }
        public void RollBackTRAN(string TransactionName)
        {
            if (_prv_Trans != null)
                _prv_Trans.Rollback(TransactionName);

            _prv_Trans = null;
        }
        public string BuildLikeFilter(string hashSeparatedFields, string searchExpression)
        {
            string sqlSentence = string.Empty;

            if (searchExpression != string.Empty)
            {
                try
                {
                    foreach (string strCampo in hashSeparatedFields.Split('#'))
                    {
                        if (sqlSentence == string.Empty)
                            sqlSentence = strCampo + " LIKE @FILTROBUSCAR ";
                        else
                            sqlSentence += " OR " + strCampo + " LIKE @FILTROBUSCAR ";
                    }
                }
                catch (Exception)
                {
                    sqlSentence = string.Empty;
                }

                if (sqlSentence != string.Empty)
                    sqlSentence = " AND (" + sqlSentence + ")";
            }

            return sqlSentence;
        }
        #endregion

        protected DataTable Execute(string TableName)
        {
            DataTable ret = new DataTable();
            this.Type = CommandType.Text;

            try
            {
                ret = this.GetDataTable();
                ret.TableName = TableName;
            }
            catch (Exception ex)
            {
                Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
                SetError(ex.ToString());
            }
            finally
            {
                this.CloseConnection();
            }

            return ret;
        }

    }
}
