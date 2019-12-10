using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAPDemoforeApproval
{
    public partial class frmBAPI_CUSTOMER_GETDETAIL2 : Form
    {
        public frmBAPI_CUSTOMER_GETDETAIL2()
        {
            InitializeComponent();
        }

        private void btnGetName_Click(object sender, EventArgs e)
        {
            BAPI_CUSTOMER_GETDETAIL2 customer = new BAPI_CUSTOMER_GETDETAIL2();
            txtName.Text= customer.GetCustomerName(txtCUSTOMERNO.Text, txtCOMPANYCODE.Text);
        }
    }
}