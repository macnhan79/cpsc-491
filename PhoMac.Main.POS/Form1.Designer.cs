namespace PhoMac.Main.POS
{
    partial class Form1
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
            PhoMac.Business.Presenter.TablePresenter tablePresenter1 = new PhoMac.Business.Presenter.TablePresenter();
            PhoMac.Model.Table table1 = new PhoMac.Model.Table();
            this.ucTable1 = new PhoMac.Main.POS.Views.UC.UCTable();
            this.SuspendLayout();
            // 
            // ucTable1
            // 
            this.ucTable1.AllowDrop = true;
            this.ucTable1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucTable1.Col = 0;
            this.ucTable1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTable1.EnableDrapAndDrop = false;
            this.ucTable1.FormCode = PhoHa7.Library.Enum.EnumFormCode.System;
            this.ucTable1.IsChange = false;
            this.ucTable1.IsSelected = false;
            this.ucTable1.IsSuccess = false;
            this.ucTable1.Location = new System.Drawing.Point(0, 0);
            this.ucTable1.Name = "ucTable1";
            this.ucTable1.PreviousInstance = null;
            this.ucTable1.Row = 0;
            this.ucTable1.ServerName = "Server Name: Server Name: Server Name: Server Name: Server Name";
            this.ucTable1.SetText = "";
            this.ucTable1.Size = new System.Drawing.Size(1840, 748);
            this.ucTable1.StatusTable = "Status: Status: Status: Status: Status";
            this.ucTable1.TabIndex = 0;
            this.ucTable1.TableID = 0;
            tablePresenter1._ButtonColor = 0;
            tablePresenter1._oBackColor = 0;
            tablePresenter1._oForeColor = 0;
            tablePresenter1._TextColor = 0;
            tablePresenter1.Active = false;
            tablePresenter1.BarTab = false;
            tablePresenter1.BiWeekKey = null;
            tablePresenter1.BottleOff = 0F;
            tablePresenter1.ButtonColor = System.Drawing.Color.Yellow;
            tablePresenter1.CategoryID = 0;
            tablePresenter1.ChgeObj = false;
            tablePresenter1.Col = 0;
            tablePresenter1.CurDTicketNum = 0;
            tablePresenter1.CurTicketNum = 0;
            tablePresenter1.DateKey = null;
            tablePresenter1.Delivery = false;
            tablePresenter1.EmployeeID = 0;
            tablePresenter1.EmployeeName = null;
            tablePresenter1.FastCash = false;
            tablePresenter1.ImageFPath = null;
            tablePresenter1.ImageID = 0;
            tablePresenter1.NewObj = false;
            tablePresenter1.oBackColor = System.Drawing.Color.Empty;
            tablePresenter1.objType = 0;
            tablePresenter1.oFontName = null;
            tablePresenter1.oFontPoint = 0F;
            tablePresenter1.oFontStyle = 0;
            tablePresenter1.oForeColor = System.Drawing.Color.Empty;
            tablePresenter1.oHeight = 0;
            tablePresenter1.oLocX = 0;
            tablePresenter1.oLocY = 0;
            tablePresenter1.oLowerColor = 0;
            tablePresenter1.oName = null;
            tablePresenter1.OnBiWeek = 0;
            tablePresenter1.OnDate = new System.DateTime(((long)(0)));
            tablePresenter1.OnDay = 0;
            tablePresenter1.OnMonth = 0;
            tablePresenter1.OnQuarter = 0;
            tablePresenter1.OnSimMonth = 0;
            tablePresenter1.OnWeek = 0;
            tablePresenter1.OnYear = 0;
            tablePresenter1.OrderBy = 0;
            tablePresenter1.oTableStyle = 0;
            tablePresenter1.oTextAlign = 0;
            tablePresenter1.oUpperColor = 0;
            tablePresenter1.oWidth = 0;
            tablePresenter1.Page = 0;
            tablePresenter1.QuarterKey = null;
            tablePresenter1.RecordGUID = null;
            tablePresenter1.RecordState = 0;
            tablePresenter1.Row = 0;
            tablePresenter1.SeatNum = 0;
            tablePresenter1.SentObj = false;
            tablePresenter1.SimMonthKey = null;
            tablePresenter1.StartTime = new System.DateTime(((long)(0)));
            tablePresenter1.TableID = 0;
            tablePresenter1.TableName = null;
            table1.Active = false;
            table1.BarTab = false;
            table1.BiWeekKey = null;
            table1.BottleOff = null;
            table1.ButtonColor = 0;
            table1.CategoryID = 0;
            table1.ChgeObj = null;
            table1.Col = 0;
            table1.CurDTicketNum = 0;
            table1.CurTicketNum = 0;
            table1.DateKey = null;
            table1.Delivery = false;
            table1.EmployeeID = 0;
            table1.EmployeeName = null;
            table1.FastCash = false;
            table1.ImageFPath = null;
            table1.ImageID = 0;
            table1.NewObj = null;
            table1.oBackColor = 0;
            table1.objType = null;
            table1.oFontName = null;
            table1.oFontPoint = 0F;
            table1.oFontStyle = 0;
            table1.oForeColor = 0;
            table1.oHeight = 0;
            table1.oLocX = 0;
            table1.oLocY = 0;
            table1.oLowerColor = 0;
            table1.oName = null;
            table1.OnBiWeek = null;
            table1.OnDate = new System.DateTime(((long)(0)));
            table1.OnDay = 0;
            table1.OnMonth = 0;
            table1.OnQuarter = null;
            table1.OnSimMonth = null;
            table1.OnWeek = null;
            table1.OnYear = 0;
            table1.OrderBy = 0;
            table1.oTableStyle = 0;
            table1.oTextAlign = 0;
            table1.oUpperColor = 0;
            table1.oWidth = 0;
            table1.Page = 0;
            table1.QuarterKey = null;
            table1.RecordGUID = null;
            table1.RecordState = null;
            table1.Row = 0;
            table1.SeatNum = 0;
            table1.SentObj = null;
            table1.SimMonthKey = null;
            table1.StartTime = new System.DateTime(((long)(0)));
            table1.TableID = 0;
            table1.TableName = null;
            table1.TableStatus = 0;
            table1.TableType = 0;
            table1.TabNum = 0;
            table1.TActive = false;
            table1.TakeOut = false;
            table1.TextColor = 0;
            table1.WeekKey = null;
            table1.ZOrder = 0;
            tablePresenter1.Tables = table1;
            tablePresenter1.TableStatus = 0;
            tablePresenter1.TableType = 0;
            tablePresenter1.TabNum = 0;
            tablePresenter1.TActive = false;
            tablePresenter1.TakeOut = false;
            tablePresenter1.TextColor = System.Drawing.Color.Empty;
            tablePresenter1.WeekKey = null;
            tablePresenter1.ZOrder = 0;
            this.ucTable1.TableInfo = tablePresenter1;
            this.ucTable1.TableName = "Table name<br> abc";
            this.ucTable1.Time = new System.DateTime(((long)(0)));
            this.ucTable1.UCColor = System.Drawing.Color.Empty;
            this.ucTable1.UCForeColor = System.Drawing.Color.Empty;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1840, 748);
            this.Controls.Add(this.ucTable1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Views.UC.UCPanelLayoutTable ucGridPanel1;
        private Views.UC.UCTable ucTable1;



    }
}