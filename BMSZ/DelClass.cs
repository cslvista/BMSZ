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


    public partial class DelClass : Form
    {
        public string HBFL = "";
        public string MC = "";

        public DelClass()
        {
            InitializeComponent();
        }

        private void DelClass_Load(object sender, EventArgs e)
        {
            label1.Text = "类别编号：  " + HBFL;
            label2.Text = "类别名称：  " + MC;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string sql = string.Format("select top 1 BB_ID from DM_BB where HBFL='{0}'", HBFL);

            DataTable isExists = new DataTable();
            
            //判断是否有子项
            try
            {
                isExists = GlobalHelper.IDBHelper.ExecuteDataTable(GlobalHelper.GloValue.ZYDB, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (isExists.Rows.Count > 0)
            {
                MessageBox.Show("请先删除所有子项！");
                return;
            }

            string sql1 = string.Format("delete from DM_BB_LX  where  HBFL='{0}'", HBFL);

            try
            {
                GlobalHelper.IDBHelper.ExecuteNonQuery(GlobalHelper.GloValue.ZYDB, sql1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            //更新主界面
            Main form = (Main)this.Owner;
            form.searchDM_BB_LX();
            this.Close();
        }
    }
}
