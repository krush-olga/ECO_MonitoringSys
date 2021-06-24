using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HelpModule
{
	public static class Config
	{
		public static string PathToHelp = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ECO_MonitoringSys2020.chm");
		public static string PathToHelpFile = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Керівництво Користувача КЕЕЕМ.pdf");
	}
}
