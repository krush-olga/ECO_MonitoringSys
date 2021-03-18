using System;
using System.IO;
using System.Text;

namespace UserMap.Services
{
    public class FileLogger : ILogger
    {
        private static readonly string separator = "\n=========================================\n";

        private string filePath;

        private TimeSpan fileCloseTimeOut;

        public FileLogger() : this(Path.Combine(Environment.CurrentDirectory, "log.txt"))
        {}
        public FileLogger(string filePath) : this(filePath, TimeSpan.FromMilliseconds(5000))
        { }
        public FileLogger(string filePath, TimeSpan closeTimeOut)
        {
            FilePath = filePath;
            FileCloseTimeOut = closeTimeOut;
        }

        public string FilePath 
        {
            get => filePath;
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

        public TimeSpan FileCloseTimeOut
        {
            get => fileCloseTimeOut;
            set
            {
                fileCloseTimeOut = value;
            }
        }

        public void Log(string text)
        {
            string formattedText = separator + DateTime.Now.ToString("f") + ": " + text;

            using (var streamWriter = new StreamWriter(filePath, true))
            {
                streamWriter.WriteLine(formattedText);
            }
        }
        public void Log(Exception ex)
        {
            StringBuilder errorStringBuilder = new StringBuilder();

            if (ex is AggregateException aggregateException)
            {
                errorStringBuilder.Append("Возникло несколько исключений:\n");

                foreach (var exception in aggregateException.Flatten().InnerExceptions)
                {
                    errorStringBuilder.Append(GetFormattingExcpetionMessage(exception, 0));
                    errorStringBuilder.Append("\n");
                }
            }
            else if (ex is System.Threading.Tasks.TaskCanceledException canceledExcpetion)
            {
                errorStringBuilder.Append("Задача была отменена. Была отменена в: \n");
                errorStringBuilder.Append(canceledExcpetion.StackTrace);
            }
            else
            {
                errorStringBuilder.Append("Возникло исключение:\n");
                errorStringBuilder.Append(GetFormattingExcpetionMessage(ex, 0));

                errorStringBuilder.Append("Возникла в:\n");
                errorStringBuilder.Append(ex.StackTrace);
            }

            errorStringBuilder.Append("\n");

            Log(errorStringBuilder.ToString());
        }

        private string GetFormattingExcpetionMessage(Exception exception, int gapAmount)
        {
            StringBuilder messageString = new StringBuilder();
            messageString.Append(' ', gapAmount)
                         .Append(exception.GetType().Name)
                         .Append(": ")
                         .Append(exception.Message)
                         .Append('\n');

            if (exception.InnerException != null)
            {
                messageString.Append(GetFormattingExcpetionMessage(exception.InnerException, gapAmount + 3));
            }
            else
            {
                messageString.Append(' ', gapAmount)
                             .Append("Стек вызова:\n")
                             .Append(exception.StackTrace)
                             .Append('\n');
            }

            return messageString.ToString();
        }
    }
}
