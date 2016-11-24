using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DisplayChanger;
using System.Runtime.InteropServices;
using System.Diagnostics;
using Newtonsoft.Json;

namespace DisplayChangerCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            //SetDisplayMode(DisplayMode.Duplicate);
            //SetDisplayMode(DisplayMode.Extend);
            //Display display = new Display();
            //List<DevMode> modes = display.GetDisplaySettings();
            //Console.WriteLine("Develop mode");
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("key", "value");
            values.Add("key2", "value2");

            string output = JsonConvert.SerializeObject(values);
            Console.WriteLine(output);
            //Newtonsoft.Json.Serialization.

            DisplayDevices display = new DisplayDevices();
            display.SetupDisplay();
            var allDisplays = display.GetActiveDisplayDevices();

            //List<DisplayDevice> devices = new List<DisplayDevice>();

            //bool error = false;
            //////Here I am listing all DisplayDevices (Monitors)
            //for (int devId = 0; !error; devId++)
            //{
            //    try
            //    {
            //        DisplayDevice device = new DisplayDevice();
            //        device.cb = Marshal.SizeOf(typeof(DisplayDevice));
            //        error = NativeMethods.EnumDisplayDevicesW(null, devId, ref device, 0) == 0;
            //        devices.Add(device);
            //    }
            //    catch (Exception)
            //    {
            //        error = true;
            //    }
            //}
            //List<string> monitors = new List<String>();
            //monitors.Add(@"\\.\DISPLAY1");
            //monitors.Add(@"\\.\DISPLAY2");

            //List<DisplaySet> devicesAndModes = new List<DisplaySet>();
            //Dictionary<DisplayDevice, List<DevMode>> devs = new Dictionary<DisplayDevice, List<DevMode>>();
            //foreach (var device in devices)
            //{
            //    error = false;
            //    for (int i = 0; !error; i++)
            //    {
            //        try
            //        {
            //            DevMode mode = new DevMode();
            //            //-1 get's the current display setting
            //            error = NativeMethods.EnumDisplaySettings(device.DeviceName, -1 + i, ref mode) == 0;
            //            if (!error)
            //            {
            //                if (!devs.Keys.Contains(device))
            //                {
            //                    devs.Add(device, new List<DevMode>());
            //                }

            //                if (!devs[device].Where(m => m.dmPelsWidth == mode.dmPelsWidth && m.dmPelsHeight == mode.dmPelsHeight && m.dmDeviceName == mode.dmDeviceName && m.dmDisplayFrequency == mode.dmDisplayFrequency && m.dmBitsPerPel == mode.dmBitsPerPel).Any())
            //                {
            //                    Console.WriteLine(string.Format("{0}: {1}x{2} {3}hz", device.DeviceName, mode.dmPelsWidth, mode.dmPelsHeight, mode.dmDisplayFrequency));
            //                    devs[device].Add(mode);
            //                }
            //                //devicesAndModes.Add(new DisplaySet { DisplayDevice = device, DevMode = mode });
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            error = false;
            //        }
            //    }
            //}

            //foreach (var dev in devices)
            //{
            //    error = false;
            //    //Here I am listing all DeviceModes (Resolutions) for each DisplayDevice (Monitors)
            //    for (int i = 0; !error; i++)
            //    {
            //        try
            //        {
            //            //DeviceMode is a wrapper. You can find it [here](http://pinvoke.net/default.aspx/Structures/DEVMODE.html)
            //            DevMode mode = new DevMode();
            //            error = NativeMethods.EnumDisplaySettings(null, -1 + i, ref mode) == 0;
            //            //error = NativeMethods.EnumDisplaySettings(dev.DeviceName, -1 + i, ref mode) == 0;
            //            //Display 
            //            //DisplayDevice test = dev;
            //            //DevMode testMode = mode;
            //            devicesAndModes.Add(new DisplaySet { DisplayDevice = dev, DevMode = mode });
            //            var info = mode.GetInfoArray();
            //            Console.WriteLine("Just another line");
            //        }
            //        catch (Exception ex)
            //        {
            //            error = true;
            //        }
            //    }
            //}

            //Select any 800x600 resolution ...
            //DevMode d800x600 = devicesAndModes.Where(s => s.DevMode.dmPelsWidth == 800).FirstOrDefault().DevMode;//.First().DevMode;
            //List<DisplaySet> d800x600Sets = devicesAndModes.Where(s => s.DevMode.dmPelsWidth == 800).ToList();
            //DisplaySet set = devicesAndModes.Where(s => s.DevMode.dmPelsWidth == 1920).FirstOrDefault();
            //IntPtr ptr = new IntPtr();

            //var code = NativeMethods.ChangeDisplaySettingsEx(set.DisplayDevice.DeviceName, ref set.DevMode, ptr, 0, ptr);
            //NativeMethods.ChangeDisplaySettingsExW(set.DisplayDevice.DeviceName, ref set.DevMode, null, 0, null);
            //NativeMethods.ChangeDisplaySettings(ref d800x600, 0);
            //DevMode devmode = new DevMode();
            //int iModeNum = NativeMethods.ENUM_CURRENT_SETTINGS;

            //var deviceName = devices[3].DeviceName;

            //int value = NativeMethods.EnumDisplaySettings(deviceName, iModeNum, ref devmode);

            //return NativeMethods.EnumDisplaySettings(null, iModeNum, ref devmode);
            ////Select any 800x600 resolution ...
            //DeviceMode d800x600 = devicesAndModes.Where(s => s.DeviceMode.dmPelsWidth == 800).First().DeviceMode;

            ////Apply the selected resolution ...
            //ChangeDisplaySettings(ref d800x600, 0);
            Console.WriteLine("Device");
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
            proc.WaitForExit();
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
