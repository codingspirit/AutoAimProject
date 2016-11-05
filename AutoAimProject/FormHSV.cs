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
        private int vmin = 10, vmax = 255, smin = 30;

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

        private void trackBarVmin_Scroll(object sender, EventArgs e)
        {
            vmin = trackBarVmin.Value;
        }

        private void trackBarVmax_Scroll(object sender, EventArgs e)
        {
            vmax = trackBarVmax.Value;
        }

        private void trackBarSmin_Scroll(object sender, EventArgs e)
        {
            smin = trackBarSmin.Value;
        }

        public int Getsmin
        {
            get { return smin; }
        }

        private void FormHSV_Load(object sender, EventArgs e)
        {
            trackBarVmin.Maximum = 255;
            trackBarVmax.Maximum = 255;
            trackBarSmin.Maximum = 255;
            trackBarSmin.Minimum = 0;
            trackBarVmax.Minimum = 0;
            trackBarVmin.Minimum = 0;
            trackBarVmin.Value = vmin;
            trackBarVmax.Value = vmax;
            trackBarSmin.Value = smin;
        }
    }
}
