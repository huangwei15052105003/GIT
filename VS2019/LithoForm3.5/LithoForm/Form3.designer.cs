namespace LithoForm

{
    partial class Form3
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.extraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.radioCd = new System.Windows.Forms.RadioButton();
            this.radioTrx = new System.Windows.Forms.RadioButton();
            this.radioTry = new System.Windows.Forms.RadioButton();
            this.radioExpX = new System.Windows.Forms.RadioButton();
            this.radioExpY = new System.Windows.Forms.RadioButton();
            this.radioRot = new System.Windows.Forms.RadioButton();
            this.radioOrt = new System.Windows.Forms.RadioButton();
            this.radioSMag = new System.Windows.Forms.RadioButton();
            this.radioSRot = new System.Windows.Forms.RadioButton();
            this.radioAMag = new System.Windows.Forms.RadioButton();
            this.radioARot = new System.Windows.Forms.RadioButton();
            this.radioOvl = new System.Windows.Forms.RadioButton();
            this.ByLot = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ByPartLayer = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.Simple = new System.Windows.Forms.RadioButton();
            this.Import = new System.Windows.Forms.RadioButton();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extraToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(470, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // extraToolStripMenuItem
            // 
            this.extraToolStripMenuItem.Name = "extraToolStripMenuItem";
            this.extraToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.extraToolStripMenuItem.Text = "Reserved";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.SystemColors.Window;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 27);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(254, 129);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
            // 
            // radioCd
            // 
            this.radioCd.AutoSize = true;
            this.radioCd.BackColor = System.Drawing.Color.LavenderBlush;
            this.radioCd.Checked = true;
            this.radioCd.Location = new System.Drawing.Point(56, 3);
            this.radioCd.Name = "radioCd";
            this.radioCd.Size = new System.Drawing.Size(35, 16);
            this.radioCd.TabIndex = 2;
            this.radioCd.TabStop = true;
            this.radioCd.Text = "CD";
            this.radioCd.UseVisualStyleBackColor = false;
            // 
            // radioTrx
            // 
            this.radioTrx.AutoSize = true;
            this.radioTrx.Location = new System.Drawing.Point(3, 24);
            this.radioTrx.Name = "radioTrx";
            this.radioTrx.Size = new System.Drawing.Size(41, 16);
            this.radioTrx.TabIndex = 2;
            this.radioTrx.Text = "TrX";
            this.radioTrx.UseVisualStyleBackColor = true;
            // 
            // radioTry
            // 
            this.radioTry.AutoSize = true;
            this.radioTry.Location = new System.Drawing.Point(56, 24);
            this.radioTry.Name = "radioTry";
            this.radioTry.Size = new System.Drawing.Size(41, 16);
            this.radioTry.TabIndex = 2;
            this.radioTry.Text = "TrY";
            this.radioTry.UseVisualStyleBackColor = true;
            // 
            // radioExpX
            // 
            this.radioExpX.AutoSize = true;
            this.radioExpX.Location = new System.Drawing.Point(3, 45);
            this.radioExpX.Name = "radioExpX";
            this.radioExpX.Size = new System.Drawing.Size(47, 16);
            this.radioExpX.TabIndex = 2;
            this.radioExpX.Text = "ExpX";
            this.radioExpX.UseVisualStyleBackColor = true;
            // 
            // radioExpY
            // 
            this.radioExpY.AutoSize = true;
            this.radioExpY.Location = new System.Drawing.Point(56, 45);
            this.radioExpY.Name = "radioExpY";
            this.radioExpY.Size = new System.Drawing.Size(47, 16);
            this.radioExpY.TabIndex = 2;
            this.radioExpY.Text = "ExpY";
            this.radioExpY.UseVisualStyleBackColor = true;
            // 
            // radioRot
            // 
            this.radioRot.AutoSize = true;
            this.radioRot.Location = new System.Drawing.Point(3, 66);
            this.radioRot.Name = "radioRot";
            this.radioRot.Size = new System.Drawing.Size(41, 16);
            this.radioRot.TabIndex = 2;
            this.radioRot.Text = "Rot";
            this.radioRot.UseVisualStyleBackColor = true;
            // 
            // radioOrt
            // 
            this.radioOrt.AutoSize = true;
            this.radioOrt.Location = new System.Drawing.Point(56, 66);
            this.radioOrt.Name = "radioOrt";
            this.radioOrt.Size = new System.Drawing.Size(41, 16);
            this.radioOrt.TabIndex = 2;
            this.radioOrt.Text = "Ort";
            this.radioOrt.UseVisualStyleBackColor = true;
            // 
            // radioSMag
            // 
            this.radioSMag.AutoSize = true;
            this.radioSMag.Location = new System.Drawing.Point(3, 87);
            this.radioSMag.Name = "radioSMag";
            this.radioSMag.Size = new System.Drawing.Size(47, 16);
            this.radioSMag.TabIndex = 2;
            this.radioSMag.Text = "SMag";
            this.radioSMag.UseVisualStyleBackColor = true;
            // 
            // radioSRot
            // 
            this.radioSRot.AutoSize = true;
            this.radioSRot.Location = new System.Drawing.Point(3, 108);
            this.radioSRot.Name = "radioSRot";
            this.radioSRot.Size = new System.Drawing.Size(47, 16);
            this.radioSRot.TabIndex = 2;
            this.radioSRot.Text = "SRot";
            this.radioSRot.UseVisualStyleBackColor = true;
            // 
            // radioAMag
            // 
            this.radioAMag.AutoSize = true;
            this.radioAMag.Location = new System.Drawing.Point(56, 87);
            this.radioAMag.Name = "radioAMag";
            this.radioAMag.Size = new System.Drawing.Size(47, 16);
            this.radioAMag.TabIndex = 2;
            this.radioAMag.Text = "AMag";
            this.radioAMag.UseVisualStyleBackColor = true;
            // 
            // radioARot
            // 
            this.radioARot.AutoSize = true;
            this.radioARot.Location = new System.Drawing.Point(56, 108);
            this.radioARot.Name = "radioARot";
            this.radioARot.Size = new System.Drawing.Size(47, 16);
            this.radioARot.TabIndex = 2;
            this.radioARot.Text = "ARot";
            this.radioARot.UseVisualStyleBackColor = true;
            // 
            // radioOvl
            // 
            this.radioOvl.AutoSize = true;
            this.radioOvl.BackColor = System.Drawing.Color.LavenderBlush;
            this.radioOvl.Location = new System.Drawing.Point(3, 3);
            this.radioOvl.Name = "radioOvl";
            this.radioOvl.Size = new System.Drawing.Size(41, 16);
            this.radioOvl.TabIndex = 2;
            this.radioOvl.Text = "Ovl";
            this.radioOvl.UseVisualStyleBackColor = false;
            // 
            // ByLot
            // 
            this.ByLot.AutoSize = true;
            this.ByLot.Location = new System.Drawing.Point(3, 3);
            this.ByLot.Name = "ByLot";
            this.ByLot.Size = new System.Drawing.Size(53, 16);
            this.ByLot.TabIndex = 3;
            this.ByLot.Text = "ByLot";
            this.ByLot.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.ByPartLayer);
            this.panel1.Controls.Add(this.ByLot);
            this.panel1.Location = new System.Drawing.Point(371, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(97, 44);
            this.panel1.TabIndex = 4;
            // 
            // ByPartLayer
            // 
            this.ByPartLayer.AutoSize = true;
            this.ByPartLayer.Checked = true;
            this.ByPartLayer.Location = new System.Drawing.Point(3, 25);
            this.ByPartLayer.Name = "ByPartLayer";
            this.ByPartLayer.Size = new System.Drawing.Size(89, 16);
            this.ByPartLayer.TabIndex = 5;
            this.ByPartLayer.TabStop = true;
            this.ByPartLayer.Text = "ByPartLayer";
            this.ByPartLayer.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radioOvl);
            this.panel2.Controls.Add(this.radioCd);
            this.panel2.Controls.Add(this.radioARot);
            this.panel2.Controls.Add(this.radioTrx);
            this.panel2.Controls.Add(this.radioSRot);
            this.panel2.Controls.Add(this.radioAMag);
            this.panel2.Controls.Add(this.radioTry);
            this.panel2.Controls.Add(this.radioExpX);
            this.panel2.Controls.Add(this.radioSMag);
            this.panel2.Controls.Add(this.radioExpY);
            this.panel2.Controls.Add(this.radioOrt);
            this.panel2.Controls.Add(this.radioRot);
            this.panel2.Location = new System.Drawing.Point(260, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(110, 129);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LightYellow;
            this.panel3.Controls.Add(this.Simple);
            this.panel3.Controls.Add(this.Import);
            this.panel3.Location = new System.Drawing.Point(373, 80);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(97, 44);
            this.panel3.TabIndex = 4;
            // 
            // Simple
            // 
            this.Simple.AutoSize = true;
            this.Simple.Checked = true;
            this.Simple.Location = new System.Drawing.Point(3, 25);
            this.Simple.Name = "Simple";
            this.Simple.Size = new System.Drawing.Size(59, 16);
            this.Simple.TabIndex = 5;
            this.Simple.TabStop = true;
            this.Simple.Text = "Simple";
            this.Simple.UseVisualStyleBackColor = true;
            // 
            // Import
            // 
            this.Import.AutoSize = true;
            this.Import.Location = new System.Drawing.Point(3, 3);
            this.Import.Name = "Import";
            this.Import.Size = new System.Drawing.Size(95, 16);
            this.Import.TabIndex = 3;
            this.Import.Text = "Import待完成";
            this.Import.UseVisualStyleBackColor = true;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(470, 155);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form3";
            this.Text = "查看R2R CD OVL和JobinStation数据";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form3_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStripMenuItem extraToolStripMenuItem;
        private System.Windows.Forms.RadioButton radioCd;
        private System.Windows.Forms.RadioButton radioTrx;
        private System.Windows.Forms.RadioButton radioTry;
        private System.Windows.Forms.RadioButton radioExpX;
        private System.Windows.Forms.RadioButton radioExpY;
        private System.Windows.Forms.RadioButton radioRot;
        private System.Windows.Forms.RadioButton radioOrt;
        private System.Windows.Forms.RadioButton radioSMag;
        private System.Windows.Forms.RadioButton radioSRot;
        private System.Windows.Forms.RadioButton radioAMag;
        private System.Windows.Forms.RadioButton radioARot;
        private System.Windows.Forms.RadioButton radioOvl;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.RadioButton ByLot;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton ByPartLayer;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton Simple;
        private System.Windows.Forms.RadioButton Import;
    }
}