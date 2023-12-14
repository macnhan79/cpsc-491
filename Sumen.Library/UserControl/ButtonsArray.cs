using System.ComponentModel;
using System.Drawing;
using DevExpress.XtraEditors.Controls;
using PhoHa7.Library.Interface;

namespace PhoHa7.Library.UserControl
{
    public partial class ButtonsArray : DevExpress.XtraEditors.XtraUserControl
    {
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventAdd_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventUpdate_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventDelete_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventSave_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventRevert_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventPrint_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventExcel_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventClose_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventSelect_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventProduceReports_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventView_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventWord_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventReset_click;
        [Category("ButtonsArray")]
        public event System.EventHandler btnEventPrintBarcode_click;
        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private bool[] ButtonVisibleArr = new bool[12];

        public ButtonsArray()
        {
            InitializeComponent();
            this.ButtonVisibleArr[11] = true; // Default ButtonThoatVisible = true;
            this.ChangeThisSize();
        }

        /// <summary>
        /// Tạo quyền cho các button
        /// </summary>
        private bool bQThem = true;
        private bool bQSua = true;
        private bool bQXoa = true;
        private bool bQInan = true;
        private bool bQXem = true;
        private bool bQWord = true;
        private bool bQExcel = true;

        void capQuyen(bool Them, bool Sua, bool Xoa, bool Inan)
        {
            bQThem = Them;
            bQSua = Sua;
            bQXoa = Xoa;
            bQInan = Inan;

            if (bQThem == false)
            {
                btnThem.Enabled = false;
                btnLap.Enabled = false;
            }
            if (bQSua == false)
            {
                btnSua.Enabled = false;
            }
            if (bQXoa == false)
            {
                btnXoa.Enabled = false;
            }
            if (bQInan == false)
            {
                btnInan.Enabled = false;
                btnWord.Enabled = false;
                btnExcel.Enabled = false;
            }
            this.ChangeStyle();
        }

        public void capQuyen(IPermission pPermission)
        {
            //IPermission pPermission = new Permission();

            bQThem = pPermission.PermissionAdd();
            bQSua = pPermission.PermissionUpdate();
            bQXoa = pPermission.PermissionDelete();
            bQInan = pPermission.PermissionPrint();
            bQXem = pPermission.PermissionView();
            bQWord = pPermission.PermissionWord();
            bQExcel = pPermission.PermissionExcel();

            if (!bQXem)
            {
                btnXem.Enabled = false;
            }
            if (bQThem == false)
            {
                btnThem.Enabled = false;
                btnLap.Enabled = false;
            }
            if (bQSua == false)
            {
                btnSua.Enabled = false;
            }
            if (bQXoa == false)
            {
                btnXoa.Enabled = false;
            }
            if (bQInan == false)
            {
                btnInan.Enabled = false;
            }
            if (bQWord == false)
            {
                btnWord.Enabled = false;
            }
            if (bQExcel == false)
            {
                btnExcel.Enabled = false;
            }
            this.ChangeStyle();
        }

        private void btn_EnabledChanged(object sender, System.EventArgs e)
        {
            if (bQThem == false)
            {
                btnThem.Enabled = false;
                btnLap.Enabled = false;
            }
            if (bQSua == false)
            {
                btnSua.Enabled = false;
            }
            if (bQXoa == false)
            {
                btnXoa.Enabled = false;
            }
            if (bQInan == false)
            {
                btnInan.Enabled = false;
                btnWord.Enabled = false;
                btnExcel.Enabled = false;
            }
        }



        /// <summary>
        /// Đổi hình nền của tất cả các button
        /// </summary>
        [Category("ButtonsArray")]
        public Image btnBackgroundImage
        {
            get
            {
                return btnThoat.BackgroundImage;
            }
            set
            {
                this.btnThem.BackgroundImage = value;
                this.btnSua.BackgroundImage = value;
                this.btnXoa.BackgroundImage = value;
                this.btnLuu.BackgroundImage = value;
                this.btnBoqua.BackgroundImage = value;
                this.btnInan.BackgroundImage = value;
                this.btnExcel.BackgroundImage = value;
                this.btnThoat.BackgroundImage = value;
                this.btnChon.BackgroundImage = value;
                this.btnLap.BackgroundImage = value;
                this.btnXem.BackgroundImage = value;
                this.btnWord.BackgroundImage = value;
                this.btnReset.BackgroundImage = value;
                this.btnPrintBarcode.BackgroundImage = value;
            }
        }

        /// <summary>
        /// Đổi FlatStyle của button
        /// </summary>
        [Category("ButtonsArray")]
        public BorderStyles btnFlatStyle
        {
            get
            {
                return btnThoat.ButtonStyle;
            }
            set
            {
                this.btnThem.ButtonStyle = value;
                this.btnSua.ButtonStyle = value;
                this.btnXoa.ButtonStyle = value;
                this.btnLuu.ButtonStyle = value;
                this.btnBoqua.ButtonStyle = value;
                this.btnInan.ButtonStyle = value;
                this.btnExcel.ButtonStyle = value;
                this.btnThoat.ButtonStyle = value;
                this.btnChon.ButtonStyle = value;
                this.btnLap.ButtonStyle = value;
                this.btnXem.ButtonStyle = value;
                this.btnWord.ButtonStyle = value;
                this.btnReset.ButtonStyle = value;
                this.btnPrintBarcode.ButtonStyle = value;
            }
        }

        /// <summary>
        /// Đổi ForeColor của button
        /// </summary>
        [Category("ButtonsArray")]
        public Color btnForeColor
        {
            get
            {
                return btnThoat.ForeColor;
            }
            set
            {
                this.btnThem.ForeColor = value;
                this.btnSua.ForeColor = value;
                this.btnXoa.ForeColor = value;
                this.btnLuu.ForeColor = value;
                this.btnBoqua.ForeColor = value;
                this.btnInan.ForeColor = value;
                this.btnExcel.ForeColor = value;
                this.btnThoat.ForeColor = value;
                this.btnChon.ForeColor = value;
                this.btnLap.ForeColor = value;
                this.btnXem.ForeColor = value;
                this.btnWord.ForeColor = value;
                this.btnReset.ForeColor = value;
                this.btnPrintBarcode.ForeColor = value;
            }
        }

        /// <summary>
        /// Hiện ẩn GroupBox
        /// </summary>
        [Category("ButtonsArray")]
        public bool btnGroupBoxShow
        {
            get
            {
                return GroupBoxShow;
            }
            set
            {
                this.GroupBoxShow = value;
                this.Controls.Clear();
                this.grpButtons.Controls.Clear();
                if (GroupBoxShow)
                {
                    this.Controls.Add(this.grpButtons);
                    this.grpButtons.Controls.Add(this.btnThoat);
                    this.grpButtons.Controls.Add(this.btnExcel);
                    this.grpButtons.Controls.Add(this.btnWord);
                    this.grpButtons.Controls.Add(this.btnInan);
                    this.grpButtons.Controls.Add(this.btnXem);
                    this.grpButtons.Controls.Add(this.btnLap);
                    this.grpButtons.Controls.Add(this.btnChon);
                    this.grpButtons.Controls.Add(this.btnXoa);
                    this.grpButtons.Controls.Add(this.btnSua);
                    this.grpButtons.Controls.Add(this.btnThem);
                    this.grpButtons.Controls.Add(this.btnLuu);
                    this.grpButtons.Controls.Add(this.btnBoqua);
                    this.grpButtons.Controls.Add(this.btnReset);
                    this.grpButtons.Controls.Add(this.btnPrintBarcode);
                }
                else
                {
                    this.Controls.Add(this.btnThoat);
                    this.Controls.Add(this.btnExcel);
                    this.Controls.Add(this.btnWord);
                    this.Controls.Add(this.btnInan);
                    this.Controls.Add(this.btnXem);
                    this.Controls.Add(this.btnLap);
                    this.Controls.Add(this.btnChon);
                    this.Controls.Add(this.btnXoa);
                    this.Controls.Add(this.btnSua);
                    this.Controls.Add(this.btnThem);
                    this.Controls.Add(this.btnLuu);
                    this.Controls.Add(this.btnBoqua);
                    this.Controls.Add(this.btnReset);
                    this.Controls.Add(this.btnPrintBarcode);
                }
            }
        }

        private bool GroupBoxShow = false;

        /// <summary>
        /// Đổi size của tất cả các button
        /// </summary>
        [Category("ButtonsArray")]
        public Size btnSize
        {
            get
            {
                return btnThoat.Size;
            }
            set
            {
                this.btnThoat.Size = value;
                ChangeThisSize();
            }
        }

        private int Spacing = 3;
        [Category("ButtonsArray")]
        public int btnSpacing
        {
            get
            {
                return Spacing;
            }
            set
            {
                Spacing = value;
                ChangeThisSize();
            }
        }


        /// <summary>
        /// Đổi color của tất cả các button
        /// </summary>
        [Category("ButtonsArray")]
        public Color btnBackColor
        {
            get
            {
                return btnThoat.BackColor;
            }
            set
            {
                this.btnThem.BackColor = value;
                this.btnSua.BackColor = value;
                this.btnXoa.BackColor = value;
                this.btnLuu.BackColor = value;
                this.btnBoqua.BackColor = value;
                this.btnInan.BackColor = value;
                this.btnExcel.BackColor = value;
                this.btnThoat.BackColor = value;
                this.btnChon.BackColor = value;
                this.btnLap.BackColor = value;
                this.btnXem.BackColor = value;
                this.btnWord.BackColor = value;
                this.btnReset.BackColor = value;
                this.btnPrintBarcode.BackColor = value;
            }
        }

        /// <summary>
        /// Đổi vị trí của button
        /// </summary>
        private void ChangeThisSize()
        {
            int i = 0;
            int x = 0;
            int y = 0;

            if (Style == btnStyleEnum.Array)
            {
                x = btnThoat.Size.Width + Spacing;
                y = 0;
            }
            else
            {
                x = 0;
                y = btnThoat.Size.Height + Spacing;
            }

            if (btnThem.Visible)
            {
                i++;
            }
            if (btnSua.Visible)
            {
                i++;
            }
            if (btnXoa.Visible)
            {
                i++;
            }
            if (btnInan.Visible)
            {
                i++;
            }
            if (btnPrintBarcode.Visible)
            {
                i++;
            }
            if (btnExcel.Visible)
            {
                i++;
            }
            if (btnThoat.Visible)
            {
                i++;
            }
            if (btnChon.Visible)
            {
                i++;
            }
            if (btnLap.Visible)
            {
                i++;
            }
            if (btnXem.Visible)
            {
                i++;
            }
            if (btnWord.Visible)
            {
                i++;
            }
            if (btnReset.Visible)
            {
                i++;
            }
            
            this.Size = new System.Drawing.Size(24 + btnThoat.Size.Width + (i - 1) * x, 24 + btnThoat.Size.Height + (i - 1) * y);
        }

        private btnStyleEnum Style;
        [Category("ButtonsArray")]
        public btnStyleEnum btnStyle
        {
            get
            {
                return Style;
            }
            set
            {
                Style = value;
                ChangeThisSize();
            }
        }


        private btnStyleGroupEnum StyleGroup;
        [Category("ButtonsArray")]
        public btnStyleGroupEnum btnStyleGroup
        {
            get
            {
                return StyleGroup;
            }
            set
            {
                StyleGroup = value;
                ChangeStyle();
            }
        }

        private void ChangeStyle()
        {
            if (StyleGroup != btnStyleGroupEnum.None)
            {
                this.btnThem.Visible = false;
                this.btnSua.Visible = false;
                this.btnXoa.Visible = false;
                this.btnLuu.Visible = false;
                this.btnBoqua.Visible = false;
                this.btnInan.Visible = false;
                this.btnPrintBarcode.Visible = false;
                this.btnExcel.Visible = false;
                this.btnThoat.Visible = false;
                this.btnChon.Visible = false;
                this.btnLap.Visible = false;
                this.btnXem.Visible = false;
                this.btnWord.Visible = false;
                this.btnReset.Visible = false;
                
                if (StyleGroup == btnStyleGroupEnum.All)
                {
                    this.btnThem.Visible = true;
                    this.btnSua.Visible = true;
                    this.btnXoa.Visible = true;
                    this.btnInan.Visible = true;
                    this.btnPrintBarcode.Visible = true;
                    this.btnExcel.Visible = true;
                    this.btnChon.Visible = true;
                    this.btnLap.Visible = true;
                    this.btnXem.Visible = true;
                    this.btnWord.Visible = true;
                    this.btnThoat.Visible = true;
                    this.btnReset.Visible = true;
                   
                }
                if (StyleGroup == btnStyleGroupEnum.Print)
                {
                    this.btnInan.Visible = true;
                }
                if (StyleGroup == btnStyleGroupEnum.Update)
                {
                    this.btnSua.Text = "&Sửa";
                    this.btnSua.Visible = true;
                }
                if (StyleGroup == btnStyleGroupEnum.CapNhat)
                {
                    this.btnSua.Text = "&Cập nhật";
                    this.btnSua.Visible = true;
                }
                if (StyleGroup == btnStyleGroupEnum.AddUpdate)
                {
                    this.btnThem.Visible = true;
                    this.btnSua.Visible = true;
                }
                if (StyleGroup == btnStyleGroupEnum.AddUpdateDelete)
                {
                    this.btnThem.Visible = true;
                    this.btnSua.Visible = true;
                    this.btnXoa.Visible = true;
                }
                if (StyleGroup == btnStyleGroupEnum.AddUpdateDeletePrint)
                {
                    this.btnThem.Visible = true;
                    this.btnSua.Visible = true;
                    this.btnXoa.Visible = true;
                    this.btnInan.Visible = true;
                }
                if (StyleGroup == btnStyleGroupEnum.UpdateDelete)
                {
                    this.btnSua.Visible = true;
                    this.btnXoa.Visible = true;
                }

                this.btnThoat.Visible = true;
                ChangeThisSize();
            }
            else
            {
                this.btnThem.Visible = ButtonVisibleArr[0];
                this.btnSua.Visible = ButtonVisibleArr[1];
                this.btnXoa.Visible = ButtonVisibleArr[2];
                this.btnChon.Visible = ButtonVisibleArr[3];
                this.btnLap.Visible = ButtonVisibleArr[4];
                this.btnXem.Visible = ButtonVisibleArr[5];
                this.btnInan.Visible = ButtonVisibleArr[6];
                this.btnPrintBarcode.Visible = ButtonVisibleArr[7];
                this.btnWord.Visible = ButtonVisibleArr[8];
                this.btnExcel.Visible = ButtonVisibleArr[9];
                this.btnReset.Visible = ButtonVisibleArr[10];
                this.btnThoat.Visible = ButtonVisibleArr[11];

                this.btnLuu.Visible = false;
                this.btnBoqua.Visible = false;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonThemVisible
        {
            set
            {
                btnThem.Visible = value;
                ButtonVisibleArr[0] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnThem.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonSuaVisible
        {
            set
            {
                btnSua.Visible = value;
                ButtonVisibleArr[1] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnSua.Visible;
            }
        }

        private btnUpdateEnum UpdateText;

        [Category("ButtonsArray")]
        public btnUpdateEnum ButtonSuaText
        {
            set
            {
                UpdateText = value;
                if (value == btnUpdateEnum.Sửa)
                    btnSua.Text = "&Sửa";
                else
                    btnSua.Text = "&Cập nhật";
            }
            get
            {
                return UpdateText;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonXoaVisible
        {
            set
            {
                btnXoa.Visible = value;
                ButtonVisibleArr[2] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnXoa.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonChonVisible
        {
            set
            {
                btnChon.Visible = value;
                ButtonVisibleArr[3] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnChon.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonLapVisible
        {
            set
            {
                btnLap.Visible = value;
                ButtonVisibleArr[4] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnLap.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonXemVisible
        {
            set
            {
                btnXem.Visible = value;
                ButtonVisibleArr[5] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnXem.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonInanVisible
        {
            set
            {
                btnInan.Visible = value;
                ButtonVisibleArr[6] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnInan.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonPrintBarcodeVisible
        {
            set
            {
                btnPrintBarcode.Visible = value;
                ButtonVisibleArr[7] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnPrintBarcode.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonWordVisible
        {
            set
            {
                btnWord.Visible = value;
                ButtonVisibleArr[8] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnWord.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonExcelVisible
        {
            set
            {
                btnExcel.Visible = value;
                ButtonVisibleArr[9] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnExcel.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonResetVisible
        {
            set
            {
                btnReset.Visible = value;
                ButtonVisibleArr[10] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnReset.Visible;
            }
        }

        [Category("ButtonsArray")]
        public bool ButtonThoatVisible
        {
            set
            {
                btnThoat.Visible = value;
                ButtonVisibleArr[11] = value;
                StyleGroup = btnStyleGroupEnum.None;
                ChangeThisSize();
            }
            get
            {
                return btnThoat.Visible;
            }
        }

      

        /// <summary>
        /// Trạng thái của button
        /// </summary>
        [Category("ButtonsArray")]
        public btnStateEnum btnState
        {
            set
            {
                if (value == btnStateEnum.Add || value == btnStateEnum.Update || value == btnStateEnum.Delete)
                {
                    this.btnLuu.Visible = true;
                    this.btnBoqua.Visible = true;

                    this.btnThem.Enabled = false;
                    this.btnSua.Enabled = false;
                    this.btnXoa.Enabled = false;
                    this.btnInan.Enabled = false;
                    this.btnExcel.Enabled = false;
                    this.btnThoat.Enabled = false;
                    this.btnChon.Enabled = false;
                    this.btnLap.Enabled = false;
                    this.btnXem.Enabled = false;
                    this.btnWord.Enabled = false;
                    this.btnReset.Enabled = false;
                    this.btnPrintBarcode.Enabled = false;
                    int i = 0;
                    if (btnThem.Visible && i < 2)
                    {
                        btnThem.Visible = false;
                        i++;
                    }
                    if (btnSua.Visible && i < 2)
                    {
                        btnSua.Visible = false;
                        i++;
                    }
                    if (btnXoa.Visible && i < 2)
                    {
                        btnXoa.Visible = false;
                        i++;
                    }
                    if (btnChon.Visible && i < 2)
                    {
                        btnChon.Visible = false;
                        i++;
                    }
                    if (btnLap.Visible && i < 2)
                    {
                        btnLap.Visible = false;
                        i++;
                    }
                    if (btnXem.Visible && i < 2)
                    {
                        btnXem.Visible = false;
                        i++;
                    }
                    if (btnInan.Visible && i < 2)
                    {
                        btnInan.Visible = false;
                        i++;
                    }
                    if (btnPrintBarcode.Visible && i < 2)
                    {
                        btnPrintBarcode.Visible = false;
                        i++;
                    }
                    if (btnWord.Visible && i < 2)
                    {
                        btnWord.Visible = false;
                        i++;
                    }
                    if (btnExcel.Visible && i < 2)
                    {
                        btnExcel.Visible = false;
                        i++;
                    }
                    if (btnReset.Visible && i < 2)
                    {
                        btnReset.Visible = false;
                        i++;
                    }
                    if (btnThoat.Visible && i < 2)
                    {
                        btnThoat.Visible = false;
                        i++;
                    }
                    
                }
                else
                {
                    this.btnLuu.Visible = false;
                    this.btnBoqua.Visible = false;

                    this.btnThem.Enabled = true;
                    this.btnSua.Enabled = true;
                    this.btnXoa.Enabled = true;
                    this.btnInan.Enabled = true;
                    this.btnPrintBarcode.Enabled = true;
                    this.btnExcel.Enabled = true;
                    this.btnChon.Enabled = true;
                    this.btnLap.Enabled = true;
                    this.btnXem.Enabled = true;
                    this.btnWord.Enabled = true;
                    this.btnReset.Enabled = true;
                    this.btnThoat.Enabled = true;
                    
                    ChangeStyle();
                }
            }
        }

        private void ButtonsArray_Resize(object sender, System.EventArgs e)
        {
            int i = 0;
            int x = 0;
            int y = 0;

            if (btnThem.Visible)
            {
                i++;
            }
            if (btnSua.Visible)
            {
                i++;
            }
            if (btnXoa.Visible)
            {
                i++;
            }
            if (btnChon.Visible)
            {
                i++;
            }
            if (btnLap.Visible)
            {
                i++;
            }
            if (btnXem.Visible)
            {
                i++;
            }
            if (btnInan.Visible)
            {
                i++;
            }
            if (btnPrintBarcode.Visible)
            {
                i++;
            }
            if (btnWord.Visible)
            {
                i++;
            }
            if (btnExcel.Visible)
            {
                i++;
            }
            if (btnReset.Visible)
            {
                i++;
            }
            if (btnThoat.Visible)
            {
                i++;
            }
            
            if (i == 0) return;
            x = 0;
            y = 0;
            if (Style == btnStyleEnum.Array)
            {
                x = (this.Size.Width - 24 - Spacing * (i - 1)) / i;
                y = this.Size.Height - 24;
            }
            else
            {
                x = this.Size.Width - 24;
                y = (this.Size.Height - 24 - Spacing * (i - 1)) / i;
            }

            Size vSize = new Size(x, y);
            this.btnThem.Size = vSize;
            this.btnSua.Size = vSize;
            this.btnXoa.Size = vSize;
            this.btnLuu.Size = vSize;
            this.btnBoqua.Size = vSize;
            this.btnInan.Size = vSize;
            this.btnPrintBarcode.Size = vSize;
            this.btnExcel.Size = vSize;
            this.btnThoat.Size = vSize;
            this.btnChon.Size = vSize;
            this.btnLap.Size = vSize;
            this.btnXem.Size = vSize;
            this.btnReset.Size = vSize;
            this.btnWord.Size = vSize;
           
            i = 0;
            if (Style == btnStyleEnum.Array)
            {
                x = btnThoat.Size.Width + Spacing;
                y = 0;
            }
            else
            {
                x = 0;
                y = btnThoat.Size.Height + Spacing;
            }

            if (btnThem.Visible)
            {
                this.btnThem.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnSua.Visible)
            {
                this.btnSua.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnXoa.Visible)
            {
                this.btnXoa.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            //if(btnLuu.Visible)
            {
                this.btnLuu.Location = new System.Drawing.Point(8 + 0 * x, 11 + 0 * y);
            }
            //if(btnBoqua.Visible)
            {
                this.btnBoqua.Location = new System.Drawing.Point(8 + 1 * x, 11 + 1 * y);
            }
            if (btnChon.Visible)
            {
                this.btnChon.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnLap.Visible)
            {
                this.btnLap.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnXem.Visible)
            {
                this.btnXem.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnInan.Visible)
            {
                this.btnInan.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnPrintBarcode.Visible)
            {
                this.btnPrintBarcode.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnWord.Visible)
            {
                this.btnWord.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnExcel.Visible)
            {
                this.btnExcel.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnReset.Visible)
            {
                this.btnReset.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            if (btnThoat.Visible)
            {
                this.btnThoat.Location = new System.Drawing.Point(8 + i * x, 11 + i * y);
                i++;
            }
            
            this.grpButtons.Size = new System.Drawing.Size(16 + btnThoat.Size.Width + (i - 1) * x, 16 + btnThoat.Size.Height + (i - 1) * y);
        }


        /// <summary>
        /// Event cho các button
        /// </summary>
        private void btnThem_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventAdd_click != null)
                this.btnEventAdd_click(sender, e);
        }

        private void btnSua_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventUpdate_click != null)
                this.btnEventUpdate_click(sender, e);
        }

        private void btnXoa_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventDelete_click != null)
                this.btnEventDelete_click(sender, e);
        }

        private void btnLuu_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventSave_click != null)
                this.btnEventSave_click(sender, e);
        }

        private void btnBoqua_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventRevert_click != null)
                this.btnEventRevert_click(sender, e);
        }

        private void btnInan_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventPrint_click != null)
                this.btnEventPrint_click(sender, e);
        }

        private void btnExcel_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventExcel_click != null)
                this.btnEventExcel_click(sender, e);
        }

        private void btnThoat_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventClose_click != null)
                this.btnEventClose_click(sender, e);
        }

        private void btnChon_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventSelect_click != null)
                this.btnEventSelect_click(sender, e);
        }

        private void btnLap_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventProduceReports_click != null)
                this.btnEventProduceReports_click(sender, e);
        }

        private void btnXem_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventView_click != null)
                this.btnEventView_click(sender, e);
        }

        private void btnWord_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventWord_click != null)
                this.btnEventWord_click(sender, e);
        }

        private void btnReset_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventReset_click != null)
                this.btnEventReset_click(sender, e);
        }

        private void btnPrintBarcode_Click(object sender, System.EventArgs e)
        {
            if (this.btnEventPrintBarcode_click != null)
                this.btnEventPrintBarcode_click(sender, e);
        }

        /// <summary>
        /// Tạo enumeration cho button dạng dòng hay cột
        /// </summary>
        public enum btnStyleEnum
        {
            Array,
            Column
        }

        /// <summary>
        /// Tạo enumeration cho trạng thái của button sửa.
        /// </summary>
        public enum btnUpdateEnum
        {
            Update,
            Sửa
        }

        /// <summary>
        /// Tạo enumeration cho trạng thái của button
        /// </summary>
        [Description("test")]
        public enum btnStateEnum
        {
            Int,
            Add,
            Update,
            Delete,
            Save,
            Revert
        }

        /// <summary>
        /// Tạo enumeration cho kiểu của các button
        /// </summary>
        public enum btnStyleGroupEnum
        {
            All,
            Print,
            Update,
            CapNhat,
            AddUpdate,
            AddUpdateDelete,
            AddUpdateDeletePrint,
            UpdateDelete,
            None
        }

        

        
    }
}
