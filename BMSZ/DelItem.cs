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
    public partial class DelItem : Form
    {
        public string HBFL = "";
        public string MC = "";
        public string BB_ID = "";
        public string BB_MC = "";

        public DelItem()
        {
            InitializeComponent();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            string sql = string.Format("select BB_ID from M_BB_SF where BB_ID='{0}'",BB_ID);

            DataTable isExists = new DataTable();

            try
            {
                isExists=GlobalHelper.IDBHelper.ExecuteDataTable(GlobalHelper.GloValue.ZYDB, sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            if (isExists.Rows.Count>0)
            {
                MessageBox.Show("该项不可删除！");
                return;
            }

            string sql1 = string.Format("delete from DM_BB where  BB_ID='{0}'", BB_ID);

            try
            {
                GlobalHelper.IDBHelper.ExecuteNonQuery(GlobalHelper.GloValue.ZYDB, sql1);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

        }

        private void DelItem_Load(object sender, EventArgs e)
        {
            label1.Text = "标本编号： " + BB_ID;
            label2.Text= "标本名称： " + BB_MC;
        }

    }
}
