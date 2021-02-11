using System;
using System.IO;

namespace UserMap.Services
{
    public class FileLogger : ILogger
    {
        private static readonly string separator = "\n=========================================\n";

        private string filePath;

        public FileLogger() : this(Path.Combine(Environment.CurrentDirectory, "log.txt"))
        {}
        public FileLogger(string filePath)
        {
            FilePath = filePath;
        }

        public string FilePath 
        {
            get { return filePath; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Название пути не может быть пустым.");
                }

                filePath = value;

                if (!File.Exists(filePath))
                {
                    var file = File.CreateText(filePath);

                    file.Close();
                }
            }
        }

        public void Log(string text)
        {
            string formattedText = separator + "Ошибка (" + DateTime.Now.ToString("f") + "):\n" + text;

            using (var streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(formattedText);
            }
        }
        public void Log(Exception ex)
        {
            Log("Причина ошибки: " + ex.Message + "\nВозника в:\n" + ex.StackTrace + separator);
        }
    }
}
