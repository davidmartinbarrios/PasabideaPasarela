using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using Lantik.Pasarela.Helpers;

namespace Lantik.Pasabidea
{
    internal sealed class DbContext : IDisposable
    {
        private readonly string _connectionString;
        private readonly SqlConnection _connection;
        private SqlTransaction _transaction;

        public int CommandTimeout { get; set; } = 30;
        public bool HayError { get; private set; }
        public string DescripcionError { get; private set; } = string.Empty;

        //private static Dictionary<string, DbContext> _dbContexts = new Dictionary<string, DbContext>();

        //public static DbContext Get(string connectionString)
        //{
        //    if (!_dbContexts.ContainsKey(connectionString))
        //        _dbContexts[connectionString] = new DbContext(ConfigurationManager.ConnectionStrings[connectionString].ConnectionString);
        //    return _dbContexts[connectionString];
        //}

        public static DbContext Get(string connectionStringName)
        {
            ConnectionStringSettings settings =
                ConfigurationManager.ConnectionStrings[connectionStringName];

            if (settings == null)
                throw new InvalidOperationException(
                    $"No existe la cadena de conexión '{connectionStringName}'.");

            return new DbContext(settings.ConnectionString);
        }

        public DbContext(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(connectionString);
        }

        public DbContext(SqlConnection connection, SqlTransaction transaction = null)
        {
            _connection = connection;
            _transaction = transaction;
            _connectionString = connection.ConnectionString;
        }

        public DataTable QueryTable(string sql, params SqlParameter[] parameters)
        {
            return QueryTable(sql, CommandType.Text, parameters);
        }

        public DataTable QueryStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            return QueryTable(procedureName, CommandType.StoredProcedure, parameters);
        }

        public DataSet QueryDataSet(string sql, params SqlParameter[] parameters)
        {
            return QueryDataSet(sql, CommandType.Text, parameters);
        }

        public DataSet QueryStoredProcedureDataSet(string procedureName, params SqlParameter[] parameters)
        { 
            return QueryDataSet(procedureName, CommandType.StoredProcedure, parameters);
        }

        public int Execute(string sql, params SqlParameter[] parameters)
        {
            return Execute(sql, CommandType.Text, parameters);
        }

        public int ExecuteStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            return Execute(procedureName, CommandType.StoredProcedure, parameters);
        }

        public object Scalar(string sql, params SqlParameter[] parameters)
        {
            return Scalar(sql, CommandType.Text, parameters);
        }

        public object ScalarStoredProcedure(string procedureName, params SqlParameter[] parameters)
        {
            return Scalar(procedureName, CommandType.StoredProcedure, parameters);
        }

        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            OpenIfNeeded();
            _transaction = _connection.BeginTransaction(isolationLevel);
        }

        public void Commit()
        {
            if (_transaction == null) return;

            _transaction.Commit();
            _transaction.Dispose();
            _transaction = null;
        }

        public void Rollback()
        {
            if (_transaction == null) return;

            _transaction.Rollback();
            _transaction.Dispose();
            _transaction = null;
        }

        public static SqlParameter Param(string name, SqlDbType type, object value)
        {
            return new SqlParameter(name, type)
            {
                Value = value ?? DBNull.Value
            };
        }

        public static SqlParameter Param(string name, SqlDbType type, int size, object value)
        {
            return new SqlParameter(name, type, size)
            {
                Value = value ?? DBNull.Value
            };
        }

        public static SqlParameter OutputParam(string name, SqlDbType type, int size = 0)
        {
            SqlParameter parameter = size > 0
                ? new SqlParameter(name, type, size)
                : new SqlParameter(name, type);

            parameter.Direction = ParameterDirection.Output;
            return parameter;
        }

        private DataTable QueryTable(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                ClearError();

                using (SqlCommand command = CreateCommand(commandText, commandType, parameters))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                RegisterError(ex);
                throw;
            }
        }

        private DataSet QueryDataSet(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                ClearError();

                using (SqlCommand command = CreateCommand(commandText, commandType, parameters))
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    return dataSet;
                }
            }
            catch (Exception ex)
            {
                RegisterError(ex);
                throw;
            }
        }

        private int Execute(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                ClearError();

                using (SqlCommand command = CreateCommand(commandText, commandType, parameters))
                {
                    OpenIfNeeded();
                    return command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                RegisterError(ex);
                throw;
            }
        }

        private object Scalar(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            try
            {
                ClearError();

                using (SqlCommand command = CreateCommand(commandText, commandType, parameters))
                {
                    OpenIfNeeded();
                    return command.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                RegisterError(ex);
                throw;
            }
        }

        private SqlCommand CreateCommand(string commandText, CommandType commandType, SqlParameter[] parameters)
        {
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = commandType;
            command.CommandTimeout = CommandTimeout;

            if (_transaction != null)
                command.Transaction = _transaction;

            if (parameters != null)
                command.Parameters.AddRange(parameters);

            return command;
        }

        private void OpenIfNeeded()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        private void ClearError()
        {
            HayError = false;
            DescripcionError = string.Empty;
        }

        private void RegisterError(Exception ex)
        {
            HayError = true;
            DescripcionError = ex.ToString();

            Logger.Error(TraceHelper.StackTracePath(new StackTrace()), ex);
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_connection != null)
                _connection.Dispose();
        }
    }
}