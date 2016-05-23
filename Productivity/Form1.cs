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
        DataTable dtProductivity;

        public Form1()
        {
            InitializeComponent();

            //todo 載入當週工作項目
            dtProductivity = new DataTable("Productivity");
            dtProductivity.Columns.Add("項目名稱", typeof(string));
            dtProductivity.Columns.Add("執行時間", typeof(string));
            dtProductivity.Columns.Add("duration", typeof(TimeSpan));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = dtProductivity;
            dataGridView1.Columns["duration"].Visible = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

            string sFormat = "";
            if (tsDuration.Hours > 0)
                sFormat += string.Format("{0}時", tsDuration.Hours);
            if (tsDuration.Minutes > 0)
                sFormat += string.Format("{0}分", tsDuration.Minutes);

            DataRow drTemp = dtProductivity.NewRow();
            drTemp[0] = sTitle;
            drTemp[1] = sFormat + string.Format("{0}秒", tsDuration.Seconds);
            drTemp[2] = tsDuration;
            dtProductivity.Rows.InsertAt(drTemp,0);
            
            textBox1.Text = "";

            //todo 存入當日檔案
            
        }

        /// <summary>
        /// 繼續執行選擇項目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0)
            {
                MessageBox.Show("請選擇一筆項目");
                return;
            }
            if (sw.Elapsed.TotalSeconds >0)
            {
                MessageBox.Show("項目正在執行中,請終止執行中項目");
                return;
            }

            sTitle = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            textBox1.Text = sTitle;
            dStart = DateTime.Now;            
            timer1.Start();
            sw.Start();

        }

    }


}
