using System;
using System.Runtime.InteropServices;

namespace DisplayChanger
{
    // Encapsulate the magic numbers for the return value in an enumeration
    public enum ReturnCodes : int
    {
        DISP_CHANGE_SUCCESSFUL = 0,
        DISP_CHANGE_BADDUALVIEW = -6,
        DISP_CHANGE_BADFLAGS = -4,
        DISP_CHANGE_BADMODE = -2,
        DISP_CHANGE_BADPARAM = -5,
        DISP_CHANGE_FAILED = -1,
        DISP_CHANGE_NOTUPDATED = -3,
        DISP_CHANGE_RESTART = 1
    }

    // To see how the DEVMODE struct was translated from the unmanaged to the managed see the Task 2 Declarations section

    // http://msdn.microsoft.com/library/default.asp?url=/library/en-us/gdi/prntspol_8nle.asp
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct DevMode
    {
        // The MarshallAs attribute is covered in the Background section of the article
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmDeviceName;

        public short dmSpecVersion;
        public short dmDriverVersion;
        public short dmSize;
        public short dmDriverExtra;
        public int dmFields;
        public int dmPositionX;
        public int dmPositionY;
        public int dmDisplayOrientation;
        public int dmDisplayFixedOutput;
        public short dmColor;
        public short dmDuplex;
        public short dmYResolution;
        public short dmTTOption;
        public short dmCollate;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string dmFormName;

        public short dmLogPixels;
        public short dmBitsPerPel;
        public int dmPelsWidth;
        public int dmPelsHeight;
        public int dmDisplayFlags;
        public int dmDisplayFrequency;
        public int dmICMMethod;
        public int dmICMIntent;
        public int dmMediaType;
        public int dmDitherType;
        public int dmReserved1;
        public int dmReserved2;
        public int dmPanningWidth;
        public int dmPanningHeight;

        public override string ToString()
        {
            return dmPelsWidth.ToString() + " x " + dmPelsHeight.ToString();
        }


        public string[] GetInfoArray()
        {
            string[] items = new string[5];

            items[0] = dmDeviceName;
            items[1] = dmPelsWidth.ToString();
            items[2] = dmPelsHeight.ToString();
            items[3] = dmDisplayFrequency.ToString();
            items[4] = dmBitsPerPel.ToString();

            return items;
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct DisplayDevice
    {
        [MarshalAs(UnmanagedType.U4)]
        public int cb;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string DeviceName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceString;
        [MarshalAs(UnmanagedType.U4)]
        public DisplayDeviceStateFlags StateFlags;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceID;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string DeviceKey;

        public override string ToString()
        {
            return DeviceString;
        }
    }

    public struct DisplaySet
    {
        public DisplayDevice DisplayDevice;
        public DevMode DevMode;
    }

    [Flags()]
    public enum DisplayDeviceStateFlags : int
    {
        /// <summary>The device is part of the desktop.</summary>
        AttachedToDesktop = 0x1,
        MultiDriver = 0x2,
        /// <summary>The device is part of the desktop.</summary>
        PrimaryDevice = 0x4,
        /// <summary>Represents a pseudo device used to mirror application drawing for remoting or other purposes.</summary>
        MirroringDriver = 0x8,
        /// <summary>The device is VGA compatible.</summary>
        VGACompatible = 0x10,
        /// <summary>The device is removable; it cannot be the primary display.</summary>
        Removable = 0x20,
        /// <summary>The device has more display modes than its output devices support.</summary>
        ModesPruned = 0x8000000,
        Remote = 0x4000000,
        Disconnect = 0x2000000
    }


    internal class NativeMethods
    {
        // http://msdn.microsoft.com/library/default.asp?url=/library/en-us/gdi/devcons_84oj.asp
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        internal static extern int EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DevMode lpDevMode);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern int EnumDisplaySettingsW(string lpszDeviceName, int iModeNum, ref DevMode lpDevMode);

        // http://msdn.microsoft.com/library/default.asp?url=/library/en-us/gdi/devcons_7gz7.asp
        [DllImport("user32.dll", CharSet = CharSet.Ansi)]
        internal static extern ReturnCodes ChangeDisplaySettings(ref DevMode lpDevMode, int dwFlags);

        [DllImport("user32.dll")]
        internal static extern ReturnCodes ChangeDisplaySettingsEx(string lpszDeviceName, ref DevMode lpDevMode, IntPtr hwnd, int dwflags, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        internal static extern ReturnCodes ChangeDisplaySettingsExW(string lpszDeviceName, ref DevMode lpDevMode, object hwnd, int dwFlags, object lParam);

        [DllImport("User32.dll")]
        internal static extern int EnumDisplayDevices(string lpDevice, int iDevNum, ref DisplayDevice lpDisplayDevice, int dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Ansi)]
        internal static extern int EnumDisplayDevicesA(string lpDevice, int iDevNum, ref DisplayDevice lpDisplayDevice, int dwFlags);

        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        internal static extern int EnumDisplayDevicesW(string lpDevice, int iDevNum, ref DisplayDevice lpDisplayDevice, int dwFlags);

        internal const int ENUM_CURRENT_SETTINGS = -1;
    }
}
