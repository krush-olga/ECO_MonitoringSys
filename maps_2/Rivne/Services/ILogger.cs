using System;

namespace UserMap.Services
{
    interface ILogger
    {
        string FilePath { get; set; }

        void Log(string text);
        void Log(Exception ex);
    }
}
