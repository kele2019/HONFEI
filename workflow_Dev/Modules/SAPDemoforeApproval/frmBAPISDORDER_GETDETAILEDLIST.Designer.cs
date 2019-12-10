namespace SAPDemoforeApproval
{
    partial class frmBAPISDORDER_GETDETAILEDLIST
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txtNET_VAL_HD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetName = new System.Windows.Forms.Button();
            this.txtVBELN = new System.Windows.Forms.TextBox();
            this.lblCUSTOMERNO = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtSOLD_TO = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtPURCH_NO = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtREQ_DATE_H = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtCURRENCY = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtPURCH_NO_C = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtPMNTTRMS = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtCONTACT = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPARTN_ROLE = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNET_VAL_HD
            // 
            this.txtNET_VAL_HD.Location = new System.Drawing.Point(134, 20);
            this.txtNET_VAL_HD.Name = "txtNET_VAL_HD";
            this.txtNET_VAL_HD.Size = new System.Drawing.Size(212, 21);
            this.txtNET_VAL_HD.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "合同总价";
            // 
            // btnGetName
            // 
            this.btnGetName.Location = new System.Drawing.Point(244, 39);
            this.btnGetName.Name = "btnGetName";
            this.btnGetName.Size = new System.Drawing.Size(114, 23);
            this.btnGetName.TabIndex = 32;
            this.btnGetName.Text = "获取销售订单信息";
            this.btnGetName.UseVisualStyleBackColor = true;
            this.btnGetName.Click += new System.EventHandler(this.btnGetName_Click);
            // 
            // txtVBELN
            // 
            this.txtVBELN.Location = new System.Drawing.Point(116, 12);
            this.txtVBELN.Name = "txtVBELN";
            this.txtVBELN.Size = new System.Drawing.Size(242, 21);
            this.txtVBELN.TabIndex = 29;
            this.txtVBELN.Tag = "";
            this.txtVBELN.Text = "0500297109";
            // 
            // lblCUSTOMERNO
            // 
            this.lblCUSTOMERNO.AutoSize = true;
            this.lblCUSTOMERNO.Location = new System.Drawing.Point(27, 15);
            this.lblCUSTOMERNO.Name = "lblCUSTOMERNO";
            this.lblCUSTOMERNO.Size = new System.Drawing.Size(35, 12);
            this.lblCUSTOMERNO.TabIndex = 28;
            this.lblCUSTOMERNO.Text = "VBELN";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtSOLD_TO);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtPURCH_NO);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtREQ_DATE_H);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtCURRENCY);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtNET_VAL_HD);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(29, 68);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(811, 109);
            this.groupBox1.TabIndex = 36;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ORDER_HEADERS_OUT";
            // 
            // txtSOLD_TO
            // 
            this.txtSOLD_TO.Location = new System.Drawing.Point(134, 74);
            this.txtSOLD_TO.Name = "txtSOLD_TO";
            this.txtSOLD_TO.Size = new System.Drawing.Size(212, 21);
            this.txtSOLD_TO.TabIndex = 42;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 77);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 41;
            this.label5.Text = "客户编号";
            // 
            // txtPURCH_NO
            // 
            this.txtPURCH_NO.Location = new System.Drawing.Point(484, 50);
            this.txtPURCH_NO.Name = "txtPURCH_NO";
            this.txtPURCH_NO.Size = new System.Drawing.Size(242, 21);
            this.txtPURCH_NO.TabIndex = 40;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(395, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 39;
            this.label4.Text = "项目号";
            // 
            // txtREQ_DATE_H
            // 
            this.txtREQ_DATE_H.Location = new System.Drawing.Point(134, 47);
            this.txtREQ_DATE_H.Name = "txtREQ_DATE_H";
            this.txtREQ_DATE_H.Size = new System.Drawing.Size(212, 21);
            this.txtREQ_DATE_H.TabIndex = 38;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 12);
            this.label3.TabIndex = 37;
            this.label3.Text = "客户要求的出厂日期";
            // 
            // txtCURRENCY
            // 
            this.txtCURRENCY.Location = new System.Drawing.Point(484, 20);
            this.txtCURRENCY.Name = "txtCURRENCY";
            this.txtCURRENCY.Size = new System.Drawing.Size(242, 21);
            this.txtCURRENCY.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(395, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 35;
            this.label2.Text = "合同币别";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtPURCH_NO_C);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.txtPMNTTRMS);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Location = new System.Drawing.Point(29, 203);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(811, 68);
            this.groupBox2.TabIndex = 37;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "BAPISDBUSI";
            // 
            // txtPURCH_NO_C
            // 
            this.txtPURCH_NO_C.Location = new System.Drawing.Point(484, 20);
            this.txtPURCH_NO_C.Name = "txtPURCH_NO_C";
            this.txtPURCH_NO_C.Size = new System.Drawing.Size(242, 21);
            this.txtPURCH_NO_C.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(395, 23);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 12);
            this.label9.TabIndex = 35;
            this.label9.Text = "项目号";
            // 
            // txtPMNTTRMS
            // 
            this.txtPMNTTRMS.Location = new System.Drawing.Point(134, 20);
            this.txtPMNTTRMS.Name = "txtPMNTTRMS";
            this.txtPMNTTRMS.Size = new System.Drawing.Size(212, 21);
            this.txtPMNTTRMS.TabIndex = 34;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(15, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(53, 12);
            this.label10.TabIndex = 33;
            this.label10.Text = "付款条款";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView1);
            this.groupBox3.Controls.Add(this.txtCONTACT);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txtPARTN_ROLE);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(29, 297);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(811, 256);
            this.groupBox3.TabIndex = 38;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "ORDER_PARTNERS_OUT";
            // 
            // txtCONTACT
            // 
            this.txtCONTACT.Location = new System.Drawing.Point(484, 20);
            this.txtCONTACT.Name = "txtCONTACT";
            this.txtCONTACT.Size = new System.Drawing.Size(242, 21);
            this.txtCONTACT.TabIndex = 36;
            this.txtCONTACT.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(395, 23);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 35;
            this.label6.Text = "联系人编号";
            this.label6.Visible = false;
            // 
            // txtPARTN_ROLE
            // 
            this.txtPARTN_ROLE.Location = new System.Drawing.Point(134, 20);
            this.txtPARTN_ROLE.Name = "txtPARTN_ROLE";
            this.txtPARTN_ROLE.Size = new System.Drawing.Size(212, 21);
            this.txtPARTN_ROLE.TabIndex = 34;
            this.txtPARTN_ROLE.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 23);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 33;
            this.label7.Text = "人员类型";
            this.label7.Visible = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 47);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(709, 189);
            this.dataGridView1.TabIndex = 37;
            // 
            // frmBAPISDORDER_GETDETAILEDLIST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 586);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnGetName);
            this.Controls.Add(this.txtVBELN);
            this.Controls.Add(this.lblCUSTOMERNO);
            this.Name = "frmBAPISDORDER_GETDETAILEDLIST";
            this.Text = "frmBAPISDORDER_GETDETAILEDLIST";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtNET_VAL_HD;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetName;
        private System.Windows.Forms.TextBox txtVBELN;
        private System.Windows.Forms.Label lblCUSTOMERNO;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtSOLD_TO;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtPURCH_NO;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtREQ_DATE_H;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCURRENCY;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtPURCH_NO_C;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtPMNTTRMS;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtCONTACT;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtPARTN_ROLE;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}