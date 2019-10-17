using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GACInstaller
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            if (string.IsNullOrEmpty(path))
                return;

            if (!Directory.Exists(path))
            {
                MessageBox.Show("No such directory!");
                return;
            }

            textBox2.Clear();

            var files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            var publisher = new Publish();

            foreach (var file in files)
            {
                try
                {
                    publisher.GacInstall(file);
                    log("PROCESSED: "+Path.GetFileName(file));
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    log("ERROR: "+ex.Message);
                }
            }

        }

        void log(string str, params string[] prms)
        {
            textBox2.Text += String.Format(str, prms) +Environment.NewLine;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            if (string.IsNullOrEmpty(path))
                return;

            if (!Directory.Exists(path))
            {
                MessageBox.Show("No such directory!");
                return;
            }

            textBox2.Clear();

            var files = Directory.GetFiles(path, "*.dll", SearchOption.AllDirectories);
            var publisher = new Publish();

            foreach (var file in files)
            {
                try
                {
                    publisher.GacRemove(file);
                    log("PROCESSED: " + Path.GetFileName(file));
                    Application.DoEvents();
                }
                catch (Exception ex)
                {
                    log("ERROR: " + ex.Message);
                }
            }
        }
    }
}
