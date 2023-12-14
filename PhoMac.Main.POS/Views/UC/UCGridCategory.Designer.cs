namespace PhoMac.Main.POS.Views.UC
{
    partial class UCGridCategory
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.TileItemElement tileItemElement1 = new DevExpress.XtraEditors.TileItemElement();
            this.tileControl1 = new DevExpress.XtraEditors.TileControl();
            this.tileGroup2 = new DevExpress.XtraEditors.TileGroup();
            this.tileItem1 = new DevExpress.XtraEditors.TileItem();
            this.SuspendLayout();
            // 
            // tileControl1
            // 
            this.tileControl1.AllowDrag = false;
            this.tileControl1.AllowGlyphSkinning = true;
            this.tileControl1.AllowSelectedItem = true;
            this.tileControl1.AppearanceItem.Normal.BackColor = System.Drawing.Color.White;
            this.tileControl1.AppearanceItem.Normal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(195)))), ((int)(((byte)(195)))), ((int)(((byte)(195)))));
            this.tileControl1.AppearanceItem.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(89)))), ((int)(((byte)(89)))), ((int)(((byte)(89)))));
            this.tileControl1.AppearanceItem.Normal.Options.UseBackColor = true;
            this.tileControl1.AppearanceItem.Normal.Options.UseBorderColor = true;
            this.tileControl1.AppearanceItem.Normal.Options.UseFont = true;
            this.tileControl1.AppearanceItem.Normal.Options.UseForeColor = true;
            this.tileControl1.AppearanceItem.Pressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(38)))), ((int)(((byte)(115)))));
            this.tileControl1.AppearanceItem.Pressed.ForeColor = System.Drawing.Color.Gainsboro;
            this.tileControl1.AppearanceItem.Pressed.Options.UseBackColor = true;
            this.tileControl1.AppearanceItem.Pressed.Options.UseFont = true;
            this.tileControl1.AppearanceItem.Pressed.Options.UseForeColor = true;
            this.tileControl1.AppearanceItem.Selected.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(81)))), ((int)(((byte)(165)))));
            this.tileControl1.AppearanceItem.Selected.BorderColor = System.Drawing.Color.Transparent;
            this.tileControl1.AppearanceItem.Selected.ForeColor = System.Drawing.Color.White;
            this.tileControl1.AppearanceItem.Selected.Options.UseBackColor = true;
            this.tileControl1.AppearanceItem.Selected.Options.UseBorderColor = true;
            this.tileControl1.AppearanceItem.Selected.Options.UseFont = true;
            this.tileControl1.AppearanceItem.Selected.Options.UseForeColor = true;
            this.tileControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.tileControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileControl1.Groups.Add(this.tileGroup2);
            this.tileControl1.HorizontalContentAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.tileControl1.IndentBetweenItems = 10;
            this.tileControl1.ItemPadding = new System.Windows.Forms.Padding(7, 7, 7, 4);
            this.tileControl1.ItemSize = 90;
            this.tileControl1.Location = new System.Drawing.Point(0, 0);
            this.tileControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tileControl1.MaxId = 4;
            this.tileControl1.Name = "tileControl1";
            this.tileControl1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileControl1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.tileControl1.Size = new System.Drawing.Size(325, 262);
            this.tileControl1.TabIndex = 2;
            this.tileControl1.Text = "tileControl1";
            // 
            // tileGroup2
            // 
            this.tileGroup2.Items.Add(this.tileItem1);
            this.tileGroup2.Name = "tileGroup2";
            // 
            // tileItem1
            // 
            tileItemElement1.Text = "tileItem1";
            this.tileItem1.Elements.Add(tileItemElement1);
            this.tileItem1.Id = 3;
            this.tileItem1.ItemSize = DevExpress.XtraEditors.TileItemSize.Wide;
            this.tileItem1.Name = "tileItem1";
            this.tileItem1.ItemClick += new DevExpress.XtraEditors.TileItemClickEventHandler(this.tileItem1_ItemClick_1);
            // 
            // GridPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tileControl1);
            this.Name = "GridPanel";
            this.Size = new System.Drawing.Size(325, 262);
            this.ResumeLayout(false);

        }

        #endregion

        protected DevExpress.XtraEditors.TileControl tileControl1;
        private DevExpress.XtraEditors.TileGroup tileGroup2;
        private DevExpress.XtraEditors.TileItem tileItem1;


    }
}
