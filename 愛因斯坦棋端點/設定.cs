using System;
using System.IO;
using System.Windows.Forms;

namespace 愛因斯坦棋端點
{
    public partial class 設定 : Form
    {


        public Setting s;


        RandSeed[] RM = new RandSeed[4] { RandSeed.Default, RandSeed.Input, RandSeed.RunTime, RandSeed.GiveSeed };

        public 設定(Setting p, Connect c, ConnSetting cs)
        {
            InitializeComponent();
            try
            {
                s = new Setting(p);
                TotalTimeLimit.Value = s.totaltimelimit;
                OneTimeLimit.Value = s.onesteptimelimit;
                PlayTimes.Value = s.maxround;
                ChangeTurn.Checked = s.change;
                TotalTimeLimit.Enabled = AllCB.Checked = s.Usetotaltimelimit;
                OneTimeLimit.Enabled = OneSteoCB.Checked = s.Useonesteptimelimit;

                WhoFirst.SelectedIndex = s.BlueFirst ? 0 : 1;
                SeleMode.SelectedIndex = s.DeBugg ? 1 : 0;

                BlueIIni.SelectedIndex = s.blue.AutoIni ? 0 : 1;
                BlueMode.SelectedIndex = s.blue.playermode == PlayerMode.People ? 0 : 1;
                BlueEXEname.Text = Path.GetFileNameWithoutExtension(s.blue.path);

                RedIIni.SelectedIndex = s.red.AutoIni ? 0 : 1;
                RedMode.SelectedIndex = s.red.playermode == PlayerMode.People ? 0 : 1;
                RedEXEname.Text = Path.GetFileNameWithoutExtension(s.red.path);

                Stop.Checked = s.Stop;


                for (int i = 0; i < RM.Length; ++i) if (s.RandSeedMode == RM[i]) RSM.SelectedIndex = i;
                RNString.Text = p.RandNumberString;
                if (c.IsConnect)
                {
                    if (cs.IsMyBlue) RedGroup.Visible = false; else BlueGroup.Visible = false;
                    if (cs.RoomOrder == false)
                    {
                        groupBox1.Enabled = false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Setting file format is invalid");

                try { File.Delete("GAMESETTING.setting"); }
                catch { }

                throw e;
            }
        }
        private void ValueChanged(object sender, EventArgs e)
        {
            if (sender == TotalTimeLimit) s.totaltimelimit = (int)TotalTimeLimit.Value;
            if (sender == OneTimeLimit) s.onesteptimelimit = (int)OneTimeLimit.Value;
            if (sender == PlayTimes) s.maxround = (int)PlayTimes.Value;
            if (sender == ChangeTurn) s.change = ChangeTurn.Checked;
            if (sender == WhoFirst) s.BlueFirst = WhoFirst.SelectedIndex == 0;
            if (sender == SeleMode) s.DeBugg = SeleMode.SelectedIndex == 1;
            if (sender == AllCB) TotalTimeLimit.Enabled = s.Usetotaltimelimit = AllCB.Checked;
            if (sender == OneSteoCB) OneTimeLimit.Enabled = s.Useonesteptimelimit = OneSteoCB.Checked;

            if (sender == Stop) s.Stop = Stop.Checked;

            if (sender == RedIIni) s.red.AutoIni = RedIIni.SelectedIndex == 0;
            if (sender == BlueIIni) s.blue.AutoIni = BlueIIni.SelectedIndex == 0;
            if (sender == BlueMode)
            {
                s.blue.playermode = BlueMode.SelectedIndex == 1 ? PlayerMode.AI : PlayerMode.People;
                BlueEXEname.Visible = s.blue.playermode == PlayerMode.AI;
                label8.Visible = s.blue.playermode == PlayerMode.AI && s.blue.path == "";
                PlayerB.AllowDrop = s.blue.playermode == PlayerMode.AI;
            }
            if (sender == RedMode)
            {
                s.red.playermode = RedMode.SelectedIndex == 1 ? PlayerMode.AI : PlayerMode.People;
                RedEXEname.Visible = label10.Visible = s.red.playermode == PlayerMode.AI;
                label10.Visible = s.red.playermode == PlayerMode.AI && s.red.path == "";
                PlayerR.AllowDrop = s.red.playermode == PlayerMode.AI;
            }

            if (sender == RSM)
            {
                s.RandSeedMode = RM[RSM.SelectedIndex];
                RNString.Visible = s.RandSeedMode == RandSeed.Input || s.RandSeedMode == RandSeed.GiveSeed;
                RNString_TextChanged();
            }
        }

        private void DragEnter_(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else e.Effect = DragDropEffects.None;
        }

        private void DragDrop_(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (Path.GetExtension(path).ToLower() != ".exe") { MessageBox.Show("= =??"); return; }
            if (sender == PlayerB)
            {
                BlueEXEname.Text = Path.GetFileNameWithoutExtension(s.blue.path = path);
                label8.Visible = s.blue.playermode == PlayerMode.AI && s.blue.path == "";
            }
            else
            {
                RedEXEname.Text = Path.GetFileNameWithoutExtension(s.red.path = path);
                label10.Visible = s.red.playermode == PlayerMode.AI && s.red.path == "";
            }
        }

        private void Click_(object sender, EventArgs e)
        {
            if (OKButtom == sender)
            {
                if ((s.blue.playermode == PlayerMode.AI && s.blue.path == string.Empty) || (s.red.playermode == PlayerMode.AI && s.red.path == string.Empty))
                {
                    MessageBox.Show("尚未加入執行檔!");
                    return;
                }
                this.DialogResult = DialogResult.OK;
            }
            else this.DialogResult = DialogResult.Cancel;
        }

        private void RNString_TextChanged(object sender = null, EventArgs e = null)
        {

            for (int i = 0; i < RNString.Text.Length; ++i)
                if (!Char.IsDigit(RNString.Text[i]) || (s.RandSeedMode == RandSeed.Input && (RNString.Text[i] <= '0' || RNString.Text[i] > '6')) || (s.RandSeedMode == RandSeed.GiveSeed && RNString.Text.Length > 8)) { RNString.Text = ""; break; }
            if (RNString.Text == string.Empty) { RNString.Text = "123456"; }
            s.RandNumberString = RNString.Text;

        }









    }
}
