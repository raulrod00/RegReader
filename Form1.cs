using System;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace RegReader
{
    public partial class Form1 : Form
    {
        // Mechanical Contraints (Can be changed, but do so at your own peril)
        public uint accessDistanceUnit = 0;
        public double leadDistanceUnit = 5;
        public double oneTurnPulseNum = 10000;

        // Control protocol packet
        private const ushort confirmCode = 0x55aa;
        private const ushort passCode = 0;
        private ushort functionCode = 0x1301;
        private ushort objectChannel = 1;
        private const ushort whoAccept = 0xFFFF;
        private const ushort whoReply = 0xFFFF;
        //private const ushort UDPverification = 0;

        // You can change the Time difference through the library!
        public uint absTime = 10; //millisecond
        public double velLimit = 1.3; //millimeters
        // For a 10 ms command, the limit is ~1.3 mm


        private uint SFXOut = 0x1234;
        private uint analogOut = 0x5678abcd;

        // udp
        private IPEndPoint mboxIPE = null;
        private IPEndPoint recvIPE = null;
        private UdpClient receiveClient = null;
        private UdpClient sendClient = null;
        private Socket socket = null;
        //private EndPoint Remote = null;

        // Variables for logging
        StreamWriter logger; // Creates a logger :)
        public string userName = "raulr"; // Can change user outside of library

        // Variables for received bytes
        private byte[] recBuffer;
        StreamWriter receivedLogger; // Get the return values

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Establishes the UDP connection between the comp and the Platform. Can take a string to rename the log
        /// </summary>
        /// <param name="filename">The Name of the Log File, include csv</param>
        public void Connect(string filename = "UDPLog.csv")
        {
            mboxIPE = new IPEndPoint(IPAddress.Parse("192.168.15.255"), 7408); // Where it's going (the .255 indicates it's a broadcast!)
            recvIPE = new IPEndPoint(IPAddress.Parse("192.168.15.100"), 8410); // Who we are (.100)

            ThreadPool.QueueUserWorkItem(delegate
            {

                receivedLogger = new StreamWriter("C:\\Users\\" + userName + "\\Documents\\receivedvalues.csv");
                receiveClient = new UdpClient();
                receiveClient.ExclusiveAddressUse = false;
                receiveClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                receiveClient.Client.Bind(recvIPE);
                recBuffer = receiveClient.Receive(ref recvIPE);

                string receivedString = BitConverter.ToString(recBuffer);

                receivedLogger.WriteLine(receivedString);
                receivedLogger.Flush();

                Form.ActiveForm.Invoke(new Action(() => replyBox.Text = receivedString));
            });
            Thread.Sleep(5);

            sendClient = new UdpClient();
            sendClient.ExclusiveAddressUse = false;
            sendClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            sendClient.Client.Bind(recvIPE);

            //socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            //Remote = (EndPoint)(recvIPE);
            logger = new StreamWriter("C:\\Users\\" + userName + "\\Documents\\" + filename);

            logger.Write(DateTime.Now.ToString("hh:mm:ss.fff"));
            logger.WriteLine(",HEX String");
            logger.Flush();

            stats.Text = "Connected";

        }

        /// <summary>
        /// Take a position, verifies that it does not exceed velocity or position limitations.
        /// Creates the HEX array, and sends it to the Platorm, and Logs the command
        /// </summary>
        // Need to track the frame number
        private int frameNum = 0;

        // The previous Positions, to check velocity
        public double pX = 0;
        public double pY = 0;
        public double pZ = 0;
        public double pU = 0;
        public double pV = 0;
        public double pW = 0;

        public double tester = 0;
        public double tester2 = 0;
        public double tester3 = 0;
        public double tester4 = 0;

        // Track whether there were any errors
        bool errTracker = false;

        public void sCommand()
        {
            StringBuilder str;
            // Builds the HEX String
            str = strBldr();

            // Lowers the Case of the String
            string sendStr = str.ToString().ToLower();

            // Form the BYTE ARRAY that is "actually" sent
            byte[] sendBuf = HexStringToByteArray(sendStr);
            //stats.Text = sendStr;

            /* UNCOMMENT WHEN READY TO SEND!*/
            ThreadPool.QueueUserWorkItem(delegate
            {
                recBuffer = receiveClient.Receive(ref recvIPE);

                receivedLogger.WriteLine(BitConverter.ToString(recBuffer));
                receivedLogger.Flush();

                string receivedString = BitConverter.ToString(recBuffer);

                receivedLogger.WriteLine(receivedString);
                receivedLogger.Flush();

                Form.ActiveForm.Invoke(new Action(() => replyBox.Text = replyBox.Text + "\r\n" + receivedString));

            });
            Thread.Sleep(5);

            // Send it!
            sendClient.Send(sendBuf, sendBuf.Length, mboxIPE);

            // Send the command to the log
            logger.Write(DateTime.Now.ToString("hh:mm:ss.fff"));

            logger.WriteLine("," + sendStr);
            logger.Flush();
            /**/


        }

        public bool SendCommand(double xPos, double yPos, double zPos, double uPos, double vPos, double wPos)
        {

            errTracker = /*(Math.Abs(xPos - pX) > velLimit) || // Verifies that velocity is within limits
                         (Math.Abs(yPos - pY) > velLimit) || // Verifies that velocity is within limits
                         (Math.Abs(zPos - pZ) > velLimit) || // Verifies that velocity is within limits
                         (Math.Abs(uPos - pU) > velLimit) || // Verifies that velocity is within limits
                         (Math.Abs(vPos - pV) > velLimit) || // Verifies that velocity is within limits
                         (Math.Abs(wPos - pW) > velLimit) || // Verifies that velocity is within limits*/
                         (xPos >= 450) || // Verifies that max arm length is not exceeded
                         (yPos >= 450) || // Verifies that max arm length is not exceeded
                         (zPos >= 450) || // Verifies that max arm length is not exceeded
                         (uPos >= 450) || // Verifies that max arm length is not exceeded
                         (vPos >= 450) || // Verifies that max arm length is not exceeded
                         (wPos >= 450);   // Verifies that max arm length is not exceeded




            if (errTracker)
            {
                //Disconnect();
                return errTracker;
            }
            else
            {
                tester += 1;
                StringBuilder str;
                // Builds the HEX String
                str = strBldr(frameNum, xPos, yPos, zPos,
                                        uPos, vPos, wPos);
                tester2 += 1;

                // Lowers the Case of the String
                string sendStr = str.ToString().ToLower();


                // Form the BYTE ARRAY that is "actually" sent
                byte[] sendBuf = HexStringToByteArray(sendStr);

                ThreadPool.QueueUserWorkItem(delegate
                {
                    recBuffer = receiveClient.Receive(ref recvIPE);

                    receivedLogger.WriteLine(BitConverter.ToString(recBuffer));
                    receivedLogger.Flush();
                });
                Thread.Sleep(5);

                // Send it!
                sendClient.Send(sendBuf, sendBuf.Length, mboxIPE);

                //socket.SendTo(sendBuf, mboxIPE);


                // Increase _GLOBAL frame counter
                frameNum += 1;

                // Update the previous recorded position
                pX = xPos;
                pY = yPos;
                pZ = zPos;
                pU = uPos;
                pV = vPos;
                pW = wPos;



                // Send the command to the log
                logger.Write(DateTime.Now.ToString("hh:mm:ss.fff"));

                logger.WriteLine("," + xPos.ToString("n3") + "," + yPos.ToString("n3") +
                                 "," + zPos.ToString("n3") + "," + uPos.ToString("n3") +
                                 "," + vPos.ToString("n3") + "," + wPos.ToString("n3") + "," + sendStr);
                logger.Flush();


                return errTracker;
            }
        }

        /// <summary>
        /// Disconnect and Release the Socket
        /// </summary>
        public void Disconnect()
        {
            sendClient.Close();
            //socket.Disconnect(true);
            //socket.Shutdown(SocketShutdown.Both);
            stats.Text = "Disconnected";
        }

        private bool conDiscon = true;
        private void bConnect_Click(object sender, EventArgs e)
        {
            if (conDiscon)
            {
                /*WHEN READY -- Connect();*/
                Connect();
                conDiscon = false;
                bConnect.Text = "DisConn";
                rwDrop.Enabled = true;
                rwDrop.SelectedIndex = 0;
                regDrop.Enabled = true;
                regDrop.SelectedIndex = 0;
                sAddress.Enabled = true;
                numParam.Enabled = true;
                sCmd.Enabled = true;
            } else
            {
                /* WHEN READY -- Disconnect();*/
                Disconnect();
                bConnect.Text = "Connect";
                sCmd.Enabled = false;
            }
        }

        ///////////////////////////////////////////////////////////////////////////////
        /// HOME OF UTILITY FUNCTIONS
        ///////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert a HEX String into a BYTE array to be sent
        /// </summary>
        private byte[] HexStringToByteArray(string s)
        {
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
            {
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            }
            return buffer;
        }


        /// <summary>
        /// Creates the packet that is sent
        /// </summary>
        /// <param name="numFrame">Current Frame Number, should be continuous</param>
        /// <param name="_xPos">Position of the X Motor</param>
        /// <param name="_yPos">Position of the Y Motor</param>
        /// <param name="_zPos">Position of the Z Motor</param>
        /// <param name="_uPos">Position of the U Motor</param>
        /// <param name="_vPos">Position of the V Motor</param>
        /// <param name="_wPos">Position of the W Motor</param>
        /// <returns></returns>
        public StringBuilder strBldr()
        {
            StringBuilder str = new StringBuilder();

            //leadDistanceUnit = 5; // Convert.ToUInt16(RotLength.Text);
            //oneTurnPulseNum = 10000; // Convert.ToUInt16(RotPulses.Text);
            var writeBool = false;
            var fnctn = rwDrop.SelectedItem.ToString();
            var objC = regDrop.SelectedItem.ToString();
            if (fnctn == "Read")
            {
                functionCode = 0x1101;
                switch (objC)
                {
                    case "DN":
                        objectChannel = 0;
                        break;
                    case "FN":
                        objectChannel = 1;
                        break;
                    default:
                        break;
                }
            } else
            {
                writeBool = true;
                functionCode = 0x1201;
                switch (objC)
                {
                    case "FNm":
                        objectChannel = 0;
                        break;
                    case "FN":
                        objectChannel = 1;
                        break;
                    case "CN":
                        objectChannel = 2;
                        break;
                    default:
                        break;
                }
            }

            str.Append(confirmCode.ToString("X4"));
            str.Append(passCode.ToString("X4"));
            str.Append(functionCode.ToString("X4"));
            str.Append(objectChannel.ToString("X4"));
            str.Append(whoAccept.ToString("X4"));
            str.Append(whoReply.ToString("X4"));

            stats.Text = ((int)sAddress.Value).ToString("X4");
            str.Append(((int)sAddress.Value).ToString("X4"));
            str.Append(((int)numParam.Value).ToString("X4"));

            if (writeBool)
            {
                for (int i = 0; i < paramCounter; i++)
                {
                    str.Append(((int)paramValue[i].Value).ToString("X4"));
                }
            }

            return str;

        }

        /// <summary>
        /// Creates the packet that is sent
        /// </summary>
        /// <param name="numFrame">Current Frame Number, should be continuous</param>
        /// <param name="_xPos">Position of the X Motor</param>
        /// <param name="_yPos">Position of the Y Motor</param>
        /// <param name="_zPos">Position of the Z Motor</param>
        /// <param name="_uPos">Position of the U Motor</param>
        /// <param name="_vPos">Position of the V Motor</param>
        /// <param name="_wPos">Position of the W Motor</param>
        /// <returns></returns>
        public StringBuilder strBldr(int numFrame, double _xPos, double _yPos, double _zPos, double _uPos, double _vPos, double _wPos)
        {
            StringBuilder str = new StringBuilder();

            //leadDistanceUnit = 5; // Convert.ToUInt16(RotLength.Text);
            //oneTurnPulseNum = 10000; // Convert.ToUInt16(RotPulses.Text);

            uint xPos;
            uint yPos;
            uint zPos;
            uint uPos;
            uint vPos;
            uint wPos;


            xPos = Convert.ToUInt32((_xPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            yPos = Convert.ToUInt32((_yPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            zPos = Convert.ToUInt32((_zPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            uPos = Convert.ToUInt32((_uPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            vPos = Convert.ToUInt32((_vPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;
            wPos = Convert.ToUInt32((_wPos + 5) / leadDistanceUnit * oneTurnPulseNum); // 255;

            tester3 += 1;

            str.Append(confirmCode.ToString("X4"));
            str.Append(passCode.ToString("X4"));
            str.Append(functionCode.ToString("X4"));
            str.Append(objectChannel.ToString("X4"));
            str.Append(whoAccept.ToString("X4"));
            str.Append(whoReply.ToString("X4"));
            str.Append(numFrame.ToString("X8"));
            str.Append(absTime.ToString("X8"));
            str.Append(xPos.ToString("X8"));
            str.Append(yPos.ToString("X8"));
            str.Append(zPos.ToString("X8"));
            str.Append(uPos.ToString("X8"));
            str.Append(vPos.ToString("X8"));
            str.Append(wPos.ToString("X8"));

            str.Append(SFXOut.ToString("X4"));
            str.Append(analogOut.ToString("X8"));
            tester4 += 1;

            return str;

        }

        public void wait(int milliseconds)
        {
            var timer1 = new System.Windows.Forms.Timer();
            if (milliseconds == 0 || milliseconds < 0) return;

            // Console.WriteLine("start wait timer");
            timer1.Interval = milliseconds;
            timer1.Enabled = true;
            timer1.Start();

            timer1.Tick += (s, e) =>
            {
                timer1.Enabled = false;
                timer1.Stop();
                // Console.WriteLine("stop wait timer");
            };

            while (timer1.Enabled)
            {
                Application.DoEvents();
            }
        }


        ///////////////////////////////////////////////////////////////////////////////
        /// HOME OF FORM FUNCTIONS
        ///////////////////////////////////////////////////////////////////////////////

        private void rwDrop_ValueChanged(object sender, EventArgs e)
        {
            if(rwDrop.SelectedIndex == 0)
            {
                regDrop.Items.Clear();
                regDrop.Items.AddRange(new object[] {
                "DN",
                "FN"});
                regDrop.SelectedIndex = 0;
                createParam.Enabled = false;
                createParam.Hide();
                clearParams();
            } else
            {
                regDrop.Items.Clear();
                regDrop.Items.AddRange(new object[] {
                "FNm",
                "FN",
                "CN"});
                regDrop.SelectedIndex = 0;
                createParam.Enabled = true;
                createParam.Show();
            }
        }
        private void resetParams()
        {
            if (rwDrop.SelectedIndex == 0)
            {
                regDrop.Items.Clear();
                regDrop.Items.AddRange(new object[] {
                "DN",
                "FN"});
                regDrop.SelectedIndex = 0;
                createParam.Enabled = false;
                createParam.Hide();
                clearParams();
            }
            else
            {
                regDrop.Items.Clear();
                regDrop.Items.AddRange(new object[] {
                "FNm",
                "FN",
                "CN"});
                regDrop.SelectedIndex = 0;
                createParam.Enabled = true;
                createParam.Show();
            }
        }

        private List<NumericUpDown> paramValue = new List<NumericUpDown>();
        private int paramCounter = 0;
        private int locx = 22;
        private int locy = 220;
        private void makeNumUpDown()
        {
            paramValue.Add(new NumericUpDown());
            paramValue[paramCounter].Value = 0;
            paramValue[paramCounter].Minimum = 0;
            paramValue[paramCounter].Maximum = 65535;
            paramValue[paramCounter].Hexadecimal = true;
            paramValue[paramCounter].Size = new System.Drawing.Size(60, 22);
            paramValue[paramCounter].Location = new System.Drawing.Point(locx, locy);
            locx += 70;
            if (locx > 550)
            {
                locx = 22;
                locy += 30;
            }
            
            Controls.Add(paramValue[paramCounter]);
            paramCounter += 1;
        }

        private void clearParams()
        {
            for (int i = 0; i < paramValue.Count; i++)
            {
                Controls.Remove(paramValue[i]);
            }
            paramValue.Clear();
            paramCounter = 0;
            locx = 22;
            locy = 220;
        }

        private void createParam_Click(object sender, EventArgs e)
        {
            clearParams();

            var numlists = numParam.Value;

            for (int i = 0; i < numlists; i++)
            {
                makeNumUpDown();

            }
        }

        private void sCmd_Click(object sender, EventArgs e)
        {
            sCmd.BackColor = System.Drawing.Color.SkyBlue;
            sCommand();
            wait(200);
            sCmd.BackColor = System.Drawing.Color.Lime;
            clearParams();
            resetParams();
        }


    }
}
