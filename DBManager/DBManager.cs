using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;

namespace Data
{
    public class DBManager
    {
        private MySqlConnection connection;                                                 
        public String connectionString; 
        private MySqlTransaction currentTransaction;

        //Nikita
        public DBManager()
        {
            StreamReader objReader = new StreamReader("init.ini");
            if (!objReader.EndOfStream)
                connectionString = objReader.ReadLine();
            connection = new MySqlConnection(connectionString);
            objReader.Close();
            Connect();
        }

        public DBManager(String connectionString)
        {
            this.connectionString = connectionString;
            connection = new MySqlConnection(connectionString);
            Connect();
        }

        ~DBManager()
        {
            Disconnect();
        }

        public void Connect()
        {
            try
            {
                if(!connection.Ping())
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
            }
        }

        public void RollbackTransaction()
        {
            if (currentTransaction != null)
            {
                currentTransaction.Rollback();
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

        private string GetUpdateStatement(string tableName, string field, string value, string cond)
        {
            String res;
            if(cond != "")
            res = "UPDATE " + tableName + " SET " + field + " = " + value + " WHERE " + cond + " ;";
            else res = "UPDATE " + tableName + " SET " + field + " = " + value +" ;";
            return res;
        }

        /// --IVAN

        public void DeleteFromDB(string table, string colName, string colValue)
        {
            try
            {
                string sqlCommand = "DELETE FROM " + table + " WHERE " + colName + " = " + colValue + " ";
                MySqlCommand deleteCmd = new MySqlCommand(sqlCommand, connection);
                deleteCmd.ExecuteNonQuery();
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void DeleteFromDB(string table, string[] colName, string[] colValue)
        {
            if (colName.Length == colValue.Length)
            {
                if (colName.Length > 1)
                {
                    string sqlCommand = "";
                    string res = "Deleted";
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
                    string colname = colName[0];
                    string colvalue = colValue[0];
                }
            }
            else { throw new ArgumentException("Field and Value list dont match."); }
        }

        public int InsertToBD(string table, string list)
        {
            string sqlCommand = "INSERT INTO " + table + " VALUES(" + list + ");";
            sqlCommand += "select last_insert_id();";
            MySqlCommand insertCmd = new MySqlCommand(sqlCommand, connection);
            return Int32.Parse(insertCmd.ExecuteScalar().ToString());
        }

        public int InsertImageToBD(string table, string list, string path)
        {
            string sqlCommand = "INSERT INTO " + table + " VALUES(" + list + ");";
            sqlCommand += "select last_insert_id();";
            MySqlCommand insertCmd = new MySqlCommand(sqlCommand, connection);
            insertCmd.Parameters.AddWithValue("@file", File.ReadAllBytes(path));
            return Int32.Parse(insertCmd.ExecuteScalar().ToString());
        }


        public int InsertToBD(string table, string[] fieldNames, string[] fieldValues)
        {
            if (fieldNames.Length == fieldValues.Length)
            {
                string sqlCommand = "INSERT INTO " + table + "(";
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

        public void InsertToBDWithoutId(string table, string[] fieldNames, string[] fieldValues)
        {
            if (fieldNames.Length == fieldValues.Length)
            {
                try
                {
                    fieldValues = ValidateStrings(fieldValues);
                    string sqlCommand = "INSERT INTO " + table + "(";
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
                catch(Exception ex)
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

        public int UpdateRecord(string tableName, string[] colNames, string[] colValues)
        {
            if (colNames.Length == colValues.Length)
            {
                colValues = ValidateStrings(colValues);

                string sqlCommand = "UPDATE " + tableName + " SET ";

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

        private string ValidateString(String str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("str");
            }

            if (str[0] == '\'')
                return '\'' + str.Trim('\'').Replace('\'', '`') + '\'';
            return str.Replace('\'', '`');
        }

        private string[] ValidateStrings(string[] strs)
        {
            string[] res = new string[strs.Length];

            for (int i = 0; i < strs.Length; i++)
            {
                res[i] = ValidateString(strs[i]);
            }

            return res;
        }
    }
}