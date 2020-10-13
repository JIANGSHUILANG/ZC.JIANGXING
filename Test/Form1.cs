using JiangXinService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZC.DAL.BLL;
using Newtonsoft.Json;
namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string sqlConn = "Database='v06';Data Source='218.93.219.194';User Id='root';Password='123456';charset='utf8';pooling=true;port=9988;";

        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("设备保修预警:GetEquipmentWarrantyWarning", "GetEquipmentWarrantyWarning");
            dic.Add("设备报废预警:GetEquipmentScrapWarning", "GetEquipmentScrapWarning");
            dic.Add("设备检修预警:GetEquipmentMaintenanceWarning", "GetEquipmentMaintenanceWarning");

            BindingSource bs = new BindingSource();
            bs.DataSource = dic;
            comboBox1.DataSource = bs;
            comboBox1.ValueMember = "Value";
            comboBox1.DisplayMember = "Key";

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //
            EquipmentparameterModule module = new EquipmentparameterModule();
            ParameterModel model = new ParameterModel();
            model.PageIndex = 1;
            model.PageSize = 5;
            model.AdvanceDays = Convert.ToInt32(textBox2.Text);
            model.FuncName = comboBox1.SelectedValue.ToString();
            //ReturnMsg msgM = module.GetEquipmentWarning(sqlConn, model);
            //textBox1.Text = JsonConvert.SerializeObject(msgM);
        }
    }
}
