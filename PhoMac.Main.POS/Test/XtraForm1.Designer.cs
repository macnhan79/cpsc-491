namespace PhoMac.Main.POS.Test
{
    partial class XtraForm1
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
            this.pos_MainView1 = new PhoMac.Main.POS.Views.Pos_MainView();
            this.SuspendLayout();
            // 
            // pos_MainView1
            // 
            this.pos_MainView1.Location = new System.Drawing.Point(12, 12);
            this.pos_MainView1.Name = "pos_MainView1";
            this.pos_MainView1.Size = new System.Drawing.Size(719, 373);
            this.pos_MainView1.TabIndex = 0;
            // 
            // XtraForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 410);
            this.Controls.Add(this.pos_MainView1);
            this.Name = "XtraForm1";
            this.Text = "XtraForm1";
            this.ResumeLayout(false);

        }

        #endregion

        private Views.Pos_MainView pos_MainView1;

    }
}