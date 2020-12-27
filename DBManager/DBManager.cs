using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public enum JoinType : Byte
    {
        LEFT = 0,
        RIGHT,
        INNER,
    }

    public class DBManager : IDisposable
    {
        private static SemaphoreSlim semaphoreSlim;

        public String connectionString;
        private MySqlConnection connection;
        private MySqlTransaction currentTransaction;

        private bool disposed;

        static DBManager()
        {
            semaphoreSlim = new SemaphoreSlim(1);
        }

        //Nikita
        public DBManager()
        {
            StreamReader objReader = new StreamReader("init.ini");
            if (!objReader.EndOfStream)
                connectionString = objReader.ReadLine();
            connection = new MySqlConnection(connectionString);
            objReader.Close();
            Connect();

            disposed = false;
        }
        public DBManager(String connectionString)
        {
            this.connectionString = connectionString;
            connection = new MySqlConnection(connectionString);
            Connect();

            disposed = false;
        }

        ~DBManager()
        {
            Dispose();
        }

        public void Connect()
        {
            try
            {
                if (!connection.Ping())
                    connection.Open();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }

        public void Disconnect()
        {
            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
            }
        }

        public void StartTransaction()
        {
            currentTransaction = connection.BeginTransaction();
        }

        public void CommitTransaction()
        {
            if (currentTransaction != null)
            {
                currentTransaction.Commit();
                currentTransaction = null;
            }
        }

        public void RollbackTransaction()
        {
            if (currentTransaction != null)
            {
                currentTransaction.Rollback();
                currentTransaction = null;
            }
        }

        // Returns single first value according to parameters
        public Object GetValue(String tableName, String fields, String cond)
        {
            //try catch
            try
            {
                MySqlCommand command = new MySqlCommand(GetSelectStatement(tableName, fields, cond), connection);

                return command.ExecuteScalar();
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private String GetSelectStatement(String tableName, String fields, String cond)
        {
            if (fields == "")
                fields = "*";
            String res = "SELECT " + fields + " FROM " + tableName;

            if (cond != "" && !cond.ToUpper().Contains("ORDER BY"))
            {
                res += " WHERE " + cond;
            }
            else
            {
                res += cond;
            }

            res += ";";
            return res;
        }

        // Returns list of rows
        public List<List<Object>> GetRows(String tableName, String fields, String cond)
        {
            //try catch
            var res = new List<List<Object>>();

            MySqlCommand command = new MySqlCommand(GetSelectStatement(tableName, fields, cond), connection);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    List<Object> row = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader[i]);
                    }
                    res.Add(row);
                }
            }
            return res;
        }

        //Returns list of experts id's which have calculations
        public List<List<Object>> GetId()
        {
            var res = new List<List<Object>>();
            MySqlCommand command = new MySqlCommand("SELECT id_of_expert FROM calculations_description ORDER BY calculation_number;", connection);
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    List<Object> row = new List<object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader[i]);
                    }
                    res.Add(row);
                }
            }
            return res;
        }
        // Update table and return number of updated rows
        public int SetValue(String tableName, String field, String value, String cond)
        {
            //try catch
            value = ValidateString(value);
            MySqlCommand command = new MySqlCommand(GetUpdateStatement(tableName, field, value, cond), connection);
            return command.ExecuteNonQuery();
        }

        private String GetUpdateStatement(String tableName, String field, String value, String cond)
        {
            String res;
            if (cond != "")
                res = "UPDATE " + tableName + " SET " + field + " = " + value + " WHERE " + cond + " ;";
            else res = "UPDATE " + tableName + " SET " + field + " = " + value + " ;";
            return res;
        }

        public List<List<Object>> GetRowsUsingJoin(String tables, String columns, String joinConditions,
                                                   String condition, JoinType joinType)
        {
            if (string.IsNullOrEmpty(tables) ||
                string.IsNullOrEmpty(columns) ||
                string.IsNullOrEmpty(joinConditions) ||
                condition == null)
            {
                return default(List<List<Object>>);
            }

            Char[] separators = new[] { ',', ';' };
            String[] _tables = tables.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            String[] _columns = columns.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            String[] _joinConditions = joinConditions.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            return GetRowsUsingJoin(_tables, _columns, _joinConditions, condition, joinType);
        }
        public List<List<Object>> GetRowsUsingJoin(IList<String> tables, IEnumerable<String> columns,
                                                   IList<String> joinConditions, String condition, JoinType joinType)
        {
            if (tables == null || tables.Count == 0 ||
                columns == null || columns.Count() == 0 ||
                condition == null || joinConditions == null ||
                joinConditions.Count < tables.Count - 1)
            {
                return default(List<List<Object>>);
            }

            String firstTable = tables.First();
            StringBuilder query = new StringBuilder("SELECT ");

            foreach (var column in columns)
            {
                query.Append(column.Trim());
                query.Append(", ");
            }
            query.Remove(query.Length - 2, 1);

            query.Append("FROM ");
            query.Append(firstTable);

            for (int i = 1; i < tables.Count; i++)
            {
                query.AppendFormat(" {0} JOIN {1} ON {2}", joinType, tables[i], joinConditions[i - 1]);
            }

            if (condition != "" && !condition.ToUpper().Contains("WHERE"))
            {
                query.Append(" WHERE ");
            }

            query.AppendFormat(" {0}", condition);

            return GetRows(query.ToString());
        }

        private List<List<Object>> GetRows(String query)
        {
            List<List<Object>> result = new List<List<object>>();

            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = null;

            try
            {
                reader = command.ExecuteReader();

                while (reader.Read())
                {
                    List<Object> row = new List<object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader[i]);
                    }

                    result.Add(row);
                }
            }
            catch (Exception ex)
            {
                string format = "{0}{0}Error message: {1}{0}{0}Stack trace: {2}{0}{0}";

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR!");
                Console.ResetColor();

                Console.WriteLine(format, Environment.NewLine, ex.Message, ex.StackTrace);

                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }

                command.Dispose();
            }

            return result;
        }

        /// --IVAN

        public void DeleteFromDB(String table, String colName, String colValue)
        {
            try
            {
                String sqlCommand = "DELETE FROM " + table + " WHERE " + colName + " = " + colValue + " ";
                MySqlCommand deleteCmd = new MySqlCommand(sqlCommand, connection);
                deleteCmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteFromDB(String table, String[] colName, String[] colValue)
        {
            if (colName.Length == colValue.Length)
            {
                if (colName.Length > 1)
                {
                    String sqlCommand = "";
                    String res = "Deleted";
                    try
                    {
                        sqlCommand = "DELETE FROM " + table + " WHERE ";
                        for (int i = 0; i < colName.Length - 1; i++)
                        {
                            sqlCommand += colName[i] + " = " + colValue[i] + " AND ";
                        }
                        sqlCommand += "" + colName[colName.Length - 1] + " = " + colValue[colValue.Length - 1] + ";";
                        MySqlCommand deleteCmd = new MySqlCommand(sqlCommand, connection);
                        deleteCmd.ExecuteNonQuery();
                    }
                    catch (Exception ex) { res = ex.ToString() + "\n" + sqlCommand; }
                }
                else
                {
                    String colname = colName[0];
                    String colvalue = colValue[0];
                }
            }
            else { throw new ArgumentException("Field and Value list dont match."); }
        }

        public int InsertToBD(String table, String list)
        {
            String sqlCommand = "INSERT INTO " + table + " VALUES(" + list + ");";
            sqlCommand += "select last_insert_id();";
            MySqlCommand insertCmd = new MySqlCommand(sqlCommand, connection);
            return Int32.Parse(insertCmd.ExecuteScalar().ToString());
        }
        public int InsertToBD(String table, String[] fieldNames, String[] fieldValues)
        {
            if (fieldNames.Length == fieldValues.Length)
            {
                String sqlCommand = "INSERT INTO " + table + "(";
                for (int i = 0; i < fieldNames.Length - 1; i++)
                {
                    sqlCommand += " " + fieldNames[i] + ",";
                }
                sqlCommand += fieldNames[fieldNames.Length - 1];
                sqlCommand += ") VALUES(";
                for (int i = 0; i < fieldValues.Length - 1; i++)
                {
                    sqlCommand += " " + fieldValues[i] + ",";
                }
                sqlCommand += fieldValues[fieldNames.Length - 1];
                sqlCommand += ");";
                sqlCommand += " select last_insert_id();";
                MySqlCommand insertCmd = new MySqlCommand(sqlCommand, connection);
                int id = Int32.Parse(insertCmd.ExecuteScalar().ToString());
                return id;
            }
            else
            {
                throw new ArgumentException("Field and Value list dont match.");
            }
        }

        public void InsertToBDWithoutId(String table, String[] fieldNames, String[] fieldValues)
        {
            if (fieldNames.Length == fieldValues.Length)
            {
                try
                {
                    fieldValues = ValidateStrings(fieldValues);
                    String sqlCommand = "INSERT INTO " + table + "(";
                    for (int i = 0; i < fieldNames.Length - 1; i++)
                    {
                        sqlCommand += " " + fieldNames[i] + ",";
                    }
                    sqlCommand += fieldNames[fieldNames.Length - 1];
                    sqlCommand += ") VALUES(";
                    for (int i = 0; i < fieldValues.Length - 1; i++)
                    {
                        sqlCommand += " " + fieldValues[i] + ",";
                    }
                    sqlCommand += fieldValues[fieldNames.Length - 1];
                    sqlCommand += ");";
                    new MySqlCommand(sqlCommand, connection).ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new ArgumentException("Field and Value list dont match.");
            }
        }

        //"INSERT INTO " + table + "(" + fieldNames[i] + "

        public int UpdateRecord(String tableName, String[] colNames, String[] colValues)
        {
            if (colNames.Length == colValues.Length)
            {
                colValues = ValidateStrings(colValues);

                String sqlCommand = "UPDATE " + tableName + " SET ";

                for (int i = 1; i < colValues.Length - 1; i++)
                {
                    sqlCommand += colNames[i] + "=" + colValues[i] + ", ";
                }
                sqlCommand += colNames[colValues.Length - 1] + "=" + colValues[colValues.Length - 1] + "";
                sqlCommand += " where " + colNames[0] + "=" + colValues[0] + "";
                MySqlCommand insertCmd = new MySqlCommand(sqlCommand, connection);
                return insertCmd.ExecuteNonQuery();
            }
            else
            {
                throw new ArgumentException("Field and Value list dont match.");
            }
        }

        private String ValidateString(String str)
        {
            if (String.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("str");
            }

            if (str[0] == '\'')
                return '\'' + str.Trim('\'').Replace('\'', '`') + '\'';
            return str.Replace('\'', '`');
        }

        private String[] ValidateStrings(String[] strs)
        {
            String[] res = new String[strs.Length];

            for (int i = 0; i < strs.Length; i++)
            {
                res[i] = ValidateString(strs[i]);
            }

            return res;
        }

        #region Async methods
        public Task<Object> GetValueAsync(String tableName, String fields, String cond)
        {
            MySqlCommand command = new MySqlCommand(GetSelectStatement(tableName, fields, cond), connection);

            try
            {
                return command.ExecuteScalarAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                command.Dispose();
            }
        }

        public Task<List<List<Object>>> GetRowsAsync(String tableName, String fields, String cond)
        {
            return GetRowsAsync(GetSelectStatement(tableName, fields, cond));
        }

        public Task<List<List<Object>>> GetRowsUsingJoinAsync(String tables, String columns, String joinConditions,
                                                              String condition, JoinType joinType)
        {
            Char[] separators = new[] { ',', ';' };
            String[] _tables = tables.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            String[] _columns = columns.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            String[] _joinConditions = joinConditions.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            return GetRowsUsingJoinAsync(_tables, _columns, _joinConditions, condition, joinType);
        }
        public Task<List<List<Object>>> GetRowsUsingJoinAsync(IList<String> tables, IEnumerable<String> columns,
                                                              IList<String> joinConditions, String condition, JoinType joinType)
        {
            if (tables == null || tables.Count == 0)
                throw new ArgumentException("Таблицы для выбора не могут отсутствовать.", "tables");
            if (columns == null || columns.Count() == 0)
                throw new ArgumentException("Колонки для выбора не могут отсутствовать.", "columns");
            if (joinConditions == null)
                throw new ArgumentException("Условия для соединения таблиц не могут отсутствовать.", "joinConditions");
            if (joinConditions.Count != tables.Count - 1)
                throw new ArgumentException("Количество условий для соединения таблиц не соответствует " +
                                            "количеству присоеденяемых таблиц (количество условий = количество всех таблиц - 1).", "joinConditions");
            if (condition == null)
                throw new ArgumentNullException("condition");

            String firstTable = tables.First();
            StringBuilder query = new StringBuilder("SELECT ");

            foreach (var column in columns)
            {
                query.Append(column.Trim());
                query.Append(", ");
            }
            query.Remove(query.Length - 2, 1);

            query.Append("FROM ");
            query.Append(firstTable);

            for (int i = 1; i < tables.Count; i++)
            {
                query.AppendFormat(" {0} JOIN {1} ON {2}", joinType, tables[i], joinConditions[i - 1]);
            }

            if (condition != "" && !condition.ToUpper().Contains("WHERE"))
            {
                query.Append(" WHERE ");
            }

            query.AppendFormat(" {0}", condition);

            return GetRowsAsync(query.ToString());
        }

        public async Task<List<List<Object>>> GetRowsAsync(String query)
        {
            List<List<Object>> result = new List<List<Object>>();
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = null;

            try
            {
                await semaphoreSlim.WaitAsync();

                reader = (MySqlDataReader)(await command.ExecuteReaderAsync());

                while (reader.Read())
                {
                    List<Object> row = new List<object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader[i]);
                    }

                    result.Add(row);
                }

                return result;
            }
            catch
            {
                throw;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }
                command.Dispose();

                semaphoreSlim.Release();
            }
        }

        public Task<int> DeleteFromDBAsync(String table, String colName, String colValue)
        {
            return DeleteFromDBAsync("DELETE FROM " + table + " WHERE " + colName + " = " + colValue + ";");
        }
        public Task<int> DeleteFromDBAsync(String table, String[] colName, String[] colValue)
        {
            if (colName.Length == colValue.Length)
            {
                StringBuilder sqlCommand = new StringBuilder("DELETE FROM ");

                sqlCommand.Append(table + " WHERE ");
                for (int i = 0; i < colName.Length - 1; i++)
                {
                    sqlCommand.AppendFormat("{0} = {1} AND ", colName[i], colValue[i]);
                }
                sqlCommand.AppendFormat("{0} = {1};", colName[colName.Length - 1], colValue[colValue.Length - 1]);

                return DeleteFromDBAsync(sqlCommand.ToString());
            }
            else
            {
                throw new ArgumentException("Количество колонок не соответствует количеству значений.");
            }
        }
        private async Task<int> DeleteFromDBAsync(String query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(query);
            }

            MySqlCommand deleteCmd = new MySqlCommand(query, connection);

            try
            {
                await semaphoreSlim.WaitAsync();

                return await deleteCmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                deleteCmd.Dispose();

                semaphoreSlim.Release();
            }
        }

        public Task<int> InsertToBDAsync(String table, String list)
        {
            String sqlCommand = "INSERT INTO " + table + " VALUES(" + list + ");" + " select last_insert_id();";

            return InsertToBDAsync(sqlCommand);
        }
        public Task<int> InsertToBDAsync(String table, String[] fieldNames, String[] fieldValues)
        {
            if (fieldNames.Length == fieldValues.Length)
            {
                StringBuilder sqlCommand = new StringBuilder();
                sqlCommand.AppendFormat("INSERT INTO {0} (", table);

                for (int i = 0; i < fieldNames.Length - 1; i++)
                {
                    sqlCommand.AppendFormat(" {0}, ", fieldNames[i]);
                }

                sqlCommand.AppendFormat("{0}) VALUES (", fieldNames[fieldNames.Length - 1]);

                for (int i = 0; i < fieldValues.Length - 1; i++)
                {
                    sqlCommand.AppendFormat(" {0}, ", fieldValues[i]);
                }

                sqlCommand.AppendFormat("{0}); select last_insert_id();", fieldValues[fieldValues.Length - 1]);

                return InsertToBDAsync(sqlCommand.ToString());
            }
            else
            {
                throw new ArgumentException("Field and Value list dont match.");
            }
        }
        private async Task<int> InsertToBDAsync(String query)
        {
            MySqlCommand insertCmd = new MySqlCommand(query, connection);

            try
            {
                await semaphoreSlim.WaitAsync();

                return (int?)await insertCmd.ExecuteScalarAsync() ?? -1;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                insertCmd.Dispose();

                semaphoreSlim.Release();
            }
        }


        #endregion

        public void Dispose()
        {
            if (!disposed)
            {
                Disconnect();

                connectionString = null;

                if (connection != null)
                {
                    connection.Dispose();
                    connection = null;
                }

                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }

                disposed = true;
            }
        }
    }
}