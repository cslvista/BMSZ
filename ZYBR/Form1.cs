using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ZYBR
{
    public partial class Form1 : Form
    {
        DataTable ZYBR = new DataTable(); //在院病人
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("在院病人一览（{0}）", GlobalHelper.UserHelper.UT_TITLE);

            string sql = string.Format("select a.HZXM,a.XINGBIE,a.CSRQ,a.RYCHMC,a.RYSJ,b.RYZDNR from T_ZYDJ a" 
                                        + " inner join (select ZYID,[RYZDNR]=stuff((select [RYZDNR]+' ； ' from T_RYZD where ZYID=c.ZYID for xml path('')), 1, 1, '') from T_RYZD c group by ZYID) b on a.ZYID=b.ZYID"
                                        + " where a.ZYZT = '1' and a.RYKBID = '{0}'", GlobalHelper.UserHelper.UT_ID);

            try
            {
                ZYBR = GlobalHelper.IDBHelper.ExecuteDataTable(GlobalHelper.GloValue.ZYDB, sql.ToString());
                gridControl1.DataSource = ZYBR;
                gridView1.BestFitColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message, "提示");
                return;
            }

        }
    }
}
