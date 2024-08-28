namespace C969App.Forms
{
    partial class AppointmentForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.btnAllAppointments = new System.Windows.Forms.Button();
            this.btnCurrentWeek = new System.Windows.Forms.Button();
            this.btnCurrentMonth = new System.Windows.Forms.Button();
            this.btnAddAppointment = new System.Windows.Forms.Button();
            this.btnEditAppointment = new System.Windows.Forms.Button();
            this.btnDeleteAppointment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Location = new System.Drawing.Point(12, 50);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.Size = new System.Drawing.Size(776, 350);
            this.dgvAppointments.TabIndex = 0;
            // 
            // btnAllAppointments
            // 
            this.btnAllAppointments.Location = new System.Drawing.Point(12, 12);
            this.btnAllAppointments.Name = "btnAllAppointments";
            this.btnAllAppointments.Size = new System.Drawing.Size(150, 23);
            this.btnAllAppointments.TabIndex = 1;
            this.btnAllAppointments.Text = "All Appointments";
            this.btnAllAppointments.UseVisualStyleBackColor = true;
            this.btnAllAppointments.Click += new System.EventHandler(this.btnAllAppointments_Click);
            // 
            // btnCurrentWeek
            // 
            this.btnCurrentWeek.Location = new System.Drawing.Point(180, 12);
            this.btnCurrentWeek.Name = "btnCurrentWeek";
            this.btnCurrentWeek.Size = new System.Drawing.Size(150, 23);
            this.btnCurrentWeek.TabIndex = 2;
            this.btnCurrentWeek.Text = "Current Week";
            this.btnCurrentWeek.UseVisualStyleBackColor = true;
            this.btnCurrentWeek.Click += new System.EventHandler(this.btnCurrentWeek_Click);
            // 
            // btnCurrentMonth
            // 
            this.btnCurrentMonth.Location = new System.Drawing.Point(348, 12);
            this.btnCurrentMonth.Name = "btnCurrentMonth";
            this.btnCurrentMonth.Size = new System.Drawing.Size(150, 23);
            this.btnCurrentMonth.TabIndex = 3;
            this.btnCurrentMonth.Text = "Current Month";
            this.btnCurrentMonth.UseVisualStyleBackColor = true;
            this.btnCurrentMonth.Click += new System.EventHandler(this.btnCurrentMonth_Click);
            // 
            // btnAddAppointment
            // 
            this.btnAddAppointment.Location = new System.Drawing.Point(538, 12);
            this.btnAddAppointment.Name = "btnAddAppointment";
            this.btnAddAppointment.Size = new System.Drawing.Size(75, 23);
            this.btnAddAppointment.TabIndex = 4;
            this.btnAddAppointment.Text = "Add";
            this.btnAddAppointment.UseVisualStyleBackColor = true;
            this.btnAddAppointment.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEditAppointment
            // 
            this.btnEditAppointment.Location = new System.Drawing.Point(619, 12);
            this.btnEditAppointment.Name = "btnEditAppointment";
            this.btnEditAppointment.Size = new System.Drawing.Size(75, 23);
            this.btnEditAppointment.TabIndex = 5;
            this.btnEditAppointment.Text = "Edit";
            this.btnEditAppointment.UseVisualStyleBackColor = true;
            this.btnEditAppointment.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDeleteAppointment
            // 
            this.btnDeleteAppointment.Location = new System.Drawing.Point(700, 12);
            this.btnDeleteAppointment.Name = "btnDeleteAppointment";
            this.btnDeleteAppointment.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteAppointment.TabIndex = 6;
            this.btnDeleteAppointment.Text = "Delete";
            this.btnDeleteAppointment.UseVisualStyleBackColor = true;
            this.btnDeleteAppointment.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // AppointmentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnDeleteAppointment);
            this.Controls.Add(this.btnEditAppointment);
            this.Controls.Add(this.btnAddAppointment);
            this.Controls.Add(this.btnCurrentMonth);
            this.Controls.Add(this.btnCurrentWeek);
            this.Controls.Add(this.btnAllAppointments);
            this.Controls.Add(this.dgvAppointments);
            this.Name = "AppointmentForm";
            this.Text = "Appointment Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.Button btnAllAppointments;
        private System.Windows.Forms.Button btnCurrentWeek;
        private System.Windows.Forms.Button btnCurrentMonth;
        private System.Windows.Forms.Button btnAddAppointment;
        private System.Windows.Forms.Button btnEditAppointment;
        private System.Windows.Forms.Button btnDeleteAppointment;
    }
}
