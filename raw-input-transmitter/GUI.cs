using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.IO.Pipes;
using System.Threading;

namespace raw_input_transmitter
{
    public partial class GUI : Form
    {
        static int rimTypeMouseCount = 0;
        static string logMouseX, logMouseY = "";
        static string log1, log2, log3, log4, log5 = "";
        uint rawDataSize;

        NamedPipeServerStream rawMouseDataPipe;
        Thread mainThread;
        object mainLoop;
        int dataCount = 0;
        bool sendData = false;
        bool closeThread = false;

        bool pipeConnected = false;

        public GUI()
        {
            InitializeComponent();
        }

        private void button_startListening_Click(object sender, EventArgs e)
        {
            rtb_console.Text = "";

            CheckForIllegalCrossThreadCalls = false;
            mainThread = new Thread(new ThreadStart(pipeConnect));
            mainThread.Start();

            User32ApiWrapper.RegisterRawInputMouse();
        }

        private void button_stopListening_Click(object sender, EventArgs e)
        {
            User32ApiWrapper.UnregisterRawInputMouse();
            rawMouseDataPipe.Close();
            log(rimTypeMouseCount);

            rimTypeMouseCount = 0;
            logMouseX = "";
            logMouseY = "";
            log1 = "";
            log2 = "";
            log3 = "";
            log4 = "";
            log5 = "";
        }

        [Flags]
        internal enum ButtonFlags : ushort
        {
            /*
            RI_MOUSE_LEFT_BUTTON_DOWN = 0x0001,
            RI_MOUSE_LEFT_BUTTON_UP = 0x0002,
            RI_MOUSE_MIDDLE_BUTTON_DOWN = 0x0010,
            RI_MOUSE_MIDDLE_BUTTON_UP = 0x0020,
            RI_MOUSE_RIGHT_BUTTON_DOWN = 0x0004,
            RI_MOUSE_RIGHT_BUTTON_UP = 0x0008,
            */
            RI_MOUSE_BUTTON_1_DOWN = 0x0001,
            RI_MOUSE_BUTTON_1_UP = 0x0002,
            RI_MOUSE_BUTTON_2_DOWN = 0x0004,
            RI_MOUSE_BUTTON_2_UP = 0x0008,
            RI_MOUSE_BUTTON_3_DOWN = 0x0010,
            RI_MOUSE_BUTTON_3_UP = 0x0020,
            RI_MOUSE_BUTTON_4_DOWN = 0x0040,
            RI_MOUSE_BUTTON_4_UP = 0x0080,
            RI_MOUSE_BUTTON_5_DOWN = 0x100,
            RI_MOUSE_BUTTON_5_UP = 0x0200,
            RI_MOUSE_WHEEL = 0x0400
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct RawMouseData
        {
            [FieldOffset(20)]
            public ButtonFlags buttonFlags;
            // +120 up, -120 down for regular mice
            // non "binary" for mouse pads
            [FieldOffset(22)]
            public short buttonData;
            [FieldOffset(28)]
            public int lastX;
            [FieldOffset(32)]
            public int lastY;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x00ff)
            {

                User32ApiWrapper.GetRawInputData((IntPtr)m.LParam, 0x10000003, (IntPtr)null, ref rawDataSize, Marshal.SizeOf(new User32ApiWrapper.RAWINPUTHEADER()));

                byte[] raw = new byte[rawDataSize];
                GCHandle handle = GCHandle.Alloc(raw, GCHandleType.Pinned);

                if (User32ApiWrapper.GetRawInputData((IntPtr)m.LParam, 0x10000003, handle.AddrOfPinnedObject(), ref rawDataSize, Marshal.SizeOf(new User32ApiWrapper.RAWINPUTHEADER())) != rawDataSize)
                {
                    Console.WriteLine("GetRawInputData (function) return size differs from rawDataSize (property)");
                }

                // TO-DO: Use RAWMOUSE struct to access data
                /*
                APIWrapper.RAWINPUT rawData = (APIWrapper.RAWINPUT)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(APIWrapper.RAWINPUT));
                */

                // Workaround: Access raw data with a custom structure, not the RAWMOUSE struct
                RawMouseData rawData = (RawMouseData)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(RawMouseData));

                // Logs all bytes of the raw data array
                /*
                log1 = "";
                int count = 1;

                foreach (byte b in raw)
                {
                    log1 += "Byte " + count + ": " + b.ToString() + " " + Environment.NewLine;
                    count++;
                }
                */

                /*
                rtb_console.Text = log1;
                textBox_lastX.Text = rawData.lastX.ToString();
                textBox_lastY.Text = rawData.lastY.ToString();
                textBox_mbutton.Text = rawData.buttonFlags.ToString();
                textBox_mwheel.Text = rawData.buttonData.ToString();
                */

                if (pipeConnected)
                {
                    rimTypeMouseCount++;
                    try
                    {
                        byte[] data;
                        data = Encoding.ASCII.GetBytes(rawData.lastX + " " + rawData.lastY + " " + rawData.buttonFlags + " " + rawData.buttonData);
                        rawMouseDataPipe.Write(data, 0, data.Length);
                        rawMouseDataPipe.Flush();
                        rawMouseDataPipe.WaitForPipeDrain();
                    }
                    catch (Exception ex)
                    {
                        if (!ex.Message.StartsWith("Pipe is borken."))
                        {
                            MessageBox.Show("Error: " + ex.Message);
                            textBox_pipeConnectionStatus.Text = "Connection lost, waiting for another connection";
                            // New thread needed?
                            pipeConnect();
                        }
                    }
                }

                handle.Free();
            }

            base.WndProc(ref m);
        }

        public void pipeConnect()
        {
            rawMouseDataPipe = new NamedPipeServerStream("RawMouseDataPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous, 1024, 1024);
            IAsyncResult pipeCall = rawMouseDataPipe.BeginWaitForConnection(null, null);

            while (!pipeCall.IsCompleted)
            {
                textBox_pipeConnectionStatus.Text = "Waiting for Connection";
            }

            rawMouseDataPipe.EndWaitForConnection(pipeCall);
            while (rawMouseDataPipe.IsConnected)
            {
                pipeConnected = true;
                textBox_pipeConnectionStatus.Text = "Is Connected";
            }
        }

        private void log(object obj)
        {
            rtb_console.Text = obj.ToString() + Environment.NewLine + Environment.NewLine + rtb_console.Text;
        }
    }
}
