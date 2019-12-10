using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAPDemoforeApproval
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmBAPISDORDER_GETDETAILEDLIST frm = new frmBAPISDORDER_GETDETAILEDLIST();
            frm.Show();
        }

        private void btnBAPI_VENDOR_GETDETAIL_Click(object sender, EventArgs e)
        {
            frmBAPI_VENDOR_GETDETAIL frm = new frmBAPI_VENDOR_GETDETAIL();
            frm.Show();
        }

        private void btnBAPI_CUSTOMER_GETDETAIL2_Click(object sender, EventArgs e)
        {
            frmBAPI_CUSTOMER_GETDETAIL2 frm = new frmBAPI_CUSTOMER_GETDETAIL2();
            frm.Show();
        }

        private void btnMSR1_MD_PAYTERMS_GETLIST_Click(object sender, EventArgs e)
        {
            frmMSR1_MD_PAYTERMS_GETLIST frm = new frmMSR1_MD_PAYTERMS_GETLIST();
            frm.Show();
        }

        private void btnBAPI_ADDRESSCONTPART_GETDETAIL_Click(object sender, EventArgs e)
        {
            frmBAPI_ADDRESSCONTPART_GETDETAIL frm = new frmBAPI_ADDRESSCONTPART_GETDETAIL();
            frm.Show();
        }

        private void btnBAPI_GetCurRate_Click(object sender, EventArgs e)
        {
            Form1 oo = new Form1();
            oo.Show();
        }
    }
}