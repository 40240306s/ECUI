using System;
using System.Windows.Forms;

namespace 愛因斯坦棋端點
{
    public partial class 連線設定 : Form
    {
        public ConnSetting s;

        public 連線設定(ConnSetting p)
        {
            InitializeComponent();
            s = new ConnSetting(p);
            EnterMode.SelectedIndex = s.RoomOrder ? 0 : 1;
            CHColor.Visible = s.RoomOrder;
            WhoFirst.SelectedIndex = s.IsMyBlue ? 0 : 1;
            IPBox.Text = s.IP;
            RoomName.Text = s.RoomName;
        }

        private void EnterMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender == EnterMode)
            {
                s.RoomOrder = EnterMode.SelectedIndex == 0;
                CHColor.Visible = s.RoomOrder;
            }
            if (sender == WhoFirst) s.IsMyBlue = WhoFirst.SelectedIndex == 0;


        }

        private void IPBox_TextChanged(object sender, EventArgs e)
        {
            if (sender == IPBox)
            {
                s.IP = IPBox.Text;
            }
            if (sender == RoomName)
            {
                s.RoomName = RoomName.Text;
            }
        }

        private void OKButtom_Click(object sender, EventArgs e)
        {
            if (OKButtom == sender)
            {
                if (IPBox.Text == string.Empty || RoomName.Text==string.Empty)
                {
                    MessageBox.Show("輸入不得是空字串");
                    return;
                }

                this.DialogResult = DialogResult.OK;
            }
            else this.DialogResult = DialogResult.Cancel;
        }
    
    
    
    
    
    }
}
