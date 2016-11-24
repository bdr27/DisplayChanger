using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Management;
using System.ComponentModel;

namespace DisplayChanger
{
    public class MonitorMode
    {
        public MonitorMode()
        {

        }

        //Checks the amount of screens currently connected
        public int ScreenCount()
        {
            var monitorCount = 0;
            var query = "select * from WmiMonitorBasicDisplayParams";
            using (var wmiSearcher = new ManagementObjectSearcher("\\root\\wmi", query))
            {
                var results = wmiSearcher.Get();
                monitorCount = results.Count;
            }
            return monitorCount;
        }

        public bool SetExtendedMode()
        {
            var hasMultipleMonitors = true;
            if (Screen.AllScreens.Count() == 1)
            {
                if (ScreenCount() > 1)
                {
                    SetDisplayMode(DisplayMode.Extend);
                }else
                {
                    //Only 1 screen connected
                    hasMultipleMonitors = false;
                }
            }
            return hasMultipleMonitors;
        }

        private static void SetDisplayMode(DisplayMode mode)
        {
            var proc = new Process();
            proc.StartInfo.FileName = "DisplaySwitch.exe";
            switch (mode)
            {
                case DisplayMode.External:
                    proc.StartInfo.Arguments = "/external";
                    break;
                case DisplayMode.Internal:
                    proc.StartInfo.Arguments = "/internal";
                    break;
                case DisplayMode.Extend:
                    proc.StartInfo.Arguments = "/extend";
                    break;
                case DisplayMode.Duplicate:
                    proc.StartInfo.Arguments = "/clone";
                    break;
            }
            proc.Start();
            //Waits so that you can get the update happening correctly
            Thread.Sleep(3000);
        }
        enum DisplayMode
        {
            Internal,
            External,
            Extend,
            Duplicate
        }
    }
}
