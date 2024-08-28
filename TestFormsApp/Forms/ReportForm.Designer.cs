namespace C969App.Forms
{
    partial class ReportForm
    {
        private System.Windows.Forms.Button btnMonthly;
        private System.Windows.Forms.Button btnReport2;
        private System.Windows.Forms.Button btnReport3;

        private void InitializeComponent()
        {
            this.btnMonthly = new System.Windows.Forms.Button();
            this.btnReport2 = new System.Windows.Forms.Button();
            this.btnReport3 = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // btnMonthly
            // 
            this.btnMonthly.Location = new System.Drawing.Point(50, 30);
            this.btnMonthly.Name = "btnMonthly";
            this.btnMonthly.Size = new System.Drawing.Size(200, 30);
            this.btnMonthly.TabIndex = 0;
            this.btnMonthly.Text = "View Appointment Types by Month";
            this.btnMonthly.UseVisualStyleBackColor = true;
            this.btnMonthly.Click += new System.EventHandler(this.btnMonthly_Click);

            // 
            // btnReport2
            // 
            this.btnReport2.Location = new System.Drawing.Point(50, 80);
            this.btnReport2.Name = "btnReport2";
            this.btnReport2.Size = new System.Drawing.Size(200, 30);
            this.btnReport2.TabIndex = 1;
            this.btnReport2.Text = "View Schedule by Consultant";
            this.btnReport2.UseVisualStyleBackColor = true;
            this.btnReport2.Click += new System.EventHandler(this.btnReport2_Click);

            // 
            // btnReport3
            // 
            this.btnReport3.Location = new System.Drawing.Point(50, 130);
            this.btnReport3.Name = "btnReport3";
            this.btnReport3.Size = new System.Drawing.Size(200, 30);
            this.btnReport3.TabIndex = 2;
            this.btnReport3.Text = "View Custom Report";
            this.btnReport3.UseVisualStyleBackColor = true;
            this.btnReport3.Click += new System.EventHandler(this.btnReport3_Click);

            // 
            // ReportForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 200);
            this.Controls.Add(this.btnMonthly);
            this.Controls.Add(this.btnReport2);
            this.Controls.Add(this.btnReport3);
            this.Name = "ReportForm";
            this.Text = "Reports";
            this.ResumeLayout(false);
        }
    }
}
