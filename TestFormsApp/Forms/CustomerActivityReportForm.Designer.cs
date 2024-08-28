namespace C969App.Forms
{
    partial class CustomerActivityReportForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.DataGridView dgvCustomerActivity;
        private System.Windows.Forms.Button btnGenerateReport;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.dgvCustomerActivity = new System.Windows.Forms.DataGridView();
            this.btnGenerateReport = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerActivity)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCustomerActivity
            // 
            this.dgvCustomerActivity.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCustomerActivity.Location = new System.Drawing.Point(12, 12);
            this.dgvCustomerActivity.Name = "dgvCustomerActivity";
            this.dgvCustomerActivity.Size = new System.Drawing.Size(776, 396);
            this.dgvCustomerActivity.TabIndex = 0;
            // 
            // btnGenerateReport
            // 
            this.btnGenerateReport.Location = new System.Drawing.Point(713, 414);
            this.btnGenerateReport.Name = "btnGenerateReport";
            this.btnGenerateReport.Size = new System.Drawing.Size(75, 23);
            this.btnGenerateReport.TabIndex = 1;
            this.btnGenerateReport.Text = "Generate";
            this.btnGenerateReport.UseVisualStyleBackColor = true;
            this.btnGenerateReport.Click += new System.EventHandler(this.btnGenerateReport_Click);
            // 
            // CustomerActivityReportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.dgvCustomerActivity);
            this.Name = "CustomerActivityReportForm";
            this.Text = "Customer Activity Report";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCustomerActivity)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
