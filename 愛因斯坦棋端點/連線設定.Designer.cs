namespace 愛因斯坦棋端點
{
    partial class 連線設定
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(連線設定));
            this.label6 = new System.Windows.Forms.Label();
            this.RoomName = new System.Windows.Forms.TextBox();
            this.EnterMode = new System.Windows.Forms.ComboBox();
            this.IPBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.WhoFirst = new System.Windows.Forms.ComboBox();
            this.NoButtom = new System.Windows.Forms.Button();
            this.OKButtom = new System.Windows.Forms.Button();
            this.CHColor = new System.Windows.Forms.Panel();
            this.CHColor.SuspendLayout();
            this.SuspendLayout();
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // RoomName
            // 
            resources.ApplyResources(this.RoomName, "RoomName");
            this.RoomName.Name = "RoomName";
            this.RoomName.TextChanged += new System.EventHandler(this.IPBox_TextChanged);
            // 
            // EnterMode
            // 
            resources.ApplyResources(this.EnterMode, "EnterMode");
            this.EnterMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EnterMode.FormattingEnabled = true;
            this.EnterMode.Items.AddRange(new object[] {
            resources.GetString("EnterMode.Items"),
            resources.GetString("EnterMode.Items1")});
            this.EnterMode.Name = "EnterMode";
            this.EnterMode.SelectedIndexChanged += new System.EventHandler(this.EnterMode_SelectedIndexChanged);
            // 
            // IPBox
            // 
            resources.ApplyResources(this.IPBox, "IPBox");
            this.IPBox.Name = "IPBox";
            this.IPBox.TextChanged += new System.EventHandler(this.IPBox_TextChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            this.WhoFirst.SelectedIndexChanged += new System.EventHandler(this.EnterMode_SelectedIndexChanged);
            // 
            // NoButtom
            // 
            resources.ApplyResources(this.NoButtom, "NoButtom");
            this.NoButtom.Name = "NoButtom";
            this.NoButtom.UseVisualStyleBackColor = true;
            this.NoButtom.Click += new System.EventHandler(this.OKButtom_Click);
            // 
            // OKButtom
            // 
            resources.ApplyResources(this.OKButtom, "OKButtom");
            this.OKButtom.Name = "OKButtom";
            this.OKButtom.UseVisualStyleBackColor = true;
            this.OKButtom.Click += new System.EventHandler(this.OKButtom_Click);
            // 
            // CHColor
            // 
            resources.ApplyResources(this.CHColor, "CHColor");
            this.CHColor.Controls.Add(this.label2);
            this.CHColor.Controls.Add(this.WhoFirst);
            this.CHColor.Name = "CHColor";
            // 
            // 連線設定
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.CHColor);
            this.Controls.Add(this.NoButtom);
            this.Controls.Add(this.OKButtom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.RoomName);
            this.Controls.Add(this.EnterMode);
            this.Controls.Add(this.IPBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "連線設定";
            this.CHColor.ResumeLayout(false);
            this.CHColor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox RoomName;
        private System.Windows.Forms.ComboBox EnterMode;
        private System.Windows.Forms.TextBox IPBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox WhoFirst;
        private System.Windows.Forms.Button NoButtom;
        private System.Windows.Forms.Button OKButtom;
        private System.Windows.Forms.Panel CHColor;
    }
}