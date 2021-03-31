using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using Data;
using Data.Entity;
using UserMap.Helpers;
using UserMap.Services;

namespace UserMap.UserControls
{
    public partial class MainMarkerInfoUC : UserControl, ISavable, IReadOnlyable
    {
        private readonly DBManager dbManager;
        private readonly int editObjId;

        private bool isReadOnly;

        private TypeOfObject previousTypeOfObject;
        private OwerType previousOwnerType;

        private ILogger logger;

        public event EventHandler ElementChanged;

        public MainMarkerInfoUC(int objId)
        {
            InitializeComponent();

            dbManager = new DBManager();

            editObjId = objId;

            logger = new FileLogger();
        }

        public TypeOfObject TypeOfObject => (TypeOfObject)EconomicActivityComboBox.SelectedItem;
        public OwerType OwerType => (OwerType)OwnerShipComboBox.SelectedItem;

        public bool ReadOnly
        {
            get => isReadOnly;
            set
            {
                isReadOnly = value;

                if (isReadOnly)
                {
                    EconomicActivityComboBox.Enabled = false;
                    OwnerShipComboBox.Enabled = false;
                }
                else
                {
                    EconomicActivityComboBox.Enabled = true;
                    OwnerShipComboBox.Enabled = true;
                }
            }
        }

        public bool HasChangedElements()
        {
            if (EconomicActivityComboBox.DataSource == null ||
                OwnerShipComboBox.DataSource == null ||
                previousTypeOfObject == null || previousOwnerType == null)
            {
                return false;
            }

            var typeOfObject = (TypeOfObject)EconomicActivityComboBox.SelectedItem;
            var owerType = (OwerType)OwnerShipComboBox.SelectedItem;

            return typeOfObject.Id != previousTypeOfObject.Id ||
                   owerType.Id != previousOwnerType.Id;
        }

        public void RestoreChanges()
        {
            var economicActivities = (IEnumerable<TypeOfObject>)EconomicActivityComboBox.DataSource;
            var owneTypes = (IEnumerable<OwerType>)OwnerShipComboBox.DataSource;

            var previousEAIndex = economicActivities.IndexOf(economActivity => economActivity.Id == previousTypeOfObject.Id);
            var previousOTIndex = owneTypes.IndexOf(owneType => owneType.Id == previousOwnerType.Id);

            EconomicActivityComboBox.SelectedIndex = previousEAIndex;
            OwnerShipComboBox.SelectedIndex = previousOTIndex;
        }

        public Task SaveChangesAsync()
        {
            var typeOfObject = (TypeOfObject)EconomicActivityComboBox.SelectedItem;
            var owerType = (OwerType)OwnerShipComboBox.SelectedItem;

            var columns = new List<string>();
            var values = new List<string>();

            if (typeOfObject.Id != previousTypeOfObject.Id)
            {
                columns.Add("Type");
                values.Add(typeOfObject.Id.ToString());
            }
            if (owerType.Id != owerType.Id)
            {
                columns.Add("owner_type");
                values.Add(owerType.Id.ToString());
            }

            return dbManager.UpdateRecordAsync("poi", columns, values, "Id = " + editObjId.ToString())
                            .ContinueWith(result =>
                            {
                                if (result.IsCompleted)
                                    MessageBox.Show("Дані маркера успішно оновлені.", "Інформація", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                else
                                {
                                    MessageBox.Show("Не вдалось оновити дані маркера.", "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                    if (result.IsFaulted)
                                        logger.Log(result.Exception);
                                }

                            });
        }

        private void MainMarkerInfoUC_Load(object sender, EventArgs e)
        {
            Task ownershipTask = OwnerShipComboBox.FillComboBoxFromBDAsync(dbManager, "owner_types", "*", "",
                                r => Data.Helpers.Mapper.Map<OwerType>(r),
                                displayComboBoxMember: "Type",
                                valueComboBoxMember: "Id",
                                falultAction: c => c.Items.Add("Не вдалось завантажити типи власності"));

            Task economicActivityTask = EconomicActivityComboBox.FillComboBoxFromBDAsync(dbManager, "type_of_object", "Id, Name, Image_Name", "",
                                r =>
                                {
                                    int id;
                                    int.TryParse(r[0].ToString(), out id);

                                    return new TypeOfObject
                                    {
                                        Id = id,
                                        Name = r[1].ToString(),
                                        ImageName = r[2].ToString()
                                    };
                                },
                                displayComboBoxMember: "Name",
                                valueComboBoxMember: "ImageName",
                                falultAction: c => c.Items.Add("Не вдалось завантажити вид економічної діяльності"));

            Task.WhenAll(ownershipTask, economicActivityTask)
                .ContinueWith(result => dbManager.GetRowsAsync("poi", "Type, owner_type", "Id = " + editObjId.ToString()))
                .Unwrap()
                .ContinueWith(_result =>
                {
                    var economicActivities = (IEnumerable<TypeOfObject>)EconomicActivityComboBox.DataSource;
                    var owneTypes = (IEnumerable<OwerType>)OwnerShipComboBox.DataSource;
                    var res = _result.Result;

                    if (res.Count != 0)
                    {
                        var typeOfObjectId = (int)res[0][0];
                        var ownerTypeId = (int)res[0][1];

                        var eaIndex = economicActivities.IndexOf(economActivity => economActivity.Id == typeOfObjectId);
                        var ownerTypeIndex = owneTypes.IndexOf(ownerType => ownerType.Id == typeOfObjectId);

                        EconomicActivityComboBox.SelectedIndex = eaIndex;
                        OwnerShipComboBox.SelectedIndex = ownerTypeIndex;

                        previousTypeOfObject = economicActivities.ElementAt(eaIndex);
                        previousOwnerType = owneTypes.ElementAt(ownerTypeIndex);

                        PreviouesEALabel.Text = previousTypeOfObject?.Name;
                        PreviousOwnershipLabel.Text = previousOwnerType?.Type;
                    }
                    else
                    {
                        PreviouesEALabel.Text = "Відсутня інформація по маркеру";
                        PreviousOwnershipLabel.Text = "Відсутня інформація по маркеру";
                        logger.Log("Ошибка при загрузке основных данных объекта по ID = " + editObjId.ToString());
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext())
                .CatchAndLog(logger)
                .ConfigureAwait(false);
        }

        private void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;

            if (comboBox.SelectedIndex == -1 || comboBox.DataSource == null)
                return;

            OnElemenChanged();

            switch (comboBox.Tag)
            {
                case "1":
                    NewEALabel.Text = ((TypeOfObject)comboBox.SelectedItem).Name;
                    break;
                case "2":
                    NewOwnershipLabel.Text = ((OwerType)comboBox.SelectedItem).Type;
                    break;
                default:
                    break;
            }
        }

        private void OnElemenChanged()
        {
            ElementChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
