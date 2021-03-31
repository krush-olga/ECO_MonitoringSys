using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UserMap.Helpers;
using Data;
using Data.Entity;
using UserMap.ViewModel;

namespace UserMap.HelpWindows
{
    public partial class MultiBindingObjectEditor : Form, Services.IReadOnlyable
    {
        private bool isReadOnly;

        private List<Services.ISavable> savables;
        private List<Services.IReadOnlyable> readOnlyables;

        private Services.ILogger logger;

        public MultiBindingObjectEditor()
        {
            InitializeComponent();

            savables = new List<Services.ISavable>();
            readOnlyables = new List<Services.IReadOnlyable>();

            logger = new Services.FileLogger();
        }

        public bool ReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = true;

                foreach (var readOnlyable in readOnlyables)
                {
                    readOnlyable.ReadOnly = value;
                }
            }
        }

        /// <summary>
        /// Добавляет новую страницу в елемент <see cref="TabControl"/> с контентом.
        /// Контент может представлять <see cref="Control"/> и <see cref="Services.ISavable"/>.
        /// </summary>
        /// <param name="pageName">Название новой страницы.</param>
        /// <param name="content">Контент, который будет пресутствовать на странице.</param>
        public void AddNewPage(string pageName, object content)
        {
            bool isSavable = false;

            if (content is Services.ISavable savable)
            {
                savables.Add(savable);
                isSavable = true;
                savable.ElementChanged += Savable_ElementChanged;
            }

            if (content is Services.IReadOnlyable readOnlyable)
            {
                readOnlyables.Add(readOnlyable);
                readOnlyable.ReadOnly = ReadOnly;
            }

            var tabPages = ContentContainerTabControl.TabPages;
            TabPage tabPage = null;

            if (tabPages.Count >= savables.Count && isSavable)
            {
                tabPages.Insert(savables.Count - 1, pageName);
                tabPage = tabPages[savables.Count - 1];
            }
            else
            {
                tabPages.Add(pageName);
                tabPage = tabPages[tabPages.Count - 1];
            }

            if (content is Control control)
            {
                tabPage.Controls.Add(control);
                control.Dock = DockStyle.Fill;
                control.Margin = new Padding(10);

                int newMinWidth = this.MinimumSize.Width;
                int newMinHeight = this.MinimumSize.Height;
                int widthOffset = this.Width - ContentContainerTabControl.Width;
                int heightOffset = this.Height - ContentContainerTabControl.Height;

                if (newMinWidth < control.MinimumSize.Width + widthOffset)
                {
                    newMinWidth = control.MinimumSize.Width + widthOffset;
                }

                if (newMinHeight < control.MinimumSize.Height + heightOffset + 20)
                {
                    newMinHeight = control.MinimumSize.Height + heightOffset + 20;
                }

                this.MinimumSize = new Size(newMinWidth, newMinHeight);
            }
            else
            {
                Label label = new Label();
                label.Text = content.ToString();
                tabPage.Controls.Add(label);
            }
        }

        private void ChangeSaveAndRestoreButtons(Services.ISavable savable)
        {
            if (savable.HasChangedElements())
            {
                SaveToBDButton.Enabled = true;
                RestoreButton.Enabled = true;
            }
            else
            {
                SaveToBDButton.Enabled = false;
                RestoreButton.Enabled = false;
            }
        }

        private void Savable_ElementChanged(object sender, EventArgs e)
        {
            var savable = sender as Services.ISavable;

            ChangeSaveAndRestoreButtons(savable);
        }

        private void RestoreEmissionsButton_Click(object sender, EventArgs e)
        {
            var savable = savables[ContentContainerTabControl.SelectedIndex];

            savable.RestoreChanges();
            ChangeSaveAndRestoreButtons(savable);
        }
        private void SaveToBDButton_Click(object sender, EventArgs e)
        {
            var savable = savables[ContentContainerTabControl.SelectedIndex];

            System.Diagnostics.Debug.WriteLine(System.Threading.Thread.CurrentThread.ManagedThreadId);

            savable.SaveChangesAsync()
                   .ContinueWith(result =>
                   {
                       if (result.IsFaulted)
                       {
                           logger.Log(result.Exception);
                       }

                       ChangeSaveAndRestoreButtons(savable);
                   }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void ContentContainerTabControl_TabIndexChanged(object sender, EventArgs e)
        {
            if (ContentContainerTabControl.SelectedIndex < savables.Count)
            {
                SaveToBDButton.Visible = true;
                RestoreButton.Visible = true;

                ChangeSaveAndRestoreButtons(savables[ContentContainerTabControl.SelectedIndex]);
            }
            else
            {
                SaveToBDButton.Visible = false;
                RestoreButton.Visible = false;
            }
        }

        private void MultiBindingObjectEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool changed = false;

            foreach (var savable in savables)
            {
                changed = changed || savable.HasChangedElements();
            }

            if (changed &&
                MessageBox.Show("Існують незбережені зміни. Ви впевнені, що хочете вийти?", "Увага",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }

            if (changed)
            {
                foreach (var savable in savables)
                {
                    savable.RestoreChanges();
                }
            }
        }
    }
}
