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
    public partial class ZYHZ : Form
    {
        DataTable ZYBR = new DataTable(); //在院病人
        public ZYHZ()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = string.Format("在院患者一览（{0}）", GlobalHelper.UserHelper.UT_TITLE);

            string sql = string.Format("select a.HZXM,a.XINGBIE,a.CSRQ,a.CHMC,a.RYSJ,b.RYZDNR from T_ZYDJ a" 
                                        + " inner join (select ZYID,[RYZDNR]=stuff((select [RYZDNR]+' ； ' from T_RYZD where ZYID=c.ZYID for xml path('')), 1, 0, '') from T_RYZD c group by ZYID) b on a.ZYID=b.ZYID"
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

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetDataObject(gridView1.GetFocusedDisplayText());
            }
            catch
            {

            }
        }
    }
}
