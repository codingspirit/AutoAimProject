using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace AutoAimProject
{
    public partial class ParSetting : Form
    {
        private delegate void SetPar(int p, int i, int d);
        int p, i, d;
        public ParSetting()
        {
            InitializeComponent();
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            SetPar setpar = new SetPar(Main.SetParameter);
            setpar(trackBarP.Value, trackBarI.Value, trackBarD.Value);
        }
        private void trackBarScroll(object sender, EventArgs e)
        {
            switch (((TrackBar)sender).Name)
            {
                case "trackBarP": labelP.Text = ((TrackBar)sender).Value.ToString(); p = ((TrackBar)sender).Value; break;
                case "trackBarI": labelI.Text = ((TrackBar)sender).Value.ToString(); i = ((TrackBar)sender).Value; break;
                case "trackBarD": labelD.Text = ((TrackBar)sender).Value.ToString(); d = ((TrackBar)sender).Value; break;
            }
        }

        private void ParSetting_Load(object sender, EventArgs e)
        {
            foreach (Control item in this.Controls)
            {
                if (item is TrackBar)
                {
                    ((TrackBar)item).Maximum = 100;
                    ((TrackBar)item).Value = 0;
                }
            }
            labelP.Text = "0";
            labelI.Text = "0";
            labelD.Text = "0";
        }

        private void checkBoxSet_CheckedChanged(object sender, EventArgs e)
        {
            System.Timers.Timer t1;
            t1 = new System.Timers.Timer(100);
            t1.Elapsed += TElapsed;
            if (((CheckBox)sender).Checked)
            {
                t1.Start();
                buttonSet.Enabled = false;
            }
            else
            {
                buttonSet.Enabled = true;
                t1.Stop();
                t1.Dispose();
            }
        }
        private void TElapsed(object sender, ElapsedEventArgs e)
        {
            SetPar setpar = new SetPar(Main.SetParameter);
            setpar(p, i, d);
        }
    }
}
