namespace sgFrmCheckWork
{
    partial class sgFrmUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(sgFrmUpdate));
            this.bAdd = new DevComponents.DotNetBar.ButtonX();
            this.bClear = new DevComponents.DotNetBar.ButtonX();
            this.labID = new DevComponents.DotNetBar.LabelX();
            this.labName = new DevComponents.DotNetBar.LabelX();
            this.txtName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labPinYin = new DevComponents.DotNetBar.LabelX();
            this.txtPinYin = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labSex = new DevComponents.DotNetBar.LabelX();
            this.labInDate = new DevComponents.DotNetBar.LabelX();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cbM = new DevComponents.Editors.ComboItem();
            this.cbW = new DevComponents.Editors.ComboItem();
            this.txtID = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dtiInDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labMessage = new DevComponents.DotNetBar.LabelX();
            this.dtiOutDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labOutDate = new DevComponents.DotNetBar.LabelX();
            this.labSound = new DevComponents.DotNetBar.LabelX();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.bSound = new DevComponents.DotNetBar.ButtonX();
            this.bUSound = new DevComponents.DotNetBar.ButtonX();
            this.txtSound = new DevComponents.DotNetBar.Controls.TextBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dtiInDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiOutDate)).BeginInit();
            this.SuspendLayout();
            // 
            // bAdd
            // 
            this.bAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bAdd.Location = new System.Drawing.Point(54, 522);
            this.bAdd.Name = "bAdd";
            this.bAdd.Size = new System.Drawing.Size(75, 23);
            this.bAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bAdd.TabIndex = 0;
            this.bAdd.Text = "提交";
            this.bAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // bClear
            // 
            this.bClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bClear.Location = new System.Drawing.Point(154, 522);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 23);
            this.bClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bClear.TabIndex = 1;
            this.bClear.Text = "关闭";
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // labID
            // 
            // 
            // 
            // 
            this.labID.BackgroundStyle.Class = "";
            this.labID.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labID.Location = new System.Drawing.Point(54, 23);
            this.labID.Name = "labID";
            this.labID.Size = new System.Drawing.Size(75, 23);
            this.labID.TabIndex = 3;
            this.labID.Text = "员工编号";
            // 
            // labName
            // 
            // 
            // 
            // 
            this.labName.BackgroundStyle.Class = "";
            this.labName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labName.Location = new System.Drawing.Point(54, 68);
            this.labName.Name = "labName";
            this.labName.Size = new System.Drawing.Size(75, 23);
            this.labName.TabIndex = 5;
            this.labName.Text = "姓名";
            // 
            // txtName
            // 
            // 
            // 
            // 
            this.txtName.Border.Class = "TextBoxBorder";
            this.txtName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtName.Location = new System.Drawing.Point(129, 68);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(100, 21);
            this.txtName.TabIndex = 4;
            // 
            // labPinYin
            // 
            // 
            // 
            // 
            this.labPinYin.BackgroundStyle.Class = "";
            this.labPinYin.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labPinYin.Location = new System.Drawing.Point(54, 113);
            this.labPinYin.Name = "labPinYin";
            this.labPinYin.Size = new System.Drawing.Size(75, 23);
            this.labPinYin.TabIndex = 7;
            this.labPinYin.Text = "拼音";
            // 
            // txtPinYin
            // 
            // 
            // 
            // 
            this.txtPinYin.Border.Class = "TextBoxBorder";
            this.txtPinYin.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPinYin.Location = new System.Drawing.Point(129, 113);
            this.txtPinYin.Name = "txtPinYin";
            this.txtPinYin.Size = new System.Drawing.Size(100, 21);
            this.txtPinYin.TabIndex = 6;
            // 
            // labSex
            // 
            // 
            // 
            // 
            this.labSex.BackgroundStyle.Class = "";
            this.labSex.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labSex.Location = new System.Drawing.Point(54, 159);
            this.labSex.Name = "labSex";
            this.labSex.Size = new System.Drawing.Size(75, 23);
            this.labSex.TabIndex = 9;
            this.labSex.Text = "性别";
            // 
            // labInDate
            // 
            // 
            // 
            // 
            this.labInDate.BackgroundStyle.Class = "";
            this.labInDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labInDate.Location = new System.Drawing.Point(54, 202);
            this.labInDate.Name = "labInDate";
            this.labInDate.Size = new System.Drawing.Size(75, 23);
            this.labInDate.TabIndex = 11;
            this.labInDate.Text = "入职日期";
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.DisplayMember = "Text";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 15;
            this.comboBoxEx1.Items.AddRange(new object[] {
            this.cbM,
            this.cbW});
            this.comboBoxEx1.Location = new System.Drawing.Point(129, 159);
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(100, 21);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 12;
            // 
            // cbM
            // 
            this.cbM.Text = "男";
            // 
            // cbW
            // 
            this.cbW.Text = "女";
            // 
            // txtID
            // 
            // 
            // 
            // 
            this.txtID.Border.Class = "TextBoxBorder";
            this.txtID.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtID.Location = new System.Drawing.Point(129, 23);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(100, 21);
            this.txtID.TabIndex = 13;
            this.txtID.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtID_MouseClick);
            // 
            // dtiInDate
            // 
            // 
            // 
            // 
            this.dtiInDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiInDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiInDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiInDate.ButtonDropDown.Visible = true;
            this.dtiInDate.IsPopupCalendarOpen = false;
            this.dtiInDate.Location = new System.Drawing.Point(129, 204);
            // 
            // 
            // 
            this.dtiInDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiInDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtiInDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtiInDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiInDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiInDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiInDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiInDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiInDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiInDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiInDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiInDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtiInDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiInDate.MonthCalendar.DisplayMonth = new System.DateTime(2011, 12, 1, 0, 0, 0, 0);
            this.dtiInDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiInDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiInDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiInDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiInDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiInDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtiInDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiInDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiInDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiInDate.Name = "dtiInDate";
            this.dtiInDate.Size = new System.Drawing.Size(100, 21);
            this.dtiInDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiInDate.TabIndex = 15;
            // 
            // labMessage
            // 
            // 
            // 
            // 
            this.labMessage.BackgroundStyle.Class = "";
            this.labMessage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labMessage.Location = new System.Drawing.Point(12, 299);
            this.labMessage.Name = "labMessage";
            this.labMessage.Size = new System.Drawing.Size(75, 23);
            this.labMessage.TabIndex = 17;
            // 
            // dtiOutDate
            // 
            // 
            // 
            // 
            this.dtiOutDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiOutDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiOutDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiOutDate.ButtonDropDown.Visible = true;
            this.dtiOutDate.IsPopupCalendarOpen = false;
            this.dtiOutDate.Location = new System.Drawing.Point(129, 248);
            // 
            // 
            // 
            this.dtiOutDate.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiOutDate.MonthCalendar.BackgroundStyle.BackColor = System.Drawing.SystemColors.Window;
            this.dtiOutDate.MonthCalendar.BackgroundStyle.Class = "";
            this.dtiOutDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiOutDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiOutDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiOutDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiOutDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiOutDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiOutDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiOutDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiOutDate.MonthCalendar.CommandsBackgroundStyle.Class = "";
            this.dtiOutDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiOutDate.MonthCalendar.DisplayMonth = new System.DateTime(2011, 12, 1, 0, 0, 0, 0);
            this.dtiOutDate.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtiOutDate.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtiOutDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiOutDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiOutDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiOutDate.MonthCalendar.NavigationBackgroundStyle.Class = "";
            this.dtiOutDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiOutDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiOutDate.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtiOutDate.Name = "dtiOutDate";
            this.dtiOutDate.Size = new System.Drawing.Size(100, 21);
            this.dtiOutDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiOutDate.TabIndex = 19;
            // 
            // labOutDate
            // 
            // 
            // 
            // 
            this.labOutDate.BackgroundStyle.Class = "";
            this.labOutDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labOutDate.Location = new System.Drawing.Point(54, 246);
            this.labOutDate.Name = "labOutDate";
            this.labOutDate.Size = new System.Drawing.Size(75, 23);
            this.labOutDate.TabIndex = 18;
            this.labOutDate.Text = "离职日期";
            // 
            // labSound
            // 
            // 
            // 
            // 
            this.labSound.BackgroundStyle.Class = "";
            this.labSound.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labSound.Location = new System.Drawing.Point(54, 289);
            this.labSound.Name = "labSound";
            this.labSound.Size = new System.Drawing.Size(75, 23);
            this.labSound.TabIndex = 20;
            this.labSound.Text = "声音文件";
            // 
            // ofd
            // 
            this.ofd.Filter = "音频文件(*.wav)|*.wav";
            // 
            // bSound
            // 
            this.bSound.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bSound.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bSound.Image = ((System.Drawing.Image)(resources.GetObject("bSound.Image")));
            this.bSound.Location = new System.Drawing.Point(237, 296);
            this.bSound.Name = "bSound";
            this.bSound.Size = new System.Drawing.Size(37, 36);
            this.bSound.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bSound.TabIndex = 21;
            this.bSound.Tooltip = "点击试听";
            this.bSound.Click += new System.EventHandler(this.bSound_Click);
            // 
            // bUSound
            // 
            this.bUSound.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bUSound.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bUSound.Location = new System.Drawing.Point(129, 289);
            this.bUSound.Name = "bUSound";
            this.bUSound.Size = new System.Drawing.Size(100, 23);
            this.bUSound.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bUSound.TabIndex = 22;
            this.bUSound.Text = "修改声音文件";
            this.bUSound.Click += new System.EventHandler(this.bUSound_Click);
            // 
            // txtSound
            // 
            // 
            // 
            // 
            this.txtSound.Border.Class = "TextBoxBorder";
            this.txtSound.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSound.Location = new System.Drawing.Point(129, 317);
            this.txtSound.Name = "txtSound";
            this.txtSound.Size = new System.Drawing.Size(100, 21);
            this.txtSound.TabIndex = 23;
            // 
            // sgFrmUpdate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(279, 575);
            this.Controls.Add(this.txtSound);
            this.Controls.Add(this.bUSound);
            this.Controls.Add(this.bSound);
            this.Controls.Add(this.labSound);
            this.Controls.Add(this.dtiOutDate);
            this.Controls.Add(this.labOutDate);
            this.Controls.Add(this.labMessage);
            this.Controls.Add(this.dtiInDate);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.comboBoxEx1);
            this.Controls.Add(this.labInDate);
            this.Controls.Add(this.labSex);
            this.Controls.Add(this.labPinYin);
            this.Controls.Add(this.txtPinYin);
            this.Controls.Add(this.labName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.labID);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "sgFrmUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改信息";
            this.Load += new System.EventHandler(this.sgFrmUpdate_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtiInDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiOutDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX bAdd;
        private DevComponents.DotNetBar.ButtonX bClear;
        private DevComponents.DotNetBar.LabelX labID;
        private DevComponents.DotNetBar.LabelX labName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtName;
        private DevComponents.DotNetBar.LabelX labPinYin;
        private DevComponents.DotNetBar.Controls.TextBoxX txtPinYin;
        private DevComponents.DotNetBar.LabelX labSex;
        private DevComponents.DotNetBar.LabelX labInDate;
        private DevComponents.DotNetBar.Controls.ComboBoxEx comboBoxEx1;
        private DevComponents.Editors.ComboItem cbM;
        private DevComponents.Editors.ComboItem cbW;
        private DevComponents.DotNetBar.Controls.TextBoxX txtID;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtiInDate;
        private DevComponents.DotNetBar.LabelX labMessage;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtiOutDate;
        private DevComponents.DotNetBar.LabelX labOutDate;
        private DevComponents.DotNetBar.LabelX labSound;
        private System.Windows.Forms.OpenFileDialog ofd;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private DevComponents.DotNetBar.ButtonX bSound;
        private DevComponents.DotNetBar.ButtonX bUSound;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSound;
    }
}