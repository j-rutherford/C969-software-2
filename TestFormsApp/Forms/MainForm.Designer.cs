namespace C969App.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button btnCustomer;
        private System.Windows.Forms.Button btnAppointment;

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
            this.btnCustomer = new System.Windows.Forms.Button();
            this.btnAppointment = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnCustomer
            // 
            this.btnCustomer.Location = new System.Drawing.Point(50, 26);
            this.btnCustomer.Name = "btnCustomer";
            this.btnCustomer.Size = new System.Drawing.Size(200, 50);
            this.btnCustomer.TabIndex = 2;
            this.btnCustomer.Text = "Manage Customers";
            this.btnCustomer.UseVisualStyleBackColor = true;
            this.btnCustomer.Click += new System.EventHandler(this.btnCustomer_Click);
            // 
            // btnAppointment
            // 
            this.btnAppointment.Location = new System.Drawing.Point(50, 98);
            this.btnAppointment.Name = "btnAppointment";
            this.btnAppointment.Size = new System.Drawing.Size(200, 50);
            this.btnAppointment.TabIndex = 3;
            this.btnAppointment.Text = "Manage Appointments";
            this.btnAppointment.UseVisualStyleBackColor = true;
            this.btnAppointment.Click += new System.EventHandler(this.btnAppointment_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 350);
            this.Controls.Add(this.btnAppointment);
            this.Controls.Add(this.btnCustomer);
            this.Name = "MainForm";
            this.Text = "Main Menu";
            this.ResumeLayout(false);

        }
    }
}
