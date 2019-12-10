namespace SAPDemoforeApproval
{
    partial class frmBAPI_ADDRESSCONTPART_GETDETAIL
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
            this.txtOBJ_ID_P = new System.Windows.Forms.TextBox();
            this.lblCOMPANYCODE = new System.Windows.Forms.Label();
            this.txtOBJ_TYPE_P = new System.Windows.Forms.TextBox();
            this.lblCUSTOMERNO = new System.Windows.Forms.Label();
            this.txtOBJ_TYPE_C = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtOBJ_ID_C = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtIV_CURRENT_COMM_DATA = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtCONTEXT = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(178, 229);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(242, 21);
            this.txtName.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 238);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "联系人姓名";
            // 
            // btnGetName
            // 
            this.btnGetName.Location = new System.Drawing.Point(306, 187);
            this.btnGetName.Name = "btnGetName";
            this.btnGetName.Size = new System.Drawing.Size(114, 23);
            this.btnGetName.TabIndex = 18;
            this.btnGetName.Text = "获取联系人姓名";
            this.btnGetName.UseVisualStyleBackColor = true;
            this.btnGetName.Click += new System.EventHandler(this.btnGetName_Click);
            // 
            // txtOBJ_ID_P
            // 
            this.txtOBJ_ID_P.Location = new System.Drawing.Point(178, 48);
            this.txtOBJ_ID_P.Name = "txtOBJ_ID_P";
            this.txtOBJ_ID_P.Size = new System.Drawing.Size(242, 21);
            this.txtOBJ_ID_P.TabIndex = 17;
            this.txtOBJ_ID_P.Text = "0000001485";
            // 
            // lblCOMPANYCODE
            // 
            this.lblCOMPANYCODE.AutoSize = true;
            this.lblCOMPANYCODE.Location = new System.Drawing.Point(25, 51);
            this.lblCOMPANYCODE.Name = "lblCOMPANYCODE";
            this.lblCOMPANYCODE.Size = new System.Drawing.Size(53, 12);
            this.lblCOMPANYCODE.TabIndex = 16;
            this.lblCOMPANYCODE.Text = "OBJ_ID_P";
            // 
            // txtOBJ_TYPE_P
            // 
            this.txtOBJ_TYPE_P.Location = new System.Drawing.Point(178, 21);
            this.txtOBJ_TYPE_P.Name = "txtOBJ_TYPE_P";
            this.txtOBJ_TYPE_P.Size = new System.Drawing.Size(242, 21);
            this.txtOBJ_TYPE_P.TabIndex = 15;
            this.txtOBJ_TYPE_P.Tag = "";
            this.txtOBJ_TYPE_P.Text = "BUS1006001";
            // 
            // lblCUSTOMERNO
            // 
            this.lblCUSTOMERNO.AutoSize = true;
            this.lblCUSTOMERNO.Location = new System.Drawing.Point(25, 24);
            this.lblCUSTOMERNO.Name = "lblCUSTOMERNO";
            this.lblCUSTOMERNO.Size = new System.Drawing.Size(65, 12);
            this.lblCUSTOMERNO.TabIndex = 14;
            this.lblCUSTOMERNO.Text = "OBJ_TYPE_P";
            // 
            // txtOBJ_TYPE_C
            // 
            this.txtOBJ_TYPE_C.Location = new System.Drawing.Point(178, 75);
            this.txtOBJ_TYPE_C.Name = "txtOBJ_TYPE_C";
            this.txtOBJ_TYPE_C.Size = new System.Drawing.Size(242, 21);
            this.txtOBJ_TYPE_C.TabIndex = 22;
            this.txtOBJ_TYPE_C.Text = "KNA1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "OBJ_TYPE_C";
            // 
            // txtOBJ_ID_C
            // 
            this.txtOBJ_ID_C.Location = new System.Drawing.Point(178, 102);
            this.txtOBJ_ID_C.Name = "txtOBJ_ID_C";
            this.txtOBJ_ID_C.Size = new System.Drawing.Size(242, 21);
            this.txtOBJ_ID_C.TabIndex = 24;
            this.txtOBJ_ID_C.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 105);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 23;
            this.label3.Text = "OBJ_ID_C";
            // 
            // txtIV_CURRENT_COMM_DATA
            // 
            this.txtIV_CURRENT_COMM_DATA.Location = new System.Drawing.Point(178, 159);
            this.txtIV_CURRENT_COMM_DATA.Name = "txtIV_CURRENT_COMM_DATA";
            this.txtIV_CURRENT_COMM_DATA.Size = new System.Drawing.Size(242, 21);
            this.txtIV_CURRENT_COMM_DATA.TabIndex = 26;
            this.txtIV_CURRENT_COMM_DATA.Text = "X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(25, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 12);
            this.label4.TabIndex = 25;
            this.label4.Text = "IV_CURRENT_COMM_DATA";
            // 
            // txtCONTEXT
            // 
            this.txtCONTEXT.Location = new System.Drawing.Point(178, 131);
            this.txtCONTEXT.Name = "txtCONTEXT";
            this.txtCONTEXT.Size = new System.Drawing.Size(242, 21);
            this.txtCONTEXT.TabIndex = 28;
            this.txtCONTEXT.Text = "0005";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 134);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 12);
            this.label5.TabIndex = 27;
            this.label5.Text = "CONTEXT";
            // 
            // frmBAPI_ADDRESSCONTPART_GETDETAIL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 328);
            this.Controls.Add(this.txtCONTEXT);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtIV_CURRENT_COMM_DATA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtOBJ_ID_C);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtOBJ_TYPE_C);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnGetName);
            this.Controls.Add(this.txtOBJ_ID_P);
            this.Controls.Add(this.lblCOMPANYCODE);
            this.Controls.Add(this.txtOBJ_TYPE_P);
            this.Controls.Add(this.lblCUSTOMERNO);
            this.Name = "frmBAPI_ADDRESSCONTPART_GETDETAIL";
            this.Text = "frmBAPI_ADDRESSCONTPART_GETDETAIL";
            this.Load += new System.EventHandler(this.frmBAPI_ADDRESSCONTPART_GETDETAIL_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnGetName;
        private System.Windows.Forms.TextBox txtOBJ_ID_P;
        private System.Windows.Forms.Label lblCOMPANYCODE;
        private System.Windows.Forms.TextBox txtOBJ_TYPE_P;
        private System.Windows.Forms.Label lblCUSTOMERNO;
        private System.Windows.Forms.TextBox txtOBJ_TYPE_C;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtOBJ_ID_C;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtIV_CURRENT_COMM_DATA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtCONTEXT;
        private System.Windows.Forms.Label label5;
    }
}