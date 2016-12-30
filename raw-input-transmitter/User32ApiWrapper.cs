using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Windows.Forms;

namespace raw_input_transmitter
{
    class User32ApiWrapper
    {
        private static int _vendorID = -1;
        public static int VendorID
        {
            get { return _vendorID; }
        }

        private static int _productID = -1;
        public static int ProductID
        {
            get { return _productID; }
        }

        internal struct RAWINPUTDEVICELIST_ELMT
        {
            public IntPtr hDevice;
            public uint dwType;
        }

        public enum RawInputDeviceType : uint
        {
            RIM_TYPEMOUSE = 0,
            RIM_TYPEKEYBOARD = 1,
            RIM_TYPEHID = 2,
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTDEVICE
        {
            public ushort usUsagePage;
            public ushort usUsage;
            public int dwFlags;
            public IntPtr hwndTarget;
        }

        public enum RawInputDeviceInfoType : uint
        {
            RIDI_DEVICENAME = 0x20000007,
            RIDI_DEVICEINFO = 0x2000000b,
            RIDI_PREPARSEDDATA = 0x20000005,
        }

        public const ushort RIDEV_INPUTSINK = 0x00000100;
        public const ushort RIDEV_PAGEONLY = 0x00000020;

        [StructLayout(LayoutKind.Sequential)]
        internal struct RID_DEVICE_INFO_HID
        {
            public int dwVendorId;
            public int dwProductId;
            public int dwVersionNumber;
            public ushort usUsagePage;
            public ushort usUsage;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RID_DEVICE_INFO_MOUSE
        {
            public int dwId;
            public int dwNumberOfButtons;
            public int dwSampleRate;
            public bool fHasHorizontalWheel;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RID_DEVICE_INFO_KEYBOARD
        {
            public int dwType;
            public int dwSubType;
            public int dwKeyboardMode;
            public int dwNumberOfFunctionKeys;
            public int dwNumberOfIndicators;
            public int dwNumberOfKeysTotal;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct RID_DEVICE_INFO
        {
            [FieldOffset(0)]
            public uint cbSize;
            [FieldOffset(4)]
            public RawInputDeviceType dwType;
            [FieldOffset(8)]
            public RID_DEVICE_INFO_MOUSE mouse;
            [FieldOffset(8)]
            public RID_DEVICE_INFO_KEYBOARD keyboard;
            [FieldOffset(8)]
            public RID_DEVICE_INFO_HID hid;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWINPUTHEADER
        {
            public RawInputDeviceType dwType;
            public int dwSize;
            public IntPtr hDevice;
            public uint wParam;
        }


        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWMOUSE
        {
            /*
            [FieldOffset(0)]
            public ushort usFlags;
            [FieldOffset(2)]
            public uint ulButtons;
            */
            [FieldOffset(0)]
            public ushort usButtonFlags;
            /*
            [FieldOffset(2)]
            public ushort usButtonData;
            [FieldOffset(6)]
            public uint ulRawButtons;
            */
            [FieldOffset(4)]
            public int lLastX;
            [FieldOffset(8)]
            public int lLastY;
            /*
            [FieldOffset(18)]
            public uint ulExtraInformation;
            */
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWKEYBOARD
        {
            public ushort MakeCode;
            public ushort Flags;
            public ushort Reserved;
            public ushort VKey;
            public uint Message;
            public uint ExtraInformation;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RAWHID
        {
            public int dwSizeHid;
            public int dwCount;
            //use of a pointer here for struct reason
            public IntPtr pbRawData;
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct RAWINPUT
        {
            [FieldOffset(0)]
            public RAWINPUTHEADER header;

            [FieldOffset(16 + 8)]
            public RAWMOUSE mouse;

            [FieldOffset(16 + 8)]
            public RAWKEYBOARD keyboard;

            [FieldOffset(16 + 8)]
            public RAWHID hid;
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetRawInputDeviceList([In, Out] RAWINPUTDEVICELIST_ELMT[] InputdeviceList, [In, Out] ref uint puiNumDevices, [In] uint cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetRawInputDeviceInfo([In] IntPtr hDevice, [In] RawInputDeviceInfoType uiCommand, [In, Out] IntPtr pData, [In, Out] ref uint pcbSize);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

        [DllImport("user32.dll")]
        static extern uint GetRegisteredRawInputDevices([In, Out] RAWINPUTDEVICE[] InputdeviceList, [In, Out] ref uint puiNumDevices, [In] uint cbSize);

        [DllImport("user32.dll", SetLastError = true)]
        static extern uint GetRawInputBuffer([In, Out] RAWINPUT[] pData, [In, Out] ref uint pcbSize, [In] uint cbSizeHeader);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetRawInputData(IntPtr hRawInput, uint command, [Out] IntPtr pData, ref uint size, int sizeHeader);

        public static bool RegisterRawInputMouse()
        {
            RAWINPUTDEVICE[] rawInputDevicesToMonitor = new RAWINPUTDEVICE[1];
            RAWINPUTDEVICE device = new RAWINPUTDEVICE();
            device.dwFlags = RIDEV_INPUTSINK;
            device.hwndTarget = Process.GetCurrentProcess().MainWindowHandle;
            device.usUsage = 0x02;
            device.usUsagePage = 0x01;
            rawInputDevicesToMonitor[0] = device;

            if (!RegisterRawInputDevices(rawInputDevicesToMonitor, 1, (uint)Marshal.SizeOf(new RAWINPUTDEVICE())))
            {
                Console.WriteLine("Registration of device was not successful - Error: " + Marshal.GetLastWin32Error());
                return false;
            }

            Console.WriteLine("Registration of device was successful");
            return true;
        }

        public static bool UnregisterRawInputMouse()
        {
            RAWINPUTDEVICE[] rawInputDevicesToMonitor = new RAWINPUTDEVICE[1];
            RAWINPUTDEVICE device = new RAWINPUTDEVICE();
            device.dwFlags = 0x00000001;
            device.hwndTarget = (IntPtr)null;
            device.usUsage = 0x02;
            device.usUsagePage = 0x01;
            rawInputDevicesToMonitor[0] = device;

            if (!RegisterRawInputDevices(rawInputDevicesToMonitor, 1, (uint)Marshal.SizeOf(new RAWINPUTDEVICE())))
            {
                Console.WriteLine("Unregistration of device was not successful - Error: " + Marshal.GetLastWin32Error());
                return false;
            }

            Console.WriteLine("Unregistration of device was successful");
            return true;
        }
    }
}
