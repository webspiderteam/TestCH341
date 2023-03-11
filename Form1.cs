using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using MPS;
using System.Runtime.InteropServices;
using System.Reflection.Emit;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace TestI2C
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mmtimer = new TMultimediaTimer(null);
            mmtimer.Interval = 100;
            mmtimer.OnTimer = mmtimerTick;
            
        }

        TMultimediaTimer mmtimer;

        [DllImport("ch341dll.DLL",CallingConvention=CallingConvention.Winapi)]
        public extern static long CH341OpenDevice(int i);

        [DllImport("ch341dll.DLL",CallingConvention=CallingConvention.Winapi)]
        public extern static void CH341CloseDevice(int iIndex);

        // CH341WriteI2C( ULONG iIndex, UCHAR iDevice, UCHAR iAddr, UCHAR iByte );
        [DllImport("ch341dll.DLL", CallingConvention = CallingConvention.Winapi)]
        public extern static bool CH341WriteI2C(int iIndex,byte i1,byte i2, byte i3);
        // Calling convention: Cdecl geht nicht, PInvokeStackImbalance- Fehler
        //.FastCall :Ungültige nicht verwaltete Aufrufkonvention ("stdcall", "cdecl" oder "thiscall" ist erforderlich). 
        // .stdCall : OK
        // .ThisCall :  PInvokeStackImbalance- Fehler
        // .WinApi : OK

        // CH341StreamI2C(ChipIndex.Value,wlen,@outbytes,rlen,@inbytes); 
        [DllImport("ch341dll.DLL", CallingConvention = CallingConvention.StdCall)]
        public extern static bool CH341StreamI2C(int iIndex, int wlen, ref byte WBuf, int rlen, ref byte RBuf);


        // CH341SetStream() konfiguriert I2C und SPI
        // Bit 1-0: I2C speed 00= low speed /20KHz
        // 01= standard /100KHz
        // 10= fast /400KHz
        // 11= high speed /750KHz           plus  0x60  , das ist wichtig. Also :
        // BOOL	WINAPI	CH341SetStream( ULONG iIndex, ULONG	iMode );*/
 
        [DllImport("ch341dll.DLL", CallingConvention = CallingConvention.Winapi)]
        public extern static long CH341SetStream(int iIndex, int iMode);

        byte[] WriteBuf = new byte[10];
        byte[] ReadBuf = new byte[10];
        DataTable tablo = new DataTable();

        private Dictionary<string, byte[]> ScanAllAdresses()
        {
            Dictionary<string, byte[]> result = new Dictionary<string, byte[]>();
            int bufLength = 256;
            dataGridView1.Rows.Clear();
            for (int i = 0; i < 128; i++)
            {

                WriteBuf[0] = (Convert.ToByte(i << 1));
                WriteBuf[1] = Convert.ToByte("00", 16);

                byte[] buf = new byte[bufLength];
                CH341StreamI2C(0, 2, ref WriteBuf[0], bufLength, ref buf[0]);
                result.Add(i.ToString("X2"), buf);
            }
            return result;
        }

        private byte[] ReadOneRegister(string devAdress, string regAdress, int readLen)
        {
            int bufLength = readLen;
            byte[] buf = new byte[bufLength];
            int addresbyte = Convert.ToInt32(devAdress, 16) << 1;
            WriteBuf[0] = (Convert.ToByte(addresbyte));
            WriteBuf[1] = Convert.ToByte(regAdress, 16);
            CH341StreamI2C(0, 2, ref WriteBuf[0], bufLength, ref buf[0]);
            return buf; 
        }

        private Dictionary<string, byte[]> ReadAllRegister(string devAdress)
        {
            Dictionary<string, byte[]> result = new Dictionary<string, byte[]>();
            int bufLength = 1;
            int addresbyte = Convert.ToInt32(devAdress, 16) << 1;
            WriteBuf[0] = (Convert.ToByte(addresbyte ));
            dataGridView1.Rows.Clear();
            for (int i = 0; i < 256; i++)
            {
                WriteBuf[1] = Convert.ToByte(i);

                string regDefinition = "";
                if (deviceInfo != null)
                {
                    XElement registersList = deviceInfo.Element("Registers");
                    var nameByValue = registersList.Descendants("Register")
                                                .Where(elem => elem.Element("RegAdress").Value.Equals(i.ToString("X2")));
                    if (nameByValue.Count() > 0)
                    {
                        regDefinition = nameByValue.FirstOrDefault().Element("Definition").Value;
                        bufLength = Convert.ToInt16(nameByValue.FirstOrDefault().Element("Length").Value);
                    }
                        
                }
                byte[] buf = new byte[bufLength];
                CH341StreamI2C(0, 2, ref WriteBuf[0], bufLength, ref buf[0]);
                string[] row = new string[] { i.ToString("X2"), regDefinition ,ByteArrayToString(buf) };
                dataGridView1.Rows.Add(row);
                result.Add(i.ToString("X2"), buf);
                bufLength = 1;
            }
            return result;
        }

        private void writeRegister(string devAdress, string regAdress, string outData)
        {
            int bufLength = 0;
            string sData = outData;
            byte[] buf = new byte[1];
            int addresbyte = Convert.ToInt32(devAdress, 16) << 1;
            WriteBuf[0] = (Convert.ToByte(addresbyte + 1));
            WriteBuf[1] = Convert.ToByte(regAdress, 16);
            sData= sData.Trim().Replace(" ","");
            for (int i = 0; i<sData.Length; i=i+2)
            {
                WriteBuf[(i/2)+2] = Convert.ToByte(sData.Substring(i,2), 16);
                bufLength++;
            }
            CH341StreamI2C(0, bufLength + 2, ref WriteBuf[0], bufLength, ref buf[0]);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:X2} ", b);
            return hex.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < 256; i++)
            {
                cmbReg.Items.Add(i.ToString("X2"));
                if (i < 128)
                    cmbDeviceAdr.Items.Add(i.ToString("X2"));
            }
            cmbDeviceAdr.SelectedIndex = 0;
            cmbReg.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            cmbMode.SelectedIndex = 0;
            cmbDelay.SelectedIndex = 0;
 
        }

        private void Label_MouseEnter(object sender, EventArgs e)
        {
            var labelx = (System.Windows.Forms.Label)sender;
            labelx.Font = new Font(labelx.Font.FontFamily, labelx.Font.Size, FontStyle.Bold);
        }

        private void Label_MouseLeave(object sender, EventArgs e)
        {
            System.Windows.Forms.Label labelx = (System.Windows.Forms.Label)sender;
            labelx.Font = new Font(labelx.Font.FontFamily, labelx.Font.Size, FontStyle.Regular);
        }

        private void cmbMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbDelay.Visible = cmbMode.SelectedIndex == 1;
            lblDelay.Visible = cmbMode.SelectedIndex == 1;
        }
        
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            long open = CH341OpenDevice(0);
            if (open != 4294967295)   // if chip not connected open is Hex 00000000FFFFFFFF
            {
                label2.Text = "Chip is open with adress " + open.ToString("X16");
                buttonClose.Enabled = true;
                buttonOpen.Enabled = false;
                btnScanAdresses.Enabled = true;
                groupBox3.Enabled = true;
                groupBox2.Enabled = true;
            }
            else label2.Text = "No Chip could be opened";


        }

         private void butStart1secTimer_Click(object sender, EventArgs e)
        {
            if (timer2.Enabled)
                timer2.Enabled = false;
            else
            {
                butInitTempADC_Click(null, null);
                timer2.Enabled = true;
            }
        }

 
         private void buttonClose_Click(object sender, EventArgs e)
        {
            label2.Text = " No Chip open";
            buttonClose.Enabled = false;
            buttonOpen.Enabled = true;
            btnScanAdresses.Enabled = false;
            groupBox3.Enabled = false;
            groupBox2.Enabled = false;
            mmtimer.Disable();
            CH341CloseDevice(0);

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            txtData.Clear();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            byte[] LED = new byte[10];
            LED[0] = Convert.ToByte( "3F",16); LED[1] = Convert.ToByte("06",16); LED[2] =Convert.ToByte("5B",16); 
            LED[3] = Convert.ToByte("4F",16); LED[4] =Convert.ToByte("66",16);
            LED[5] = Convert.ToByte("6D",16); LED[6] = Convert.ToByte("7D",16); LED[7] = Convert.ToByte("07",16); 
            LED[8] = Convert.ToByte("7F",16); LED[9] = Convert.ToByte("6F",16);
            byte[] LEDp = new byte[10];
            LEDp[0] = Convert.ToByte("BF",16); LEDp[1] = Convert.ToByte("86",16); LEDp[2] = Convert.ToByte("DB",16); 
            LEDp[3] = Convert.ToByte("CF",16); LEDp[4] = Convert.ToByte("E6",16);
            LEDp[5] = Convert.ToByte("ED", 16); LEDp[6] = Convert.ToByte("FD", 16); LEDp[7] = Convert.ToByte("87", 16); 
            LEDp[8] = Convert.ToByte("FF",16); LEDp[9] = Convert.ToByte("EF",16);
 

            // read Temp. - Sensor
            WriteBuf[0] = Convert.ToByte("91", 16);
            WriteBuf[1] = Convert.ToByte("02", 16);
            CH341StreamI2C(0, 2, ref WriteBuf[0], 2, ref ReadBuf[0]);
            
            sbyte high = (sbyte)ReadBuf[0];
            short low = ReadBuf[1];
            //labTemp.Text = (high + low / 256.0).ToString("f2") + "°";

            
            // Read ADC
            

            //textBox2.Text = label9.Text;  // read ADC
            WriteBuf[0] = Convert.ToByte("93", 16);
            WriteBuf[1] = Convert.ToByte("02", 16);
            CH341StreamI2C(0, 2, ref WriteBuf[0], 2, ref ReadBuf[0]);
            textBox1.Clear();
            textBox1.Text = ReadBuf[0].ToString("x2") + " " + ReadBuf[1].ToString("x2");
            high = (sbyte)ReadBuf[0];
            mvolt = high * 32 + ReadBuf[1] / 32;
            //int mVoutcorr = mvolt * 4095 / (int)numericUpDown3.Value;
            //labADCOut.Text = mvolt.ToString("f3") + " V";

            // Write to 4 * 7 Seg ELV- Display
            int mvt = mvolt / 1000;
            int rest = mvolt - mvt * 1000;
            int mvh = rest / 100;
            rest=rest -mvh * 100;
            int mvz = rest / 10;
            int mve = rest - mvz * 10;
            WriteBuf[0] = Convert.ToByte("70", 16);
            WriteBuf[1] = Convert.ToByte("00", 16);
            WriteBuf[2] = Convert.ToByte("17", 16);
            WriteBuf[3] = LEDp[mvt];
            WriteBuf[4] = LED[mvh];
            WriteBuf[5] = LED[mvz];
            WriteBuf[6] = LED[mve];
            CH341StreamI2C(0, 7, ref WriteBuf[0], 0, ref ReadBuf[0]);

        }

        int mvolt = 0;



        private void tempLbl_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Label lblSender = (System.Windows.Forms.Label)sender;
            var lblDatas = lblSender.Tag.ToString().Split('#');
            foreach(var lblData in lblDatas)
            {
                var lblDatalParts = lblData.Split(':');
                switch (lblDatalParts[0])
                {
                    case "Reg": cmbReg.Text = lblDatalParts[1];break;
                    case "Length": numericUpDown1.Value = Convert.ToDecimal(lblDatalParts[1]); break;
                    case "Data": txtData.Text = lblDatalParts[1]; break;
                    case "Type": cmbType.Text = lblDatalParts[1]; break;
                }
            }
            //cmbReg.SelectedIndex=10;

            /*
            textBox2.Text = label3.Text;  // Ausgabe "1234" auf 4 * 7 Seg
            WriteBuf[0] = Convert.ToByte("70", 16);
            WriteBuf[1] = Convert.ToByte("00", 16);
            WriteBuf[2] = Convert.ToByte("17", 16);
            WriteBuf[3] = Convert.ToByte("06", 16);
            WriteBuf[4] = Convert.ToByte("5b", 16);
            WriteBuf[5] = Convert.ToByte("4f", 16);
            WriteBuf[6] = Convert.ToByte("66", 16);
            CH341StreamI2C(0, 7, ref WriteBuf[0], 0, ref ReadBuf[0]);*/

        }

        private void label4_Click(object sender, EventArgs e)
        {
            textBox2.Text = label4.Text;  // Init Temp.- Sensor
            WriteBuf[0] = Convert.ToByte("90", 16);
            WriteBuf[1] = Convert.ToByte("01", 16);
            WriteBuf[2] = Convert.ToByte("60", 16);
            CH341StreamI2C(0, 3, ref WriteBuf[0], 0, ref ReadBuf[0]);

        }

        private void label5_Click(object sender, EventArgs e)
        {
            textBox2.Text = label5.Text;  // Temp auf Conversion
            WriteBuf[0] = Convert.ToByte("90", 16);
            WriteBuf[1] = Convert.ToByte("00", 16);
            CH341StreamI2C(0, 2, ref WriteBuf[0], 0, ref ReadBuf[0]);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            textBox2.Text = label6.Text;  // Read Temp
            WriteBuf[0] = Convert.ToByte("91", 16);
            WriteBuf[1] = Convert.ToByte("02", 16);
            CH341StreamI2C(0, 2, ref WriteBuf[0], 2, ref ReadBuf[0]);
            textBox1.Clear();
            textBox1.Text = ReadBuf[0].ToString("x2") + " " + ReadBuf[1].ToString("x2") ;
            sbyte high = (sbyte)ReadBuf[0];
            short low = ReadBuf[1];
            //labTemp.Text = (high + low / 256.0).ToString("f2") + "°";

        }

        bool sw = true;
        private XElement deviceInfo;

        void mmtimerTick(TMultimediaTimer sender)
        {
            // Sample and Hold first read ADC, than output on DAC

            // Rectangle to  DAC

            if (0==1)
            {
                if (sw)
                {
                    WriteBuf[0] = 196;
                    WriteBuf[1] = 0;
                    WriteBuf[2] = 0;
                    CH341StreamI2C(0, 3, ref WriteBuf[0], 0, ref ReadBuf[0]);

                    sw = false;
                }
                else
                {
                    WriteBuf[0] = 196;
                    WriteBuf[1] = 15;
                    WriteBuf[2] = 255;
                    CH341StreamI2C(0, 3, ref WriteBuf[0], 0, ref ReadBuf[0]);

                    sw = true;
                }
            }
            else   // Sample & Hold
            {
                WriteBuf[0] = Convert.ToByte("93", 16);
                WriteBuf[1] = Convert.ToByte("02", 16);
                CH341StreamI2C(0, 2, ref WriteBuf[0], 2, ref ReadBuf[0]);
                byte high = (byte)ReadBuf[0];
                mvolt = high * 32 + ReadBuf[1] / 32;  // Gesampelter ADC- Wert
                int mVoutcorr = mvolt * 4095 / 1;// (int)numericUpDown3.Value;  //Auf 5 V - DAC korrigierter Wert
                WriteBuf[0] = Convert.ToByte("C4", 16);
                byte highout=Convert.ToByte( mVoutcorr / 256);
                WriteBuf[1] =highout; 
                WriteBuf[2] =Convert.ToByte (mVoutcorr-highout*256) ;
                CH341StreamI2C(0, 3, ref WriteBuf[0], 0, ref ReadBuf[0]);
 

            }
           
            
        }

        private void butStartMM_Click(object sender, EventArgs e)
        {
            if (mmtimer.Enabled)
            {
                mmtimer.Enabled = false;
                //butStartMM.Text = "Start again";
            }

            else
            {
                mmtimer.Enabled = true;
                //butStartMM.Text = "Stop MMTimer";
            }

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            mmtimer.Disable();
            //mmtimer.Interval = (uint)numericUpDown1.Value;
            mmtimer.Enable();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            int mVOut = 0;//(int)numericUpDown2.Value;
            int mVoutcorr = mVOut * 4095 / 1;// (int)numericUpDown3.Value;
            textBox2.Text = "S C4 " + mVoutcorr.ToString("x4");
            
            // Write to DAC
            WriteBuf[0] = Convert.ToByte("C4", 16);
            byte highout = Convert.ToByte(mVoutcorr / 256);
            WriteBuf[1] = highout;
            WriteBuf[2] = Convert.ToByte(mVoutcorr - highout * 256);
            CH341StreamI2C(0, 3, ref WriteBuf[0], 0, ref ReadBuf[0]);
        }

        private void butInitTempADC_Click(object sender, EventArgs e)
        {
            ReadAllRegister(cmbDeviceAdr.Text);
            dataGridView1.Columns[0].HeaderText = "Register";
            dataGridView1.Columns[1].Visible = true;
            dataGridView1.Columns[3].Visible = true;
            dataGridView1.Columns[4].Visible = true;
            dataGridView1.Columns[5].Visible = true;
        }

        private void rB20k_CheckedChanged(object sender, EventArgs e)
        {
            if (rB100k.Checked) CH341SetStream(0, 0x61);
            if (rB20k.Checked)  CH341SetStream(0, 0x60);
            if (rB400k.Checked) CH341SetStream(0, 0x62);
            if (rB750k.Checked) CH341SetStream(0, 0x63);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnScanAdresses_Click(object sender, EventArgs e)
        {
            var listAdresses=ScanAllAdresses();
            dataGridView1.Columns[0].HeaderText= "Adresses";
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            foreach (var item in listAdresses)
            {
                string[] row = new string[] { item.Key, "", ByteArrayToString(item.Value) };
                dataGridView1.Rows.Add(row);
            }

        }

        private void cmdReadRegister_Click(object sender, EventArgs e)
        {
            txtData.Text = ByteArrayToString(ReadOneRegister(cmbDeviceAdr.Text, cmbReg.Text, (int)numericUpDown1.Value));
            //writeRegister(cmbDeviceAdr.Text, cmbReg.Text, txtData.Text);
        }

        private void btnLoadXml_Click(object sender, EventArgs e)
        {
            cmbDevices.DisplayMember = "Key";
            //cmbDevices.ValueMember = "Value";
            cmbDevices.Items.Clear();
            cmbDevices.Items.Add(new KeyValuePair<string, string>("Not in List", ""));
            var files = Directory.GetFiles(".", "*.xml");
            foreach (var file in files)
            {
                XElement root = XElement.Load(file);
                foreach (var deviceInfos in root.Elements("Device"))
                {
                    var devID = (string)deviceInfos.Attribute("DeviceId");
                    var devName = (string)deviceInfos.Element("Name");
                    
                    cmbDevices.Items.Add(new KeyValuePair<string, string>(devName, file + "::" + devID));
                    Console.WriteLine("Dev Name: " + devName + " Id: " + devID);
                }
                
                // Copy file.
            }
            if (cmbDevices.Items.Count > 0)
            {
                label12.Visible= false;
            }
        }

        private void cmbDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDevices.SelectedIndex > 0)
            {

                var itemValues = ((KeyValuePair<string, string>)cmbDevices.SelectedItem).Value.Split(new string[] { "::" }, StringSplitOptions.RemoveEmptyEntries);


                Console.WriteLine("File Path: " + itemValues[0] + " DeviceId: " + itemValues[1]);
                XElement root = XElement.Load(itemValues[0]);
                IEnumerable<XElement> selectedDevice =
                                            from el in root.Elements("Device")
                                            where (string)el.Attribute("DeviceId") == itemValues[1]
                                            select el;
                deviceInfo = selectedDevice.FirstOrDefault();
                textBox1.Text = deviceInfo.Element("Info").Value;
                cmbDeviceAdr.Text = deviceInfo.Element("Adresses").Elements("Adress").FirstOrDefault().Value;
                int lblTop = 0;
                try
                {
                    IEnumerable<XElement> listCommands =
                            from elc in deviceInfo.Element("Commands").Elements("Command")
                                //where (string) elc.Element("Info").Value == itemValues[1]
                            select elc;
                    foreach (Control lblEx in panel1.Controls.Find("lblCommands",true))
                    {
                        panel1.Controls.Remove(lblEx);
                    }
                    foreach (XElement XElementcmd in listCommands)
                    {
                        System.Windows.Forms.Label tempLbl = new System.Windows.Forms.Label();
                        panel1.Controls.Add(tempLbl);
                        tempLbl.AutoSize = true;
                        tempLbl.Cursor = System.Windows.Forms.Cursors.Hand;
                        tempLbl.Location = new System.Drawing.Point(15, lblTop);
                        tempLbl.Name = "lblCommands";
                        tempLbl.Size = new System.Drawing.Size(210, 13);
                        tempLbl.TabIndex = 20;
                        tempLbl.Text = XElementcmd.Element("Definition").Value;
                        tempLbl.Tag = XElementcmd.Element("Data").Value;
                        tempLbl.Click += new System.EventHandler(tempLbl_Click);
                        tempLbl.MouseEnter += new System.EventHandler(Label_MouseEnter);
                        tempLbl.MouseLeave += new System.EventHandler(Label_MouseLeave);
                        tempLbl.Visible = true;
                        lblTop += 16;
                    }
                }
                catch { }


            } else
                deviceInfo= null;
        }
    }
}
