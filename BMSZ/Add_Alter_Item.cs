using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BMSZ
{   
    public partial class Add_Alter_Item : Form
    {
        public string HBFL = "";
        public string MC = "";
        public string BB_ID = "";
        public string BB_MC = "";
        public string ShuoMing = "";
        bool success = false;
        public bool alter = false;
        public Add_Alter_Item()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBox3.Text==""  || textBox2.Text == "")
            {
                MessageBox.Show("标本名称和编号不得为空！");
                return;
            }

            if (alter == true)
            {
               success=Alter();
            }
            else
            {
               success = Add();
            }

            //更新主界面
            if (success == true)
            {
                Main form = (Main)this.Owner;
                form.gridControl1_Click(null, null);
                this.Close();
            }
            

        }

        private bool Add()
        {
            try
            {
                Convert.ToInt32(textBox2.Text.Trim());
            }
            catch 
            {
                MessageBox.Show("标本编号必须为数字！");
                return false;
            }

            string sql =string.Format( "select BB_ID from DM_BB where BB_ID='{0}'", textBox2.Text.Trim());

            DataTable isExists = new DataTable();

            try
            {
                isExists=GlobalHelper.IDBHelper.ExecuteDataTable(GlobalHelper.GloValue.ZYDB, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            if (isExists.Rows.Count > 0)
            {
                MessageBox.Show("该编号已经存在，请换另一个编号！");
                return false;
            }


            string sql1 = string.Format("insert into DM_BB (BB_ID,BB_MC,ShuoMing,HBFL) values ('{0}','{1}','{2}','{3}')", textBox2.Text.Trim(), textBox3.Text.Trim(), textBox4.Text.Trim(),HBFL);

            try
            {
                GlobalHelper.IDBHelper.ExecuteNonQuery(GlobalHelper.GloValue.ZYDB, sql1);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private bool Alter()
        {

            string sql = string.Format("update DM_BB set BB_MC='{0}',ShuoMing='{1}' where BB_ID='{2}' and HBFL='{3}'", textBox3.Text.Trim(), textBox4.Text.Trim(), BB_ID, HBFL);

            try
            {
                GlobalHelper.IDBHelper.ExecuteNonQuery(GlobalHelper.GloValue.ZYDB, sql);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            if (alter == true)
            {
                this.Text = "修改";
                textBox1.Text = MC;
                textBox1.ReadOnly = true;
                textBox2.Text = BB_ID;
                textBox2.ReadOnly = true;
                textBox3.Text = BB_MC;
                textBox4.Text = ShuoMing;
            }
            else
            {
                this.Text = "新增";
                textBox1.Text = MC;
                textBox1.ReadOnly = true;
            }
        }
    }
}
