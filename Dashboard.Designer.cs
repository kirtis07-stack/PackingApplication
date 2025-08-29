namespace PackingApplication
{
    partial class Dashboard
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Text = "Dashboard";
            this.poypackingform = new System.Windows.Forms.Label();
            this.dtypackingform = new System.Windows.Forms.Label();
            this.bcfpackingform = new System.Windows.Forms.Label();
            this.chipspackingform = new System.Windows.Forms.Label();

            // 
            // poypackingform
            // 
            this.poypackingform.AutoSize = true;
            this.poypackingform.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.poypackingform.Location = new System.Drawing.Point(57, 73);
            this.poypackingform.Name = "poypackingform";
            this.poypackingform.Size = new System.Drawing.Size(85, 14);
            this.poypackingform.TabIndex = 1;
            this.poypackingform.Text = "1. POY Packing";
            this.poypackingform.Click += new System.EventHandler(this.POYPacking_Click);
            // 
            // dtypackingform
            // 
            this.dtypackingform.AutoSize = true;
            this.dtypackingform.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtypackingform.Location = new System.Drawing.Point(57, 100);
            this.dtypackingform.Name = "dtypackingform";
            this.dtypackingform.Size = new System.Drawing.Size(84, 14);
            this.dtypackingform.TabIndex = 2;
            this.dtypackingform.Text = "2. DTY Packing";
            this.dtypackingform.Click += new System.EventHandler(this.DTYPacking_Click);
            // 
            // bcfpackingform
            // 
            this.bcfpackingform.AutoSize = true;
            this.bcfpackingform.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bcfpackingform.Location = new System.Drawing.Point(58, 129);
            this.bcfpackingform.Name = "bcfpackingform";
            this.bcfpackingform.Size = new System.Drawing.Size(83, 14);
            this.bcfpackingform.TabIndex = 3;
            this.bcfpackingform.Text = "3. BCF Packing";
            this.bcfpackingform.Click += new System.EventHandler(this.BCFPacking_Click);
            // 
            // chipspackingform
            // 
            this.chipspackingform.AutoSize = true;
            this.chipspackingform.Font = new System.Drawing.Font("Microsoft Tai Le", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chipspackingform.Location = new System.Drawing.Point(57, 156);
            this.chipspackingform.Name = "chipspackingform";
            this.chipspackingform.Size = new System.Drawing.Size(92, 14);
            this.chipspackingform.TabIndex = 4;
            this.chipspackingform.Text = "4. Chips Packing";
            this.chipspackingform.Click += new System.EventHandler(this.ChipsPacking_Click);

            this.Controls.Add(this.chipspackingform);
            this.Controls.Add(this.bcfpackingform);
            this.Controls.Add(this.dtypackingform);
            this.Controls.Add(this.poypackingform);
        }

        #endregion
        private System.Windows.Forms.Label poypackingform;
        private System.Windows.Forms.Label dtypackingform;
        private System.Windows.Forms.Label bcfpackingform;
        private System.Windows.Forms.Label chipspackingform;
    }
}