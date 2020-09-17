
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace Data
{
    public class ParserToDB
    {
        private static Mutex mutex = new Mutex();
        private string table = String.Empty;
        private string[] tableColumns;
        private string[] userColumns;

        private List<List<string>> parsedData = new List<List<string>>();
        private DBManager db = new DBManager();
        private Parser parser;

        public bool isDoneUploading = true;

        public ParserToDB(Parser parser, string tableName, string[] tableColumns, string[] userColumns)
        {
            Initialize(tableName, tableColumns, userColumns, parser);
        }
        public ParserToDB(string path, char delimeter, Encoding encoding, string tableName, string[] tableColumns, string[] userColumns)
        {
            Initialize(tableName, tableColumns, userColumns, new Parser(path, delimeter, encoding));
        }
        private void Initialize(string tableName, string[] tableColumns, string[] userColumns, Parser parser)
        {
            this.table = tableName;
            this.parser = parser ?? throw new ArgumentNullException("Parameter is null!");
            this.tableColumns = tableColumns;
            this.userColumns = userColumns;
            this.parsedData = parser.readCsv();
        }

        public async void uploadDataAsync()
        {
            MessageBox.Show("Початок процесу завантаження даних....");
            isDoneUploading = false;
            Stopwatch stopwatch = Stopwatch.StartNew();
            await Task.Run(() => uploadData());
            stopwatch.Stop();
            isDoneUploading = true;
            MessageBox.Show($"Завантаження даних завершено\n Time: {stopwatch.ElapsedMilliseconds}ms");
        }

        public void uploadData()
        {

            #region old
            //List<string> values = new List<string>();

            //Блок коду для формування списку назв колонок, які потрібно зчитати з файлу

            ////Для обраних назв колонки таблиці
            //for (int i = 0; i < tableColumns.Length; i++)
            //{
            //    //Шукаємо відповідну у файлі
            //    for (int j = 0; j < parser.Headers.Count(); j++)
            //    {
            //        //Якщо обрана користувацька назва колонки відповідає назві у файлі
            //        if(userColumns[i].Equals(parser.Headers[j]))
            //        {
            //            //Записуємо позиції колонок
            //            columnsMap[tableColumns[i]] = j;
            //            break;
            //        }
            //    }
            //}
            //Dictionary<string, int> columnsMap = tableColumns.Zip(userColumns, (k, v) => new { k, v })
            //                                                  .ToDictionary(x => x.k, x => parser.Headers.IndexOf(x.v));
            #endregion old
            //отримуємо масив з індексами колонок які потрібно зчитувати
            var indexes = userColumns.Where(column => parser.Headers.Any(element => element.Equals(column)) )
                                     .Select(filteredCol => parser.Headers.IndexOf(filteredCol))
                                     .ToArray();
            try
            {
                Parallel.ForEach(parsedData, (row) => {
                    
                    
                        //беремо значення кожної потрібної колонки та формуємо масив
                        var values = (indexes.Select(index => $"'{row[index]}'").ToArray());

                        DBManager manager = new DBManager();
                        manager.InsertToBD(table, tableColumns, values);
                        manager.Disconnect();
 
                    
                });
                //parsedData.AsParallel()
                //          .Select((row) =>
                //          {
                //              var values = (indexes.Select(index => $"'{row[index]}'").ToArray();
                //              var manager = new DBManager();
                //              manager.InsertToBD(table, tableColumns, values);
                //              manager.Disconnect();
                //          }));
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }

            #region check
            //var filtered_rows = parsedData.Select(row => indexes.Select(index => row[index]).ToArray()).ToArray();
            //foreach (var row in filtered_rows)
            //{
            //    StringBuilder stringBuilder = new StringBuilder();
            //    foreach(var col in row)
            //    {
            //        stringBuilder.Append(col);
            //        stringBuilder.Append(" ");
            //    }
            //    MessageBox.Show(stringBuilder.ToString());
            //    stringBuilder = null;
            //}
            #endregion check

            #region old
            //var complete = parsedData.SelectMany(row => row.IndexOf();
            //foreach(var col in indexes)
            //{
            //    MessageBox.Show($"{col}");
            //}
            ////Для кожного рядка файлу
            //for (dataRowIndex = 1; dataRowIndex < parsedData.Count; dataRowIndex++)
            //{
            //    values.Clear();

            //    mutex.WaitOne();
            //    tmp = dataRowIndex;
            //    mutex.ReleaseMutex();

            //    //Формуємо масив значень, які потрібно записати у таблицю
            //    for (int tableColIndex = 0; tableColIndex < tableColumns.Length; tableColIndex++)
            //    {
            //        //Для колонки таблиці знаходимо відповідну колонку у файлі
            //        int dataColIndex = columnsMap[tableColumns[tableColIndex]];
            //        //записуємо у список всі значення відповідних колонок 
            //        values.Add($"'{parsedData[dataRowIndex][dataColIndex]}'");
            //    }
            //    try
            //    {
            //        db.InsertToBD(table, tableColumns, values.ToArray());
            //    }
            //    catch (Exception e)
            //    {
            //        log.Enqueue($"{e.Message} error at {parsedData[dataRowIndex][0]}");
            //    }
            //}
            #endregion old
        }
    }
}
