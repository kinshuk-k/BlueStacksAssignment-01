using AltoHttp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Downloader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        HttpDownloader httpDownloader;

        private void downButton_Click(object sender, EventArgs e)
        {
            string dir = @"D:\Test";
            if(!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            httpDownloader = new HttpDownloader(textUrl.Text, dir);
            httpDownloader.DownloadCompleted += HttpDownloader_DownloadCompleted;
            httpDownloader.ProgressChanged += HttpDownloader_ProgressChanged;
            httpDownloader.Start();
        }

        private void HttpDownloader_ProgressChanged(object sender, AltoHttp.ProgressChangedEventArgs e)
        {
            progressBar.Value = (int)e.Progress;
            lblPercent.Text = $"{e.Progress.ToString("0.00")} %";
            lblSpeed.Text = string.Format("{0} Mb/s", (e.SpeedInBytes / 1024d / 1024d).ToString("0.00"));
            lblDownloaded.Text = string.Format("{0} Mb/s", (httpDownloader.TotalBytesReceived / 1024d / 1024d).ToString("0.00"));
            lblStatus.Text = "Downloading..";
        }

        private void HttpDownloader_DownloadCompleted(object sender, EventArgs e)
        {
            this.Invoke((MethodInvoker)delegate
            {
                lblStatus.Text = "Finished";
                lblPercent.Text = "100 %";
            });
        }
    }
}
