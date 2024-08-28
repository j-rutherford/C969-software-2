namespace C969App.Forms
{
    partial class MonthlyReportForm
    {
        private System.Windows.Forms.DataGridView dgvMonthlyReport;

        private void InitializeComponent()
        {
            this.dgvMonthlyReport = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthlyReport)).BeginInit();
            this.SuspendLayout();

            // 
            // dgvMonthlyReport
            // 
            this.dgvMonthlyReport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMonthlyReport.Location = new System.Drawing.Point(12, 12);
            this.dgvMonthlyReport.Name = "dgvMonthlyReport";
            this.dgvMonthlyReport.Size = new System.Drawing.Size(400, 250);
            this.dgvMonthlyReport.TabIndex = 0;

            // 
            // MonthlyForm
            // 
            this.ClientSize = new System.Drawing.Size(424, 281);
            this.Controls.Add(this.dgvMonthlyReport);
            this.Name = "MonthlyForm";
            this.Text = "Monthly Appointment Report";
            this.Load += new System.EventHandler(this.MonthlyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthlyReport)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
