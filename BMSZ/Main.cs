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
    public partial class Main : Form
    {
        DataTable DM_BB = new DataTable();
        DataTable DM_BB_LX = new DataTable();

        public StringBuilder HBFL = new StringBuilder();
        public StringBuilder MC = new StringBuilder();
        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            searchControl1.Properties.NullValuePrompt = " ";

            searchDM_BB_LX();

        }

        public void searchDM_BB_LX()
        {
            string sql = "select HBFL,MC from DM_BB_LX";

            try
            {
                DM_BB_LX = GlobalHelper.IDBHelper.ExecuteDataTable(GlobalHelper.GloValue.ZYDB, sql);
                gridControl1.DataSource = DM_BB_LX;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message);
                return;
            }
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            HBFL.Clear();
            MC.Clear();

            try
            {
                HBFL.Append(gridView1.GetFocusedRowCellValue("HBFL").ToString());
                MC.Append(gridView1.GetFocusedRowCellValue("MC").ToString());
                                
            }
            catch 
            {
                return;
            }


            try
            {
                string sql = string.Format("select a.HBFL,b.MC,a.BB_ID,a.BB_MC,a.ShuoMing from DM_BB a inner join DM_BB_LX b on a.HBFL=b.HBFL where a.HBFL='{0}'", HBFL.ToString());
                DM_BB = GlobalHelper.IDBHelper.ExecuteDataTable(GlobalHelper.GloValue.ZYDB, sql);
                gridControl2.DataSource = DM_BB;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        private void searchControl1_TextChanged(object sender, EventArgs e)
        {
            DM_BB.DefaultView.RowFilter = string.Format("BB_MC like '%{0}%'", searchControl1.Text);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            gridControl1_Click(null, null);
        }

        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            //if (gridView1.GetFocusedRowCellValue("HBFL").ToString())
            //Add_Alter_Item form = new Add_Alter_Item();
            //form.HBFL = gridView2.GetFocusedRowCellValue("HBFL").ToString();
            //form.MC = gridView2.GetFocusedRowCellValue("MC").ToString();
            //form.Show(this);
           
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            searchDM_BB_LX();
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            ButtonAlter_Click(null, null);
        }

        private void ButtonAlter_Click(object sender, EventArgs e)
        {
            try
            {
                Add_Alter_Item form = new Add_Alter_Item();
                form.HBFL = gridView2.GetFocusedRowCellValue("HBFL").ToString();
                form.MC = gridView2.GetFocusedRowCellValue("MC").ToString();
                form.BB_ID = gridView2.GetFocusedRowCellValue("BB_ID").ToString();
                form.BB_MC = gridView2.GetFocusedRowCellValue("BB_MC").ToString();
                form.ShuoMing = gridView2.GetFocusedRowCellValue("ShuoMing").ToString();
                form.alter = true;
                form.Show(this);
            }
            catch
            {

            }

            
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DelItem form = new DelItem();
                form.HBFL = gridView2.GetFocusedRowCellValue("HBFL").ToString();
                form.BB_ID = gridView2.GetFocusedRowCellValue("BB_ID").ToString();
                form.BB_MC = gridView2.GetFocusedRowCellValue("BB_MC").ToString();
                form.Show(this);
            }catch
            {

            }

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Add_Alter_Class form = new Add_Alter_Class();
            form.Show(this);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            string sql = "select a.HBFL,b.MC,a.BB_ID,a.BB_MC,a.ShuoMing from DM_BB a inner join DM_BB_LX b on a.HBFL = b.HBFL";

            try
            {
                DM_BB = GlobalHelper.IDBHelper.ExecuteDataTable(GlobalHelper.GloValue.ZYDB, sql);
                gridControl2.DataSource = DM_BB;
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误:" + ex.Message);
                return;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Add_Alter_Class form = new Add_Alter_Class();
                form.HBFL = gridView1.GetFocusedRowCellValue("HBFL").ToString();
                form.MC = gridView1.GetFocusedRowCellValue("MC").ToString();
                form.alter = true;
                form.Show(this);
            }
            catch
            {

            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            try
            {
                DelClass form = new DelClass();
                form.HBFL = gridView1.GetFocusedRowCellValue("HBFL").ToString();
                form.MC = gridView1.GetFocusedRowCellValue("MC").ToString();
                form.Show(this);
            }
            catch
            {

            }

        }
    }
}
