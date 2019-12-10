using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAPDemoforeApproval
{
    public partial class frmBAPI_ADDRESSCONTPART_GETDETAIL : Form
    {
        public frmBAPI_ADDRESSCONTPART_GETDETAIL()
        {
            InitializeComponent();
        }

        private void btnGetName_Click(object sender, EventArgs e)
        {
            BAPI_ADDRESSCONTPART_GETDETAIL bapi = new BAPI_ADDRESSCONTPART_GETDETAIL();
            //txtName.Text= bapi.GetContactName();
        }

        private void frmBAPI_ADDRESSCONTPART_GETDETAIL_Load(object sender, EventArgs e)
        {

        }
    }
}