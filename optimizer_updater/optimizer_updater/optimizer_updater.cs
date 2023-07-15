using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Security.Policy;

namespace optimizer_updater
{
    public partial class optimizer_updater : Form
    {
        public optimizer_updater()
        {
            InitializeComponent();
        }

        private void optimizer_updater_LoadAsync(object sender, EventArgs e)
        {
            try
            {
                foreach (var process in Process.GetProcessesByName("optimizer"))
                {
                    process.Kill();
                }
            }
            catch (Exception ex)
            {

            }
          



            string url_asar = "";

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string response = webClient.DownloadString("https://raw.githubusercontent.com/tirldev/optimizer/main/update/l_asar.optimizer");
                    url_asar = response.Replace("\n", ""); ;
                }
                catch (WebException ex)
                {
                }
            }

            using (WebClient webClient2 = new WebClient())
            {
                try
                {
                    webClient2.DownloadFileCompleted += DescargaCompletada;
                    webClient2.DownloadFileAsync(new Uri(url_asar), Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\programs\optimizer\resources\app.asar");
                }
                catch (WebException ex)
                { 
                }
            }
        }
        static void DescargaCompletada(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    Process.Start("start "+ Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Programs\optimizer\optimizer.exe");
                }
                catch (Exception ex)
                {

                }
                Thread.Sleep(1000);
                Application.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
