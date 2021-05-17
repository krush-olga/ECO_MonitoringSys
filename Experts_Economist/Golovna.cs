using Data;
using UserMap;
using oprForm;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using HelpModule;
using HelpModule.Forms;
using HelpModule.Models;
using UserLoginForm;

namespace Experts_Economist
{
    public partial class Golovna : Form
    {
        public int id_of_exp; //переменная для хранения id експерта
        public int id_of_user;
        private DBManager db = new DBManager();

        public Golovna(int id) //класс формы, input - id експерта
        {
            InitializeComponent();
            id_of_user = id;
            id_of_exp = (int)db.GetValue("user","id_of_expert",$"id_of_user = '{id_of_user}'");
        }

        private Rozrah RozrahMDIChild;
        private Result ResultMDIChild;
        private Redakt RedaktMDIChild;
        private IssuesForm IssuesMDIChild;
        private Dovidka dovidkaElements;
        private Dovidka dovidkaEnvironment;
        private Dovidka dovidkaGDK;
        private Dovidka dovidkaEmissions;
        private ImportDB importDB_csv;
        private ImportSQL importSQL;
        private ExportDB exportDB;
        private ConnectDB connectDB;
        private MapWindow MapMDIChild;
        private LookEventsForm look;
        private NormOfCalc norm;

        private List<ToolStripMenuItem> _toolStripMenuItems = new List<ToolStripMenuItem>();

        private void Golovna_Load(object sender, EventArgs e)
        {
            label1.Text += " " + this.id_of_exp;
            label2.Text += " " + db.GetValue("expert", "expert_name", "id_of_expert = " + id_of_exp).ToString();
            if (id_of_exp == 0)
            {
                RedaktTSM.Visible = true;
                user_redakt_button.Visible = true;
                базаДанихToolStripMenuItem.Visible = true;
            }

            InitToolStripMenuItems();

            menuStrip1.Items.OfType<ToolStripMenuItem>().ToList().ForEach(x =>
            {
	            x.MouseHover += (obj, arg) => ((ToolStripDropDownItem)obj).ShowDropDown();
            });
        }

        private void MapMDIChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            MapMDIChild.Dispose();
            MapMDIChild = null;
        }

        //событие нажатия на кнопку Розрахунок - запуск формы Rozrah в главном окне(Mdi)
        private void RozrahTSM_Click(object sender, EventArgs e)
        {
            if (RozrahMDIChild == null)
            {
                RozrahMDIChild = new Rozrah();
                RozrahMDIChild.id_of_exp = id_of_exp;
                RozrahMDIChild.MdiParent = this;
                RozrahMDIChild.Show();
                RozrahMDIChild.FormClosed += RozrahMDIChild_FormClosed;
            }
            RozrahMDIChild.BringToFront();
        }

        private void RozrahMDIChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            RozrahMDIChild.Dispose();
            RozrahMDIChild = null;
        }

        //событие нажатия на кнопку Перегляд результатів - запуск формы Result в главном окне(Mdi)
        private void ResultTSM_Click(object sender, EventArgs e)
        {
            if (ResultMDIChild == null)
            {
                ResultMDIChild = new Result();
                ResultMDIChild.id_of_exp = id_of_exp;
                ResultMDIChild.MdiParent = this;
                ResultMDIChild.Show();
                ResultMDIChild.FormClosed += ResultMDIChild_FormClosed;
            }
            ResultMDIChild.BringToFront();
        }

        private void ResultMDIChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            ResultMDIChild.Dispose();
            ResultMDIChild = null;
        }

        //событие нажатия на кнопку Редактор формул - запуск формы Redakt в главном окне(Mdi)
        private void RedaktTSM_Click(object sender, EventArgs e)
        {
            if (RedaktMDIChild == null)
            {
                RedaktMDIChild = new Redakt();
                RedaktMDIChild.MdiParent = this;
                RedaktMDIChild.Show();
                RedaktMDIChild.FormClosed += RedaktMDIChild_FormClosed;
            }
            RedaktMDIChild.BringToFront();
        }

        private void RedaktMDIChild_FormClosed(object sender, FormClosedEventArgs e)
        {
            RedaktMDIChild.Dispose();
            RedaktMDIChild = null;
        }

        private void eventToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlannedEventsForm child = new PlannedEventsForm(id_of_exp);
            child.MdiParent = this;
            child.Show();
        }

        private void переглядЗаходiвToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (look == null)
            {
                look = new LookEventsForm(id_of_exp);
                look.MdiParent = this;
                look.Show();
                look.FormClosed += (send, ev) => { look.Dispose(); look = null; };
            }
            look.BringToFront();
        }

        private void переглядПроблемToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child = new IssuesForm();
            child.MdiParent = this;
            child.Show();
        }

        private void шаблониToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child = new AddTemplateForm();
            child.MdiParent = this;
            child.Show();
        }

        private void додатиРесурсToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child = new AddMaterialForm();
            child.MdiParent = this;
            child.Show();
        }

        private void ресурсиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child = new MaterialsForm();
            child.MdiParent = this;
            child.Show();
        }

        private void змiнитиЗахiдToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child = new AlterEventForm();
            child.MdiParent = this;
            child.Show();
        }

        private void переглядЗаходiвToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            LookEventsForm child = new LookEventsForm(id_of_exp);
            child.MdiParent = this;
            child.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var child = new AlterEventForm();
            child.MdiParent = this;
            child.Show();
        }

        private void eventsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlannedEventsForm child = new PlannedEventsForm(id_of_exp);
            child.MdiParent = this;
            child.Show();
        }

        private void змiнитиШаблонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var child = new AlterTemplateForm();
            child.MdiParent = this;
            child.Show();
        }

        private void user_redakt_button_Click(object sender, EventArgs e)
        {
            try
            {
                new User_editor().ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void картаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MapMDIChild == null)
            {
                MapMDIChild = new MapWindow((Role)id_of_exp, id_of_user);
                MapMDIChild.MdiParent = this;
                MapMDIChild.Show();
                MapMDIChild.FormClosed += (send, ev) => { MapMDIChild.Dispose(); MapMDIChild = null; };
            }
            MapMDIChild.BringToFront();
        }

        private void гДКToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dovidkaElements == null)
            {
                dovidkaElements = new Dovidka("elements", id_of_exp);
                dovidkaElements.Text = "ДОВІДКА - РЕЧОВИНИ";
                dovidkaElements.MdiParent = this;
                dovidkaElements.Show();
                dovidkaElements.FormClosed += (_sender, _event) =>
                {
                    dovidkaElements.Dispose();
                    dovidkaElements = null;
                };
            }
            dovidkaElements.BringToFront();
        }

        private void речовиниToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dovidkaGDK == null)
            {
                dovidkaGDK = new Dovidka("gdk", id_of_exp);
                dovidkaGDK.Text = "ДОВІДКА - ГДК";
                dovidkaGDK.MdiParent = this;
                dovidkaGDK.Show();
                dovidkaGDK.FormClosed += (_sender, _event) =>
                {
                    dovidkaGDK.Dispose();
                    dovidkaGDK = null;
                };
            }
            dovidkaGDK.BringToFront();
        }

        private void середовищеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dovidkaEnvironment == null)
            {
                dovidkaEnvironment = new Dovidka("environment", id_of_exp);
                dovidkaEnvironment.Text = "ДОВІДКА - СЕРЕДОВИЩА";
                dovidkaEnvironment.MdiParent = this;
                dovidkaEnvironment.Show();
                dovidkaEnvironment.FormClosed += (_sender, _event) =>
                {
                    dovidkaEnvironment.Dispose();
                    dovidkaEnvironment = null;
                };
            }
            dovidkaEnvironment.BringToFront();
        }

        private void забруднюючаРечовинаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dovidkaEmissions == null)
            {
                dovidkaEmissions = new Dovidka("emission", id_of_exp);
                dovidkaEmissions.Text = "ДОВІДКА - ЗАБРУДНЕННЯ";
                dovidkaEmissions.MdiParent = this;
                dovidkaEmissions.Show();
                dovidkaEmissions.FormClosed += (_sender, _event) =>
                {
                    dovidkaEmissions.Dispose();
                    dovidkaEmissions = null;
                };
            }
            dovidkaEmissions.BringToFront();
        }

        private void cSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (importDB_csv == null)
            {
                importDB_csv = new ImportDB();
                importDB_csv.MdiParent = this;
                importDB_csv.Show();
                importDB_csv.FormClosed += (_sender, _event) =>
                {
                    importDB_csv.Dispose();
                    importDB_csv = null;
                };
            }
            importDB_csv.BringToFront();
        }

        private void exportSQLToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (exportDB == null)
            {
                exportDB = new ExportDB();
                exportDB.MdiParent = this;
                exportDB.Show();
                exportDB.FormClosed += (_sender, _event) =>
                {
                    exportDB.Dispose();
                    exportDB = null;
                };
            }
            exportDB.BringToFront();
        }

        private void importSQLToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (importSQL == null)
            {
                importSQL = new ImportSQL();
                importSQL.MdiParent = this;
                importSQL.Show();
                importSQL.FormClosed += (_sender, _event) =>
                {
                    importSQL.Dispose();
                    importSQL = null;
                };
            }
            importSQL.BringToFront();
        }

        private void підключитисяToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (connectDB == null)
            {
                connectDB = new ConnectDB();
                connectDB.MdiParent = this;
                connectDB.Show();
                connectDB.FormClosed += (_sender, _event) =>
                {
                    connectDB.Dispose();
                    connectDB = null;
                };
            }
            connectDB.BringToFront();
        }


        private void видЕкномічноїДіяльностіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                MarkersForm mk = new MarkersForm(connectDB, db);
                mk.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Помилка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ставкиПодатківToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dovidkaEmissions == null)
            {
                dovidkaEmissions = new Dovidka("tax_values", id_of_exp);
                dovidkaEmissions.Text = "ДОВІДКА - ПОДАТКИ";
                dovidkaEmissions.MdiParent = this;
                dovidkaEmissions.Show();
                dovidkaEmissions.FormClosed += (_sender, _event) =>
                {
                    dovidkaEmissions.Dispose();
                    dovidkaEmissions = null;
                };
            }
            dovidkaEmissions.BringToFront();
        }


        private void результатиНормуванняToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (norm == null)
            {
                norm = new NormOfCalc(db);
                norm.Show();
                norm.FormClosed += (_sender, _event) =>
                {
                    norm.Dispose();
                    norm = null;
                };
            }
            norm.BringToFront();
        }

        private void LayoutWindowMenuItem_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripMenuItem)
            {
                ToolStripMenuItem menuItem = (ToolStripMenuItem)sender;

                switch (menuItem.Tag?.ToString())
                {
                    case "1":
                        this.LayoutMdi(MdiLayout.Cascade);
                        break;
                    case "2":
                        this.LayoutMdi(MdiLayout.TileVertical);
                        break;
                    case "3":
                        this.LayoutMdi(MdiLayout.TileHorizontal);
                        break;
                    default:
                        this.LayoutMdi(MdiLayout.Cascade);
                        break;
                }
            }
        }

        private void CloseCurrentWindowMenuItem_Click(object sender, EventArgs e)
        {
            this.ActiveMdiChild?.Close();
        }

        private void CloseAllWindowsMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var children in MdiChildren)
            {
                children.Close();
            }
        }

		private void довідкаToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Help.ShowHelp(this, Config.PathToHelp);
        }

		// Событие при нажатии на элемент меню Help-Сценарії
        private void сценаріїToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// ищем форму ListScenarioForm, если ее нет то создаем и показываем
			var form = Application.OpenForms.Cast<Form>().FirstOrDefault(x => x.Name == "ListScenarioForm");
			if (form == null)
			{
				form = new ListScenarioForm();
				form.Show();
				form.FormClosed += delegate
				{
					form.Dispose();
					form = null;
				};
			}
			form.BringToFront();
        }

		private void selectMenuItemToolStrip_SelectedIndexChanged(object sender, EventArgs e)
		{
			_toolStripMenuItems.ForEach(x=>x.BackColor = SystemColors.Control);
			var selectedItem =
				_toolStripMenuItems.FirstOrDefault(x => x.Text == selectMenuItemToolStrip.SelectedItem.ToString());
			if (selectedItem != null)
			{
				selectedItem.BackColor = Color.CornflowerBlue;
				ShowParentMenuItem(selectedItem);
			}
		}

		private void ShowParentMenuItem(ToolStripItem item)
		{
			if (item.OwnerItem is ToolStripMenuItem parent)
			{
                parent.ShowDropDown();
                ShowParentMenuItem(parent);
			}
		}


        private void InitToolStripMenuItems()
		{
			foreach (ToolStripItem toolItem in menuStrip1.Items)
			{
				if (toolItem is ToolStripMenuItem toolStripMenuItem)
				{
					_toolStripMenuItems.Add(toolStripMenuItem);
                }
                _toolStripMenuItems.AddRange(GetItems(toolItem));
			}
        }

        private IEnumerable<ToolStripMenuItem> GetItems(ToolStripItem item)
		{
			if (item is ToolStripMenuItem toolStripMenuItem)
			{
				foreach (ToolStripItem dropDownItem in toolStripMenuItem.DropDownItems)
				{
					if (dropDownItem is ToolStripMenuItem dropDownRecord)
					{
						if (dropDownRecord.HasDropDownItems)
						{
							foreach (var subItem in GetItems(dropDownItem))
								yield return subItem;
						}
						yield return dropDownRecord;
					}
				}
            }
		}

		private void selectMenuItemToolStrip_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter && selectMenuItemToolStrip.SelectedIndex == -1)
			{
				return;
			}

			Help.ShowHelp(this, Config.PathToHelp, HelpNavigator.Topic, $"p{selectMenuItemToolStrip.SelectedIndex}.html");
		}

		private int _tutorial;

		private void startTutorial_Click(object sender, EventArgs e)
		{
			_tutorial = 0;
            if (!panel1.Visible)
			{
				panel1.Visible = true;
				linkLabel1.Click += TutorialClick;
                ShowTutorial(GetHints().FirstOrDefault());
			}
			else
			{
				_toolStripMenuItems.ForEach(x => x.BackColor = SystemColors.Control);
				panel1.Visible = false;
				linkLabel1.Click -= TutorialClick;
			}

		}

		private void ShowTutorial(GolovnaHintModel model)
		{
			_toolStripMenuItems.ForEach(x => x.BackColor = SystemColors.Control);
			var selectedItem =
				_toolStripMenuItems.FirstOrDefault(x => x.Text == model.CurrentItem.ToString());
			if (selectedItem != null)
			{
				selectedItem.BackColor = Color.CornflowerBlue;
				ShowParentMenuItem(selectedItem);
			}
			label3.Text = model.Text;
            _tutorial++;
            if (_tutorial == GetHints().Count())
            {
	            linkLabel1.Text = "Завершити";
            }
        }

		private void TutorialClick(object sender, EventArgs e)
		{
			GolovnaHintModel[] hints = GetHints().ToArray();
			if (_tutorial == hints.Length)
			{
				_toolStripMenuItems.ForEach(x => x.BackColor = SystemColors.Control);
				panel1.Visible = false;
				linkLabel1.Click -= TutorialClick;
				_tutorial = 0;
				linkLabel1.Text = "Далі";
                return;
			}
            if (_tutorial < hints.Length)
				ShowTutorial(hints[_tutorial]);
            
        }

		private void startTutorial_MouseEnter(object sender, EventArgs e)
		{
			startTutorial.Font = new Font(startTutorial.Font, FontStyle.Bold);
		}

		private void startTutorial_MouseLeave(object sender, EventArgs e)
		{
			startTutorial.Font = new Font(startTutorial.Font, FontStyle.Regular);
		}

		private IEnumerable<GolovnaHintModel> GetHints()
		{
			var result = new List<GolovnaHintModel>
            {
                new GolovnaHintModel
                {
                    CurrentItem = переглядПроблемToolStripMenuItem,
                    Text = "Пункт меню \"Задачі\" дозволяє створити нову задачу, переглянути чи відредагувати існуючі задачі"
                },
                new GolovnaHintModel
                {
                    CurrentItem = toolStripMenuItem2,
                    Text = "Пункт меню \"Змінити захід\" дозволяє переглянути та змінити існуючі заходи"
                },
                new GolovnaHintModel
                {
                    CurrentItem = eventsToolStripMenuItem,
                    Text = "Пункт меню \"Новий захід\" дозволяє створити новий захід"
                },
                new GolovnaHintModel
                {
                    CurrentItem = toolStripMenuItem3,
                    Text = "Пункт меню \"Новий шаблон\" дозволяє додати новий шаблон"
                },
                new GolovnaHintModel
                {
                    CurrentItem = toolStripMenuItem6,
                    Text = "Пункт меню \"Змінити шаблон\" дозволяє відредагувати існуючий шаблон"
                },
                new GolovnaHintModel
                {
                    CurrentItem = ресурсиToolStripMenuItem,
                    Text = "Пункт меню \"Ресурси\" дозволяє створити новий ресурс, переглянути, відредагувати чи видалити існуючі ресурси"
                },
                new GolovnaHintModel
                {
                    CurrentItem = RozrahTSM,
                    Text = "Пункт меню \"Розрахунок\" дозволяє переглянути формули, провести і зберегти розрахунки"
                },
                new GolovnaHintModel
                {
                    CurrentItem = ResultTSM,
                    Text = "Пункт меню \"Перегляд результатів\" довзляє переглянути результати розрахунків"
                },
                new GolovnaHintModel
                {
                    CurrentItem = картаToolStripMenuItem,
                    Text = "Пункт меню \"Карта\" відкриває модуль \"Карта\" та дозволяє працювати з нею"
                },
                new GolovnaHintModel
                {
                    CurrentItem = гДКToolStripMenuItem,
                    Text = "Пункт меню \"Речовини\" відкриває вікно \"Довідник\" по речовинам"
                },
                new GolovnaHintModel
                {
                    CurrentItem = речовиниToolStripMenuItem,
                    Text = "Пункт меню \"ГДК\" відкриває вікно \"Довідник\" по ГДК"
                },
                new GolovnaHintModel
                {
                    CurrentItem = середовищеToolStripMenuItem,
                    Text = "Пункт меню \"Середовище\" відкриває вікно \"Довідник\" по середовищу"
                },
                new GolovnaHintModel
                {
                    CurrentItem = видЕкномічноїДіяльностіToolStripMenuItem,
                    Text = "Пункт меню \"Вид екномічної діяльності\" відкриває вікно \"Перегляд економічної діяльності\" де користувач може редагувати та переглядати інформацію про економічну діяльність"
                },
                new GolovnaHintModel
                {
                    CurrentItem = ставкиПодатківToolStripMenuItem,
                    Text = "Пункт меню \"Ставки податків\" відкриває вікно \"Довідка - Податки\""
                },
                new GolovnaHintModel
                {
                    CurrentItem = CascadeMenuItem,
                    Text = "Пункт меню \"Каскадом\" розташовує відкриті вікна каскадом"
                },
                new GolovnaHintModel
                {
                    CurrentItem = VerticalMenuItem,
                    Text = "Пункт меню \"Вертикально\" розташовує відкриті вікна вертикально"
                },
                new GolovnaHintModel
                {
                    CurrentItem = HorizontalMenuItem,
                    Text = "Пункт меню \"Горизонтально\" розташовує відкриті вікна горизонтально"
                },
                new GolovnaHintModel
                {
                    CurrentItem = CloseCurrentWindowMenuItem,
                    Text = "Пункт меню \"Закрити поточне вікно\" призначений для закриття вікна, яке наразі є активним"
                },
                new GolovnaHintModel
                {
                    CurrentItem = CloseAllWindowsMenuItem,
                    Text = "Пункт меню \"Закрити всі вікна\" призначений для закриття всіх відкритих вікон на робочій області"
                }
            };

			if (id_of_exp == 0)
			{
				result.AddRange(
					new[]
					{
						new GolovnaHintModel
						{
							CurrentItem = підключитисяToolStripMenuItem,
							Text =
								"Пункт меню \"Підключитися\" відкриває вікно з змінною параметрів підключення до бази даних"
						},
						new GolovnaHintModel
						{
							CurrentItem = імпортуватиToolStripMenuItem,
							Text =
								"Пункт меню \"Імпортувати\" дозволяє імпортувати дані в базу данних в двох форматах SQL та CSV"
						},
						new GolovnaHintModel
						{
							CurrentItem = експортуватиToolStripMenuItem,
							Text =
								"Пункт меню \"Експортувати\" дозволяє експортувати дані із бази данних в двох форматах SQL та CSV"
						}
					});
			}

			return result;
		}

		private void Golovna_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.F1)
			{
				Help.ShowHelp(this, Config.PathToHelp);
			}
        }
	}
}