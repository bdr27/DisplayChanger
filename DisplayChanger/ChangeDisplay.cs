﻿using System;

namespace DisplayChanger
{
    public class ChangeDisplay
    {
        public ReturnCodes Execute(DisplayDevice device, DevMode mode)
        {
            IntPtr ptr = new IntPtr();

            return NativeMethods.ChangeDisplaySettingsEx(device.DeviceName, ref mode, ptr, 0, ptr);
        }
    }
}
