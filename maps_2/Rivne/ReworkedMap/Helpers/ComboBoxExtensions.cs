using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserMap.Helpers
{
    public static class ComboBoxExtensions
    {
        public static async Task FillComboBoxFromBDAsync<TResult>(this ComboBox comboBox, Data.DBManager dbManager, 
                                                                  string table, string columns,
                                                                  string condition, Func<List<object>, TResult> func,
                                                                  string displayComboBoxMember = null,
                                                                  string valueComboBoxMember = null,
                                                                  Action<ComboBox> falultAction = null)
        {
            if (comboBox == null)
                throw new ArgumentNullException("comboBox");
            if (condition == null)
                throw new ArgumentNullException("condition");
            if (func == null)
                throw new ArgumentNullException("func");
            if (dbManager == null)
                throw new ArgumentNullException("dbManager");
            if (string.IsNullOrEmpty(table))
                throw new ArgumentException("Таблица для выбора не может быть пустой.");
            if (columns == null)
                throw new ArgumentException("Колонки для выбора не могут быть пустыми.");


            Action<ComboBox, string, string, Action<ComboBox>, List<TResult>> syncAction = SyncActionComboBox;
            Action<ComboBox> syncStartFill = SyncStartFill;

            comboBox.Invoke(syncStartFill, comboBox);

            try
            {
                var result = (await dbManager.GetRowsAsync(table, columns, condition))
                                             .Select(func)
                                             .ToList();

                comboBox.Invoke(syncAction, comboBox, displayComboBoxMember, valueComboBoxMember, falultAction, result);
            }
            catch
            {
                if (falultAction != null)
                {
                    comboBox.Invoke(falultAction, comboBox);
                }

                throw;
            }
        }

        private static void SyncStartFill(ComboBox comboBox)
        {
            comboBox.Items.Add("Йде завантаження...");
            comboBox.SelectedIndex = 0;
        }
        private static void SyncActionComboBox<TResult>(ComboBox comboBox, string displayComboBoxMember,
                                                         string valueComboBoxMember, Action<ComboBox> falultAction,
                                                         List<TResult> result)
        {
            comboBox.Items.Clear();

            if (result.Count != 0)
            {
                comboBox.DataSource = result;
                comboBox.DisplayMember = displayComboBoxMember;
                comboBox.ValueMember = valueComboBoxMember;
            }
            else if (result != null)
            {
                falultAction(comboBox);
            }
        }
    }
}
