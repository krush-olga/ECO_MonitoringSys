using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserMap.Helpers
{
    /// <include file='Docs/Helpers/ComboBoxExtensionsDoc.xml' path='docs/members[@name="combobox_extensions"]/ComboBoxExtensions/*'/>
    public static class ComboBoxExtensions
    {
        /// <include file='Docs/Helpers/ComboBoxExtensionsDoc.xml' path='docs/members[@name="combobox_extensions"]/FillComboBoxFromBDAsync/*'/>
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

            Action<ComboBox> syncStartFill = SyncStartFill;

            comboBox.Invoke(syncStartFill, comboBox);

            try
            {
                await dbManager.GetRowsAsync(table, columns, condition)
                               .ContinueWith(result =>
                               {
                                   return result.Result.Select(func)
                                                       .ToList();
                               }, TaskContinuationOptions.OnlyOnRanToCompletion)
                               .ContinueWith(result => 
                               {
                                   SyncActionComboBox(comboBox, displayComboBoxMember, valueComboBoxMember,
                                                      falultAction, result.Result);
                               }, System.Threading.CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
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
