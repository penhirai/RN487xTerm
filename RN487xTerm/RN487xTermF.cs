using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;

using FTD2XX_NET;

namespace RN487xTerm
{
    public partial class RN487xTermF : Form
    {
        protected UInt32 ftdiDeviceCount;
        protected FTDI.FT_STATUS ftStatus;

        // Create new instance of the FTDI device class
        protected FTDI myFtdiDevice;

        // Allocate storage for device info list
        protected FTDI.FT_DEVICE_INFO_NODE[] ftdiDeviceList;

        // FT243X 経由でデータ送受信用タイマー
        private System.Timers.Timer receiveFt243xTimer;
        private System.Timers.Timer transferFt243xTimer;


        private string transferText;
        private Int32 transferCountMax;
        private Int32 transferCount;
        private volatile Boolean transferEndFlag;


        public RN487xTermF()
        {
            InitializeComponent();

            // Form 初期化
            InitForm();

            // FT234X スキャンボタンを開放
            buttonScanFt234x.Enabled = true;

            // FTDI インスタンス生成
            myFtdiDevice = new FTDI();

            // コンストラクタでFT234X デバイススキャンを行う
            ftStatus = ScanFt234x();
            if(ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                changeFormScanFt234xOk();
            }

        }

        private FTDI.FT_STATUS ScanFt234x()
        {
            textBoxConsole.AppendText(Environment.NewLine);

            // Determine the number of FTDI devices connected to the machine
            ftStatus = myFtdiDevice.GetNumberOfDevices(ref ftdiDeviceCount);
            // Check status
            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                textBoxConsole.AppendText("Number of FTDI devices: " + ftdiDeviceCount.ToString());
                textBoxConsole.AppendText(Environment.NewLine);
            }
            else
            {
                // Wait for a key press
                textBoxConsole.AppendText("Failed to get number of devices (error " + ftStatus.ToString() + ")");
                textBoxConsole.AppendText(Environment.NewLine);
                return ftStatus;
            }

            // If no devices available, return
            if (ftdiDeviceCount == 0)
            {
                // Wait for a key press
                textBoxConsole.AppendText("Failed to get number of devices (error " + ftStatus.ToString() + ")");
                textBoxConsole.AppendText(Environment.NewLine);
                return ftStatus;
            }

            // Allocate storage for device info list
            ftdiDeviceList = new FTDI.FT_DEVICE_INFO_NODE[ftdiDeviceCount];

            // Populate our device list
            ftStatus = myFtdiDevice.GetDeviceList(ftdiDeviceList);

            // 再スキャンするかもしれないので，FT234X コンボボックスクリア
            comboBoxFt234x.Items.Clear();

            if (ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                for (UInt32 i = 0; i < ftdiDeviceCount; i++)
                {
                    textBoxConsole.AppendText("Device Index: " + i.ToString());
                    textBoxConsole.AppendText(Environment.NewLine);
                    textBoxConsole.AppendText("Flags: " + String.Format("{0:x}", ftdiDeviceList[i].Flags));
                    textBoxConsole.AppendText(Environment.NewLine);
                    textBoxConsole.AppendText("Type: " + ftdiDeviceList[i].Type.ToString());
                    textBoxConsole.AppendText(Environment.NewLine);
                    textBoxConsole.AppendText("ID: " + String.Format("{0:x}", ftdiDeviceList[i].ID));
                    textBoxConsole.AppendText(Environment.NewLine);
                    textBoxConsole.AppendText("Location ID: " + String.Format("{0:x}", ftdiDeviceList[i].LocId));
                    textBoxConsole.AppendText(Environment.NewLine);
                    textBoxConsole.AppendText("Serial Number: " + ftdiDeviceList[i].SerialNumber.ToString());
                    textBoxConsole.AppendText(Environment.NewLine);
                    textBoxConsole.AppendText("Description: " + ftdiDeviceList[i].Description.ToString());
                    textBoxConsole.AppendText(Environment.NewLine);

                    comboBoxFt234x.Items.Add("ID: " + ftdiDeviceList[i].ID + "  SN: " + ftdiDeviceList[i].SerialNumber.ToString());
                }
            }
            else
            {
                return ftStatus;
            }

            comboBoxFt234x.SelectedIndex = 0;

            return ftStatus;
        }


        private FTDI.FT_STATUS OpenFt234x()
        {
            // Open first device in our list by serial number
            ftStatus = myFtdiDevice.OpenBySerialNumber(ftdiDeviceList[comboBoxFt234x.SelectedIndex].SerialNumber);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                textBoxConsole.AppendText("Failed to open device (error " + ftStatus.ToString() + ")");
                textBoxConsole.AppendText(Environment.NewLine);
                return ftStatus;
            }

            // Set up device data parameters
            // Set Baud rate to 115200
            ftStatus = myFtdiDevice.SetBaudRate(115200);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                textBoxConsole.AppendText("Failed to set Baud rate (error " + ftStatus.ToString() + ")");
                textBoxConsole.AppendText(Environment.NewLine);
                return ftStatus;
            }

            // Set data characteristics - Data bits, Stop bits, Parity
            ftStatus = myFtdiDevice.SetDataCharacteristics(FTDI.FT_DATA_BITS.FT_BITS_8, FTDI.FT_STOP_BITS.FT_STOP_BITS_1, FTDI.FT_PARITY.FT_PARITY_NONE);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                textBoxConsole.AppendText("Failed to set data characteristics (error " + ftStatus.ToString() + ")");
                textBoxConsole.AppendText(Environment.NewLine);
                return ftStatus;
            }

            // Set read timeout to 1 seconds, write timeout to infinite
            ftStatus = myFtdiDevice.SetTimeouts(1000, 0);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                textBoxConsole.AppendText("Failed to set timeouts (error " + ftStatus.ToString() + ")");
                textBoxConsole.AppendText(Environment.NewLine);
                return ftStatus;
            }

            // FT234X への読み込みタイマースタート
            receiveFt243xTimer = new System.Timers.Timer();
            receiveFt243xTimer.Enabled = true;
            receiveFt243xTimer.AutoReset = true;
            receiveFt243xTimer.Interval = 200;
            receiveFt243xTimer.Elapsed += new ElapsedEventHandler(OnReceiveFt243xTimerEvent);

            StartReceiveFt243xTimer();

            // FT234X への書き込みタイマーインスタンス生成
            transferFt243xTimer = new System.Timers.Timer();
            transferFt243xTimer.Enabled = true;
            transferFt243xTimer.AutoReset = true;
            transferFt243xTimer.Interval = 200;
            transferFt243xTimer.Elapsed += new ElapsedEventHandler(OnTransferFt243xTimerEvent);

            StopTransferFt243xTimer();

            return ftStatus;
        }

        private FTDI.FT_STATUS CloseFt234x()
        {
            StopReceiveFt243xTimer();

            // Close our device
            ftStatus = myFtdiDevice.Close();

            return ftStatus;
        }

        private void ParseBleName()
        {
            StopReceiveFt243xTimer();

            string temp;
            Int32 index = textBoxConsole.Lines.Length;
            Int32 indexOfCount;
            bool scanTextFlag = false;
            do
            {
                --index;
                temp = textBoxConsole.Lines[index];
                indexOfCount = temp.IndexOf("Scanning");
                if (indexOfCount > 0)
                {
                    scanTextFlag = true;
                }
            } while (scanTextFlag == false);

            // 追加するため，クリア
            comboBoxBle.Items.Clear();

            scanTextFlag = false;
            Int32 stringIndexEnd;
            string bleInfo;
            Int32 transferParse;
            do
            {
                ++index;
                if (index >= textBoxConsole.Lines.Length)
                {
                    scanTextFlag = true;
                    continue;
                }
                temp = textBoxConsole.Lines[index];
                transferParse = temp.IndexOf("T:");
                if ((temp == "") || (transferParse >= 0))
                {
                    continue;
                }
                stringIndexEnd = temp.IndexOf("%", 1);
                bleInfo = temp.Substring(1, stringIndexEnd - 1);
                indexOfCount = bleInfo.IndexOf("BLE_");
                if (indexOfCount > 0)
                {
                    comboBoxBle.Items.Add(bleInfo);
                }
                indexOfCount = bleInfo.IndexOf("BLE-");
                if (indexOfCount > 0)
                {
                    comboBoxBle.Items.Add(bleInfo);
                }
            } while (scanTextFlag == false);

            comboBoxBle.SelectedIndex = 0;

            StartReceiveFt243xTimer();
        }


        public void StartReceiveFt243xTimer()
        {
            receiveFt243xTimer.Start();
        }

        public void StopReceiveFt243xTimer()
        {
            receiveFt243xTimer.Stop();
        }

        private void OnReceiveFt243xTimerEvent(object source, ElapsedEventArgs e)
        {
            UInt32 numBytesAvailable = 0;

            ftStatus = myFtdiDevice.GetRxBytesAvailable(ref numBytesAvailable);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Invoke(new MethodInvoker(() => textBoxConsole.AppendText("Failed to get number of bytes available to read (error " + ftStatus.ToString() + ")\r")));
                return;
            }

            if (numBytesAvailable != 0)
            {
                // Now that we have the amount of data we want available, read it
                string readData;
                UInt32 numBytesRead = 0;
                // Note that the Read method is overloaded, so can read string or byte array data
                ftStatus = myFtdiDevice.Read(out readData, numBytesAvailable, ref numBytesRead);
                if (ftStatus != FTDI.FT_STATUS.FT_OK)
                {
                    // Wait for a key press
                    Invoke(new MethodInvoker(() => textBoxConsole.AppendText("Failed to read data (error " + ftStatus.ToString() + ")\r")));
                    return;
                }
                Invoke(new MethodInvoker(() => textBoxConsole.AppendText(readData)));
            }
        }

        public void StartTransferFt243xTimer()
        {
            transferEndFlag = false;
            transferFt243xTimer.Start();
        }

        public void StopTransferFt243xTimer()
        {
            transferFt243xTimer.Stop();
            transferEndFlag = true;
        }

        private void OnTransferFt243xTimerEvent(object source, ElapsedEventArgs e)
        {
            // Perform loop back - make sure loop back connector is fitted to the device
            // Write string data to the device
            UInt32 numBytesWritten = 0;
            string st = transferText[transferCount].ToString();

            ++transferCount;
            if (transferCount >= transferCountMax)
            {
                transferCount = transferCountMax - 1;
                StopTransferFt243xTimer();
            }

            // Note that the Write method is overloaded, so can write string or byte array data
            ftStatus = myFtdiDevice.Write(st, st.Length, ref numBytesWritten);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                Invoke(new MethodInvoker(() => textBoxConsole.AppendText("Failed to write to device (error " + ftStatus.ToString() + ")\r")));
                return;
            }
        }


        private void InitForm()
        {
            // comboBox 初期化
            comboBoxFt234x.Enabled = false;
            comboBoxBle.Enabled = false;

            // button 初期化
            buttonScanFt234x.Enabled = false;
            buttonOpenFt234x.Enabled = false;
            buttonCloseFt234x.Enabled = false;
            buttonCmdMode.Enabled = false;
            buttonGattMode.Enabled = false;
            buttonScanBle.Enabled = false;
            buttonStopScanBle.Enabled = false;
            buttonConnectBle.Enabled = false;
            buttonDisconnectBle.Enabled = false;

            // FTDI 関係初期化
            ftdiDeviceCount = 0;
            ftStatus = FTDI.FT_STATUS.FT_OK;
        }


        private void changeFormScanFt234xOk()
        {
            comboBoxFt234x.Enabled = true;
            buttonOpenFt234x.Enabled = true;
        }

        private void changeFormOpenFt234xOk()
        {
            buttonCmdMode.Enabled = true;
            buttonGattMode.Enabled = true;
            buttonCloseFt234x.Enabled = true;
            buttonScanBle.Enabled = true;

            comboBoxFt234x.Enabled = false;
            buttonOpenFt234x.Enabled = false;
            buttonScanFt234x.Enabled = false;
        }

        private void changeFormScanBle()
        {
            buttonStopScanBle.Enabled = true;
        }

        private void changeFormStopScanBle()
        {
            comboBoxBle.Enabled = true;
            buttonConnectBle.Enabled = true;

            buttonStopScanBle.Enabled = false;
        }

        private void changeFormConnectBle()
        {
            buttonDisconnectBle.Enabled = true;

            comboBoxBle.Enabled = false;
            buttonConnectBle.Enabled = false;
        }

        private void changeFormDisconnectBle()
        {
            buttonConnectBle.Enabled = true;
            comboBoxBle.Enabled = true;

            buttonDisconnectBle.Enabled = false;
        }


        private void buttonScanFt234x_Click(object sender, EventArgs e)
        {
            ftStatus = ScanFt234x();
            if(ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                changeFormScanFt234xOk();
            }
        }

        private void buttonOpenFt234x_Click(object sender, EventArgs e)
        {
            ftStatus = OpenFt234x();

            if(ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                changeFormOpenFt234xOk();
            }
        }

        private void buttonCloseFt234x_Click(object sender, EventArgs e)
        {
            ftStatus = CloseFt234x();

            if(ftStatus == FTDI.FT_STATUS.FT_OK)
            {
                InitForm();

                // Scan FT234X のみ可能
                buttonScanFt234x.Enabled = true;
            }
        }

        private void textBoxConsole_KeyPress(object sender, KeyPressEventArgs e)
        {
            string keyText = e.KeyChar.ToString();

            // Write string data to the device
            UInt32 numBytesWritten = 0;
            // Note that the Write method is overloaded, so can write string or byte array data
            ftStatus = myFtdiDevice.Write(keyText, keyText.Length, ref numBytesWritten);
            if (ftStatus != FTDI.FT_STATUS.FT_OK)
            {
                // Wait for a key press
                textBoxConsole.AppendText("Failed to write to device (error " + ftStatus.ToString() + ")");
                textBoxConsole.AppendText(Environment.NewLine);
                return;
            }
        }

        private void buttonCmdMode_Click(object sender, EventArgs e)
        {
            setTransferString("$$$");
        }

        private void buttonGattMode_Click(object sender, EventArgs e)
        {
            setTransferString("---\r");
        }

        private void buttonScanBle_Click(object sender, EventArgs e)
        {
            setTransferString("$$$");

            setTransferString("f\r");

            changeFormScanBle();
        }

        private void setTransferString(string transferString)
        {
            transferText = transferString;
            textBoxConsole.AppendText("T:" + transferText + Environment.NewLine);
            transferCountMax = transferString.Length;
            transferCount = 0;
            StartTransferFt243xTimer();

            do
            {
                // wait
            } while (transferEndFlag == false);
        }

        private void buttonStopScanBle_Click(object sender, EventArgs e)
        {
            setTransferString("x\r");

            ParseBleName();

            changeFormStopScanBle();
        }

        private void buttonConnectBle_Click(object sender, EventArgs e)
        {
            string temp = comboBoxBle.SelectedItem.ToString();
            Int32 indexEnd = temp.IndexOf(",");

            setTransferString("c,0," + temp.Substring(0, indexEnd) + "\r");

            changeFormConnectBle();
        }

        private void buttonDisconnectBle_Click(object sender, EventArgs e)
        {
            textBoxConsole.AppendText(Environment.NewLine);

            setTransferString("$$$");

            setTransferString("k,1\r");

            setTransferString("---\r");

            changeFormDisconnectBle();
        }

        private void buttonSaveFile_Click(object sender, EventArgs e)
        {
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            //はじめに「ファイル名」で表示される文字列を指定する
            sfd.FileName = System.DateTime.Now.ToString().Replace("/", "").Replace(" ", "_").Replace(":", "") + ".txt";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            //指定しない（空の文字列）の時は、現在のディレクトリが表示される
            sfd.Filter = "txtファイル(*.txt)|*.txt|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに選択されるものを指定する
            //1番目が選択されているようにする
            sfd.FilterIndex = 1;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            //デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき、
                //選択された名前で新しいファイルを作成し、
                //読み書きアクセス許可でそのファイルを開く。
                //既存のファイルが選択されたときはデータが消える恐れあり。
                System.IO.Stream stream;
                stream = sfd.OpenFile();
                if (stream != null)
                {
                    //ファイルに書き込む
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);
                    sw.Write(textBoxConsole.Text);
                    //閉じる
                    sw.Close();
                    stream.Close();
                }
            }
        }
    }
}
