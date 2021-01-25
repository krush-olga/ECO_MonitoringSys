using System;
using Maps.Core;
using System.Windows.Forms;

namespace Maps.UserControls
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

            ChangeButton.Location = new System.Drawing.Point((this.Width - ChangeButton.Width) / 2, ChangeButton.Location.Y);
        }
        public void ShowDeleteButton()
        {
            DeleteButton.Visible = true;

            ChangeButton.Location = new System.Drawing.Point((int)((this.Width + ChangeButton.Width) / 1.5), ChangeButton.Location.Y);
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
