using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoAimProject
{
    public partial class PortSetting : Form
    {
        public delegate void SetPortEventHandler(string portname, int baudrate, int databits, int stopbits);
        public PortSetting()
        {
            InitializeComponent();
        }

        private void PortSetting_Load(object sender, EventArgs e)
        {
            try
            {
                string[] port = SerialPort.GetPortNames();
                Array.Sort(port);
                comboBoxPortName.Items.AddRange(port);
                comboBoxPortName.SelectedIndex = 0;
                comboBoxBaud.SelectedIndex = 3;
                comboBoxDataBits.SelectedIndex = 3;
                comboBoxStopBits.SelectedIndex = 0;
            }
            catch (Exception)
            {
                MessageBox.Show("No Port Found,Please check your ComPort", "Error!");
            }

        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            SetPortEventHandler setdelegate = new SetPortEventHandler(Main.SetPort);
            setdelegate(comboBoxPortName.Text, Convert.ToInt32(comboBoxBaud.Text),
                        Convert.ToInt32(comboBoxDataBits.Text), Convert.ToInt32(comboBoxStopBits.Text));
            this.Close();
        }
    }
}
