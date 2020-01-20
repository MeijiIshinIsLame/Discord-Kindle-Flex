using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KindleFlex
{
    public partial class MainWindow : Form
    {
        public System.Threading.Thread mainThread;
        public System.Threading.Thread statusUpdateThread;
        public Kindle kindle = new Kindle();
        public MainWindow()
        {
            InitializeComponent();
        }

        public void MainWindowLoop()
        {
            while(true)
            {
                kindle.UpdateAllValues();
                //statusUpdateThread = new Thread(() => GetStatus(kindle));
                //statusUpdateThread.Start();
                this.Text = kindle.GetStatusUpdate();
                Thread.Sleep(1000);
            }
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            mainThread = new System.Threading.Thread(MainWindowLoop);
            mainThread.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            kindle.UpdateAllValues();
            label1.Text = $"Percent: {kindle.GetPercentComplete()}  Title: {kindle.GetBookTitle()}" +
                $"\n CurrPage: {kindle.GetCurrentPage()} " +
                $"          MaxPage: {kindle.GetMaxPage()} Baseaddr: {kindle.GetMemoryReader().GetBaseAddr().ToString("X")}";
        }
    }
}
