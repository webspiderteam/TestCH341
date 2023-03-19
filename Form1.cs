using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Xml.Linq;

namespace TestI2C
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


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
            if (dataGridView1.Rows.Count < 256 || refreshDataGrid)
                dataGridView1.Rows.Clear();
            refreshDataGrid= false;
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
                var dataVal = ByteArrayToString(buf).Trim();
                string[] row = new string[] { i.ToString("X2"), regDefinition, dataVal, ByteArrayToAsciiString(buf).Trim(), ByteArrayToDecimalString(buf).Trim(), ByteArrayToBinaryString(buf).Trim() };
            
                if (dataGridView1.Rows.Count < i + 1) 
                    dataGridView1.Rows.Add(row);
                else
                {
                    if ((string)dataVal != (string)dataGridView1.Rows[i].Cells[2].Value) 
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.DarkSeaGreen;
                    else
                        dataGridView1.Rows[i].Cells[2].Style.BackColor = Color.White;
                    dataGridView1.Rows[i].Cells[2].Value = dataVal;
                    dataGridView1.Rows[i].Cells[3].Value = ByteArrayToAsciiString(buf).Trim();
                    dataGridView1.Rows[i].Cells[4].Value = ByteArrayToDecimalString(buf).Trim();
                    dataGridView1.Rows[i].Cells[5].Value = ByteArrayToBinaryString(buf).Trim();

                }
                    
                result.Add(i.ToString("X2"), buf);
                bufLength = 1;
            }
            return result;
        }

        private void writeRegister(string devAdress, string regAdress, string outData, string sType)
        {
            int bufLength = 0;
            string sData = outData;
            switch (sType)
            {
                case "Ascii": sData = outData.ToCharArray().Aggregate("", (a, b) => a + ((byte)b).ToString("X") + " ").ToUpper().Trim(); break;
                case "Dec": sData = int.Parse(outData).ToString("X2"); break;
                case "Bin": sData= string.Join(" ", Enumerable.Range(0, outData.Trim().Replace(" ", "").Length / 8)
                                            .Select(i => Convert.ToByte(outData.Trim().Replace(" ", "").Substring(i * 8, 8), 2).ToString("X2"))); break;
            }
            
                
            byte[] buf = new byte[1];
            int addresbyte = Convert.ToInt32(devAdress, 16) << 1;
            WriteBuf[0] = (Convert.ToByte(addresbyte));
            WriteBuf[1] = Convert.ToByte(regAdress, 16);
            sData= sData.Trim().Replace(" ","");
            for (int i = 0; i<sData.Length; i+=2)
            {
                WriteBuf[(i/2)+2] = Convert.ToByte(sData.Substring(i,2), 16);
                bufLength++;
            }
            CH341StreamI2C(0, bufLength + 2, ref WriteBuf[0], bufLength, ref buf[0]);
            //Console.WriteLine(sData);
        }

        public static string ByteArrayToString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:X2} ", b);
            return hex.ToString();
        }

        public static string ByteArrayToDecimalString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:X2}", b);
            // ds3231 temp calculation
            //var result = (Convert.ToInt32(hex.ToString(), 16) & 0x7FC0) >> 6;  
            //var minus = Convert.ToInt32(hex.ToString(), 16) >> 15;
            //if (minus == 1)
            //    result = result * -1;
            //return (result / 4.00).ToString("F");
            return Convert.ToInt32(hex.ToString(), 16).ToString(); 
        }

        public static string ByteArrayToAsciiString(byte[] ba)
        {
            // Old Method
            //StringBuilder hex = new StringBuilder(ba.Length * 2);
            //foreach (byte b in ba)
            //    hex.AppendFormat("{0}", (char)b);

            return System.Text.Encoding.ASCII.GetString(ba);// hex.ToString();
        }

        public static string ByteArrayToBinaryString(byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0} ", Convert.ToString(b, 2).PadLeft(8, '0'));
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
            cmbDelay.DisplayMember = "Key";
            cmbDelay.Items.Add(new KeyValuePair<string, int>("100 ms", 100));
            cmbDelay.Items.Add(new KeyValuePair<string, int>("200 ms", 200));
            cmbDelay.Items.Add(new KeyValuePair<string, int>("500 ms", 500));
            cmbDelay.Items.Add(new KeyValuePair<string, int>("1 s", 1000));
            cmbDelay.Items.Add(new KeyValuePair<string, int>("2 s", 2000));
            cmbDelay.Items.Add(new KeyValuePair<string, int>("3 s", 3000));
            cmbDelay.Items.Add(new KeyValuePair<string, int>("5 s", 5000));
            cmbDeviceAdr.SelectedIndex = 0;
            cmbReg.SelectedIndex = 0;
            cmbType.SelectedIndex = 0;
            cmbMode.SelectedIndex = 0;
            cmbDelay.SelectedIndex = 3;
 
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
            timer1.Enabled = cmbMode.SelectedIndex == 1;
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

 
         private void buttonClose_Click(object sender, EventArgs e)
        {
            label2.Text = " No Chip open";
            buttonClose.Enabled = false;
            buttonOpen.Enabled = true;
            btnScanAdresses.Enabled = false;
            groupBox3.Enabled = false;
            groupBox2.Enabled = false;
            timer1.Enabled=false;
            CH341CloseDevice(0);

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            txtData.Clear();
        }

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
                    case "Data": txtDataRawHex.Text = lblDatalParts[1]; break;
                    case "Type": cmbType.Text = lblDatalParts[1]; break;
                }
            }
        }

        private XElement deviceInfo;
        private int currentMode = 0;
        private string currentType="Hex";
        private bool refreshDataGrid;

        private void butInitTempADC_Click(object sender, EventArgs e)
        {
            ReadAllRegister(cmbDeviceAdr.Text);
            if (currentMode == 1)
            {
                dataGridView1.Columns[0].HeaderText = "Register";
                dataGridView1.Columns[1].Visible = true;
                dataGridView1.Columns[3].Visible = true;
                dataGridView1.Columns[4].Visible = true;
                dataGridView1.Columns[5].Visible = true;
                currentMode= 0;
            }

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
            currentType = cmbType.Text;
            txtDataRawHex_TextChanged(sender, e);
        }

        private void btnScanAdresses_Click(object sender, EventArgs e)
        {
            var listAdresses=ScanAllAdresses();
            if (currentMode == 0)
            {
                dataGridView1.Columns[0].HeaderText = "Adresses";
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;
                dataGridView1.Columns[5].Visible = false;
                currentMode= 1;
            }

            foreach (var item in listAdresses)
            {
                string[] row = new string[] { item.Key, "", ByteArrayToString(item.Value) };
                dataGridView1.Rows.Add(row);
            }
        }

        private void cmdReadRegister_Click(object sender, EventArgs e)
        {
            txtDataRawHex.Text = ByteArrayToString(ReadOneRegister(cmbDeviceAdr.Text, cmbReg.Text, (int)numericUpDown1.Value));
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
            refreshDataGrid = true;
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

        private void cmbDelay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDelay.SelectedIndex > -1)
            {
                timer1.Interval = ((KeyValuePair<string, int>)cmbDelay.SelectedItem).Value;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            butInitTempADC_Click(sender, e);
        }

        private void cmdSend_Click(object sender, EventArgs e)
        {
            //TODO: convert data to hex before send!!
            writeRegister(cmbDeviceAdr.Text, cmbReg.Text, txtData.Text, currentType);
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                cmbReg.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim();
                if (deviceInfo != null)
                {
                    XElement registersList = deviceInfo.Element("Registers");
                    var nameByValue = registersList.Descendants("Register")
                                                .Where(elem => elem.Element("RegAdress").Value.Equals(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().Trim()));
                    if (nameByValue.Count() > 0)
                    {
                        numericUpDown1.Value = Convert.ToInt16(nameByValue.FirstOrDefault().Element("Length").Value);
                    }
                }
                txtDataRawHex.Text = ByteArrayToString(ReadOneRegister(cmbDeviceAdr.Text, cmbReg.Text, (int)numericUpDown1.Value));
            }
        }

        private void txtDataRawHex_TextChanged(object sender, EventArgs e)
        {
            if (cmbType.SelectedIndex != -1 )
            {
                var rawData = txtDataRawHex.Text.Trim().Replace(" ", "");
                var ba = Enumerable.Range(0, rawData.Length)
                     .Where(x => x % 2 == 0)
                     .Select(x => Convert.ToByte(rawData.Substring(x, 2), 16))
                     .ToArray();
                switch (cmbType.SelectedIndex)
                {
                    case 0:txtData.Text= txtDataRawHex.Text; break;
                    case 1: txtData.Text = ByteArrayToAsciiString(ba); break;
                    case 2: txtData.Text = ByteArrayToDecimalString(ba); break;
                    case 3: txtData.Text = ByteArrayToBinaryString(ba); break;
                }
            }
        }
    }
}
