using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BMSZ
{
    public partial class Add_Alter_Class : Form
    {
        public string HBFL = "";
        public string MC = "";
        public bool alter = false;
        bool success = false;
        public Add_Alter_Class()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == ""  || textBox2.Text == "")
            {
                MessageBox.Show("编号和类别不得为空！");
                return;
            }

            try
            {
                Convert.ToInt32(textBox1.Text.Trim());
            }
            catch
            {
                MessageBox.Show("编号必须为数字！");
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
                form.searchDM_BB_LX();
                this.Close();
            }
            

        }

        private bool Add()
        {

            //检查编号是否有误
            string sql = string.Format("select HBFL from DM_BB_LX where HBFL='{0}'", textBox1.Text.Trim());

            DataTable isExist = new DataTable();
            try
            {
                isExist=GlobalHelper.IDBHelper.ExecuteDataTable(GlobalHelper.GloValue.ZYDB, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }

            if (isExist.Rows.Count > 0)
            {
                MessageBox.Show("该编号已存在，请更换其他编号！");
                return false;
            }

            string sql1 = string.Format("insert into DM_BB_LX (HBFL,MC) values ('{0}','{1}')", textBox1.Text.Trim(), textBox2.Text.Trim());

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
            //更新或插入数据
            string sql =string.Format("update DM_BB_LX set MC='{0}' where HBFL='{1}'",textBox2.Text.Trim(), HBFL);

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

        private void add_alter_del_Load(object sender, EventArgs e)
        {
            if (alter == true)
            {
                textBox1.Text = HBFL;
                textBox1.ReadOnly = true;
                textBox2.Text = MC;
                this.Text = "修改类别";
            }
            else
            {
                this.Text = "新增类别";
            }
        }
    }
}
