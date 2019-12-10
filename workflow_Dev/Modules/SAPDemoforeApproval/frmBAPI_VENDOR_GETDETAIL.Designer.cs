namespace SAPDemoforeApproval
{
    partial class frmBAPI_VENDOR_GETDETAIL
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
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnGetName = new System.Windows.Forms.Button();
            this.txtCOMPANYCODE = new System.Windows.Forms.TextBox();
            this.lblCOMPANYCODE = new System.Windows.Forms.Label();
            this.txtVendorNO = new System.Windows.Forms.TextBox();
            this.lblCUSTOMERNO = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(103, 121);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(242, 21);
            this.txtName.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 12;
            this.label1.Text = "供应商名称";
            // 
            // btnGetName
            // 
            this.btnGetName.Location = new System.Drawing.Point(231, 80);
            this.btnGetName.Name = "btnGetName";
            this.btnGetName.Size = new System.Drawing.Size(114, 23);
            this.btnGetName.TabIndex = 11;
            this.btnGetName.Text = "获取供应商名称";
            this.btnGetName.UseVisualStyleBackColor = true;
            this.btnGetName.Click += new System.EventHandler(this.btnGetName_Click);
            // 
            // txtCOMPANYCODE
            // 
            this.txtCOMPANYCODE.Location = new System.Drawing.Point(103, 39);
            this.txtCOMPANYCODE.Name = "txtCOMPANYCODE";
            this.txtCOMPANYCODE.Size = new System.Drawing.Size(242, 21);
            this.txtCOMPANYCODE.TabIndex = 10;
            // 
            // lblCOMPANYCODE
            // 
            this.lblCOMPANYCODE.AutoSize = true;
            this.lblCOMPANYCODE.Location = new System.Drawing.Point(14, 42);
            this.lblCOMPANYCODE.Name = "lblCOMPANYCODE";
            this.lblCOMPANYCODE.Size = new System.Drawing.Size(71, 12);
            this.lblCOMPANYCODE.TabIndex = 9;
            this.lblCOMPANYCODE.Text = "COMPANYCODE";
            // 
            // txtVendorNO
            // 
            this.txtVendorNO.Location = new System.Drawing.Point(103, 12);
            this.txtVendorNO.Name = "txtVendorNO";
            this.txtVendorNO.Size = new System.Drawing.Size(242, 21);
            this.txtVendorNO.TabIndex = 8;
            this.txtVendorNO.Tag = "";
            this.txtVendorNO.Text = "0000103011";
            // 
            // lblCUSTOMERNO
            // 
            this.lblCUSTOMERNO.AutoSize = true;
            this.lblCUSTOMERNO.Location = new System.Drawing.Point(14, 15);
            this.lblCUSTOMERNO.Name = "lblCUSTOMERNO";
            this.lblCUSTOMERNO.Size = new System.Drawing.Size(47, 12);
            this.lblCUSTOMERNO.TabIndex = 7;
            this.lblCUSTOMERNO.Text = "VENDORN";
            // 
            // frmBAPI_VENDOR_GETDETAIL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 319);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetName);
            this.Controls.Add(this.txtCOMPANYCODE);
            this.Controls.Add(this.lblCOMPANYCODE);
            this.Controls.Add(this.txtVendorNO);
            this.Controls.Add(this.lblCUSTOMERNO);
            this.Name = "frmBAPI_VENDOR_GETDETAIL";
            this.Text = "frmBAPI_VENDOR_GETDETAIL";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetName;
        private System.Windows.Forms.TextBox txtCOMPANYCODE;
        private System.Windows.Forms.Label lblCOMPANYCODE;
        private System.Windows.Forms.TextBox txtVendorNO;
        private System.Windows.Forms.Label lblCUSTOMERNO;
    }
}