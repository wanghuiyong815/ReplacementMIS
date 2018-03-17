using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ReplacementMIS.common;

namespace ReplacementMIS
{
    public partial class MainFrm : DevExpress.XtraEditors.XtraForm
    {

        private MySqlobj sqlOperator = null; //数据库操作

        public MainFrm()
        {
            InitializeComponent();
        }


        //扫描条形码
        private void txtbarCode_RPbase_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                //根据条形码检索备件库
                SearchRPbase();

            }

        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            sqlOperator = new MySqlobj();
        }


        void SearchRPbase()
        {
            string sql;
            DataSet ds = new DataSet();
            sql = "SELECT * FROM replacementpart where barcode=";
            sql += "'";
            sql += txtbarCode_RPbase.Text;
            sql += "'";
            ds = sqlOperator.GetDataSet(sql);

            gridControl_rpbase.DataSource = ds.Tables[0];


        }


        private void navBarItem1_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
                xtraTabControl1.SelectedTabPage=xtraTabPage_rpbase;
        }




    }
}
