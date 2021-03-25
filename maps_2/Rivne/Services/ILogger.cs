using System;

namespace UserMap.Services
{
    /// <summary>
    /// Логирует сообщение или исключение.
    /// </summary>
    interface ILogger
    {
        /// <summary>
        /// Путь файл, в который будет логироваться.
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Логирование строки.
        /// </summary>
        /// <param name="text">Строка для логирования.</param>
        void Log(string text);
        /// <summary>
        /// Логирование исключения.
        /// </summary>
        /// <param name="ex">Исключение для логирования.</param>
        void Log(Exception ex);
    }
}
