using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Threading;
using System.Windows.Forms;

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

        //Checks if extended mode can be set if only 1 monitor is avalible will return false
        public bool SetExtendedMode()
        {
            var hasMultipleMonitors = true;
            //Checks if only 1 monitor is active this can be the case if only 1 monitor is plugged in or display is running in mirror mode
            if (Screen.AllScreens.Count() == 1)
            {
                //Checks for how many displays are actually connected to device and if more then 1 is set's extended mode
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

        private void SetDisplayMode(DisplayMode mode)
        {
            using (var proc = new Process())
            {
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
            }
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
