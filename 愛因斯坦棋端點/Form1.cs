using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using ECUI.Properties;


namespace 愛因斯坦棋端點
{

    delegate void Chesser();
    public enum GameMode { }
    public partial class Form1 : Form
    {
        static public void Message(string s)
        {
            if (gamesetting.DeBugg) Console.WriteLine(DateTime.Now.ToShortTimeString() + " >>" + s + "<<");
        }
        static public void MessageEx(string s)
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString() + " >>" + s + "<<");
        }
        public Connect node = new Connect();

        Recorder record;
        RecordReader ReadRecord;

        const char SpliKey = ' ';
        string ConnectName;
        void Work()
        {
            try
            {
                while (true)
                {

                    string[] spli = node.ReadLineEx().Trim().Split(SpliKey);

                    switch (spli[0].Trim().ToLower())
                    {
                        case "enter":
                            node.IsCanGame = true;
                            StartGameBottom.Enabled = true;
                            ArgBigSet();
                            node.WriteLine("talk ask");
                            MessageBox.Show("進房了!");
                            break;
                        case "bigset":
                            gamesetting.BlueFirst = spli[1] == "B";
                            gamesetting.Usetotaltimelimit = Convert.ToBoolean(spli[2]);
                            gamesetting.totaltimelimit = Convert.ToInt32(spli[3]);
                            gamesetting.Useonesteptimelimit = Convert.ToBoolean(spli[4]);
                            gamesetting.onesteptimelimit = Convert.ToInt32(spli[5]);

                            gamesetting.maxround = Convert.ToInt32(spli[6]);
                            gamesetting.change = Convert.ToBoolean(spli[7]);
                            ConnectName = spli[8];
                            connectsetting.IsMyBlue = spli[9] != "B";
                            game.AllIni(gamesetting);

                            if (connectsetting.IsMyBlue)
                            {
                                GameInfgroup.Text = game.GName = string.Format("{0} VS {1}", Path.GetFileNameWithoutExtension(gamesetting.blue.path), ConnectName);
                            }
                            else
                            {
                                GameInfgroup.Text = game.GName = string.Format("{0} VS {1}", ConnectName, Path.GetFileNameWithoutExtension(gamesetting.red.path));
                            }
                            break;
                        case "smallset":
                            ConnectName = spli[1];
                            if (connectsetting.IsMyBlue)
                            {
                                GameInfgroup.Text = game.GName = string.Format("{0} VS {1}", Path.GetFileNameWithoutExtension(gamesetting.blue.path), ConnectName);
                            }
                            else
                            {
                                GameInfgroup.Text = game.GName = string.Format("{0} VS {1}", ConnectName, Path.GetFileNameWithoutExtension(gamesetting.red.path));
                            }
                            IniUi();
                            break;
                        case "ask":
                            if (connectsetting.RoomOrder)
                            {
                                ArgBigSet();
                            }
                            else
                            {
                                ArgSmallSet();
                            }

                            break;
                        case "start":
                            StartGameBottom_Click(null, null);
                            return;
                        case "ok":
                            return;
                        default:


                            break;
                    }

                }
            }
            catch
            {
                DisConnectButtom_Click();
                return;
            }
        }
        /* gamesetting.BlueFirst = spli[1] == "B";
                                    gamesetting.Usetotaltimelimit = Convert.ToBoolean(spli[2]);
                                    gamesetting.totaltimelimit = Convert.ToInt32(spli[3]);
                                    gamesetting.Useonesteptimelimit = Convert.ToBoolean(spli[4]);
                                    gamesetting.onesteptimelimit = Convert.ToInt32(spli[5]);

                                    gamesetting.maxround = Convert.ToInt32(spli[6]);
                                    gamesetting.change = Convert.ToBoolean(spli[7]);
                                    ConnectName = spli[8];
                                    connectsetting.IsMyBlue = spli[9] != "B";*/
        private void ArgBigSet()
        {
            PlayerInf pla = (connectsetting.IsMyBlue ? gamesetting.blue : gamesetting.red);
            node.WriteLineEx(string.Format("talk bigset {0} {1} {2} {3} {4} {5} {6} {7} {8}",
                (gamesetting.BlueFirst ? "B" : "R"),
                gamesetting.Usetotaltimelimit.ToString(),
                gamesetting.totaltimelimit.ToString(),
                 gamesetting.Useonesteptimelimit.ToString(),
                gamesetting.onesteptimelimit.ToString(),
                gamesetting.maxround.ToString(),
                gamesetting.change.ToString(),
                (pla.playermode == PlayerMode.People ? "Hand" : Path.GetFileNameWithoutExtension(pla.path)),
                (connectsetting.IsMyBlue ? "B" : "R")
                 ));
        }
        void ArgSmallSet()
        {
            PlayerInf pla = (connectsetting.IsMyBlue ? gamesetting.blue : gamesetting.red);

            node.WriteLineEx(string.Format("talk smallset {0}",
                (pla.playermode == PlayerMode.People ? "Hand" : Path.GetFileNameWithoutExtension(pla.path))
                 ));

        }

        public void EXIT()
        {
            try
            {
                node.EXIT();
                Message("結束連線!");
            }
            catch { }
        }

        void RenewePb()
        {
            foreach (PictureBox p in Pb)
                this.Controls.Remove(p);

            int N = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    N = j + i * 5;
                    this.Controls.Add(Pb[N] = new PictureBox());
                    Pb[N].Height = 70;
                    Pb[N].Width = 70;
                    Pb[N].Top = i * 70 + 140;
                    Pb[N].Left = j * 70 + 18;
                    Pb[N].SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                }
            }
            Pb[24].BackgroundImage = Resources.紅棋end;
            Pb[0].BackgroundImage = Resources.藍棋end;
            ShowBoard();
            switch (UseUIMode.SelectedIndex)
            {
                case 1:

                    foreach (PictureBox p in Pb)
                    {
                        p.MouseClick += TestMClick;
                    }
                    break;
            }

        }
        void AddPb()
        {
            int N = 0;
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    N = j + i * 5;
                    this.Controls.Add(Pb[N] = new PictureBox());
                    Pb[N].Height = 70;
                    Pb[N].Width = 70;
                    Pb[N].Top = i * 70 + 140;
                    Pb[N].Left = j * 70 + 18;
                    Pb[N].SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
                }
            }
            Pb[24].BackgroundImage = Resources.紅棋end;
            Pb[0].BackgroundImage = Resources.藍棋end;
            ShowBoard();
        }
        System.Timers.Timer UITimer = new System.Timers.Timer();
        public Form1()
        {

            InitializeComponent();
            Form.CheckForIllegalCrossThreadCalls = false;

            AddPb();

            Size size = this.Size;
            size.Width = 650;
            this.Size = size;

            size = GameRecordList.Size;
            size.Height = 388;
            GameRecordList.Size = size;




            ReadSettingRecord();
            defC = this.Cursor;

            game.AllIni(gamesetting);
            IniUi();

            Testgroup.Location = Gamegroup.Location;
            TestInfgroup.Location = GameInfgroup.Location;
            ReadInfoGroup.Location = GameInfgroup.Location;
            TestButtomGroup.Location = GameButtomGroup.Location;
            ReadButtomGroup.Location = GameButtomGroup.Location;
            RecordInf.Location = DiceGroup.Location;
            EXEname.Text = Path.GetFileNameWithoutExtension(gamesetting.test.path);

            RSM.SelectedIndex = 1;
            TestColar.SelectedIndex = 0;

            KillAITimer.Elapsed += KillAITimer_Tick;

            UITimer.Elapsed += UITimer_Tick;
            UITimer.AutoReset = true;
            UITimer.Interval = 999;

            UseUIMode.SelectedIndex = 0;

            Chlabel = new Label[6] { LB1, LB2, LB3, LB4, LB5, LB6 };
            ChPic = new PictureBox[2] { BP, RP };
            IniTestUI();

            record = new Recorder(GameRecordList);
            ReadRecord = new RecordReader(GameRecordList, ReadRecNum);
        }

        #region 遊戲運作區


        #region UI相關變數
        const int numofpb = 25;
        PictureBox[] Pb = new PictureBox[numofpb];
        Bitmap[] RedCh = { (Bitmap)Resources.紅棋1.Clone(), (Bitmap)Resources.紅棋2.Clone(), (Bitmap)Resources.紅棋3.Clone(), (Bitmap)Resources.紅棋4.Clone(), (Bitmap)Resources.紅棋5.Clone(), (Bitmap)Resources.紅棋6.Clone() };
        Bitmap[] BlueCh = { (Bitmap)Resources.藍棋1.Clone(), (Bitmap)Resources.藍棋2.Clone(), (Bitmap)Resources.藍棋3.Clone(), (Bitmap)Resources.藍棋4.Clone(), (Bitmap)Resources.藍棋5.Clone(), (Bitmap)Resources.藍棋6.Clone() };
        Cursor defC;
        #endregion

        #region 遊戲設定變數
        Board nowboard = new Board(), startboard = new Board();
        List<int> 可選 = new List<int>();
        Random rand = new Random(DateTime.Today.Millisecond);
        static Setting gamesetting = new Setting();
        GameingInf game = new GameingInf();
        ConnSetting connectsetting = new ConnSetting();
        #endregion

        private void UpdataUI()
        {
            BPIC.BackColor = game.NowBlueTurn ? Color.Green : SystemColors.Control;
            RPIC.BackColor = game.NowBlueTurn ? SystemColors.Control : Color.Green;
            if (BTimeS.Visible = RTimeS.Visible = gamesetting.Usetotaltimelimit)
            {
                BTimeS.Text = "T " + ((int)game.TimeB).ToString() + 's';
                RTimeS.Text = "T " + ((int)game.TimeR).ToString() + 's';
            }
            if (OneStepTimeS.Visible = gamesetting.Useonesteptimelimit)
            {
                OneStepTimeS.Text = ((int)game.OneStepTime).ToString();
            }
            BWString.Text = game.Bwin.ToString();
            RWString.Text = game.Rwin.ToString();
            RoundString.Text = game.Round.ToString();
        }

        void IniUi()
        {
            UpdataUI();
            BWPString.Text = Enum.GetName(typeof(PlayerMode), gamesetting.blue.playermode);
            RWPString.Text = Enum.GetName(typeof(PlayerMode), gamesetting.red.playermode);
            GameInfgroup.Text = game.GName;
            //  DiceP.Visible = gamesetting.RandSeedMode == RandSeed.RunTime;
        }

        private void ReadSettingRecord()
        {
            Setting g = new Setting();// gamesetting;
            ConnSetting c = new ConnSetting();
            try
            {
                using (StreamReader rF = new StreamReader(settingPath))
                {
                    string line;
                    while ((line = rF.ReadLine()) != null)
                    {
                        string[] spli = line.Split('=');
                        //MessageEx(spli[0].Trim() + "__" + spli[1].Trim());
                        switch (spli[0].Trim())
                        {
                            case "blue":
                                g.blue.ReadFile(spli[1].Trim());
                                break;
                            case "red":
                                g.red.ReadFile(spli[1].Trim());
                                break;
                            case "totaltimelimit":
                                g.totaltimelimit = Convert.ToInt32(spli[1].Trim());
                                break;
                            case "onesteptimelimit":
                                g.onesteptimelimit = Convert.ToInt32(spli[1].Trim());
                                break;
                            case "Usetotaltimelimit":
                                g.Usetotaltimelimit = Convert.ToBoolean(spli[1].Trim());
                                break;
                            case "Useonesteptimelimit":
                                g.Useonesteptimelimit = Convert.ToBoolean(spli[1].Trim());
                                break;
                            case "maxround":
                                g.maxround = Convert.ToInt32(spli[1].Trim());
                                break;
                            case "change":
                                g.change = Convert.ToBoolean(spli[1].Trim());
                                break;
                            case "BlueFirst":
                                g.BlueFirst = Convert.ToBoolean(spli[1].Trim());
                                break;
                            case "DeBugg":
                                g.DeBugg = Convert.ToBoolean(spli[1].Trim());
                                break;
                            case "RandSeedMode":
                                g.RandSeedMode = (RandSeed)Enum.Parse(typeof(RandSeed), spli[1].Trim());
                                break;
                            case "RandNumberString":
                                g.RandNumberString = spli[1].Trim();
                                break;
                            case "Stop":
                                g.Stop = Convert.ToBoolean(spli[1].Trim());
                                break;
                            ///////////////////////////////////////////////////////////////////////////////////////////////////////
                            case "test":
                                g.test.ReadFile(spli[1].Trim());
                                break;
                            ///////////////////////////////////////////////////////////////////////////////////////////////////////
                            case "RoomName":
                                c.RoomName = spli[1].Trim();
                                break;
                            case "IP":
                                c.IP = spli[1].Trim();
                                break;
                            case "RoomOrder":
                                c.RoomOrder = Convert.ToBoolean(spli[1].Trim());
                                break;
                            case "IsMyBlue":
                                c.IsMyBlue = Convert.ToBoolean(spli[1].Trim());
                                break;
                        }

                    }
                }
            }
            catch (Exception e) { Message(e.Message); return; }
            gamesetting = new Setting(g);
            connectsetting = new ConnSetting(c);
            game.AllIni(gamesetting);
            IniUi();
        }
        string settingPath = "GAMESETTING.setting";
        private void WriteSettingRecord()
        {
            try
            {
                try
                {
                    System.IO.FileInfo fa = new FileInfo(settingPath);
                    fa.Attributes = FileAttributes.Normal;
                }
                catch { }
                using (StreamWriter oF = new StreamWriter(settingPath))
                {
                    gamesetting.WriteFile(oF);
                    connectsetting.WriteFile(oF);
                }
                try
                {
                    System.IO.FileInfo fa2 = new FileInfo(settingPath);
                    fa2.Attributes = FileAttributes.ReadOnly;
                }
                catch { }
            }
            catch (Exception e) { Message(e.Message); }
        }

        int ClickNum = -1;
        void IniMClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                for (int i = 0; i < 25; ++i)
                    if (Pb[i] == sender)
                    {
                        if (ClickNum == -1)
                        {
                            if (Pb[i].Image == null || (i > 12 && gamesetting.blue.AutoIni) || (i < 12 && gamesetting.red.AutoIni))
                            {
                            }
                            else
                            {
                                this.Cursor = new Cursor(new Bitmap(Pb[i].Image).GetHicon());
                                ClickNum = i;
                                PreOk.Enabled = EndGameBottom.Enabled = false;
                            }
                            break;
                        }
                        else
                        {
                            if ((i > 12) == (ClickNum > 12) && Pb[i] != null)
                                nowboard.Swap(i, ClickNum);
                            ShowBoard();
                            ClickNum = -1;
                            this.Cursor = defC;
                            PreOk.Enabled = EndGameBottom.Enabled = true;
                            break;
                        }
                    }
        }

        public void ShowBoard()
        {
            if (nowboard.Blue.Length != 6 || nowboard.Red.Length != 6 || nowboard.Boa.Length != 25) nowboard = new Board();
            try
            {
                for (int i = 0; i < 25; ++i)
                {
                    if (Pb[i].BackColor != Color.Silver || Pb[i].BackColor != Color.White) Pb[i].BackColor = (i % 2 == 0 ? Color.Silver : Color.White);
                    if (nowboard.Boa[i] == 0 && Pb[i].Image != null)
                    {
                        Pb[i].Image = null;
                    }
                }
                for (int i = 0; i < 6; ++i)
                {
                    if (nowboard.Blue[i] != 0 && Pb[nowboard.Blue[i] - 1].Image != BlueCh[i]) Pb[nowboard.Blue[i] - 1].Image = BlueCh[i];
                    if (nowboard.Red[i] != 0 && Pb[nowboard.Red[i] - 1].Image != RedCh[i]) Pb[nowboard.Red[i] - 1].Image = RedCh[i];
                }
            }
            catch //(Exception ec) 
            {

            }
        }


        private void GameSettingButtom_Click(object sender, EventArgs e)
        {
            設定 s = new 設定(gamesetting, node, connectsetting);
            if (s.ShowDialog() == DialogResult.OK)
            {
                gamesetting = s.s;
                WriteSettingRecord();
                game.AllIni(gamesetting);

                if (node.IsConnect && node.IsCanGame)
                {
                    if (connectsetting.RoomOrder) ArgBigSet();
                    else ArgSmallSet();
                    if (connectsetting.IsMyBlue)
                        GameInfgroup.Text = game.GName = string.Format("{0} VS {1}", Path.GetFileNameWithoutExtension(gamesetting.blue.path), ConnectName);
                    else
                        GameInfgroup.Text = game.GName = string.Format("{0} VS {1}", ConnectName, Path.GetFileNameWithoutExtension(gamesetting.red.path));
                }
                IniUi();
            }
        }

        Thread t;
        void Newer(PlayerInf p)
        {
            switch (p.playermode)
            {
                case PlayerMode.AI:
                    if (!File.Exists(p.path)) p.path = Path.GetFileName(p.path);
                    if (!File.Exists(p.path))
                    {
                        p.path = "";
                        MessageEx("AI not exist");
                        throw new Exception();
                    }
                    p.g = new AIer(p.path);
                    break;
                case PlayerMode.Connect:
                    p.g = new Neter(node);
                    break;
                case PlayerMode.People:
                    p.g = new Hander();
                    break;
            }
        }

        bool IsInCanUse(int[] bao, int num)
        {
            foreach (int i in bao) if (i == num) return true;
            return false;
        }

        bool IsValidIni(int[] det, int[] bao)
        {
            if (bao == null || (bao.Length != bao.Length)) return false;
            int[] cou = new int[26];
            for (int i = 0; i < 26; ++i) cou[i] = 0;
            foreach (int i in bao)
                if (cou[i] == 0 && IsInCanUse(det, i))
                { ++cou[i]; }
                else return false;
            return true;
        }
        List<GameRecord> RecordGameList;
        private void StartGameBottom_Click(object sender, EventArgs e)
        {
            GameSettingButtom.Enabled = false;
            DisConnectButtom.Enabled = false;
            StartGameBottom.Enabled = false;
            ConnectButtom.Enabled = false;
            LanguageChange.Enabled = false;
            UseUIMode.Enabled = false;
            GameRecordList.Enabled = false;

            nowboard = new Board(startboard);
            game.AllIni(gamesetting);
            if (node.IsConnect && node.IsCanGame)
            {
                if (connectsetting.IsMyBlue)
                    GameInfgroup.Text = game.GName = string.Format("{0} VS {1}", Path.GetFileNameWithoutExtension(gamesetting.blue.path), ConnectName);
                else
                    GameInfgroup.Text = game.GName = string.Format("{0} VS {1}", ConnectName, Path.GetFileNameWithoutExtension(gamesetting.red.path));
            }


            if (gamesetting.RandSeedMode == RandSeed.GiveSeed) rand = new Random(Convert.ToInt32(gamesetting.RandNumberString));
            IniUi();
            ShowBoard();
            Thread t = new Thread(StartTheGame);
            t.Start();
        }

        void StartTheGame()
        {
            if (node.IsConnect)
            {
                try
                {
                    if (connectsetting.IsMyBlue)
                    {
                        if (gamesetting.blue.playermode == PlayerMode.People)
                        {
                            MessageBox.Show("請切換至AI模式");
                            throw new Exception();
                        }
                        Newer(gamesetting.blue);
                    }
                    else
                    {
                        if (gamesetting.red.playermode == PlayerMode.People)
                        {
                            MessageBox.Show("請切換至AI模式");
                            throw new Exception();
                        }
                        Newer(gamesetting.red);
                    }
                }
                catch
                {
                    try
                    {
                        if (node.IsConnect) node.WriteLine("talk error");
                    }
                    catch
                    {
                        Message("斷線了");
                        EndGameBottom_Click();
                        DisConnectButtom_Click();
                        return;
                    }
                    Message("玩家載入失敗:請檢察AI是否存在或網路狀況!");
                    EndGameBottom_Click(null, null);
                    return;
                }
                try
                {
                    if (connectsetting.RoomOrder)
                    {
                        node.WriteLine("talk start");
                    }
                    else
                    {
                        node.WriteLine("talk ok");
                    }
                }
                catch
                {
                    Message("斷線了");
                    EndGameBottom_Click();
                    DisConnectButtom_Click();
                    return;
                }
                record.ChreatRecord(game.GName);
                MyNet = (connectsetting.IsMyBlue) ? gamesetting.blue : gamesetting.red;
                StartThreadNet();
            }
            else
            {
                EndGameBottom.Enabled = true;
                BackBottom.Enabled = (gamesetting.blue.playermode == PlayerMode.People) || (gamesetting.red.playermode == PlayerMode.People);
                try
                {
                    Newer(gamesetting.blue);
                    Newer(gamesetting.red);
                }
                catch
                {
                    Message("玩家載入失敗:請檢察AI是否存在或網路狀況!");
                    EndGameBottom_Click(null, null);
                    return;
                }

                record.ChreatRecord(game.GName);
                StartThread();
            }

        }


        void StartThread()
        {
            t = new Thread(GamePre);
            t.Start();
        }
        void StartThreadNet()
        {
            t = new Thread(GamePreNet);
            t.Start();
        }
        void GamePre()
        {
            if (gamesetting.blue.playermode == PlayerMode.People || gamesetting.red.playermode == PlayerMode.People) RecordGameList = new List<GameRecord>();
            if ((!gamesetting.blue.AutoIni) || (!gamesetting.red.AutoIni))
            {
                PreOk.Enabled = true;

                if (gamesetting.blue.AutoIni)
                    if (gamesetting.blue.playermode != PlayerMode.People)
                    {
                        try
                        {
                            nowboard.Blue = gamesetting.blue.g.Get("ini B");
                        }
                        catch
                        {
                            EndGame(false, "Error of blue AI : red win!");
                            return;
                        }
                        if (!IsValidIni(new int[6] { 25, 24, 20, 23, 19, 15 }, nowboard.Blue))
                        {
                            EndGame(false, "Error initialization of blue AI : red win!");
                            return;
                        }
                    }
                    else { nowboard.Blue = RandSequence(new int[6] { 25, 24, 20, 23, 19, 15 }); }
                if (gamesetting.red.AutoIni)
                    if (gamesetting.red.playermode != PlayerMode.People)
                    {
                        try
                        {
                            nowboard.Red = gamesetting.red.g.Get("ini R");
                        }
                        catch
                        {
                            EndGame(true, "Error of red AI : blue win!");
                            return;
                        }
                        if (!IsValidIni(new int[6] { 1, 2, 3, 6, 7, 11 }, nowboard.Red))
                        {
                            EndGame(true, "Error initialization of red AI : blue win!");
                            return;
                        }
                    }
                    else { nowboard.Red = RandSequence(new int[6] { 1, 2, 3, 6, 7, 11 }); }
                nowboard.Re();
                ShowBoard();
                for (int i = 0; i < 6; ++i)
                {
                    Pb[nowboard.Blue[i] - 1].MouseClick += IniMClick;
                    Pb[nowboard.Red[i] - 1].MouseClick += IniMClick;
                }
            }
            else
            {
                if (gamesetting.blue.playermode != PlayerMode.People)
                {
                    try
                    {
                        nowboard.Blue = gamesetting.blue.g.Get("ini B");
                    }
                    catch
                    {
                        EndGame(false, "Error of blue AI : red win!");
                        return;
                    }
                    if (!IsValidIni(new int[6] { 25, 24, 20, 23, 19, 15 }, nowboard.Blue))
                    {
                        EndGame(false, "Error initialization of blue AI : red win!");
                        return;
                    }
                }
                else { nowboard.Blue = RandSequence(new int[6] { 25, 24, 20, 23, 19, 15 }); }
                if (gamesetting.red.playermode != PlayerMode.People)
                {
                    try
                    {
                        nowboard.Red = gamesetting.red.g.Get("ini R");
                    }
                    catch
                    {
                        EndGame(true, "Error of red AI : blue win!");
                        return;
                    }
                    if (!IsValidIni(new int[6] { 1, 2, 3, 6, 7, 11 }, nowboard.Red))
                    {
                        EndGame(true, "Error initialization of red AI : blue win!");
                        return;
                    }
                }
                else { nowboard.Red = RandSequence(new int[6] { 1, 2, 3, 6, 7, 11 }); }


                nowboard.Re();
                ShowBoard();

                record.Add("ini " + nowboard.ToString());

                startboard = new Board(nowboard);
                UITimer.Start();
                sw.Reset();
                sw.Start();
                GameDiceTurn();
            }

        }

        private int[] RandSequence(int[] p)
        {
            int[] k = p;
            Random r = new Random();
            List<int> l = new List<int>(p), j = new List<int>();
            for (int i = 0; i < p.Length; ++i)
            {
                int index = r.Next() % l.Count;
                j.Add(l[index]);
                l.RemoveAt(index);
            }
            for (int i = 0; i < p.Length; ++i)
            {
                k[i] = j[i];
            }
            return k;
        }

        string GetString(int[] ar)
        {
            string line = "";
            foreach (int i in ar)
                line += (i.ToString() + " ");

            return line;
        }

        public int[] Get(string arg)
        {
            string[] line = arg.Split(' ');
            int[] bo = new int[line.Length];
            for (int i = 0; i < line.Length; ++i)
            {
                bo[i] = Convert.ToInt32(line[i]);
            }
            return bo;
        }

        void GamePreNet()
        {
            try
            {
                string line;
                if (connectsetting.IsMyBlue)
                {
                    nowboard.Blue = gamesetting.blue.g.Get("ini B");
                    if (!IsValidIni(new int[6] { 25, 24, 20, 23, 19, 15 }, nowboard.Blue))
                    {
                        node.WriteLineEx("talk error");
                        EndGameNet(false, "Error initialization of blue AI : red win!");
                        return;
                    }
                    else
                        node.WriteLineEx("talk " + GetString(nowboard.Blue));

                    line = node.ReadLineEx();
                    if (line == "error")
                    {
                        EndGameNet(connectsetting.IsMyBlue, "Error initialization of 對方的 AI : you win!");
                        return;
                    }
                    else nowboard.Red = Get(line);
                }
                else
                {
                    line = node.ReadLineEx();
                    if (line == "error")
                    {
                        EndGameNet(connectsetting.IsMyBlue, "Error initialization of 對方的 AI : you win!");
                        return;
                    }
                    else nowboard.Blue = Get(line);

                    nowboard.Red = gamesetting.red.g.Get("ini R");
                    if (!IsValidIni(new int[6] { 1, 2, 3, 6, 7, 11 }, nowboard.Red))
                    {
                        node.WriteLineEx("talk error");
                        EndGameNet(true, "Error initialization of red AI : blue win!");
                        return;
                    }
                    else
                        node.WriteLineEx("talk " + GetString(nowboard.Red));

                }
                nowboard.Re();
                ShowBoard();
                record.AddRecocd_EX("ini " + nowboard.ToString());

                UITimer.Start();
                sw.Reset();
                sw.Start();
                GameDiceTurnNet();
            }
            catch
            {
                Message("斷線了");
                EndGameBottom_Click();
                DisConnectButtom_Click();
                return;
            }
        }
        int GetDiceNet()
        {
            switch (gamesetting.RandSeedMode)
            {
                case RandSeed.Input:
                    {
                        int num = gamesetting.RandNumberString[game.RandNumPos] - '0';
                        NowDice = num;
                        ++game.RandNumPos;
                        game.RandNumPos %= gamesetting.RandNumberString.Length;
                        DiceB.Text = num.ToString();
                        return num;
                    }
                default:
                    {
                        int num = rand.Next(1, 7);
                        NowDice = num;
                        DiceB.Text = num.ToString();
                        return num;
                    }
            }
        }
        PlayerInf MyNet;
        bool IsVailid(bool IsBlueTurn, int[] ans, int dice)
        {
            if (ans == null || ans.Length != 2) return false;
            可選.Clear();
            int num = dice;

            if (IsBlueTurn)
            {

                if (nowboard.Blue[num - 1] != 0)
                {
                    可選.Add(nowboard.Blue[num - 1] - 1);
                }
                else
                {
                    for (int i = num + 1; i <= 6; ++i)
                        if (nowboard.Blue[i - 1] != 0)
                        {
                            可選.Add(nowboard.Blue[i - 1] - 1);
                            break;
                        }
                    for (int i = num - 1; i > 0; --i)
                        if (nowboard.Blue[i - 1] != 0)
                        {
                            可選.Add(nowboard.Blue[i - 1] - 1);
                            break;
                        }
                }
                if (InCanChoose(AnsSet[0] - 1) == false) return false;
                可選.Clear();
                num = AnsSet[0] - 1;
                int x = num % 5;
                int y = num / 5;
                if (x != 0)
                {
                    可選.Add(num - 1);
                    if (y != 0) 可選.Add(num - 6);
                }
                if (y != 0) 可選.Add(num - 5);
                if (InCanChoose(AnsSet[1] - 1) == false) return false;
            }
            else
            {
                if (nowboard.Red[num - 1] != 0)
                {
                    可選.Add(nowboard.Red[num - 1] - 1);
                }
                else
                {
                    for (int i = num + 1; i <= 6; ++i)
                        if (nowboard.Red[i - 1] != 0)
                        {
                            可選.Add(nowboard.Red[i - 1] - 1);
                            break;
                        }
                    for (int i = num - 1; i > 0; --i)
                        if (nowboard.Red[i - 1] != 0)
                        {
                            可選.Add(nowboard.Red[i - 1] - 1);
                            break;
                        }
                }
                if (InCanChoose(AnsSet[0] - 1) == false) return false;
                num = AnsSet[0] - 1;
                int x = num % 5;
                int y = num / 5;
                if (x != 4)
                {
                    可選.Add(num + 1);
                    if (y != 4) 可選.Add(num + 6);
                }
                if (y != 4) 可選.Add(num + 5);
                if (InCanChoose(AnsSet[1] - 1) == false) return false;
            }
            return true;
        }
        bool IsVailid(int[] ans, int dice)
        {
            if (ans == null || ans.Length != 2) return false;
            可選.Clear();
            int num = dice;

            if (game.NowBlueTurn)
            {

                if (nowboard.Blue[num - 1] != 0)
                {
                    可選.Add(nowboard.Blue[num - 1] - 1);
                }
                else
                {
                    for (int i = num + 1; i <= 6; ++i)
                        if (nowboard.Blue[i - 1] != 0)
                        {
                            可選.Add(nowboard.Blue[i - 1] - 1);
                            break;
                        }
                    for (int i = num - 1; i > 0; --i)
                        if (nowboard.Blue[i - 1] != 0)
                        {
                            可選.Add(nowboard.Blue[i - 1] - 1);
                            break;
                        }
                }
                if (InCanChoose(AnsSet[0] - 1) == false) return false;
                可選.Clear();
                num = AnsSet[0] - 1;
                int x = num % 5;
                int y = num / 5;
                if (x != 0)
                {
                    可選.Add(num - 1);
                    if (y != 0) 可選.Add(num - 6);
                }
                if (y != 0) 可選.Add(num - 5);
                if (InCanChoose(AnsSet[1] - 1) == false) return false;
            }
            else
            {
                if (nowboard.Red[num - 1] != 0)
                {
                    可選.Add(nowboard.Red[num - 1] - 1);
                }
                else
                {
                    for (int i = num + 1; i <= 6; ++i)
                        if (nowboard.Red[i - 1] != 0)
                        {
                            可選.Add(nowboard.Red[i - 1] - 1);
                            break;
                        }
                    for (int i = num - 1; i > 0; --i)
                        if (nowboard.Red[i - 1] != 0)
                        {
                            可選.Add(nowboard.Red[i - 1] - 1);
                            break;
                        }
                }
                if (InCanChoose(AnsSet[0] - 1) == false) return false;
                num = AnsSet[0] - 1;
                int x = num % 5;
                int y = num / 5;
                if (x != 4)
                {
                    可選.Add(num + 1);
                    if (y != 4) 可選.Add(num + 6);
                }
                if (y != 4) 可選.Add(num + 5);
                if (InCanChoose(AnsSet[1] - 1) == false) return false;
            }
            return true;
        }
        void GameDiceTurnNet()
        {
            try
            {
                string line;
                int num;
                if (connectsetting.RoomOrder)
                {
                    num = GetDiceNet();
                    if (connectsetting.IsMyBlue == game.NowBlueTurn)
                    {
                        AnsSet = MyNet.g.Get("get " + (connectsetting.IsMyBlue ? "B" : "R") + " " + num.ToString() + " " + nowboard.ToString());
                        if (IsVailid(AnsSet, num))
                        {
                            node.WriteLineEx("talk " + GetString(AnsSet));
                        }
                        else
                        {
                            node.WriteLineEx("talk error");
                            EndGameNet(!connectsetting.IsMyBlue, "Error of 己方的 AI : 對方 win!");
                            return;
                        }
                    }
                    else
                    {
                        node.WriteLineEx("talk " + num.ToString());
                        line = node.ReadLineEx();
                        if (line == "error")
                        {
                            EndGameNet(connectsetting.IsMyBlue, "Error of 對方的 AI : you win!");
                            return;
                        }
                        else
                        {
                            AnsSet = Get(line);
                        }

                    }
                }
                else
                {
                    if (connectsetting.IsMyBlue != game.NowBlueTurn)
                    {
                        line = node.ReadLineEx();
                        if (line == "error")
                        {
                            EndGameNet(connectsetting.IsMyBlue, "Error of 對方的 AI : you win!");
                            return;
                        }
                        else
                        {
                            AnsSet = Get(line);
                        }
                    }
                    else
                    {
                        num = Convert.ToInt32(node.ReadLineEx());
                        DiceB.Text = num.ToString();
                        AnsSet = MyNet.g.Get("get " + (connectsetting.IsMyBlue ? "B" : "R") + " " + num.ToString() + " " + nowboard.ToString());
                        if (IsVailid(AnsSet, num))
                        {
                            node.WriteLineEx("talk " + GetString(AnsSet));
                        }
                        else
                        {
                            node.WriteLineEx("talk error");
                            EndGameNet(!connectsetting.IsMyBlue, "Error of 己方的 AI : 對方 win!");
                            return;
                        }
                    }
                }
                ChMove = AnsSet[1] - 1;
                nowboard.To(AnsSet[0] - 1, AnsSet[1] - 1);
                ShowBoard();
                DetIsEndNet();
            }
            catch
            {
                Message("斷線了");
                EndGameBottom_Click();
                DisConnectButtom_Click();
                return;
            }
        }
        int NowDice = 0;
        void GameDiceTurn()
        {
            switch (gamesetting.RandSeedMode)
            {
                case RandSeed.Input:
                    {
                        int num = gamesetting.RandNumberString[game.RandNumPos] - '0';
                        NowDice = num;
                        ++game.RandNumPos;
                        game.RandNumPos %= gamesetting.RandNumberString.Length;
                        DiceB.Text = num.ToString();
                        GameDetGo(num);
                    }
                    break;
                case RandSeed.RunTime:
                    DiceB.MouseClick += DiceB_Click;
                    NumBox.Enabled = true;
                    break;
                default:
                    {
                        int num = rand.Next(1, 7);
                        NowDice = num;
                        DiceB.Text = num.ToString();
                        GameDetGo(num);
                    }
                    break;
            }
        }
        int[] AnsSet = new int[2];

        void GameDetGo(int num)
        {
            if ((gamesetting.blue.playermode == PlayerMode.People) || (gamesetting.red.playermode == PlayerMode.People)) EndGameBottom.Enabled = true;
            可選.Clear();
            if (game.NowBlueTurn)
            {
                if (nowboard.Blue[num - 1] != 0)
                {
                    可選.Add(nowboard.Blue[num - 1] - 1);
                }
                else
                {
                    for (int i = num + 1; i <= 6; ++i)
                        if (nowboard.Blue[i - 1] != 0)
                        {
                            可選.Add(nowboard.Blue[i - 1] - 1);
                            break;
                        }
                    for (int i = num - 1; i > 0; --i)
                        if (nowboard.Blue[i - 1] != 0)
                        {
                            可選.Add(nowboard.Blue[i - 1] - 1);
                            break;
                        }
                }
                if (gamesetting.blue.playermode != PlayerMode.People)
                {
                    try
                    {
                        AnsSet = gamesetting.blue.g.Get("get B " + num.ToString() + " " + nowboard.ToString());
                        if (AnsSet == null) throw new Exception();
                    }
                    catch
                    {
                        EndGame(false, "Error of blue AI : red win!");
                        return;
                    }
                    if (InCanChoose(AnsSet[0] - 1) == false)
                    {
                        EndGame(false, ("Error of blue AI : You Can't Choose" + AnsSet[0].ToString() + ": red win!"));
                        return;
                    }
                    GameChooseMove(AnsSet[0] - 1);
                    return;
                }
            }
            else
            {
                if (nowboard.Red[num - 1] != 0)
                {
                    可選.Add(nowboard.Red[num - 1] - 1);
                }
                else
                {
                    for (int i = num + 1; i <= 6; ++i)
                        if (nowboard.Red[i - 1] != 0)
                        {
                            可選.Add(nowboard.Red[i - 1] - 1);
                            break;
                        }
                    for (int i = num - 1; i > 0; --i)
                        if (nowboard.Red[i - 1] != 0)
                        {
                            可選.Add(nowboard.Red[i - 1] - 1);
                            break;
                        }
                }
                if (gamesetting.red.playermode != PlayerMode.People)
                {
                    try
                    {
                        AnsSet = gamesetting.red.g.Get("get R " + num.ToString() + " " + nowboard.ToString());
                        if (AnsSet == null) throw new Exception();
                    }
                    catch
                    {
                        EndGame(true, "Error of red AI : blue win!");
                        return;
                    }
                    if (InCanChoose(AnsSet[0] - 1) == false)
                    {
                        EndGame(true, ("Error of red AI : You Can't Choose" + AnsSet[0].ToString() + ": blue win!"));
                        return;
                    }
                    GameChooseMove(AnsSet[0] - 1);
                    return;
                }
            }

            foreach (int i in 可選)
            {
                Pb[i].BackColor = Color.Green;
            }
            if (可選.Count == 1)
            {
                GameChooseMove(可選[0]);
            }
            else
            {
                foreach (int i in 可選)
                {
                    Pb[i].MouseClick += MulCanMoveClick;
                }
            }

        }
        bool InCanChoose(int i)
        {
            foreach (int j in 可選)
                if (i == j) return true;
            return false;
        }

        void MulCanMoveClick(object sender, MouseEventArgs e)
        {
            int j = 0;
            ShowBoard();
            foreach (int i in 可選)
            {
                if (Pb[i] == sender)
                {
                    Pb[i].BackColor = Color.Green;
                    j = i;
                }
                Pb[i].MouseClick -= MulCanMoveClick;
            }

            GameChooseMove(j);
        }

        void GameChooseMove(int num)
        {
            AnsSet[0] = num + 1;
            ChMove = num;
            可選.Clear();
            int x = num % 5;
            int y = num / 5;
            if (game.NowBlueTurn)
            {
                if (x != 0)
                {
                    可選.Add(num - 1);
                    if (y != 0) 可選.Add(num - 6);
                }
                if (y != 0) 可選.Add(num - 5);


                if (gamesetting.blue.playermode != PlayerMode.People)
                {

                    MoveCh();

                    return;
                }
            }
            else
            {
                if (x != 4)
                {
                    可選.Add(num + 1);
                    if (y != 4) 可選.Add(num + 6);
                }
                if (y != 4) 可選.Add(num + 5);

                if (gamesetting.red.playermode != PlayerMode.People)
                {

                    MoveCh();

                    return;
                }
            }

            foreach (int i in 可選)
            {
                Pb[i].BackColor = Color.LightSeaGreen;
                Pb[i].MouseClick += MoveMClick;
            }

        }
        void MoveCh()
        {
            if (InCanChoose(AnsSet[1] - 1) == false)
            {
                EndGame(!game.NowBlueTurn, ("Error of " + (game.NowBlueTurn ? "blue" : "red") + " AI : You Can't Choose" + AnsSet[1].ToString() + ": " + (game.NowBlueTurn ? "blue" : "red") + " win!"));
                return;
            }
            ChMove = AnsSet[1] - 1;
            if ((gamesetting.blue.playermode == PlayerMode.People) || (gamesetting.red.playermode == PlayerMode.People))
                RecordGameList.Add(new GameRecord(nowboard, game.HAVETimeB, game.HAVETimeR));
            nowboard.To(AnsSet[0] - 1, AnsSet[1] - 1);
            ShowBoard();
            DetIsEnd();
        }
        int ChMove;

        void MoveMClick(object sender, MouseEventArgs e)
        {
            if ((gamesetting.blue.playermode == PlayerMode.People) || (gamesetting.red.playermode == PlayerMode.People))
                RecordGameList.Add(new GameRecord(nowboard, game.HAVETimeB, game.HAVETimeR));
            foreach (int i in 可選)
            {
                if (sender == Pb[i])
                {
                    nowboard.To(ChMove, i);
                    ChMove = i;
                    AnsSet[1] = i + 1;
                }
                Pb[i].MouseClick -= MoveMClick;
            }
            ShowBoard();
            DetIsEnd();
        }

        void DetIsEndNet()
        {
            sw.Stop();
            UITimer.Stop();
            if (gamesetting.Usetotaltimelimit)
            {
                if (game.NowBlueTurn) game.HAVETimeB -= sw.Elapsed.TotalMilliseconds / 1000;
                else game.HAVETimeR -= sw.Elapsed.TotalMilliseconds / 1000;
            }

            record.AddRecocd_EX(string.Format("{5:D2} {0} {1} {2:D2} {3:D2} {4:#,000.00}", (game.NowBlueTurn ? "B" : "R"), NowDice, AnsSet[0], AnsSet[1], (game.NowBlueTurn ? game.HAVETimeB : game.HAVETimeR), game.Turn));
            game.DiceCount[NowDice] += 1;
            if (gamesetting.Useonesteptimelimit && ((sw.Elapsed.TotalMilliseconds / 1000) > gamesetting.onesteptimelimit))
            {
                EndGameNet(!game.NowBlueTurn, game.NowBlueTurn ? "R win: B one stept time out!" : "B win: R one stept time out!"); return;
            }
            if (gamesetting.Usetotaltimelimit && game.TimeB <= 0)
            {
                EndGameNet(false, "R win: B time out!"); return;
            }
            if (gamesetting.Usetotaltimelimit && game.TimeR <= 0)
            {
                EndGameNet(true, "B win: R time out!"); return;
            }
            bool One = false;
            foreach (int i in nowboard.Blue) if (i != 0)
                {
                    One = true;
                    break;
                }
            if (One == false)
            {
                EndGameNet(false, "R win: No B !"); return;
            }
            One = false;
            foreach (int i in nowboard.Red) if (i != 0)
                {
                    One = true;
                    break;
                }
            if (One == false) { EndGameNet(true, "B win: No R !"); return; }

            if (ChMove == 0 || ChMove == 24) { EndGameNet(ChMove != 24, (ChMove == 24 ? "R win: End!" : "B win: End!")); return; }

            //////////////////////////////
            game.NowBlueTurn = !game.NowBlueTurn;
            game.OneStepTime = gamesetting.onesteptimelimit;
            ++game.Turn;
            ShowBoard();
            UpdataUI();
            game.TimeB = game.HAVETimeB;
            game.TimeR = game.HAVETimeR;
            sw.Reset();
            sw.Start();

            UITimer.Start();
            GameDiceTurnNet();
        }

        void EndGameNet(bool IsBoolWin)
        {
            EndGameNeedDo(IsBoolWin);
            if (gamesetting.maxround > game.Round)
            {
                nowboard = new Board(startboard);
                game.NextRound(gamesetting);
                GameRecordList.Items.Clear();
                ShowBoard();
                StartThreadNet();
            }
            else
            {
                EndGameBottom_Click(null, null);
            }
        }
        void EndGameNet(bool IsBlueWin, string why)
        {
            Message(why);
            record.AddRecocd_EX("end " + why);
            EndGameNet(IsBlueWin);
        }

        void EndGame(bool IsBlueWin, string why)
        {
            Message(why);
            record.Add("end " + why);
            record.ListFlush();
            EndGame(IsBlueWin);
        }
        void DetIsEnd()
        {
            sw.Stop();
            UITimer.Stop();
            if (gamesetting.Usetotaltimelimit)
            {
                if (game.NowBlueTurn) game.HAVETimeB -= sw.Elapsed.TotalMilliseconds / 1000;
                else game.HAVETimeR -= sw.Elapsed.TotalMilliseconds / 1000;
            }
            record.Add(string.Format("{5:D2} {0} {1} {2:D2} {3:D2} {4:#,000.00}", (game.NowBlueTurn ? "B" : "R"), NowDice, AnsSet[0], AnsSet[1], (game.NowBlueTurn ? game.HAVETimeB : game.HAVETimeR), game.Turn));
            if (gamesetting.Stop)
            {
                Mark(AnsSet);
                MessageBox.Show("Stop!");
            }
            game.DiceCount[NowDice] += 1;

            if (gamesetting.Useonesteptimelimit && ((sw.Elapsed.TotalMilliseconds / 1000) > gamesetting.onesteptimelimit))
            {
                EndGame(!game.NowBlueTurn, game.NowBlueTurn ? "R win: B one stept time out!" : "B win: R one stept time out!"); return;
            }
            if (gamesetting.Usetotaltimelimit && game.TimeB <= 0)
            {
                EndGame(false, "R win: B time out!"); return;
            }
            if (gamesetting.Usetotaltimelimit && game.TimeR <= 0)
            {
                EndGame(true, "B win: R time out!"); return;
            }
            bool One = false;
            foreach (int i in nowboard.Blue) if (i != 0)
                {
                    One = true;
                    break;
                }
            if (One == false)
            {
                EndGame(false, "R win: No B !"); return;
            }
            One = false;
            foreach (int i in nowboard.Red) if (i != 0)
                {
                    One = true;
                    break;
                }
            if (One == false) { EndGame(true, "B win: No R !"); return; }

            if (ChMove == 0 || ChMove == 24) { EndGame(ChMove != 24, (ChMove == 24 ? "R win: End!" : "B win: End!")); return; }

            //////////////////////////////
            game.NowBlueTurn = !game.NowBlueTurn;
            game.OneStepTime = gamesetting.onesteptimelimit;
            ++game.Turn;
            ShowBoard();
            UpdataUI();
            game.TimeB = game.HAVETimeB;
            game.TimeR = game.HAVETimeR;
            sw.Reset();
            sw.Start();
            UITimer.Start();
            GameDiceTurn();
        }

        private void Mark(int[] AnsSet)
        {
            Pb[AnsSet[0]-1].BackColor = Color.Green;
            Pb[AnsSet[1]-1].BackColor = Color.LightSeaGreen;

        }
        void EndGame(bool IsBoolWin)
        {
            EndGameNeedDo(IsBoolWin);

            if (gamesetting.maxround > game.Round)
            {
                nowboard = new Board(startboard);
                game.NextRound(gamesetting);
                GameRecordList.Items.Clear();
                ShowBoard();
                StartThread();
            }
            else
            {
                EndGameBottom_Click(null, null);
            }
            return;
        }

        private void EndGameNeedDo(bool IsBoolWin)
        {
            if (IsBoolWin)
            { game.Bwin += 1; }
            else { game.Rwin += 1; }
            UITimer.Stop();
            IniUi();
            game.AllTurn += game.Turn;
            if (game.Turn > game.MaxTurn) game.MaxTurn = game.Turn;
            if (game.Turn < game.MinTurn) game.MinTurn = game.Turn;
            game.RoundInf[game.Turn] += 1;

        }
        private void EndGameBottom_Click(object sender = null, EventArgs e = null)
        {
            //MessageBox.Show("");
            BackBottom.Enabled = false;
            if (node.IsConnect)
            {
                Thread th = new Thread(Work);
                th.Start();
                StartGameBottom.Enabled = connectsetting.RoomOrder;
            }
            else
            {
                LanguageChange.Enabled = true;
                StartGameBottom.Enabled = true;
            }
            sw.Stop();
            UseUIMode.Enabled = true;
            GameRecordList.Enabled = true;
            PreOk.Enabled = false;
            EndGameBottom.Enabled = false;
            GameSettingButtom.Enabled = true;
            DiceB.MouseClick -= DiceB_Click;
            NumBox.Enabled = false;
            //    DiceP.Visible = false;
            ConnectButtom.Enabled = !node.IsConnect;
            DisConnectButtom.Enabled = node.IsConnect;


            for (int i = 0; i < 25; ++i)
            {
                Pb[i].MouseClick -= IniMClick;
                Pb[i].MouseClick -= MoveMClick;
                Pb[i].MouseClick -= MulCanMoveClick;
            }
            UITimer.Stop();
            try { gamesetting.blue.g.Freeer(); }
            catch { }
            try { gamesetting.red.g.Freeer(); }
            catch { }
            //  ExeKiller(gamesetting.blue.path);
            // ExeKiller(gamesetting.red.path);

            try
            {
                record.AddRecocd("endgame");
                record.AddRecocd(" ");

                record.AddRecocd("result");
                record.AddRecocd("Total round |" + "Blue |" + " Red |");
                record.AddRecocd(string.Format("   {0:D5}    |" + "{1:D5}|" + "{2:D5}|", game.Round, game.Bwin, game.Rwin));
                record.AddRecocd(" ");
                record.AddRecocd("平均回合數: " + ((double)game.AllTurn / game.Round).ToString("f1"));
                record.AddRecocd(string.Format("回合區間:[{0:D2},{1:D2}]", game.MinTurn, game.MaxTurn));

                Message("平均回合數: " + ((double)game.AllTurn / game.Round).ToString("f1"));
                Message(string.Format("回合區間:[{0:D2},{1:D2}]", game.MinTurn, game.MaxTurn));
                record.AddRecocd("回合   共幾場");
                for (int i = game.MinTurn; i <= game.MaxTurn; ++i)
                    record.AddRecocd(string.Format(" {0:D2}    {1:D4}", i, game.RoundInf[i]));
                record.AddRecocd(" ");
                record.AddRecocd("骰子點數   共幾次");
                for (int i = 1; i < 7; ++i)
                    record.AddRecocd(string.Format("   {0:D2}      {1:D4}", i, game.DiceCount[i]));


                record.AddRecocd(" ");
                record.CloseRecord();
            }
            catch { Message("No record be written!"); }
        }
        private void UITimer_Tick(object sender, EventArgs e)
        {
            if (game.NowBlueTurn)
            {
                if (gamesetting.Usetotaltimelimit && game.TimeB > 0) { --game.TimeB; BTimeS.Text = "T " + ((int)game.TimeB).ToString() + 's'; }
            }
            else
            {
                if (gamesetting.Usetotaltimelimit && game.TimeR > 0) { --game.TimeR; RTimeS.Text = "T " + ((int)game.TimeR).ToString() + 's'; }
            }
            if (gamesetting.Useonesteptimelimit && game.OneStepTime > 0)
            {
                --game.OneStepTime; OneStepTimeS.Text = ((int)game.OneStepTime).ToString();
            }
        }
        private void PreOk_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 6; ++i)
            {
                Pb[nowboard.Blue[i] - 1].MouseClick -= IniMClick;
                Pb[nowboard.Red[i] - 1].MouseClick -= IniMClick;
            }
            record.Add("ini " + nowboard.ToString());
            startboard = new Board(nowboard);
            PreOk.Enabled = false;
            UITimer.Start();
            sw.Reset();
            sw.Start();
            Thread t = new Thread(GameDiceTurn);
            t.Start();
        }
        private void DiceB_Click(object sender = null, EventArgs e = null)
        {
            DiceB.MouseClick -= DiceB_Click;
            NumBox.Enabled = false;
            for (int i = 0; i < 20; ++i)
            {
                DiceB.Text = rand.Next(1, 6).ToString();
                DiceB.Refresh();
                Thread.Sleep(8);
            }
            int num = rand.Next(1, 6);
            NowDice = num;
            DiceB.Text = num.ToString();
            GameDetGo(num);
        }
        private void textBox1_TextChanged(object sender = null, EventArgs e = null)
        {
            if (NumBox.Text != string.Empty && Char.IsNumber(NumBox.Text[0]))
            {
                int num = NumBox.Text[0] - '0';
                if (num > 0 && num < 7)
                {
                    NumBox.Text = "";
                    DiceB.MouseClick -= DiceB_Click;
                    NumBox.Enabled = false;
                    DiceB.Text = num.ToString();
                    NowDice = num;
                    GameDetGo(num);
                }
            }
        }
        #endregion

        #region 其他運作
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try { gamesetting.blue.g.Freeer(); }
            catch { }
            try { gamesetting.red.g.Freeer(); }
            catch { }
            try { gamesetting.test.g.Freeer(); }
            catch { }
            ExeKiller(gamesetting.blue.path);
            ExeKiller(gamesetting.red.path);
            ExeKiller(gamesetting.test.path);
        }
        void ExeKiller(string s)
        {
            if (s == string.Empty) return;
            try
            {
                Process[] p = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(s));
                if (p.Length != 0) foreach (Process pro in p) pro.Kill();
            }
            catch { }
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            record.CloseRecord();
            ReadRecord.CloseRecord();
            try { node.EXIT(); }
            catch { }
            try { this.Dispose(); }
            catch { }
            Environment.Exit(Environment.ExitCode);
        }


        private void DisConnectButtom_Click(object sender = null, EventArgs e = null)
        {
            ConnectButtom.Enabled = true;
            DisConnectButtom.Enabled = false;
            StartGameBottom.Enabled = true;
            GameSettingButtom.Enabled = true;
            UseUIMode.Enabled = true;
            LanguageChange.Enabled = true;
            EXIT();
        }

        private void ConnectButtom_Click(object sender, EventArgs e)
        {
            連線設定 s = new 連線設定(connectsetting);
            if (s.ShowDialog() == DialogResult.OK)
            {
                connectsetting = s.s;
                WriteSettingRecord();
                string line;
                ConnectButtom.Enabled = false;
                DisConnectButtom.Enabled = false;
                StartGameBottom.Enabled = false;
                UseUIMode.Enabled = false;
                LanguageChange.Enabled = false;
                if (node.StartConnect(connectsetting))
                {
                    if (connectsetting.RoomOrder)
                    {
                        PlayerInf pla = (connectsetting.IsMyBlue ? gamesetting.blue : gamesetting.red);
                        line = string.Format("addroom {0} {1}",
                                                             connectsetting.RoomName,
                                                             (pla.playermode == PlayerMode.People ? "Hand" : Path.GetFileNameWithoutExtension(pla.path))
                                                             );
                    }
                    else
                    {
                        line = string.Format("enterroom {0}",
                            connectsetting.RoomName);
                        node.IsCanGame = true;
                    }
                    node.WriteLineEx(line.Replace(' ', SpliKey));
                    line = node.ReadLineEx().Trim();
                    if (line == "OK")
                    {
                        DisConnectButtom.Enabled = true;
                        UseUIMode.Enabled = false;
                        Thread th = new Thread(Work);
                        th.Start();
                    }
                    else
                    {
                        MessageBox.Show(line);
                        Message(line);
                        Message("結束連線!");
                        DisConnectButtom_Click();
                    }
                }
                else
                {
                    MessageBox.Show("連線失敗!");
                    DisConnectButtom_Click();
                    return;
                }

            }
        }

        private void About_Click(object sender, EventArgs e)
        {
            關於 s = new 關於();
            s.ShowDialog();
        }

        #endregion

        #region 測試模式


        Label[] Chlabel;
        PictureBox[] ChPic;
        int ChPbindex = 0;
        int ChLbindex = 0;
        void IniTestUI()
        {
            for (int i = 0; i < 2; ++i)
            {
                if (i == ChPbindex && ChPic[i].BackColor != Color.Green) ChPic[i].BackColor = Color.Green;
                else if (i != ChPbindex && ChPic[i].BackColor != SystemColors.Control) ChPic[i].BackColor = SystemColors.Control;
            }
            for (int i = 0; i < 6; ++i)
            {
                if (i == ChLbindex && Chlabel[i].BackColor != Color.Green) Chlabel[i].BackColor = Color.Green;
                else if (i != ChLbindex && Chlabel[i].BackColor != SystemColors.Control) Chlabel[i].BackColor = SystemColors.Control;
            }
        }


        void TestMWhe(object sender, MouseEventArgs e)
        {
            if ((ChLbindex == 0 && e.Delta > 0) || (ChLbindex == 5 && e.Delta < 0)) return;
            ChLbindex -= (e.Delta / 120);
            ChLbindex %= 6;
            if (ChLbindex < 0) ChLbindex += 6;
            IniTestUI();
        }
        void TestMClick(object sender, MouseEventArgs e)
        {
            int chnum = 0;
            for (int i = 0; i < 25; ++i)
                if (sender == Pb[i])
                {
                    chnum = i; break;
                }
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if ((ChPbindex == 0 && nowboard.Blue[ChLbindex] != 0) || (ChPbindex == 1 && nowboard.Red[ChLbindex] != 0))
                    {
                        if (ChPbindex == 0) nowboard.Blue[ChLbindex] = 0; else nowboard.Red[ChLbindex] = 0;
                        nowboard.Ini();
                    }
                    if (nowboard.Boa[chnum] != 0)
                    {
                        if (nowboard.Boa[chnum] <= 6) nowboard.Blue[nowboard.Boa[chnum] - 1] = 0;
                        else nowboard.Red[nowboard.Boa[chnum] - 6 - 1] = 0;
                        nowboard.Ini();
                    }
                    if (ChPbindex == 0) nowboard.Blue[ChLbindex] = chnum + 1; else nowboard.Red[ChLbindex] = chnum + 1;
                    nowboard.Ini();
                    ShowBoard();
                    break;

                case MouseButtons.Middle:
                    if (nowboard.Boa[chnum] != 0)
                    {
                        if (nowboard.Boa[chnum] <= 6) nowboard.Blue[nowboard.Boa[chnum] - 1] = 0;
                        else nowboard.Red[nowboard.Boa[chnum] - 6 - 1] = 0;
                        nowboard.Ini();
                        ShowBoard();
                    }
                    break;
                case MouseButtons.Right:
                    ChPbindex = (ChPbindex == 0 ? 1 : 0);
                    IniTestUI();
                    break;
            }
            TestBoardText.Text = nowboard.ToString();
        }


        private void LB1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 2; ++i) if (sender == ChPic[i]) { ChPbindex = i; break; }
            for (int i = 0; i < 6; ++i) if (sender == Chlabel[i]) { ChLbindex = i; break; }
            IniTestUI();
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
            EXEname.Text = Path.GetFileNameWithoutExtension(gamesetting.test.path = path);
            WriteSettingRecord();
        }

        private void DefaultBoaButtom_Click(object sender, EventArgs e)
        {
            if (sender == DefaultBoaButtom)
            {
                nowboard = new Board();
            }
            else
            {
                nowboard = new Board(startboard);
            }
            ShowBoard();
            TestBoardText.Text = nowboard.ToString();
        }

        private void RSM_SelectedIndexChanged(object sender, EventArgs e)
        {
            DiceNum.Enabled = RSM.SelectedIndex != 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Newer(gamesetting.test);
            }
            catch
            {
                Message("啟動失敗，請檢察AI是否存在或有異常!");
                StopTestAI_Click();
                return;
            }
            StartTestAI.Enabled = false;
            StopTestAI.Enabled = true;
            AITestGroup.Enabled = true;
            BlueGroup.Enabled = false;
            UseUIMode.Enabled = false;
        }

        private void StopTestAI_Click(object sender = null, EventArgs e = null)
        {

            try { gamesetting.test.g.Freeer(); }
            catch { }
            StartTestAI.Enabled = true;
            AITestGroup.Enabled = false;
            StopTestAI.Enabled = false;
            BlueGroup.Enabled = true;
            UseUIMode.Enabled = true;
            for (int i = 0; i < 25; ++i) Pb[i].Enabled = true;
            //   ExeKiller(gamesetting.blue.path);
        }
        System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
        private void GetGo_Click(object sender, EventArgs e)
        {
            Thread t = new Thread(ToTest);
            t.Start();
        }
        void ToTest()
        {
            LockTestUI();
            startboard = new Board(nowboard);
            int num;

            double alltime = 0;
            for (int i = 0; i < PlayTimes.Value; ++i)
            {
                sw.Reset();
                sw.Start();
                Message("Test:" + (i + 1).ToString());
                ShowBoard();
                num = ((RSM.SelectedIndex == 0) ? (int)(DiceNum.Value = (decimal)rand.Next(1, 7)) : (int)DiceNum.Value);
                StartTimeDown((int)TimeLimit.Value, gamesetting.test);
                try
                {
                    AnsSet = gamesetting.test.g.Get("get " + (TestColar.SelectedIndex == 0 ? "B" : "R") + " " + num.ToString() + " " + nowboard.ToString());
                    if (IsVailid(TestColar.SelectedIndex == 0, AnsSet, num))
                    {
                        Pb[AnsSet[0] - 1].BackColor = Color.Green;
                        Pb[AnsSet[1] - 1].BackColor = Color.LightSeaGreen;
                    }
                    else
                    {
                        Message("非法走步:" + GetString(AnsSet));
                        throw new Exception();
                    }
                }
                catch
                {
                    sw.Stop();
                    StopTimeDown();
                    Message("Error of AI !");
                    nowboard = new Board(startboard);
                    StopTestAI_Click();
                    return;
                }
                StopTimeDown();
                sw.Stop();
                double d = sw.Elapsed.TotalMilliseconds;
                alltime += d;
                TestNowTime.Text = (d / 1000).ToString(((d / 1000) < 10 ? "f4" : "f3"));
                TestMeanTime.Text = ((alltime / (i + 1)) / 1000).ToString(((((alltime / (i + 1)) / 1000)) < 10 ? "f4" : "f3"));
                TestAllTime.Text = (alltime / 1000).ToString(((alltime / 1000) < 10 ? "f4" : "f3"));
            }
            OpenTestUI();
        }
        private void GetIni_Click(object sender, EventArgs e)
        {

            LockTestUI();
            StartTimeDown(1, gamesetting.test);
            GetTestIni();
            TestBoardText.Text = nowboard.ToString();
        }
        void LockTestUI()
        {
            StopTestAI.Enabled = false;
            AITestGroup.Enabled = false;
            TestButtomGroup.Enabled = false;
            for (int i = 0; i < 25; ++i) Pb[i].Enabled = false;
        }
        void OpenTestUI()
        {
            StopTestAI.Enabled = true;
            AITestGroup.Enabled = true;
            TestButtomGroup.Enabled = true;
            for (int i = 0; i < 25; ++i) Pb[i].Enabled = true;
        }
        void GetTestIni()
        {
            try
            {
                nowboard = new Board();
                if (TestColar.SelectedIndex == 0)
                {
                    nowboard.Blue = gamesetting.test.g.Get("ini B");
                    if (!IsValidIni(new int[6] { 25, 24, 20, 23, 19, 15 }, nowboard.Blue))
                    {
                        Message("Error initialization of AI !");
                        throw new Exception();
                    }
                }
                else
                {
                    nowboard.Red = gamesetting.test.g.Get("ini R");
                    if (!IsValidIni(new int[6] { 1, 2, 3, 6, 7, 11 }, nowboard.Red))
                    {
                        Message("Error initialization of AI !");
                        throw new Exception();
                    }
                }
            }
            catch
            {
                StopTimeDown();
                Message("Error of AI !");
                nowboard = new Board(startboard);
                ShowBoard();
                StopTestAI_Click();
                return;
            }
            StopTimeDown();
            startboard = new Board(nowboard);
            nowboard.Ini();
            ShowBoard();

            OpenTestUI();
        }
        PlayerInf BeTimePlayer;
        System.Timers.Timer KillAITimer = new System.Timers.Timer();
        private void KillAITimer_Tick(object sender, EventArgs e)
        {
            Message("此AI毫無反應,故砍了");
            ExeKiller(BeTimePlayer.path);
            StopTestAI_Click();
        }
        void StartTimeDown(int num, PlayerInf BTP)
        {
            KillAITimer.AutoReset = false;
            KillAITimer.Interval = num * 1000;
            BeTimePlayer = BTP;
            KillAITimer.Start();
        }
        void StopTimeDown()
        {
            KillAITimer.Stop();
        }

        void ReadRecordWhe(object sender, MouseEventArgs e)
        {
            if ((GameRecordList.SelectedIndex <= 0 && e.Delta > 0) || (GameRecordList.SelectedIndex == (GameRecordList.Items.Count - 1) && e.Delta < 0)) return;
            GameRecordList.SelectedIndex -= (e.Delta / 120);


        }


        #endregion
        void DeletEventAndLockAll()
        {
            foreach (PictureBox p in Pb)
            {
                p.MouseClick -= TestMClick;
            }
            this.MouseWheel -= TestMWhe;
            this.MouseWheel -= ReadRecordWhe;
            this.DragEnter -= DragEnter_;
            GameRecordList.MouseWheel -= ReadRecordWhe;
            Testgroup.Visible = false;
            TestInfgroup.Visible = false;
            TestButtomGroup.Visible = false;
            DiceGroup.Visible = false;

            Gamegroup.Visible = false;
            StartGameBottom.Enabled = false;
            GameSettingButtom.Enabled = false;
            GameInfgroup.Visible = false;
            ConnectButtom.Enabled = false;
            GameButtomGroup.Visible = false;

            ReadButtomGroup.Visible = false;
            ReadButtomGroup.Enabled = false;

            ReadInfoGroup.Visible = false;

            RecordInf.Visible = false;

            if (record != null) record.CloseRecord();
            if (ReadRecord != null) ReadRecord.CloseRecord();
        }
        private void UseUIMode_SelectedIndexChanged(object sender, EventArgs e)
        {

            switch (UseUIMode.SelectedIndex)
            {
                case 0:
                    DeletEventAndLockAll();

                    Gamegroup.Visible = true;
                    StartGameBottom.Enabled = true;
                    GameSettingButtom.Enabled = true;
                    GameInfgroup.Visible = true;
                    ConnectButtom.Enabled = true;
                    GameButtomGroup.Visible = true;
                    DiceGroup.Visible = true;

                    break;
                case 1:
                    DeletEventAndLockAll();

                    Testgroup.Visible = true;
                    TestInfgroup.Visible = true;
                    TestButtomGroup.Visible = true;
                    foreach (PictureBox p in Pb)
                    {
                        p.MouseClick += TestMClick;
                    }
                    this.MouseWheel += TestMWhe;
                    TestBoardText.Text = nowboard.ToString();
                    break;
                case 2:
                    DeletEventAndLockAll();
                    GameRecordList.MouseWheel += ReadRecordWhe;
                    this.MouseWheel += ReadRecordWhe;
                    ReadButtomGroup.Visible = true;
                    ReadButtomGroup.Enabled = false;
                    Gamegroup.Visible = true;
                    ReadInfoGroup.Visible = true;
                    RecordInf.Visible = true;
                    this.DragEnter += DragEnter_;
                    break;
            }

            toolStrip1.Focus();

        }
        void IniPicture(PictureBox p, Color c)
        {
            if (p.BackColor != c) p.BackColor = c;
        }
        private void GameRecordList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                IniPicture(SHPBB, SystemColors.Control);
                IniPicture(SHPBR, SystemColors.Control);

                int seleind = GameRecordList.SelectedIndex;
                if (seleind == -1) return;
                string line = GameRecordList.Items[0].ToString().Trim();
                string[] sp;
                if ((line.Split(' '))[0] == "ini")
                {
                    int[] nu = Get(line.Replace("ini ", ""));
                    if (nu.Length != 12) return;
                    nowboard.Ini(nu);
                    ShowBoard();
                }
                else return;
                sp = GameRecordList.Items[seleind].ToString().Trim().Split(' ');
                bool EndFlag = false;
                if (sp[0] == "end") { --seleind; if (seleind > 2)EndFlag = true; }
                int BT = 0, RT = 0;

                for (int i = 1; i < seleind; ++i)
                {
                    line = GameRecordList.Items[i].ToString().Trim();
                    sp = line.Split(' ');
                    if (sp[1] == "B") BT = (int)Convert.ToDouble(sp[5]); else RT = (int)Convert.ToDouble(sp[5]);
                    nowboard.To(Convert.ToInt32(sp[3]) - 1, Convert.ToInt32(sp[4]) - 1);
                }

                if (seleind > 0)
                {
                    line = GameRecordList.Items[seleind].ToString().Trim();
                    sp = line.Split(' ');
                    SHDice.Text = sp[2];
                    if (sp[1] == "B") { BT = (int)Convert.ToDouble(sp[5]); } else { RT = (int)Convert.ToDouble(sp[5]); }
                    IniPicture((sp[1] == "B") ? SHPBB : SHPBR, Color.Green);
                    SHBT.Text = BT.ToString();
                    SHRT.Text = RT.ToString();
                    if (EndFlag)
                    {
                        nowboard.To(Convert.ToInt32(sp[3]) - 1, Convert.ToInt32(sp[4]) - 1);
                        ShowBoard();
                    }
                    else
                    {
                        ShowBoard();
                        IniPicture(Pb[Convert.ToInt32(sp[3]) - 1], Color.Green);
                        IniPicture(Pb[Convert.ToInt32(sp[4]) - 1], Color.LightSeaGreen);
                    }
                }
            }
            catch //(Exception ee)
            { /*Message(ee.ToString());*/ }
        }


        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string path = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
            if (Path.GetExtension(path) != ".recordfile") { MessageBox.Show("= =?"); return; }
            try
            {
                ReadRecord.OpenRecord(path);
            }
            catch
            {
                Message("Error!"); MessageBox.Show("紀錄無法讀取!");
                return;
            }
            string line = Path.GetFileNameWithoutExtension(path);
            string[] sp = line.Split('=');
            ReadName.Text = sp[0];
            ReadTime.Text = sp[1];
            ReadButtomGroup.Enabled = true;
            UprecordButtom.Enabled = true;
        }

        private void UprecordButtom_Click(object sender, EventArgs e)
        {
            if (ReadRecord.GetUp())
            {
                ReadTurn.Text = (ReadRecord.Pos + 1).ToString();
                //DownrecordButtom.Enabled = true;
            }
            else
            {
                //UprecordButtom.Enabled = false;
            }
        }

        private void DownrecordButtom_Click(object sender, EventArgs e)
        {
            if (ReadRecord.GetDown())
            {
                ReadTurn.Text = (ReadRecord.Pos + 1).ToString();
                //UprecordButtom.Enabled = true;
            }
            else
            {
                //DownrecordButtom.Enabled = false;
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            ReadRecord.GetRecord((int)ReadRecNum.Value - 1);
            ReadTurn.Text = (ReadRecord.Pos + 1).ToString();
        }

        private void 語言_Click(object sender, EventArgs e)
        {
            using (StreamWriter sr = new StreamWriter("Language"))
            {
                if (Langu_def == sender)
                {
                    sr.WriteLine("");
                }
                else
                {
                    sr.WriteLine("en-us");
                }
            }
            //   throw new ChangeLanguage();
            this.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < 55; ++i) GameRecordList.Items.Add("11111");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            GameRecordList.Enabled = false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RenewePb();
        }

        private void BackBottom_Click(object sender, EventArgs e)
        {
            if (RecordGameList.Count >= 3)
            {
                GameRecord g = RecordGameList[RecordGameList.Count - 2];
                RecordGameList.RemoveAt(RecordGameList.Count - 1);
                RecordGameList.RemoveAt(RecordGameList.Count - 1);
                game.HAVETimeB = g.bluet;
                game.HAVETimeR = g.redt;
                nowboard = new Board(g.b);
                record.RemoveBack2();

                foreach (PictureBox p in Pb)
                {
                    p.MouseClick -= MulCanMoveClick;
                    p.MouseClick -= MoveMClick;
                }
                DiceB.MouseClick -= DiceB_Click;
                NumBox.Enabled = false;
                game.Turn -= 2;
                Message(game.Turn.ToString());
                Message(game.GetThisRoundBlueFirst(gamesetting).ToString());
                game.NowBlueTurn = ((game.Turn % 2) == 1) == game.GetThisRoundBlueFirst(gamesetting);
                Message(game.NowBlueTurn.ToString());
                UpdataUI();
                ShowBoard();
                GameDiceTurn();

            }
        }





    }

    public enum RandSeed { Default, Input, RunTime, GiveSeed }
    public class Setting
    {
        public Setting()
        {
            blue = new PlayerInf(); red = new PlayerInf(); test = new PlayerInf();
            test.playermode = PlayerMode.AI;
        }
        public Setting(Setting s)
        {
            blue = new PlayerInf(s.blue);
            red = new PlayerInf(s.red);
            test = new PlayerInf(s.test);
            totaltimelimit = s.totaltimelimit;
            onesteptimelimit = s.onesteptimelimit;
            Usetotaltimelimit = s.Usetotaltimelimit;
            Useonesteptimelimit = s.Useonesteptimelimit;

            maxround = s.maxround;
            change = s.change;
            BlueFirst = s.BlueFirst;
            DeBugg = s.DeBugg;
            RandSeedMode = s.RandSeedMode;
            RandNumberString = s.RandNumberString;

            Stop = s.Stop;
        }
        /*public void ReadFile(StreamReader s)
        {
            blue.ReadFile(s);
            red.ReadFile(s);
            test.ReadFile(s);
            timelimit = Convert.ToInt32(s.ReadLine());
            maxround = Convert.ToInt32(s.ReadLine());
            change = Convert.ToBoolean(s.ReadLine());
            BlueFirst = Convert.ToBoolean(s.ReadLine());
            DeBugg = Convert.ToBoolean(s.ReadLine());
            RandSeedMode = (RandSeed)Enum.Parse(typeof(RandSeed), s.ReadLine());
            RandNumberString = s.ReadLine();
        }*/
        public void WriteFile(StreamWriter w)
        {
            blue.WriteFile("blue", w);
            red.WriteFile("red", w);
            test.WriteFile("test", w);
            w.WriteLine("totaltimelimit = " + totaltimelimit);
            w.WriteLine("Usetotaltimelimit = " + Usetotaltimelimit);
            w.WriteLine("onesteptimelimit = " + onesteptimelimit);
            w.WriteLine("Useonesteptimelimit = " + Useonesteptimelimit);
            w.WriteLine("maxround = " + maxround);
            w.WriteLine("change = " + change);
            w.WriteLine("BlueFirst = " + BlueFirst);
            w.WriteLine("DeBugg = " + DeBugg);
            w.WriteLine("RandSeedMode = " + Enum.GetName(typeof(RandSeed), RandSeedMode));
            w.WriteLine("RandNumberString = " + RandNumberString);
            w.WriteLine("Stop = " + Stop);
        }
        public PlayerInf blue, red, test;
        public int totaltimelimit = 240;
        public int onesteptimelimit = 30;
        public bool Usetotaltimelimit = false;
        public bool Useonesteptimelimit = true;
        public int maxround = 1;
        public bool change = false;
        public bool BlueFirst = true;
        public bool DeBugg = false;
        public RandSeed RandSeedMode = RandSeed.Default;
        public string RandNumberString = "123456";
        public bool Stop = false;
    }
    public class ConnSetting
    {
        public ConnSetting() { }
        public ConnSetting(ConnSetting s)
        {
            RoomName = s.RoomName;
            IP = s.IP;
            RoomOrder = s.RoomOrder;
            IsMyBlue = s.IsMyBlue;
        }
        /*public void ReadFile(StreamReader s)
        {
            RoomName = s.ReadLine();
            IP = s.ReadLine();
            RoomOrder = Convert.ToBoolean(s.ReadLine());
            IsMyBlue = Convert.ToBoolean(s.ReadLine());

        }*/
        public void WriteFile(StreamWriter w)
        {
            w.WriteLine("RoomName = " + RoomName);
            w.WriteLine("IP = " + IP);
            w.WriteLine("RoomOrder = " + RoomOrder);
            w.WriteLine("IsMyBlue = " + IsMyBlue);

        }
        public string RoomName = "RoomName";
        public string IP = "140.122.184.66";
        public bool RoomOrder = true;
        public bool IsMyBlue = true;

    }
    public enum PlayerMode { People, AI, Connect }
    public class PlayerInf
    {
        public PlayerInf() { }
        public PlayerInf(PlayerInf p)
        {
            playermode = p.playermode;
            path = p.path;
            AutoIni = p.AutoIni;
        }
        /*  public void ReadFile(StreamReader s)
          {
              playermode = (PlayerMode)Enum.Parse(typeof(PlayerMode), s.ReadLine());
              path = s.ReadLine();
          }*/
        public void WriteFile(string s, StreamWriter w)
        {
            w.WriteLine(s + " = " + AutoIni + " > " + Enum.GetName(typeof(PlayerMode), playermode) + " > " + path);
        }
        public PlayerMode playermode = PlayerMode.People;
        public string path = "";
        public Gamer g;
        public bool AutoIni = true;
        internal void ReadFile(string s)
        {
            string[] spli = s.Trim().Split('>');
            AutoIni = Convert.ToBoolean(spli[0].Trim());
            playermode = (PlayerMode)Enum.Parse(typeof(PlayerMode), spli[1].Trim());
            path = spli[2].Trim();
        }
    }
    public class GameingInf
    {
        public string GName = "";
        public int Turn = 1;
        public int Round = 1;
        public int AllTurn = 0;
        public int MinTurn = 150;
        public int MaxTurn = 0;
        public double TimeB = 240;
        public double TimeR = 240;
        public double OneStepTime = 30;
        public double HAVETimeB = 240;
        public double HAVETimeR = 240;
        public int Bwin = 0;
        public int Rwin = 0;
        public bool NowBlueTurn = true;
        public int RandNumPos = 0;
        public int[] RoundInf = new int[150];
        public int[] DiceCount = new int[7];
        public void AllIni(Setting s)
        {
            DiceCount = new int[7];
            RoundInf = new int[150];
            MinTurn = 100;
            MaxTurn = 0;
            Bwin = 0;
            Rwin = 0;
            RandNumPos = 0;
            HAVETimeR = HAVETimeB = TimeB = TimeR = s.totaltimelimit;
            OneStepTime = s.onesteptimelimit;
            AllTurn = 0;
            Turn = 1;
            Round = 1;
            NowBlueTurn = s.BlueFirst;
            string line = "";
            line += (s.blue.playermode != PlayerMode.People) ? Path.GetFileNameWithoutExtension(s.blue.path) : "Hand";
            line += " VS ";
            line += (s.red.playermode != PlayerMode.People) ? Path.GetFileNameWithoutExtension(s.red.path) : "Hand";
            GName = line;
        }
        public bool GetThisRoundBlueFirst(Setting s)
        {
            return s.BlueFirst == (Round % 2 == 1);
        }


        public void NextRound(Setting s)
        {
            // RandNumPos = 0;
            HAVETimeR = HAVETimeB = TimeB = TimeR = s.totaltimelimit;
            OneStepTime = s.onesteptimelimit;
            Round += 1;
            Turn = 1;
            if (s.change) NowBlueTurn = s.BlueFirst == (Round % 2 == 1);
            else NowBlueTurn = s.BlueFirst;
        }

    }

    public class Connect
    {
        public Connect() { }

        public bool StartConnect(ConnSetting c)
        {
            if (!IsConnect)
            {
                IPEndPoint ipep = new IPEndPoint(IPAddress.Parse(c.IP), port);
                Cli = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                Cli.NoDelay = true;
                const int MaxTry = 5;
                for (int i = 0; i < MaxTry; ++i)
                {
                    try
                    {
                        Form1.Message(string.Format("連線至:{2}...{0}/{1}", i + 1, MaxTry, c.IP));
                        Cli.Connect(ipep);
                        Form1.Message("連線成功!");
                        IsConnect = true;
                        str = new NetworkStream(Cli);
                        rea = new StreamReader(str);
                        wri = new StreamWriter(str);
                        WriteLine(NodeKey);
                        break;
                    }
                    catch
                    {
                        if (i == 4)
                        {
                            Form1.Message("無法連線,放棄了!");
                            IsConnect = false;
                            return false;
                        }
                        Thread.Sleep(1500);
                    }
                }
            }
            return IsConnect;
        }
        const string NodeKey = "Key";
        public Socket Cli;
        NetworkStream str;
        StreamReader rea;
        StreamWriter wri;
        const int port = 10500;
        public bool IsConnect = false;
        public bool IsCanGame = false;
        public string ReadLine()
        {
            try
            {
                return rea.ReadLine().Trim();
            }
            catch (Exception e) { IsConnect = false; throw e; }
        }
        public string ReadLineEx()
        {
            string line = ReadLine().Trim();
            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": 收到 >>" + line + "<<");
            return line;
        }
        public void WriteLine(string s)
        {
            try
            {
                wri.WriteLine(s);
                wri.Flush();
            }
            catch (Exception e) { IsConnect = false; throw e; }
        }
        public void WriteLineEx(string s)
        {
            WriteLine(s.Trim());
            Console.WriteLine(DateTime.Now.ToShortTimeString() + ": 送出 >>" + s.Trim() + "<<");
        }
        public void EXIT()
        {
            IsConnect = false;
            IsCanGame = false;
            //Cli.Shutdown(SocketShutdown.Both);
            Cli.Dispose();
            str.Dispose();
            rea.Dispose();
            wri.Dispose();


        }

    }
    public class GameRecord
    {
        public GameRecord(GameRecord g)
        {
            b = new Board(g.b);
            bluet = g.bluet;
            redt = g.redt;
        }
        public GameRecord(Board bb, double bt, double rt)
        {
            b = new Board(bb);
            bluet = bt;
            redt = rt;
        }
        public Board b;
        public double bluet;
        public double redt;
    }

    public class Board
    {
        public Board()
        {
            Ini();
        }
        public Board(Board b)
        {
            for (int i = 0; i < 6; ++i)
            {
                Blue[i] = b.Blue[i];
                Red[i] = b.Red[i];
            }
            for (int i = 0; i < 25; ++i) Boa[i] = b.Boa[i];
        }
        public void Ini()
        {
            for (int i = 0; i < 25; ++i) Boa[i] = 0;
            Re();
        }
        public void Re()
        {
            for (int i = 0; i < 6; ++i)
            {
                if (Blue[i] != 0) Boa[Blue[i] - 1] = i + 1;
                if (Red[i] != 0) Boa[Red[i] - 1] = i + 1 + 6;
            }
        }
        public void Swap(int i, int j)
        {
            int a = Boa[i], b = Boa[j], temp;
            if (a <= 6)
            {
                temp = Blue[a - 1];
                Blue[a - 1] = Blue[b - 1];
                Blue[b - 1] = temp;
            }
            else
            {
                temp = Red[a - 1 - 6];
                Red[a - 1 - 6] = Red[b - 1 - 6];
                Red[b - 1 - 6] = temp;
            }
            temp = Boa[i];
            Boa[i] = Boa[j];
            Boa[j] = temp;
        }
        public void To(int from, int to)
        {

            if (Boa[to] != 0)
            {
                int j = Boa[to];
                if (j > 6) Red[j - 6 - 1] = 0; else Blue[j - 1] = 0;
            }
            Boa[to] = Boa[from];


            if (Boa[from] <= 6) Blue[Boa[to] - 1] = to + 1;
            else Red[Boa[to] - 6 - 1] = to + 1;
            Boa[from] = 0;
        }
        public void Ini(int[] num)
        {
            if (num.Length != 12) return;
            for (int i = 0; i < 6; ++i)
            {
                Blue[i] = num[i];
                Red[i] = num[i + 6];
            }
            Ini();
        }
        public override string ToString()
        {
            string s = "";
            for (int i = 0; i < 6; ++i) s += (Blue[i].ToString() + " ");
            for (int i = 0; i < 6; ++i) s += (Red[i].ToString() + " ");
            return s.Trim();
        }
        public int[] Blue = new int[6] { 25, 24, 20, 23, 19, 15 };
        public int[] Red = new int[6] { 1, 2, 3, 6, 7, 11 };
        public int[] Boa = new int[25];
    }

    #region 玩家類別

    public abstract class Gamer
    {
        virtual public int[] Get(string arg) { return null; }
        virtual public void exit() { }
        virtual public void Freeer() { }



    }
    public class Hander : Gamer
    {

    }

    public class AIer : Gamer
    {

        public AIer(string p)
        {
            // TODO: Complete member initialization

            Exe.StartInfo.FileName = p;
            Exe.StartInfo.WorkingDirectory = Path.GetDirectoryName(p);
            Form1.Message("AI位置:" + Path.GetDirectoryName(p));
            Exe.StartInfo.UseShellExecute = false;  //啟動標準輸出為True
            Exe.StartInfo.RedirectStandardOutput = true;
            Exe.StartInfo.RedirectStandardInput = true;
            Exe.StartInfo.CreateNoWindow = true;
            Exe.Start();
        }
        public Process Exe = new Process();
        public override int[] Get(string arg)
        {
            try
            {
                Form1.Message(" 給AI >>" + arg + "<<");
                Exe.StandardInput.WriteLine(arg);
                string s = Exe.StandardOutput.ReadLine().Trim();
                string[] line = s.Split(' ');
                Form1.Message(" 收AI >>" + s + "<<");
                int[] bo = new int[line.Length];
                for (int i = 0; i < line.Length; ++i)
                {
                    bo[i] = Convert.ToInt32(line[i]);
                    // Form1.Message(" 轉數字 >>" + i.ToString() + ">>" + bo[i].ToString() + "<<");
                }
                return bo;
            }
            catch { Form1.Message("AI ERROR!"); }
            return null;

        }
        public override void Freeer()
        {
            try
            {
                Exe.StandardInput.WriteLine("exit");
                Form1.Message(" 給AI >>exit<<");
            }
            catch { }
        }
    }
    public class Neter : Gamer
    {
        Connect node;
        public Neter(Connect c)
        {
            node = c;

        }



    }

    #endregion

    public class Recorder
    {
        ListBox list;
        StreamWriter sw;
        public Recorder(ListBox l)
        {
            list = l;
        }
        public void ChreatRecord(string name)
        {
            if (!Directory.Exists(@"Record"))
            {
                Directory.CreateDirectory(@"Record");
            }
            if (!Directory.Exists(@"Record\" + name))
            {
                Directory.CreateDirectory(@"Record\" + name);
            }
            string pathname = @"Record\" + name + @"\" + name + DateTime.Now.ToString("=yyyy-MM-dd-H-mm-ss") + ".recordfile";
            sw = new StreamWriter(pathname);
            list.Items.Clear();
        }

        public void CloseRecord()
        {
            try
            {
                sw.Dispose();
            }
            catch { }
        }
        public void Add(string line)
        {
            list.Items.Add(line);
        }
        public void RemoveBack2()
        {
            list.Items.RemoveAt(list.Items.Count - 1);
            list.Items.RemoveAt(list.Items.Count - 1);
        }
        public void RemoveBack1()
        {
            list.Items.RemoveAt(list.Items.Count - 1);
        }
        public void AddRecocd_EX(string line)
        {
            sw.WriteLine(line);
            sw.Flush();
            list.Items.Add(line);
        }
        public void AddRecocd(string line)
        {
            sw.WriteLine(line);
            sw.Flush();
        }

        internal void ListFlush()
        {
            foreach (string line in list.Items)
            {
                sw.WriteLine(line);
            }
            sw.Flush();
        }
    }
    public class RecordReader
    {
        ListBox list;
        NumericUpDown nud;
        StreamReader sr;
        List<List<string>> Recordlist = new List<List<string>>();
        public int Pos = 0;
        public RecordReader(ListBox l, NumericUpDown n)
        {
            list = l;
            nud = n;
        }
        public int MaxNum
        {
            get { return Recordlist.Count; }
        }
        public void OpenRecord(string name)
        {
            Recordlist.Clear();
            CloseRecord();
            sr = new StreamReader(name);
            list.Items.Clear();
            GetNextRecord();
            Pos = 0;
            Add(0);
            nud.Value = 1;
            nud.Maximum = 1;
        }
        public void CloseRecord()
        {
            try
            {
                sr.Dispose();
            }
            catch { }
        }



        internal bool GetUp()
        {
            if (Pos == 0) return false;
            else
            {
                --Pos;
                Add(Pos);
                return true;
            }
        }
        void Add(int num)
        {
            list.Items.Clear();
            foreach (object o in Recordlist[num])
                list.Items.Add(o);

        }
        public void GetRecord(int num)
        {
            Add(num);
            Pos = num;
        }
        void GetNextRecord()
        {
            string line;
            List<string> OC = new List<string>();
            while (true)
            {
                line = sr.ReadLine();
                if (line == null || line == "") throw new Exception();
                switch ((line.Split(' '))[0])
                {
                    case "end":
                        OC.Add(line);
                        Recordlist.Add(OC);
                        return;
                    case "endgame":
                        sr.Dispose();
                        throw new Exception();
                    default:
                        OC.Add(line);
                        break;
                }
            }
        }
        internal bool GetDown()
        {
            try
            {
                if (Recordlist.Count == Pos + 1)
                {
                    GetNextRecord();
                    nud.Maximum += 1;
                }
                ++Pos;
                Add(Pos);
                return true;
            }
            catch { return false; }
        }
    }

    class ChangeLanguage : System.Exception
    {
    }
}
