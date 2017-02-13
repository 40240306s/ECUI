using System;
using System.Windows.Forms;

namespace 愛因斯坦棋端點
{
    public partial class 關於 : Form
    {
        public 關於()
        {
            InitializeComponent();
            textBox1.Text =
@"
2016/6/11
bug fix
新增初始化盤面選擇
新增有限機能的回覆盤面
新增出步暫停

2016/5/26
修改紀錄檔格式
增加一步時限

2016/5/9
修正英文版中的中文
執行檔換圖示

2016/2/12
語言切換功能新增

2016/2/5
讀檔機能完成

2016/2/4
紀錄再現機能添加,尚無讀譜機能


2016/2/4
測試模式,紀錄功能新增
增加計時器的準度
尚未新增讀譜機能

2016/2/1
測試模式建置

2016/1/31
連線與AI的錯誤處理添加
不打算處理當入房端尚未切成AI就進行遊戲的狀況

2016/1/25
有條件下的網路功能,程式對下功能
尚未實作記錄功能,以及強化錯誤處理
";
            textBox1.SelectionStart = 0;

        }

        private void OKButtom_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }


    }
}
