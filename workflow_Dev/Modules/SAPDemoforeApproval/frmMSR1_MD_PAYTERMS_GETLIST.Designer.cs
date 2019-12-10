namespace SAPDemoforeApproval
{
    partial class frmMSR1_MD_PAYTERMS_GETLIST
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
            this.txtPI_ZTERM = new System.Windows.Forms.TextBox();
            this.lblCOMPANYCODE = new System.Windows.Forms.Label();
            this.txtPI_LANGU = new System.Windows.Forms.TextBox();
            this.lblCUSTOMERNO = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 121);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(242, 21);
            this.txtName.TabIndex = 27;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 124);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 26;
            this.label1.Text = "付款条件名称";
            // 
            // btnGetName
            // 
            this.btnGetName.Location = new System.Drawing.Point(248, 80);
            this.btnGetName.Name = "btnGetName";
            this.btnGetName.Size = new System.Drawing.Size(114, 23);
            this.btnGetName.TabIndex = 25;
            this.btnGetName.Text = "获取付款条件名称";
            this.btnGetName.UseVisualStyleBackColor = true;
            this.btnGetName.Click += new System.EventHandler(this.btnGetName_Click);
            // 
            // txtPI_ZTERM
            // 
            this.txtPI_ZTERM.Location = new System.Drawing.Point(120, 39);
            this.txtPI_ZTERM.Name = "txtPI_ZTERM";
            this.txtPI_ZTERM.Size = new System.Drawing.Size(242, 21);
            this.txtPI_ZTERM.TabIndex = 24;
            this.txtPI_ZTERM.Text = "0001";
            // 
            // lblCOMPANYCODE
            // 
            this.lblCOMPANYCODE.AutoSize = true;
            this.lblCOMPANYCODE.Location = new System.Drawing.Point(31, 42);
            this.lblCOMPANYCODE.Name = "lblCOMPANYCODE";
            this.lblCOMPANYCODE.Size = new System.Drawing.Size(53, 12);
            this.lblCOMPANYCODE.TabIndex = 23;
            this.lblCOMPANYCODE.Text = "PI_ZTERM";
            // 
            // txtPI_LANGU
            // 
            this.txtPI_LANGU.Location = new System.Drawing.Point(120, 12);
            this.txtPI_LANGU.Name = "txtPI_LANGU";
            this.txtPI_LANGU.Size = new System.Drawing.Size(242, 21);
            this.txtPI_LANGU.TabIndex = 22;
            this.txtPI_LANGU.Tag = "";
            this.txtPI_LANGU.Text = "EN";
            // 
            // lblCUSTOMERNO
            // 
            this.lblCUSTOMERNO.AutoSize = true;
            this.lblCUSTOMERNO.Location = new System.Drawing.Point(31, 15);
            this.lblCUSTOMERNO.Name = "lblCUSTOMERNO";
            this.lblCUSTOMERNO.Size = new System.Drawing.Size(53, 12);
            this.lblCUSTOMERNO.TabIndex = 21;
            this.lblCUSTOMERNO.Text = "PI_LANGU";
            // 
            // frmMSR1_MD_PAYTERMS_GETLIST
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 316);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetName);
            this.Controls.Add(this.txtPI_ZTERM);
            this.Controls.Add(this.lblCOMPANYCODE);
            this.Controls.Add(this.txtPI_LANGU);
            this.Controls.Add(this.lblCUSTOMERNO);
            this.Name = "frmMSR1_MD_PAYTERMS_GETLIST";
            this.Text = "frmMSR1_MD_PAYTERMS_GETLIST";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetName;
        private System.Windows.Forms.TextBox txtPI_ZTERM;
        private System.Windows.Forms.Label lblCOMPANYCODE;
        private System.Windows.Forms.TextBox txtPI_LANGU;
        private System.Windows.Forms.Label lblCUSTOMERNO;
    }
}