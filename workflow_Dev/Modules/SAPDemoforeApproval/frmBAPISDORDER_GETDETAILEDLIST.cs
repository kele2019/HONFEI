using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SAPDemoforeApproval
{
    public partial class frmBAPISDORDER_GETDETAILEDLIST : Form
    {
        public frmBAPISDORDER_GETDETAILEDLIST()
        {
            InitializeComponent();
        }

        private void btnGetName_Click(object sender, EventArgs e)
        {
            BAPISDORDER_GETDETAILEDLIST bapi = new BAPISDORDER_GETDETAILEDLIST();
            SalesOrder saleOrder= bapi.GetSalesOrder(txtVBELN.Text);

            txtNET_VAL_HD.Text=saleOrder.NET_VAL_HD.ToString();
            txtCURRENCY.Text = saleOrder.CURRENCY;
            txtREQ_DATE_H.Text=saleOrder.REQ_DATE_H;
            txtPURCH_NO.Text=saleOrder.PURCH_NO;
            txtSOLD_TO.Text=saleOrder.SOLD_TO;
            txtPMNTTRMS.Text=saleOrder.PMNTTRMS;
            txtPURCH_NO_C.Text=saleOrder.PURCH_NO_C;
            //txtPARTN_ROLE.Text=saleOrder.PARTN_ROLE;
            //txtCONTACT.Text=saleOrder.CONTACT;
            dataGridView1.DataSource = saleOrder.Contacts;
            

        }
    }
}