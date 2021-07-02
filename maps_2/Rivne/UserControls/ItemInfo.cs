using System;
using UserMap.Core;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;

namespace UserMap.UserControls
{
    public partial class ItemInfo : UserControl
    {
        private string oldName;
        private string oldDescrtiption;

        private Button saveChangesButton;
        private Button cancelChangesButton;

        private IDescribable currentItem;

        public ItemInfo()
        {
            InitializeComponent();

            saveChangesButton = new Button
            {
                Text = "Зберегти",
                Name = "saveChangesButton",
                Height = AdditionInfoButton.Height,
                Location = new Point(AdditionInfoButton.Location.X, AdditionInfoButton.Location.Y + AdditionInfoButton.Height + 10)
            };
            cancelChangesButton = new Button
            {
                Text = "Відмінити",
                Name = "cancelChangesButton",
                Height = DeleteButton.Height,
                Location = new Point(DeleteButton.Location.X, DeleteButton.Location.Y + DeleteButton.Height + 10)
            };

            this.Controls.Add(saveChangesButton);
            this.Controls.Add(cancelChangesButton);
        }

        public string EntityName => NameTextBox.Text;
        public string EntityDescription => DescriptionTextBox.Text;

        public bool IsEditMode => !(NameTextBox.ReadOnly && DescriptionTextBox.ReadOnly);

        public IDescribable CurrentItem => currentItem;

        public void SetData(IDescribable describableEntity)
        {
            if (describableEntity == null)
                throw new ArgumentNullException(nameof(describableEntity));

            ObjectTypeLabel.Text = describableEntity.Type;
            NameTextBox.Text = describableEntity.Name;
            DescriptionTextBox.Text = describableEntity.Description;
            CreatorNameLabel.Text = describableEntity.Creator.ToString();
            ExpertLabel.Text = GetStringRole(describableEntity.Creator.Role);

            currentItem = describableEntity;
        }
        public void ClearData()
        {
            ObjectTypeLabel.Text = string.Empty;
            NameTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
            CreatorNameLabel.Text = string.Empty;
            ExpertLabel.Text = string.Empty;

            currentItem = null;
        }

        public void HideDeleteButton()
        {
            DeleteButton.Visible = false;

            AdditionInfoButton.Location = new Point((this.Width - AdditionInfoButton.Width) / 2, AdditionInfoButton.Location.Y);

            if (IsEditMode)
            {
                LiftAdditionalButtons(AdditionInfoButton);
            }
        }
        public void ShowDeleteButton()
        {
            if (IsEditMode)
            {
                PutAdditionalButtons(AdditionInfoButton);
            }

            DeleteButton.Visible = true;

            AdditionInfoButton.Location = new Point(this.Width / 2 - AdditionInfoButton.Width - 10, AdditionInfoButton.Location.Y);
        }
        public void HideAdditionInfoButton()
        {
            AdditionInfoButton.Visible = false;

            DeleteButton.Location = new Point((this.Width - DeleteButton.Width) / 2, DeleteButton.Location.Y);

            if (IsEditMode)
            {
                LiftAdditionalButtons(DeleteButton);
            }
        }
        public void ShowAdditionInfoButton()
        {
            if (IsEditMode)
            {
                PutAdditionalButtons(DeleteButton);
            }

            AdditionInfoButton.Visible = true;

            DeleteButton.Location = new Point(this.Width / 2 + 10, DeleteButton.Location.Y);
        }

        public void SubscribeDeleteItemClickEvent(EventHandler eventHandler)
        {
            DeleteButton.Click += eventHandler;
        }
        public void DescribeDeleteItemClickEvent(EventHandler eventHandler)
        {
            DeleteButton.Click -= eventHandler;
        }
        public void SubscribeAdditionInfoClickEvent(EventHandler eventHandler)
        {
            AdditionInfoButton.Click += eventHandler;
        }
        public void DescribeAdditionInfoClickEvent(EventHandler eventHandler)
        {
            AdditionInfoButton.Click -= eventHandler;
        }
        public void SubscribeSaveChangesClickEvent(EventHandler eventHandler)
        {
            saveChangesButton.Click += eventHandler;
        }
        public void DescribeSaveChangesClickEvent(EventHandler eventHandler)
        {
            saveChangesButton.Click -= eventHandler;
        }
        public void SubscribeCancleChangesClickEvent(EventHandler eventHandler)
        {
            cancelChangesButton.Click += eventHandler;
        }
        public void DescribeCancleChangesClickEvent(EventHandler eventHandler)
        {
            cancelChangesButton.Click -= eventHandler;
        }

        public void StartEditNameAndDescribe()
        {
            if (ObjectTypeLabel.Text == string.Empty)
                throw new InvalidOperationException("Отсутствует объект для изменения.");

            NameTextBox.ReadOnly = false;
            DescriptionTextBox.ReadOnly = false;

            oldName = NameTextBox.Text;
            oldDescrtiption = DescriptionTextBox.Text;

            this.Height += saveChangesButton.Height + 10;

            if (!AdditionInfoButton.Visible || !DeleteButton.Visible)
            {
                this.Height -= saveChangesButton.Height + 10;

                LiftAdditionalButtons(AdditionInfoButton.Visible ? AdditionInfoButton : DeleteButton);
            }
        }

        public void CancelEditNameAndDescribe()
        {
            if (NameTextBox.ReadOnly)
                return;

            NameTextBox.ReadOnly = true;
            DescriptionTextBox.ReadOnly = true;

            NameTextBox.Text = oldName;
            DescriptionTextBox.Text = oldDescrtiption;

            oldName = null;
            oldDescrtiption = null;

            this.Height -= saveChangesButton.Height + 10;

            if (!AdditionInfoButton.Visible || !DeleteButton.Visible)
            {
                PutAdditionalButtons(AdditionInfoButton.Visible ? AdditionInfoButton : DeleteButton);
            }
        }

        public void EndEditNameAndDescribe()
        {
            NameTextBox.ReadOnly = true;
            DescriptionTextBox.ReadOnly = true;

            oldName = null;
            oldDescrtiption = null;

            this.Height -= saveChangesButton.Height + 10;

            if (!AdditionInfoButton.Visible || !DeleteButton.Visible)
            {
                PutAdditionalButtons(AdditionInfoButton.Visible ? AdditionInfoButton : DeleteButton);
            }
        }

        private string GetStringRole(Data.Role role)
        {
            string res = string.Empty;

            switch (role)
            {
                case Data.Role.Admin:
                    res = "Адміністратор";
                    break;
                case Data.Role.Economist:
                    res = "Економіст";
                    break;
                case Data.Role.Ecologist:
                    res = "Еколог";
                    break;
                case Data.Role.Medic:
                    res = "Медик";
                    break;
                case Data.Role.Lawyer:
                    res = "Юрист";
                    break;
                case Data.Role.Analyst:
                    res = "Аналітик";
                    break;
                case Data.Role.PowerEngineer:
                    res = "Енергетик";
                    break;
                default:
                    break;
            }

            return res;
        }

        private void LiftAdditionalButtons(Button pivotButton)
        {
            saveChangesButton.Location = new Point(pivotButton.Location.X - saveChangesButton.Width - 10, pivotButton.Location.Y);
            cancelChangesButton.Location = new Point(pivotButton.Location.X + pivotButton.Width + 10, pivotButton.Location.Y);
        }

        private void PutAdditionalButtons(Button pivotButton)
        {
            saveChangesButton.Location = new Point(this.Width / 2 - saveChangesButton.Width - 10, pivotButton.Location.Y + saveChangesButton.Height + 10);
            cancelChangesButton.Location = new Point(this.Width / 2 + 10, pivotButton.Location.Y + cancelChangesButton.Height + 10);
        }

        private void HideButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
