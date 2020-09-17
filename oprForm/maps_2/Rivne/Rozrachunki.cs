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
    public partial class Calculations : Form
    {
        private int id_of_exp, comBox2;
        private DBManager db = new DBManager();
        private List<List<Object>> list_calc;
        private List<List<Object>> list_params;
        private List<List<Object>> list_issues;
        private NewMap newMap;
        private string comBox1;
        private double lat1, lat2;
        ColorDialog colorDialog1;
        bool add = false;
        bool tube = false;

        //конструктор для полигона
        public Calculations(int id, ColorDialog color, NewMap map, DBManager d)
        {
            id_of_exp = id;
            colorDialog1 = color;
            newMap=map;
            db = d;
            InitializeComponent();
            cbIssueFill();
            cbCalcFill();
            list_params = db.GetId();
            for (int i = 0; i < list_params.Count; i++)
                cbParams.Items.Add(list_params[i][0].ToString());
            
        }
        //конструктор для маркера
        public Calculations(int id, string cb1, int cb2, double lat1, double lat2, NewMap map)
        {
            id_of_exp = id;
            comBox1 = cb1;
            comBox2 = cb2;
            this.lat1 = lat1;
            this.lat2 = lat2;
            newMap = map;
            InitializeComponent();
            cbIssueFill();
            cbCalcFill();
            list_params = db.GetId();
            for (int i = 0; i < list_params.Count; i++)
                cbParams.Items.Add(list_params[i][0].ToString());
            description.Enabled = false;
        }

        //конструктор для водопровода
        public Calculations(int id, NewMap map)
        {
            id_of_exp = id;
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
        private void cdCalc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCalc.SelectedIndex > 0)
            {
                string condition;
                if (cbIssues.SelectedIndex == 0)
                {
                    //заполнить существующие задачи по расчетам 
                    cbIssues.Items.Clear();

                    if (id_of_exp == 0)
                        condition = "calculation_name ='" + cbCalc.Text + "'";

                    else
                        condition = "calculation_name ='" + cbCalc.Text + "' AND id_of_expert ='" + id_of_exp + "'";

                    list_issues = db.GetRows("calculations_description", "issue_id", condition);
                    for (int i = 0; i < list_issues.Count; i++)
                        cbIssues.Items.Add(list_issues[i][0].ToString());
                    condition = "issue_id = '" + cbIssues.Items[0] + "'";
                    list_issues = db.GetRows("issues", "name", condition);
                    cbIssues.Items.Clear();
                    for (int i = 0; i < list_issues.Count; i++)
                        cbIssues.Items.Add(list_issues[i][0].ToString());

                }
                //заполнить существующие серии params of description
                cbParams.SelectedValue = null;
                cbParams.Text = null;
                cbParams.Items.Clear();
                list_params = db.GetId();
                for (int i = 0; i < list_params.Count; i++)
                    cbParams.Items.Add(list_params[i][0].ToString());
                string id = cbParams.Items[cbCalc.SelectedIndex-1].ToString();
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
            else if (cbCalc.SelectedIndex == 0)
            {
                cbParams.Enabled = false;
                if(cbIssues.Text == null)
                    cbIssueFill();
            }
        }

        private void Decline_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            //сохранить маркер
            if (description.Enabled == false)//сохранить маркер
            {
                if (cbIssues.SelectedIndex > 0)
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.AddObjectToDB(id_of_exp.ToString(), comBox2, lat1, lat2, comBox1, name.Text, list_issues[0][0].ToString(),
                                list_calc[cbCalc.SelectedIndex - 1][0].ToString(), list_params[cbParams.SelectedIndex - 1][0].ToString());
                        else
                            newMap.AddObjectToDB(id_of_exp.ToString(), comBox2, lat1, lat2, comBox1, name.Text, list_issues[0][0].ToString(),
                                list_calc[cbCalc.SelectedIndex - 1][0].ToString(), "0");
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.AddObjectToDB(id_of_exp.ToString(), comBox2, lat1, lat2, comBox1, name.Text, list_issues[0][0].ToString(), "0", "0");
                        add = true;
                        Close();
                    }
                }
                else
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.AddObjectToDB(id_of_exp.ToString(), comBox2, lat1, lat2, comBox1, name.Text, "0",
                                list_calc[cbCalc.SelectedIndex - 1][0].ToString(), list_params[cbParams.SelectedIndex - 1][0].ToString());
                        else
                            newMap.AddObjectToDB(id_of_exp.ToString(), comBox2, lat1, lat2, comBox1, name.Text, "0",
                                list_calc[cbCalc.SelectedIndex - 1][0].ToString(), "0");
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.AddObjectToDB(id_of_exp.ToString(), comBox2, lat1, lat2, comBox1, name.Text, "0", "0", "0");
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
                            newMap.SaveTubeToDB(id_of_exp.ToString(), description.Text, name.Text, list_issues[0][0].ToString(), list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                 list_params[cbParams.SelectedIndex - 1][0].ToString());
                        else
                            newMap.SaveTubeToDB(id_of_exp.ToString(), description.Text, name.Text, list_issues[0][0].ToString(), list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                  "0");
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.SaveTubeToDB(id_of_exp.ToString(), description.Text, name.Text, list_issues[0][0].ToString(), "0", "0");
                        add = true;
                        Close();
                    }
                }
                else
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.SaveTubeToDB(id_of_exp.ToString(), description.Text, name.Text, "0", list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                 list_params[cbParams.SelectedIndex - 1][0].ToString());
                        else
                            newMap.SaveTubeToDB(id_of_exp.ToString(), description.Text, name.Text, "0", list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                  "0");
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.SaveTubeToDB(id_of_exp.ToString(), description.Text, name.Text, "0", "0", "0");
                        add = true;
                        Close();
                    }
                }
            }
            //сохранить водопровод
            else
            {
                if (cbIssues.SelectedIndex > 0)
                {
                    if (cbCalc.SelectedIndex > 0)
                    {
                        if (cbParams.SelectedIndex > 0)
                            newMap.SaveCoordinateToDataBase(list_issues[0][0].ToString(), list_calc[cbCalc.SelectedIndex - 1][0].ToString(),
                                 list_params[cbParams.SelectedIndex - 1][0].ToString(), id_of_exp.ToString(), colorDialog1, description.Text, name.Text);
                        else
                            newMap.SaveCoordinateToDataBase(list_issues[0][0].ToString(), list_calc[cbCalc.SelectedIndex][0].ToString(),
                                             "0", id_of_exp.ToString(), colorDialog1, description.Text, name.Text);
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.SaveCoordinateToDataBase(list_issues[0][0].ToString(), "0",
                                         "0", id_of_exp.ToString(), colorDialog1, description.Text, name.Text);
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
                                 list_params[cbParams.SelectedIndex - 1][0].ToString(), id_of_exp.ToString(), colorDialog1, description.Text, name.Text);
                        else
                            newMap.SaveCoordinateToDataBase("0", list_calc[cbCalc.SelectedIndex][0].ToString(),
                                             "0", id_of_exp.ToString(), colorDialog1, description.Text, name.Text);
                        add = true;
                        Close();
                    }
                    else
                    {
                        newMap.SaveCoordinateToDataBase("0", "0", "0", id_of_exp.ToString(), colorDialog1, description.Text, name.Text);
                        add = true;
                        Close();
                    }
                }
            }
        }

        public bool Save()
        {
            return add;
        }

        private void cbIssues_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIssues.SelectedIndex > 0 && cbCalc.Text == "")
            {
                string condition = "name = '" + cbIssues.Text + "'";
                list_issues = db.GetRows("issues", "issue_id", condition);
                cbCalc.Items.Clear();
                for (int i = 0; i < list_issues.Count; i++)
                    cbCalc.Items.Add(list_issues[i][0].ToString());
                condition = "issue_id = '" + cbCalc.Items[0] + "'";
                list_calc = db.GetRows("calculations_description", "calculation_name", condition);
                cbCalc.Items.Clear();
                cbCalc.Items.Add("");
                if (list_calc.Count != 0)
                {
                    for (int i = 0; i < list_issues.Count; i++)
                        cbCalc.Items.Add(list_calc[i][0].ToString());
                }
            }
            else if (cbIssues.SelectedIndex == 0)
                cbCalcFill();
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
                if (cbIssues.Items.Count != 1)
                {
                    for (int i = 0; i < cbIssues.Items.Count-1; i++)
                        condition += "issue_id = '" + cbIssues.Items[i] + "' OR ";
                    condition += "issue_id = '" + cbIssues.Items[cbIssues.Items.Count-1] + "'";
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
        
    }
    
}
