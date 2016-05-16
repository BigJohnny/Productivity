using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Productivity
{
    public partial class Form1 : Form
    {
        Stopwatch sw = new Stopwatch();
        DateTime dStart;
        TimeSpan tsDuration;
        string sTitle = "";

        public Form1()
        {
            InitializeComponent();

            //todo 載入當週工作項目
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sTitle = textBox1.Text;
            dStart = DateTime.Now;
            timer1.Start();
            sw.Start();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            sw.Stop();
            tsDuration = sw.Elapsed;


            string sTitle = textBox1.Text;
            string sFormat = string.Format("項目:{0} 從 {1}:{2} 開始到現在,已做了", sTitle, dStart.Hour, dStart.Minute);
            if (tsDuration.Hours > 0)
                sFormat += string.Format("{0}時",tsDuration.Hours);
            if (tsDuration.Minutes > 0)
                sFormat += string.Format("{0}分",tsDuration.Minutes);

            label2.Text = sFormat + string.Format("{0}秒", tsDuration.Seconds);

            sw.Start();
        }

        /// <summary>
        /// 暫停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Visible = false;
            button4.Visible = true;
            sw.Stop();
            timer1.Stop();
        }
        /// <summary>
        /// 繼續
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            button4.Visible = false;
            button2.Visible = true;
            timer1.Start();
            sw.Start();
        }
        /// <summary>
        /// 結束並加入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            tsDuration = sw.Elapsed;
            sw.Stop();
            sw.Reset();
            timer1.Stop();

            string sFormat = string.Format("{0},", sTitle);
            if (tsDuration.Hours > 0)
                sFormat += string.Format("{0}時", tsDuration.Hours);
            if (tsDuration.Minutes > 0)
                sFormat += string.Format("{0}分", tsDuration.Minutes);
            
            checkedListBox1.Items.Insert(0, sFormat + string.Format("{0}秒", tsDuration.Seconds));
            textBox1.Text = "";

            //todo 存入當日檔案
            
        }
    }


}
