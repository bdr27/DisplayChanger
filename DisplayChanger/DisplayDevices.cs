using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace DisplayChanger
{
    public class DisplayDevices
    {
        Dictionary<DisplayDevice, List<DevMode>> displayAndSettings;
        List<DisplayDevice> devices;

        //Sets the dictionary for display device
        public void SetupDisplay()
        {
            if(displayAndSettings == null)
            {
                displayAndSettings = new Dictionary<DisplayDevice, List<DevMode>>();
            }
            else
            {
                displayAndSettings.Clear();
            }
            if(devices == null)
            {
                devices = new List<DisplayDevice>();
            }
            else
            {
                devices.Clear();
            }            

            bool error = false;
            //Here I am listing all DisplayDevices (Monitors)
            for (int devId = 0; !error; devId++)
            {
                try
                {
                    DisplayDevice device = new DisplayDevice();
                    device.cb = Marshal.SizeOf(typeof(DisplayDevice));
                    //Gets list of display devies (can include printers)
                    error = NativeMethods.EnumDisplayDevicesW(null, devId, ref device, 0) == 0;
                    DisplayDevice monitorName = device;//.Clone();
                    error = NativeMethods.EnumDisplayDevicesW(monitorName.DeviceName, 0, ref monitorName, 0) == 0;
                    device.DeviceString = monitorName.DeviceString;
                    devices.Add(device);
                }
                catch (Exception)
                {
                    error = true;
                }
            }

            List<DisplaySet> devicesAndModes = new List<DisplaySet>();

            foreach (var device in devices)
            {
                error = false;
                for (int i = 0; !error; i++)
                {
                    try
                    {
                        DevMode mode = new DevMode();
                        //-1 get's the current display setting
                        error = NativeMethods.EnumDisplaySettings(device.DeviceName, -1 + i, ref mode) == 0;
                        if (!error)
                        {
                            //Adds new device to display and settings if not already included
                            if (!displayAndSettings.Keys.Contains(device))
                            {
                                displayAndSettings.Add(device, new List<DevMode>());
                            }

                            //Checks if Screen Resolution, Refresh rate and bits per pixel are already contained within the list to remove duplicates that can show up
                            if (!displayAndSettings[device].Where(m => m.dmPelsWidth == mode.dmPelsWidth && m.dmPelsHeight == mode.dmPelsHeight && m.dmDeviceName == mode.dmDeviceName && m.dmDisplayFrequency == mode.dmDisplayFrequency && m.dmBitsPerPel == mode.dmBitsPerPel).Any())
                            {
                                Console.WriteLine(string.Format("{0}: {1}x{2} {3}hz", device.DeviceName, mode.dmPelsWidth, mode.dmPelsHeight, mode.dmDisplayFrequency));
                                displayAndSettings[device].Add(mode);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        error = false;
                    }
                }
            }
        }

        public Dictionary<DisplayDevice, DevMode> GetCurrentDisplayDevices()
        {
            Dictionary<DisplayDevice, DevMode> currentDisplayAndSettings = new Dictionary<DisplayDevice, DevMode>();

            if(displayAndSettings != null)
            {
                //first entry in each Display Device is current settings
                foreach(var key in displayAndSettings.Keys)
                {
                    currentDisplayAndSettings.Add(key, displayAndSettings[key][0]);
                }
            }
            return currentDisplayAndSettings;
        }

        //Get's list of DisplayDevices that are able to set a screen resolution
        public Dictionary<DisplayDevice, List<DevMode>> GetActiveDisplayDevices()
        {
            if(displayAndSettings == null)
            {
                displayAndSettings = new Dictionary<DisplayDevice, List<DevMode>>();
            }

            return displayAndSettings;
        }
    }
}
