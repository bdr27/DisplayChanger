using DisplayChanger;
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

namespace DisplayChangeWinForm
{
    public partial class FormDisplay : Form
    {
        DisplayDevices devices;
        MonitorMode monitorMode;

        public FormDisplay()
        {
            devices = new DisplayDevices();
            monitorMode = new MonitorMode();
            InitializeComponent();
        }

        private void FormDisplay_Load(object sender, EventArgs e)
        {
            if (!monitorMode.SetExtendedMode())
            {
                MessageBox.Show("2 displays are not connected");
            }
            devices.SetupDisplay();
            foreach(var display in devices.GetActiveDisplayDevices().Keys)
            {
                cbDisplays.Items.Add(display);
            }
        }

        private void cbDisplays_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateLvDisplayModes();
        }

        private void UpdateLvDisplayModes()
        {
            lvDisplayModes.Items.Clear();
            var device = (DisplayDevice)cbDisplays.SelectedItem;

            foreach (var resolutions in devices.GetActiveDisplayDevices()[device].ToList())
            {
                string[] items = new string[5];
                items[0] = resolutions.dmDeviceName;
                items[1] = resolutions.dmPelsWidth + "";
                items[2] = resolutions.dmPelsHeight + "";
                items[3] = resolutions.dmDisplayFrequency + "";
                items[4] = resolutions.dmBitsPerPel + "";
                ListViewItem newItem = new ListViewItem(items);
                newItem.Tag = resolutions;
                lvDisplayModes.Items.Add(newItem);
            }
        }

        private void lvDisplayModes_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ChangeDisplay display = new ChangeDisplay();
            try
            {
                var mode = (DevMode)lvDisplayModes.SelectedItems[0].Tag;
                var device = (DisplayDevice)cbDisplays.SelectedItem;
                display.Execute(device, mode);
                devices.SetupDisplay();
                UpdateLvDisplayModes();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
