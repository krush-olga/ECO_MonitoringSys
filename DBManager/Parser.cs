using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Data
{
    public class Parser
    {
        private String path = String.Empty;
        private char delimeter;
        private Encoding encoding;
        private List<string> headers = new List<string>();

        public List<List<string>> bufferedData = new List<List<string>>();

        /// <summary>
        /// 
        /// </summary>
        public List<object> Headers
        {
            get
            {
                //Зчитувач
                StreamReader reader = new StreamReader(path);
                //Список заголовків з файлу
                List<object> headers = new List<object>();
                try
                {
                    //зчитуємо перший рядок, ділимо за допомогою розділювача на масив
                    headers = reader.ReadLine().Split(delimeter).ToList<object>();

                }
                catch (IOException io)
                {
                    headers = null;
                    throw io;
                }
                finally
                {
                    reader.Close();
                    reader = null;
                }
                return headers;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="delimeter"></param>
        /// <param name="encoding"></param>
        public Parser(String path, char delimeter = ',', Encoding encoding = null)
        {
            this.path = path;
            this.delimeter = delimeter;
            this.encoding = (encoding == null) ? encoding : Encoding.Default;
        }

        /// <summary>
        /// Читає данні з файлу
        /// </summary>
        /// <returns></returns>
        public List<List<string>> readCsv()
        {
            StreamReader stream = new StreamReader(path, encoding);
            List<List<String>> data = new List<List<string>>();
            string line = String.Empty;

            try
            {
                //Зчитуємо перший рядок, який є заголовками файлу
                headers = stream.ReadLine().Split(delimeter).ToList();
                //Якщо зчитаний рядок не пустий, то записуємо його у подвійний список
                while ((line = stream.ReadLine()) != null)
                {
                    data.Add(line.Split(delimeter).ToList());
                }
                //Записуємо дані у змінну для зберігання останніх зчитаних даних
                bufferedData = data;
                return data;
            }
            catch(IOException)
            {
                throw new IOException("Can't read the file");
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                stream.Close();
            }
        
            
        }
    }
}
