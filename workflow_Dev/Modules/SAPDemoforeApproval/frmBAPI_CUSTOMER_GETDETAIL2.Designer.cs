namespace SAPDemoforeApproval
{
    partial class frmBAPI_CUSTOMER_GETDETAIL2
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
            this.lblCUSTOMERNO = new System.Windows.Forms.Label();
            this.txtCUSTOMERNO = new System.Windows.Forms.TextBox();
            this.txtCOMPANYCODE = new System.Windows.Forms.TextBox();
            this.lblCOMPANYCODE = new System.Windows.Forms.Label();
            this.btnGetName = new System.Windows.Forms.Button();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCUSTOMERNO
            // 
            this.lblCUSTOMERNO.AutoSize = true;
            this.lblCUSTOMERNO.Location = new System.Drawing.Point(12, 15);
            this.lblCUSTOMERNO.Name = "lblCUSTOMERNO";
            this.lblCUSTOMERNO.Size = new System.Drawing.Size(65, 12);
            this.lblCUSTOMERNO.TabIndex = 0;
            this.lblCUSTOMERNO.Text = "CUSTOMERNO";
            // 
            // txtCUSTOMERNO
            // 
            this.txtCUSTOMERNO.Location = new System.Drawing.Point(101, 12);
            this.txtCUSTOMERNO.Name = "txtCUSTOMERNO";
            this.txtCUSTOMERNO.Size = new System.Drawing.Size(242, 21);
            this.txtCUSTOMERNO.TabIndex = 1;
            this.txtCUSTOMERNO.Text = "0001000018";
            // 
            // txtCOMPANYCODE
            // 
            this.txtCOMPANYCODE.Location = new System.Drawing.Point(101, 39);
            this.txtCOMPANYCODE.Name = "txtCOMPANYCODE";
            this.txtCOMPANYCODE.Size = new System.Drawing.Size(242, 21);
            this.txtCOMPANYCODE.TabIndex = 3;
            // 
            // lblCOMPANYCODE
            // 
            this.lblCOMPANYCODE.AutoSize = true;
            this.lblCOMPANYCODE.Location = new System.Drawing.Point(12, 42);
            this.lblCOMPANYCODE.Name = "lblCOMPANYCODE";
            this.lblCOMPANYCODE.Size = new System.Drawing.Size(71, 12);
            this.lblCOMPANYCODE.TabIndex = 2;
            this.lblCOMPANYCODE.Text = "COMPANYCODE";
            // 
            // btnGetName
            // 
            this.btnGetName.Location = new System.Drawing.Point(253, 80);
            this.btnGetName.Name = "btnGetName";
            this.btnGetName.Size = new System.Drawing.Size(90, 23);
            this.btnGetName.TabIndex = 4;
            this.btnGetName.Text = "获取客户名称";
            this.btnGetName.UseVisualStyleBackColor = true;
            this.btnGetName.Click += new System.EventHandler(this.btnGetName_Click);
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(101, 121);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(242, 21);
            this.txtName.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "客户名称";
            // 
            // frmBAPI_CUSTOMER_GETDETAIL2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 259);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetName);
            this.Controls.Add(this.txtCOMPANYCODE);
            this.Controls.Add(this.lblCOMPANYCODE);
            this.Controls.Add(this.txtCUSTOMERNO);
            this.Controls.Add(this.lblCUSTOMERNO);
            this.Name = "frmBAPI_CUSTOMER_GETDETAIL2";
            this.Text = "frmBAPI_CUSTOMER_GETDETAIL2";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCUSTOMERNO;
        private System.Windows.Forms.TextBox txtCUSTOMERNO;
        private System.Windows.Forms.TextBox txtCOMPANYCODE;
        private System.Windows.Forms.Label lblCOMPANYCODE;
        private System.Windows.Forms.Button btnGetName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
    }
}