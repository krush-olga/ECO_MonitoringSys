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


namespace Maps.OldMap
{
    public partial class Calculations : Form
    {
        private int id_of_user, id_of_exp, comBox2, ownerId;
        private DBManager db;
        private List<List<Object>> list_calc;
        private List<List<Object>> list_params;
        private List<List<Object>> list_issues;
        private NewMap newMap;
        private string comBox1, condition;
        private double lat1, lat2;
        ColorDialog colorDialog1;
        bool add = false, tube = false, marker = false;

        //конструктор для полигона
        public Calculations(int id_of_user, int id_of_exp, ColorDialog color, NewMap map, DBManager db)
        {
            this.id_of_user = id_of_user;
            this.id_of_exp = id_of_exp;
            this.db = db;
            colorDialog1 = color;
            newMap=map;
            InitializeComponent();
            cbIssueFill();
            cbCalcFill();
            list_params = db.GetId();
            for (int i = 0; i < list_params.Count; i++)
                cbParams.Items.Add(list_params[i][0].ToString());
            
        }
        //конструктор для маркера
        public Calculations(int id_of_user, int id_of_exp, string cb1, int cb2, double lat1, double lat2, NewMap map, DBManager db)
        {
            this.id_of_user = id_of_user;
            this.id_of_exp = id_of_exp;
            comBox1 = cb1;
            comBox2 = cb2;
            this.lat1 = lat1;
            this.lat2 = lat2;
            this.db = db;
            newMap = map;
            InitializeComponent();
            cbIssueFill();
            cbCalcFill();
            list_params = db.GetId();
            for (int i = 0; i < list_params.Count; i++)
                cbParams.Items.Add(list_params[i][0].ToString());
            marker = true;
            ownerId = (int)this.db.GetValue("owner_types", "id", $"type = '{comBox1}'");
        }

        //конструктор для водопровода
        public Calculations(int id_of_user, int id_of_exp, NewMap map, DBManager db)
        {
            this.id_of_user = id_of_user;
            this.id_of_exp = id_of_exp;
            this.db = db;
            newMap = map;
            InitializeComponent();
            cbIssueFill();
            cbCalcFill();
            list_params = db.GetId();
            for (int i = 0; i < list_params.Count; i++)
                cbParams.Items.Add(list_params[i][0].ToString());
            tube = true;
        }

        //заполнения параметров и серии расчетов
        private void cbCalc_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndexChange();
        }

        private void Decline_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            //сохранить маркер
            if (marker == true)
            {
                if (cbIssues.SelectedIndex > 0)
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.AddObjectToDB(id_of_user.ToString(), comBox2, ownerId, lat1, lat2, description.Text, name.Text, list_issues[0][0].ToString(),
                                list_calc[cbCalc.SelectedIndex - 1][0].ToString(), list_params[cbParams.SelectedIndex - 1][0].ToString());
                        else
                            newMap.AddObjectToDB(id_of_user.ToString(), comBox2, ownerId, lat1, lat2, description.Text, name.Text, list_issues[0][0].ToString(),
                                list_calc[cbCalc.SelectedIndex - 1][0].ToString(), "0");
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.AddObjectToDB(id_of_user.ToString(), comBox2, ownerId, lat1, lat2, description.Text, name.Text, list_issues[0][0].ToString(), "0", "0");
                        add = true;
                        Close();
                    }
                }
                else
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.AddObjectToDB(id_of_user.ToString(), comBox2, ownerId, lat1, lat2, description.Text, name.Text, "0",
                                list_calc[cbCalc.SelectedIndex - 1][0].ToString(), list_params[cbParams.SelectedIndex - 1][0].ToString());
                        else
                            newMap.AddObjectToDB(id_of_user.ToString(), comBox2, ownerId, lat1, lat2, description.Text, name.Text, "0",
                                list_calc[cbCalc.SelectedIndex - 1][0].ToString(), "0");
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.AddObjectToDB(id_of_user.ToString(), comBox2, ownerId, lat1, lat2, description.Text, name.Text, "0", "0", "0");
                        add = true;
                        Close();
                    }
                }
            }
            //сохранить водопровод
            else if (tube == true)
            {
                if (cbIssues.SelectedIndex > 0)
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.SaveTubeToDB(id_of_user.ToString(), description.Text, name.Text, list_issues[0][0].ToString(), list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                 list_params[cbParams.SelectedIndex - 1][0].ToString());
                        else
                            newMap.SaveTubeToDB(id_of_user.ToString(), description.Text, name.Text, list_issues[0][0].ToString(), list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                  "0");
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.SaveTubeToDB(id_of_user.ToString(), description.Text, name.Text, list_issues[0][0].ToString(), "0", "0");
                        add = true;
                        Close();
                    }
                }
                else
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.SaveTubeToDB(id_of_user.ToString(), description.Text, name.Text, "0", list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                 list_params[cbParams.SelectedIndex - 1][0].ToString());
                        else
                            newMap.SaveTubeToDB(id_of_user.ToString(), description.Text, name.Text, "0", list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                  "0");
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.SaveTubeToDB(id_of_user.ToString(), description.Text, name.Text, "0", "0", "0");
                        add = true;
                        Close();
                    }
                }
            }
            //сохранить полигон
            else
            {
                if (cbIssues.SelectedIndex > 0)
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.SaveCoordinateToDataBase(list_issues[0][0].ToString(), list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                 list_params[cbParams.SelectedIndex - 1][0].ToString(), id_of_user.ToString(), colorDialog1, description.Text, name.Text);
                        else
                            newMap.SaveCoordinateToDataBase(list_issues[0][0].ToString(), list_calc[cbCalc.SelectedIndex][0].ToString(),
                                             "0", id_of_user.ToString(), colorDialog1, description.Text, name.Text);
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.SaveCoordinateToDataBase(list_issues[0][0].ToString(), "0",
                                         "0", id_of_user.ToString(), colorDialog1, description.Text, name.Text);
                        add = true;
                        Close();
                    }
                }
                else
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.SaveCoordinateToDataBase("0", list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                 list_params[cbParams.SelectedIndex - 1][0].ToString(), id_of_user.ToString(), colorDialog1, description.Text, name.Text);
                        else
                            newMap.SaveCoordinateToDataBase("0", list_calc[cbCalc.SelectedIndex][0].ToString(),
                                             "0", id_of_user.ToString(), colorDialog1, description.Text, name.Text);
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.SaveCoordinateToDataBase("0", "0", "0", id_of_user.ToString(), colorDialog1, description.Text, name.Text);
                        add = true;
                        Close();
                    }
                }
            }
        }

        public bool Save()
        {
            List<List<Object>> userName;
            userName = db.GetRows("user", "user_name", $"id_of_user = '{id_of_user}'");
            newMap.currentMarker.ToolTipText = "Експерт: " + userName[0][0] + "\nНазва:" + name.Text + "\nОпис:" + description.Text;
            return add;
        }

        private void cbIssues_SelectedIndexChanged(object sender, EventArgs e)
        {
            IndexChange();
        }

        private void cbIssueFill()
        {
            //заполнить существующие задачи
            if (id_of_exp == 0)
                list_issues = db.GetRows("issues", "name", "");
            else
            {
                string condition = "id_of_expert ='" + id_of_exp + "'";
                list_issues = db.GetRows("calculations_description", "issue_id", condition);
                for (int i = 0; i < list_issues.Count; i++)
                    cbIssues.Items.Add(list_issues[i][0].ToString());
                condition = null;
                if (cbIssues.Items.Count > 1)
                {
                    for (int i = 0; i < cbIssues.Items.Count-1; i++) 
                        condition += "issue_id = '" + cbIssues.Items[i] + "' OR ";
                    condition += "issue_id = '" + cbIssues.Items[cbIssues.Items.Count-1] + "'";
                }
                else if(cbIssues.Items.Count == 0)
                {
                    condition = "";
                }
                else
                    condition = "issue_id = '" + cbIssues.Items[0] + "'";
                list_issues = db.GetRows("issues", "name", condition);
            }
            cbIssues.Items.Clear();
            cbIssues.Items.Add("");
            for (int i = 0; i < list_issues.Count; i++)
                cbIssues.Items.Add(list_issues[i][0].ToString());
        }

        private void name_TextChanged(object sender, EventArgs e)
       {
            if (name.Text != "")
                Confirm.Enabled = true;
            else
                Confirm.Enabled = false;
        }

        private void cbCalcFill()
        {
            //заполнить существующие серии расчетов
            if (id_of_exp != 0)
                list_calc = db.GetRows("calculations_description", "calculation_name", "id_of_expert=" + id_of_exp.ToString());

            if (id_of_exp == 0)
                list_calc = db.GetRows("calculations_description", "calculation_name", "");

            cbCalc.Items.Clear();
            cbCalc.Items.Add("");
            for (int i = 0; i < list_calc.Count; i++)
                cbCalc.Items.Add(list_calc[i][0].ToString());
        }
        
        private void IndexChange()
        {
            if (cbCalc.Text != "")
            {
                if (id_of_exp == 0)
                    condition = "calculation_name ='" + cbCalc.Text + "'";

                else
                    condition = "calculation_name ='" + cbCalc.Text + "' AND id_of_expert ='" + id_of_exp + "'";

                list_issues = db.GetRows("calculations_description", "issue_id", condition);
                condition = "issue_id = '" + list_issues[0][0] + "'";
                list_issues = db.GetRows("issues", "name", condition);

                if (cbIssues.Text == "")
                {
                    //заполнить существующие задачи по расчетам 
                    cbIssues.Items.Clear();
                    cbIssues.Items.Add("");
                    for (int i = 0; i < list_issues.Count; i++)
                        cbIssues.Items.Add(list_issues[i][0].ToString());

                }
                else if(cbIssues.Text != "")
                {
                    for (int i = 0; i < list_issues.Count; i++)
                    {
                        if (cbIssues.Text == list_issues[i][0].ToString())
                            break;
                        else if (i == list_issues.Count - 1)
                        {
                            cbIssues.Items.Clear();
                            cbIssues.Items.Add("");
                        }
                    }

                }
                //заполнить существующие серии params of description
                cbParams.SelectedValue = null;
                cbParams.Text = null;
                cbParams.Items.Clear();
                list_params = db.GetId();
                for (int i = 0; i < list_params.Count; i++)
                    cbParams.Items.Add(list_params[i][0].ToString());
                string id = cbParams.Items[cbCalc.SelectedIndex - 1].ToString();
                condition = "calculations_description.calculation_name ='" + cbCalc.Text +
                    "' and calculations_description.id_of_expert = " + id +
                    " and calculations_description.calculation_number = calculations_result.calculation_number and " +
                    " calculations_result.id_of_expert = " + id +
                    " and formulas.id_of_formula = calculations_result.id_of_formula and formulas.id_of_expert=" + id;
                list_params = db.GetRows("calculations_description, calculations_result, formulas",
                    "formulas.id_of_formula, name_of_formula, description_of_formula, measurement_of_formula, result", condition);
                cbParams.Items.Clear();
                if (list_params.Count > 0)
                {
                    cbParams.Items.Add("");
                    for (int i = 0; i < list_params.Count; i++)
                        cbParams.Items.Add(list_params[i][2].ToString() + "(" + list_params[i][1].ToString() + ")");
                    cbParams.Enabled = true;
                }
            }

            else if (cbCalc.Text == "" && cbIssues.Text == "")
            {
                cbCalcFill();
                cbIssueFill();
                cbParams.Enabled = false;
            }

            else if (cbCalc.Text == "" && cbIssues.Text != "")
            {
                condition = "name = '" + cbIssues.Text + "'";
                list_issues = db.GetRows("issues", "issue_id", condition);
                condition = "issue_id = '" + list_issues[0][0].ToString() + "'";
                list_calc = db.GetRows("calculations_description", "calculation_name", condition);
                cbCalc.Items.Clear();
                cbCalc.Items.Add("");
                if (list_calc.Count != 0)
                {
                    for (int i = 0; i < list_issues.Count; i++)
                        cbCalc.Items.Add(list_calc[i][0].ToString());
                }
                cbParams.Enabled = false;
            }

        }
    }
}
