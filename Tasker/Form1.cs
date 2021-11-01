using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasker
{
    public partial class frmTasker : Form
    {
        public frmTasker()
        {
            InitializeComponent();
            try
            {
                txtLocation.Text = File.ReadLines("location.txt").First();
                lblInfo.Text = "location.txt found";
            }
            catch (Exception ex)
            {
                lblInfo.Text = "location.txt NOT FOUND, RECREATING";
                txtLocation.Text = "D:/work/tasks";
                CreateConfig();
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (txtTaskName.Text == String.Empty) txtTaskName.Text = $"Unnamed Task";

            var loc = $"{txtLocation.Text}/{DateTime.Now.Year}{DateTime.Now.Month.ToString().PadLeft(2, '0')}{DateTime.Now.Day.ToString().PadLeft(2, '0')}-{txtTaskName.Text.ToLower().Replace(' ', '-')}";

            if (Directory.Exists(loc)) loc += $"-{DateTime.Now.Ticks}";

            try
            {
                Directory.CreateDirectory(loc);
                lblInfo.Text = loc;

                if (chkOpen.Checked)
                {
                    var psi = new ProcessStartInfo() { FileName = loc, UseShellExecute = true };
                    Process.Start(psi);
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }

        private void txtLocation_TextChanged(object sender, EventArgs e)
        {
            CreateConfig();
        }

        private void CreateConfig()
        {
            try
            {
                File.WriteAllText("location.txt", txtLocation.Text);
                lblInfo.Text = "location.txt UPDATED";
            }
            catch (Exception ex)
            {
                lblInfo.Text = $"location.txt {ex.Message}";
            }
        }
    }
}