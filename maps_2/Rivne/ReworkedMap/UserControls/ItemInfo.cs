using System;
using UserMap.Core;
using System.Windows.Forms;

namespace UserMap.UserControls
{
    public partial class ItemInfo : UserControl
    {
        public ItemInfo()
        {
            InitializeComponent();
        }

        public void SetData(IDescribable describableEntity)
        {
            ObjectTypeLabel.Text = describableEntity.Type;
            NameTextBox.Text = describableEntity.Name;
            DescriptionTextBox.Text = describableEntity.Description;
            CreatorNameLabel.Text = describableEntity.CreatorFullName;
            ExpertLabel.Text = GetStringRole(describableEntity.CreatorRole);
        }
        public void ClearData()
        {
            ObjectTypeLabel.Text = string.Empty;
            NameTextBox.Text = string.Empty;
            DescriptionTextBox.Text = string.Empty;
            CreatorNameLabel.Text = string.Empty;
            ExpertLabel.Text = string.Empty;
        }

        public void HideDeleteButton()
        {
            DeleteButton.Visible = false;

            AdditionInfoButton.Location = new System.Drawing.Point(AdditionInfoButton.Location.X + DeleteButton.Width / 2 + 10, AdditionInfoButton.Location.Y);
            ChangeButton.Location = new System.Drawing.Point(ChangeButton.Location.X - DeleteButton.Width / 2 - 10, ChangeButton.Location.Y);
        }
        public void ShowDeleteButton()
        {
            DeleteButton.Visible = true;

            AdditionInfoButton.Location = new System.Drawing.Point(AdditionInfoButton.Location.X - DeleteButton.Width / 2 - 10, AdditionInfoButton.Location.Y);
            ChangeButton.Location = new System.Drawing.Point(ChangeButton.Location.X + DeleteButton.Width / 2 + 10, ChangeButton.Location.Y);
        }

        public void SubscriChangeItemClickEvent(EventHandler eventHandler)
        {
            ChangeButton.Click += eventHandler;
        }
        public void DescribeChangeItemClickEvent(EventHandler eventHandler)
        {
            ChangeButton.Click -= eventHandler;
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

        private void HideButton_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
