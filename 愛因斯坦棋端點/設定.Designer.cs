namespace 愛因斯坦棋端點
{
    partial class 設定
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(設定));
            this.TotalTimeLimit = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.PlayTimes = new System.Windows.Forms.NumericUpDown();
            this.ChangeTurn = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.WhoFirst = new System.Windows.Forms.ComboBox();
            this.SeleMode = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Stop = new System.Windows.Forms.CheckBox();
            this.OneSteoCB = new System.Windows.Forms.CheckBox();
            this.AllCB = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.RNString = new System.Windows.Forms.TextBox();
            this.RSM = new System.Windows.Forms.ComboBox();
            this.OneTimeLimit = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.PlayerR = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.RedIIni = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.RedEXEname = new System.Windows.Forms.Label();
            this.RedMode = new System.Windows.Forms.ComboBox();
            this.PlayerB = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.BlueIIni = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.BlueMode = new System.Windows.Forms.ComboBox();
            this.BlueEXEname = new System.Windows.Forms.Label();
            this.BlueGroup = new System.Windows.Forms.GroupBox();
            this.RedGroup = new System.Windows.Forms.GroupBox();
            this.OKButtom = new System.Windows.Forms.Button();
            this.NoButtom = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.TotalTimeLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayTimes)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OneTimeLimit)).BeginInit();
            this.PlayerR.SuspendLayout();
            this.PlayerB.SuspendLayout();
            this.BlueGroup.SuspendLayout();
            this.RedGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // TotalTimeLimit
            // 
            resources.ApplyResources(this.TotalTimeLimit, "TotalTimeLimit");
            this.TotalTimeLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.TotalTimeLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.TotalTimeLimit.Name = "TotalTimeLimit";
            this.TotalTimeLimit.Value = new decimal(new int[] {
            240,
            0,
            0,
            0});
            this.TotalTimeLimit.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // PlayTimes
            // 
            resources.ApplyResources(this.PlayTimes, "PlayTimes");
            this.PlayTimes.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.PlayTimes.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PlayTimes.Name = "PlayTimes";
            this.PlayTimes.Value = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.PlayTimes.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // ChangeTurn
            // 
            resources.ApplyResources(this.ChangeTurn, "ChangeTurn");
            this.ChangeTurn.Name = "ChangeTurn";
            this.ChangeTurn.UseVisualStyleBackColor = true;
            this.ChangeTurn.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // WhoFirst
            // 
            resources.ApplyResources(this.WhoFirst, "WhoFirst");
            this.WhoFirst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.WhoFirst.FormattingEnabled = true;
            this.WhoFirst.Items.AddRange(new object[] {
            resources.GetString("WhoFirst.Items"),
            resources.GetString("WhoFirst.Items1")});
            this.WhoFirst.Name = "WhoFirst";
            this.WhoFirst.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // SeleMode
            // 
            resources.ApplyResources(this.SeleMode, "SeleMode");
            this.SeleMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SeleMode.FormattingEnabled = true;
            this.SeleMode.Items.AddRange(new object[] {
            resources.GetString("SeleMode.Items"),
            resources.GetString("SeleMode.Items1")});
            this.SeleMode.Name = "SeleMode";
            this.SeleMode.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.Stop);
            this.groupBox1.Controls.Add(this.OneSteoCB);
            this.groupBox1.Controls.Add(this.AllCB);
            this.groupBox1.Controls.Add(this.groupBox4);
            this.groupBox1.Controls.Add(this.OneTimeLimit);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.WhoFirst);
            this.groupBox1.Controls.Add(this.SeleMode);
            this.groupBox1.Controls.Add(this.TotalTimeLimit);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.PlayTimes);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.ChangeTurn);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // Stop
            // 
            resources.ApplyResources(this.Stop, "Stop");
            this.Stop.Name = "Stop";
            this.Stop.UseVisualStyleBackColor = true;
            this.Stop.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // OneSteoCB
            // 
            resources.ApplyResources(this.OneSteoCB, "OneSteoCB");
            this.OneSteoCB.Checked = true;
            this.OneSteoCB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.OneSteoCB.Name = "OneSteoCB";
            this.OneSteoCB.UseVisualStyleBackColor = true;
            this.OneSteoCB.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // AllCB
            // 
            resources.ApplyResources(this.AllCB, "AllCB");
            this.AllCB.Name = "AllCB";
            this.AllCB.UseVisualStyleBackColor = true;
            this.AllCB.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.RNString);
            this.groupBox4.Controls.Add(this.RSM);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // RNString
            // 
            resources.ApplyResources(this.RNString, "RNString");
            this.RNString.Name = "RNString";
            this.RNString.TextChanged += new System.EventHandler(this.RNString_TextChanged);
            // 
            // RSM
            // 
            resources.ApplyResources(this.RSM, "RSM");
            this.RSM.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RSM.FormattingEnabled = true;
            this.RSM.Items.AddRange(new object[] {
            resources.GetString("RSM.Items"),
            resources.GetString("RSM.Items1"),
            resources.GetString("RSM.Items2"),
            resources.GetString("RSM.Items3")});
            this.RSM.Name = "RSM";
            this.RSM.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // OneTimeLimit
            // 
            resources.ApplyResources(this.OneTimeLimit, "OneTimeLimit");
            this.OneTimeLimit.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.OneTimeLimit.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OneTimeLimit.Name = "OneTimeLimit";
            this.OneTimeLimit.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.OneTimeLimit.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // PlayerR
            // 
            resources.ApplyResources(this.PlayerR, "PlayerR");
            this.PlayerR.AllowDrop = true;
            this.PlayerR.Controls.Add(this.label9);
            this.PlayerR.Controls.Add(this.label3);
            this.PlayerR.Controls.Add(this.RedIIni);
            this.PlayerR.Controls.Add(this.label10);
            this.PlayerR.Controls.Add(this.RedEXEname);
            this.PlayerR.Controls.Add(this.RedMode);
            this.PlayerR.Name = "PlayerR";
            this.PlayerR.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDrop_);
            this.PlayerR.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnter_);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // RedIIni
            // 
            resources.ApplyResources(this.RedIIni, "RedIIni");
            this.RedIIni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RedIIni.FormattingEnabled = true;
            this.RedIIni.Items.AddRange(new object[] {
            resources.GetString("RedIIni.Items"),
            resources.GetString("RedIIni.Items1")});
            this.RedIIni.Name = "RedIIni";
            this.RedIIni.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // RedEXEname
            // 
            resources.ApplyResources(this.RedEXEname, "RedEXEname");
            this.RedEXEname.Name = "RedEXEname";
            // 
            // RedMode
            // 
            resources.ApplyResources(this.RedMode, "RedMode");
            this.RedMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RedMode.FormattingEnabled = true;
            this.RedMode.Items.AddRange(new object[] {
            resources.GetString("RedMode.Items"),
            resources.GetString("RedMode.Items1")});
            this.RedMode.Name = "RedMode";
            this.RedMode.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // PlayerB
            // 
            resources.ApplyResources(this.PlayerB, "PlayerB");
            this.PlayerB.AllowDrop = true;
            this.PlayerB.Controls.Add(this.label13);
            this.PlayerB.Controls.Add(this.label12);
            this.PlayerB.Controls.Add(this.BlueIIni);
            this.PlayerB.Controls.Add(this.label8);
            this.PlayerB.Controls.Add(this.BlueMode);
            this.PlayerB.Controls.Add(this.BlueEXEname);
            this.PlayerB.Name = "PlayerB";
            this.PlayerB.DragDrop += new System.Windows.Forms.DragEventHandler(this.DragDrop_);
            this.PlayerB.DragEnter += new System.Windows.Forms.DragEventHandler(this.DragEnter_);
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // BlueIIni
            // 
            resources.ApplyResources(this.BlueIIni, "BlueIIni");
            this.BlueIIni.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlueIIni.FormattingEnabled = true;
            this.BlueIIni.Items.AddRange(new object[] {
            resources.GetString("BlueIIni.Items"),
            resources.GetString("BlueIIni.Items1")});
            this.BlueIIni.Name = "BlueIIni";
            this.BlueIIni.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // BlueMode
            // 
            resources.ApplyResources(this.BlueMode, "BlueMode");
            this.BlueMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BlueMode.FormattingEnabled = true;
            this.BlueMode.Items.AddRange(new object[] {
            resources.GetString("BlueMode.Items"),
            resources.GetString("BlueMode.Items1")});
            this.BlueMode.Name = "BlueMode";
            this.BlueMode.SelectedIndexChanged += new System.EventHandler(this.ValueChanged);
            // 
            // BlueEXEname
            // 
            resources.ApplyResources(this.BlueEXEname, "BlueEXEname");
            this.BlueEXEname.Name = "BlueEXEname";
            // 
            // BlueGroup
            // 
            resources.ApplyResources(this.BlueGroup, "BlueGroup");
            this.BlueGroup.Controls.Add(this.PlayerB);
            this.BlueGroup.Name = "BlueGroup";
            this.BlueGroup.TabStop = false;
            // 
            // RedGroup
            // 
            resources.ApplyResources(this.RedGroup, "RedGroup");
            this.RedGroup.Controls.Add(this.PlayerR);
            this.RedGroup.Name = "RedGroup";
            this.RedGroup.TabStop = false;
            // 
            // OKButtom
            // 
            resources.ApplyResources(this.OKButtom, "OKButtom");
            this.OKButtom.Name = "OKButtom";
            this.OKButtom.UseVisualStyleBackColor = true;
            this.OKButtom.Click += new System.EventHandler(this.Click_);
            // 
            // NoButtom
            // 
            resources.ApplyResources(this.NoButtom, "NoButtom");
            this.NoButtom.Name = "NoButtom";
            this.NoButtom.UseVisualStyleBackColor = true;
            this.NoButtom.Click += new System.EventHandler(this.Click_);
            // 
            // 設定
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.NoButtom);
            this.Controls.Add(this.OKButtom);
            this.Controls.Add(this.RedGroup);
            this.Controls.Add(this.BlueGroup);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "設定";
            ((System.ComponentModel.ISupportInitialize)(this.TotalTimeLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PlayTimes)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OneTimeLimit)).EndInit();
            this.PlayerR.ResumeLayout(false);
            this.PlayerR.PerformLayout();
            this.PlayerB.ResumeLayout(false);
            this.PlayerB.PerformLayout();
            this.BlueGroup.ResumeLayout(false);
            this.RedGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown TotalTimeLimit;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown PlayTimes;
        private System.Windows.Forms.CheckBox ChangeTurn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox WhoFirst;
        private System.Windows.Forms.ComboBox SeleMode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Panel PlayerR;
        private System.Windows.Forms.Label RedEXEname;
        private System.Windows.Forms.ComboBox RedMode;
        private System.Windows.Forms.Panel PlayerB;
        private System.Windows.Forms.Label BlueEXEname;
        private System.Windows.Forms.ComboBox BlueMode;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox BlueGroup;
        private System.Windows.Forms.GroupBox RedGroup;
        private System.Windows.Forms.Button OKButtom;
        private System.Windows.Forms.Button NoButtom;
        private System.Windows.Forms.ComboBox RSM;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox RNString;
        private System.Windows.Forms.NumericUpDown OneTimeLimit;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox AllCB;
        private System.Windows.Forms.CheckBox OneSteoCB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox RedIIni;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox BlueIIni;
        private System.Windows.Forms.CheckBox Stop;
    }
}