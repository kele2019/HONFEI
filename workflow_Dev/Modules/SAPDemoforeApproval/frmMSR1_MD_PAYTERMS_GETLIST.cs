using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAPDemoforeApproval
{
    public partial class frmMSR1_MD_PAYTERMS_GETLIST : Form
    {
        public frmMSR1_MD_PAYTERMS_GETLIST()
        {
            InitializeComponent();
        }

        private void btnGetName_Click(object sender, EventArgs e)
        {
            MSR1_MD_PAYTERMS_GETLIST bapi = new MSR1_MD_PAYTERMS_GETLIST();
            txtName.Text= bapi.GetPaymenttermName(txtPI_LANGU.Text, txtPI_ZTERM.Text);
        }
    }
}