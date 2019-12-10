namespace SAPDemoforeApproval
{
    partial class Mainform
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
            this.button1 = new System.Windows.Forms.Button();
            this.btnBAPI_VENDOR_GETDETAIL = new System.Windows.Forms.Button();
            this.btnBAPI_CUSTOMER_GETDETAIL2 = new System.Windows.Forms.Button();
            this.btnMSR1_MD_PAYTERMS_GETLIST = new System.Windows.Forms.Button();
            this.btnBAPI_ADDRESSCONTPART_GETDETAIL = new System.Windows.Forms.Button();
            this.btnBAPI_GetCurRate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(91, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(156, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "获取销售订单信息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnBAPI_VENDOR_GETDETAIL
            // 
            this.btnBAPI_VENDOR_GETDETAIL.Location = new System.Drawing.Point(91, 41);
            this.btnBAPI_VENDOR_GETDETAIL.Name = "btnBAPI_VENDOR_GETDETAIL";
            this.btnBAPI_VENDOR_GETDETAIL.Size = new System.Drawing.Size(156, 23);
            this.btnBAPI_VENDOR_GETDETAIL.TabIndex = 1;
            this.btnBAPI_VENDOR_GETDETAIL.Text = "获取供应商名称";
            this.btnBAPI_VENDOR_GETDETAIL.UseVisualStyleBackColor = true;
            this.btnBAPI_VENDOR_GETDETAIL.Click += new System.EventHandler(this.btnBAPI_VENDOR_GETDETAIL_Click);
            // 
            // btnBAPI_CUSTOMER_GETDETAIL2
            // 
            this.btnBAPI_CUSTOMER_GETDETAIL2.Location = new System.Drawing.Point(91, 70);
            this.btnBAPI_CUSTOMER_GETDETAIL2.Name = "btnBAPI_CUSTOMER_GETDETAIL2";
            this.btnBAPI_CUSTOMER_GETDETAIL2.Size = new System.Drawing.Size(156, 23);
            this.btnBAPI_CUSTOMER_GETDETAIL2.TabIndex = 2;
            this.btnBAPI_CUSTOMER_GETDETAIL2.Text = "获取客户名称";
            this.btnBAPI_CUSTOMER_GETDETAIL2.UseVisualStyleBackColor = true;
            this.btnBAPI_CUSTOMER_GETDETAIL2.Click += new System.EventHandler(this.btnBAPI_CUSTOMER_GETDETAIL2_Click);
            // 
            // btnMSR1_MD_PAYTERMS_GETLIST
            // 
            this.btnMSR1_MD_PAYTERMS_GETLIST.Location = new System.Drawing.Point(91, 97);
            this.btnMSR1_MD_PAYTERMS_GETLIST.Name = "btnMSR1_MD_PAYTERMS_GETLIST";
            this.btnMSR1_MD_PAYTERMS_GETLIST.Size = new System.Drawing.Size(156, 23);
            this.btnMSR1_MD_PAYTERMS_GETLIST.TabIndex = 3;
            this.btnMSR1_MD_PAYTERMS_GETLIST.Text = "获取付款条款";
            this.btnMSR1_MD_PAYTERMS_GETLIST.UseVisualStyleBackColor = true;
            this.btnMSR1_MD_PAYTERMS_GETLIST.Click += new System.EventHandler(this.btnMSR1_MD_PAYTERMS_GETLIST_Click);
            // 
            // btnBAPI_ADDRESSCONTPART_GETDETAIL
            // 
            this.btnBAPI_ADDRESSCONTPART_GETDETAIL.Location = new System.Drawing.Point(91, 126);
            this.btnBAPI_ADDRESSCONTPART_GETDETAIL.Name = "btnBAPI_ADDRESSCONTPART_GETDETAIL";
            this.btnBAPI_ADDRESSCONTPART_GETDETAIL.Size = new System.Drawing.Size(156, 23);
            this.btnBAPI_ADDRESSCONTPART_GETDETAIL.TabIndex = 4;
            this.btnBAPI_ADDRESSCONTPART_GETDETAIL.Text = "获取联系人";
            this.btnBAPI_ADDRESSCONTPART_GETDETAIL.UseVisualStyleBackColor = true;
            this.btnBAPI_ADDRESSCONTPART_GETDETAIL.Click += new System.EventHandler(this.btnBAPI_ADDRESSCONTPART_GETDETAIL_Click);
            // 
            // btnBAPI_GetCurRate
            // 
            this.btnBAPI_GetCurRate.Location = new System.Drawing.Point(91, 156);
            this.btnBAPI_GetCurRate.Name = "btnBAPI_GetCurRate";
            this.btnBAPI_GetCurRate.Size = new System.Drawing.Size(156, 23);
            this.btnBAPI_GetCurRate.TabIndex = 5;
            this.btnBAPI_GetCurRate.Text = "获取当前汇率";
            this.btnBAPI_GetCurRate.UseVisualStyleBackColor = true;
            this.btnBAPI_GetCurRate.Click += new System.EventHandler(this.btnBAPI_GetCurRate_Click);
            // 
            // Mainform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnBAPI_GetCurRate);
            this.Controls.Add(this.btnBAPI_ADDRESSCONTPART_GETDETAIL);
            this.Controls.Add(this.btnMSR1_MD_PAYTERMS_GETLIST);
            this.Controls.Add(this.btnBAPI_CUSTOMER_GETDETAIL2);
            this.Controls.Add(this.btnBAPI_VENDOR_GETDETAIL);
            this.Controls.Add(this.button1);
            this.Name = "Mainform";
            this.Text = "SAP接口调用Demo";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnBAPI_VENDOR_GETDETAIL;
        private System.Windows.Forms.Button btnBAPI_CUSTOMER_GETDETAIL2;
        private System.Windows.Forms.Button btnMSR1_MD_PAYTERMS_GETLIST;
        private System.Windows.Forms.Button btnBAPI_ADDRESSCONTPART_GETDETAIL;
        private System.Windows.Forms.Button btnBAPI_GetCurRate;
    }
}

