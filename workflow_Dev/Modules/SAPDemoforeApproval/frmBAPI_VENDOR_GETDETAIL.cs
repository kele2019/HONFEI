using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAPDemoforeApproval
{
    public partial class frmBAPI_VENDOR_GETDETAIL : Form
    {
        public frmBAPI_VENDOR_GETDETAIL()
        {
            InitializeComponent();
        }

        private void btnGetName_Click(object sender, EventArgs e)
        {
            BAPI_VENDOR_GETDETAIL bapi = new BAPI_VENDOR_GETDETAIL();
            txtName.Text= bapi.GetVendorName(txtVendorNO.Text, txtCOMPANYCODE.Text);
        }
    }
}