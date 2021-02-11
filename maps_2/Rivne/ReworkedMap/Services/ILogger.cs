using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserMap.Services
{
    interface ILogger
    {
        string FilePath { get; set; }

        void Log(string text);
        void Log(Exception ex);
    }
}
