namespace Terminal
{
    partial class Form2
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
            this.labelExit1 = new System.Windows.Forms.Label();
            this.labelExit2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.GetEvents = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // labelExit1
            // 
            this.labelExit1.BackColor = System.Drawing.Color.Transparent;
            this.labelExit1.Location = new System.Drawing.Point(57, 45);
            this.labelExit1.Name = "labelExit1";
            this.labelExit1.Size = new System.Drawing.Size(125, 85);
            this.labelExit1.TabIndex = 0;
            this.labelExit1.Click += new System.EventHandler(this.labelExit1_Click);
            // 
            // labelExit2
            // 
            this.labelExit2.BackColor = System.Drawing.Color.Transparent;
            this.labelExit2.Location = new System.Drawing.Point(883, 31);
            this.labelExit2.Name = "labelExit2";
            this.labelExit2.Size = new System.Drawing.Size(123, 108);
            this.labelExit2.TabIndex = 1;
            this.labelExit2.Click += new System.EventHandler(this.labelExit1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(364, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "label1";
            this.label1.Visible = false;
            // 
            // GetEvents
            // 
            this.GetEvents.Enabled = true;
            this.GetEvents.Interval = 120000;
            this.GetEvents.Tick += new System.EventHandler(this.GetEvents_Tick);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Terminal.Properties.Resources.BackGround;
            this.ClientSize = new System.Drawing.Size(1084, 753);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelExit2);
            this.Controls.Add(this.labelExit1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form2";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form2";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.Form2_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelExit1;
        private System.Windows.Forms.Label labelExit2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer GetEvents;
    }
}