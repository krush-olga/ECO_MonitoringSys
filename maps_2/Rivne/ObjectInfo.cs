using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Data;

namespace Maps
{
    public partial class ObjectInfo : Form
    {
        GMap.NET.WindowsForms.GMapMarker item;
        DBManager db;
        List<List<Object>> list , listIssue, listCalc, listFormul, listIssueId;
        Object idPoligon;
        int id_of_exp;
        string issueId;

        public ObjectInfo(GMap.NET.WindowsForms.GMapMarker item, int id_of_exp, DBManager db)
        {
            InitializeComponent();
            this.item = item;
            this.id_of_exp = id_of_exp;
            this.db = db;
        }

        private void ObjectInfo_Load(object sender, EventArgs e)
        {
            ReLoad();
        }
        void ReLoad()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            list = db.GetRows("poi", "Name_Object, Description, id_of_issue, calculations_description_number, id_of_formula",
                          "Coord_Lat = " + item.Position.Lat.ToString().Replace(',', '.') +
                          " AND " + "Coord_Lng = " + item.Position.Lng.ToString().Replace(',', '.') + ";");
            if (list == null)
            {
                idPoligon = db.GetValue("point_poligon", "Id_of_poligon", "longitude = " +
                    item.Position.Lat.ToString().Replace(',', '.'));
                list = db.GetRows("poligon_calculation_descriprion", "poligon.name, " +
                    "poligon.description, id_of_issue, calculation_description_number, id_of_formula",
                    $"poigon.Id_of_poligon = {idPoligon} OR id_poligon = {idPoligon}; ");
            }
            listIssue = db.GetRows("issues", "name", $"issue_id = {list[0][2].ToString().Replace(";", " OR issue_id = ")}");
            listCalc = db.GetRows("calculations_description", "calculation_name",
                $"calculation_number = {list[0][3].ToString().Replace(";", " OR calculation_number = ")}");
            listFormul = db.GetRows("formulas", "name_of_formula",
                $"id_of_formula = {list[0][4].ToString().Replace(";", " OR id_of_formula = ")}");
            textBox4.Text = list[0][0].ToString();
            textBox5.Text = list[0][1].ToString();
            for (int i = 0; i < listIssue.Count; i++)
            {
                textBox1.Text += listIssue[i][0].ToString();
                if (i != listIssue.Count - 1)
                    textBox1.Text += ", ";
            }
            for (int i = 0; i < listCalc.Count; i++)
            {
                textBox2.Text += listCalc[i][0].ToString();
                if (i != listCalc.Count - 1)
                    textBox2.Text += ", ";
            }
            for (int i = 0; i < listFormul.Count; i++)
            {
                textBox3.Text += listFormul[i][0].ToString();
                if (i != listFormul.Count - 1)
                    textBox3.Text += ", ";
            }

            if (textBox1.Text != "")
            {
                listIssue = db.GetRows("issues", "name", $"name != '{textBox1.Text.Replace(", ", "' OR name != '")}'");
                listIssueId = db.GetRows("issues", "issue_id", $"name = '{textBox1.Text.Replace(", ", "OR name = ")}'");
                for (int i = 0; i < listIssueId.Count; i++)
                {
                    issueId += listIssueId[i][0].ToString();
                    if (i != listIssueId.Count - 1)
                        issueId += ", ";
                }
                listCalc = db.GetRows("calculations_description", "calculation_name", 
                    $"issue_id = {issueId.Replace(", "," OR issue_id = ")}");
            }
            if (textBox2.Text != "")
            {
                listCalc = db.GetRows("calculations_description", "calculation_name",
                    $"calculation_name != '{textBox2.Text.Replace(", ", "' OR calculation_name != '")}'");
                listIssueId = db.GetRows("calculations_description", "issue_id",
                    $"calculation_name = '{textBox2.Text.Replace(", ", "' OR calculation_name = '")}'");
                for (int i = 0; i < listIssueId.Count; i++)
                {
                    issueId += listIssueId[i][0].ToString();
                    if (i != listIssueId.Count - 1)
                        issueId += ", ";
                }
                listIssue = db.GetRows("issues", "name", $"issue_id = {issueId.Replace(", ", " OR issue_id = ")}");
            }

            for (int i = 0; i < listIssue.Count; i++)
                comboBox1.Items.Add(listIssue[i][0]);
            for (int i = 0; i < listCalc.Count; i++)
                comboBox2.Items.Add(listCalc[i][0]);
        }
    }
}
