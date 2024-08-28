namespace C969App.Forms
{
    partial class ConsultantScheduleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private System.Windows.Forms.ComboBox cmbConsultants;
        private System.Windows.Forms.Button btnViewSchedule;
        private System.Windows.Forms.DataGridView dgvSchedule;

        private void InitializeComponent()
        {
            this.cmbConsultants = new System.Windows.Forms.ComboBox();
            this.btnViewSchedule = new System.Windows.Forms.Button();
            this.dgvSchedule = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbConsultants
            // 
            this.cmbConsultants.FormattingEnabled = true;
            this.cmbConsultants.Location = new System.Drawing.Point(30, 30);
            this.cmbConsultants.Name = "cmbConsultants";
            this.cmbConsultants.Size = new System.Drawing.Size(200, 21);
            this.cmbConsultants.TabIndex = 0;
            // 
            // btnViewSchedule
            // 
            this.btnViewSchedule.Location = new System.Drawing.Point(250, 30);
            this.btnViewSchedule.Name = "btnViewSchedule";
            this.btnViewSchedule.Size = new System.Drawing.Size(100, 23);
            this.btnViewSchedule.TabIndex = 1;
            this.btnViewSchedule.Text = "View Schedule";
            this.btnViewSchedule.UseVisualStyleBackColor = true;
            this.btnViewSchedule.Click += new System.EventHandler(this.btnViewSchedule_Click);
            // 
            // dgvSchedule
            // 
            this.dgvSchedule.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSchedule.Location = new System.Drawing.Point(30, 70);
            this.dgvSchedule.Name = "dgvSchedule";
            this.dgvSchedule.Size = new System.Drawing.Size(400, 200);
            this.dgvSchedule.TabIndex = 2;
            // 
            // ConsultantScheduleForm
            // 
            this.ClientSize = new System.Drawing.Size(464, 281);
            this.Controls.Add(this.dgvSchedule);
            this.Controls.Add(this.btnViewSchedule);
            this.Controls.Add(this.cmbConsultants);
            this.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSchedule)).EndInit();
        }


        #endregion
    }
}