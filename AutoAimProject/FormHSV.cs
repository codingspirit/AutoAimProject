using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoAimProject
{
    public partial class FormHSV : Form
    {
        private int vmin = 10, vmax = 255, smin = 30, bins = 20;

        public FormHSV()
        {
            InitializeComponent();
        }
        public int Getvmin
        {
            get { return vmin; }
        }
        public int Getvmax
        {
            get { return vmax; }
        }
        public int Getsmin
        {
            get { return smin; }
        }
        public int Getbins
        {
            get { return bins; }
        }

        private void trackBarVmin_Scroll(object sender, EventArgs e)
        {
            vmin = trackBarVmin.Value;
            labelVmin.Text = "Vmin:" + vmin.ToString();
        }

        private void trackBarVmax_Scroll(object sender, EventArgs e)
        {
            vmax = trackBarVmax.Value;
            labelVmax.Text = "Vmax:" + vmax.ToString();
        }

        private void trackBarSmin_Scroll(object sender, EventArgs e)
        {
            smin = trackBarSmin.Value;
            labelSmin.Text = "Smin:" + smin.ToString();
        }

        private void trackBarBins_Scroll(object sender, EventArgs e)
        {
            bins = trackBarBins.Value;
            labelBins.Text = "Bins:" + bins.ToString();
        }



        private void FormHSV_Load(object sender, EventArgs e)
        {
            trackBarVmin.Maximum = 256;
            trackBarVmax.Maximum = 256;
            trackBarSmin.Maximum = 256;
            trackBarBins.Maximum = 60;
            trackBarSmin.Minimum = 0;
            trackBarVmax.Minimum = 0;
            trackBarVmin.Minimum = 0;
            trackBarBins.Minimum = 1;
            trackBarVmin.Value = vmin;
            trackBarVmax.Value = vmax;
            trackBarSmin.Value = smin;
            trackBarBins.Value = 16;
        }
    }
}
