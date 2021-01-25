using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maps.HelpWindows;

namespace Maps.ViewModel
{
    public abstract class Loader<T>
    {
        private ItemConfigurationWindow ItemConfigurationWindow;
        private Data.DBManager dbManager;


        public abstract T LoadFromDB(string additionalTables, string additionalColumns, 
                                     string additionalJoinConditions,
                                     string additionalConditions);
        public abstract void SaveToDB(T obj);

    }
}
