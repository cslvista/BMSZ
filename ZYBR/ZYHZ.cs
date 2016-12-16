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
        StringBuilder RYKBID = new StringBuilder();//入院科别ID
        public ZYHZ()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            switch (GlobalHelper.UserHelper.UT_TITLE)
            {
                case "妇产科住院部": comboBox1.Text = "妇产科住院部";  break;
                case "新生儿科住院部": comboBox1.Text = "新生儿科住院部"; break;
                case "儿科住院部": comboBox1.Text = "儿科住院部"; break;
                default: comboBox1.Text = ""; break;
            }

            
        }

        private void SearchPatient()
        {
            string sql = string.Format("select a.HZXM,a.XINGBIE,a.CSRQ,a.CHMC,a.RYSJ,b.RYZDNR from T_ZYDJ a"
                                        + " inner join (select ZYID,[RYZDNR]=stuff((select [RYZDNR]+' ； ' from T_RYZD where ZYID=c.ZYID for xml path('')), 1, 0, '') from T_RYZD c group by ZYID) b on a.ZYID=b.ZYID"
                                        + " where a.ZYZT = '1' and a.RYKBID = '{0}'", RYKBID.ToString());

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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RYKBID.Clear();

            switch (comboBox1.Text)
            {
                case "妇产科住院部":RYKBID.Append("A0010001"); break;
                case "新生儿科住院部":RYKBID.Append("A0010003"); break;
                case "儿科住院部": RYKBID.Append("A0010010"); break;
            }

            SearchPatient();
        }
    }
}
