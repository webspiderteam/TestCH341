namespace TestI2C
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.btnLoadXml = new System.Windows.Forms.Button();
            this.btnScanAdresses = new System.Windows.Forms.Button();
            this.rB750k = new System.Windows.Forms.RadioButton();
            this.rB400k = new System.Windows.Forms.RadioButton();
            this.rB20k = new System.Windows.Forms.RadioButton();
            this.rB100k = new System.Windows.Forms.RadioButton();
            this.buttonClose = new System.Windows.Forms.Button();
            this.lblDelay = new System.Windows.Forms.Label();
            this.lblMode = new System.Windows.Forms.Label();
            this.cmbDelay = new System.Windows.Forms.ComboBox();
            this.cmbMode = new System.Windows.Forms.ComboBox();
            this.butInitTempADC = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Register = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Definition = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ascii = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Dec = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Binary = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cmdReadRegister = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.cmbDeviceAdr = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cmdSend = new System.Windows.Forms.Button();
            this.cmbType = new System.Windows.Forms.ComboBox();
            this.txtData = new System.Windows.Forms.TextBox();
            this.cmbReg = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonClear = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(6, 19);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(102, 32);
            this.buttonOpen.TabIndex = 1;
            this.buttonOpen.Text = "Connect";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(10, 145);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(346, 120);
            this.textBox1.TabIndex = 4;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 606);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(748, 22);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(18, 97);
            this.textBox2.Name = "textBox2";
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox2.Size = new System.Drawing.Size(333, 20);
            this.textBox2.TabIndex = 14;
            this.textBox2.Text = "S700047 06 5b 4f 66";
            this.textBox2.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "No CH341A is open";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.cmbDevices);
            this.groupBox1.Controls.Add(this.btnLoadXml);
            this.groupBox1.Controls.Add(this.btnScanAdresses);
            this.groupBox1.Controls.Add(this.rB750k);
            this.groupBox1.Controls.Add(this.rB400k);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.rB20k);
            this.groupBox1.Controls.Add(this.rB100k);
            this.groupBox1.Controls.Add(this.buttonClose);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonOpen);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(361, 271);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Connect USB- Chip CH341A";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.label12.Location = new System.Drawing.Point(114, 122);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(224, 13);
            this.label12.TabIndex = 49;
            this.label12.Text = "No device Loaded yet. Please Load Xml Files!";
            // 
            // cmbDevices
            // 
            this.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(110, 118);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(245, 21);
            this.cmbDevices.TabIndex = 48;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.cmbDevices_SelectedIndexChanged);
            // 
            // btnLoadXml
            // 
            this.btnLoadXml.Location = new System.Drawing.Point(10, 112);
            this.btnLoadXml.Name = "btnLoadXml";
            this.btnLoadXml.Size = new System.Drawing.Size(92, 27);
            this.btnLoadXml.TabIndex = 47;
            this.btnLoadXml.Text = "Load Xml Files";
            this.btnLoadXml.UseVisualStyleBackColor = true;
            this.btnLoadXml.Click += new System.EventHandler(this.btnLoadXml_Click);
            // 
            // btnScanAdresses
            // 
            this.btnScanAdresses.Enabled = false;
            this.btnScanAdresses.Location = new System.Drawing.Point(9, 76);
            this.btnScanAdresses.Name = "btnScanAdresses";
            this.btnScanAdresses.Size = new System.Drawing.Size(93, 30);
            this.btnScanAdresses.TabIndex = 46;
            this.btnScanAdresses.Text = "Scan Adresses";
            this.btnScanAdresses.UseVisualStyleBackColor = true;
            this.btnScanAdresses.Click += new System.EventHandler(this.btnScanAdresses_Click);
            // 
            // rB750k
            // 
            this.rB750k.AutoSize = true;
            this.rB750k.Location = new System.Drawing.Point(242, 89);
            this.rB750k.Name = "rB750k";
            this.rB750k.Size = new System.Drawing.Size(99, 17);
            this.rB750k.TabIndex = 24;
            this.rB750k.Text = "Speed 750 kHz";
            this.rB750k.UseVisualStyleBackColor = true;
            this.rB750k.CheckedChanged += new System.EventHandler(this.rB20k_CheckedChanged);
            // 
            // rB400k
            // 
            this.rB400k.AutoSize = true;
            this.rB400k.Location = new System.Drawing.Point(242, 68);
            this.rB400k.Name = "rB400k";
            this.rB400k.Size = new System.Drawing.Size(99, 17);
            this.rB400k.TabIndex = 23;
            this.rB400k.Text = "Speed 400 kHz";
            this.rB400k.UseVisualStyleBackColor = true;
            this.rB400k.CheckedChanged += new System.EventHandler(this.rB20k_CheckedChanged);
            // 
            // rB20k
            // 
            this.rB20k.AutoSize = true;
            this.rB20k.Location = new System.Drawing.Point(242, 26);
            this.rB20k.Name = "rB20k";
            this.rB20k.Size = new System.Drawing.Size(93, 17);
            this.rB20k.TabIndex = 22;
            this.rB20k.Text = "Speed 20 kHz";
            this.rB20k.UseVisualStyleBackColor = true;
            this.rB20k.CheckedChanged += new System.EventHandler(this.rB20k_CheckedChanged);
            // 
            // rB100k
            // 
            this.rB100k.AutoSize = true;
            this.rB100k.Checked = true;
            this.rB100k.Location = new System.Drawing.Point(242, 47);
            this.rB100k.Name = "rB100k";
            this.rB100k.Size = new System.Drawing.Size(99, 17);
            this.rB100k.TabIndex = 21;
            this.rB100k.TabStop = true;
            this.rB100k.Text = "Speed 100 kHz";
            this.rB100k.UseVisualStyleBackColor = true;
            this.rB100k.CheckedChanged += new System.EventHandler(this.rB20k_CheckedChanged);
            // 
            // buttonClose
            // 
            this.buttonClose.Enabled = false;
            this.buttonClose.Location = new System.Drawing.Point(126, 19);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(92, 32);
            this.buttonClose.TabIndex = 16;
            this.buttonClose.Text = "Disconnect";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // lblDelay
            // 
            this.lblDelay.AutoSize = true;
            this.lblDelay.Location = new System.Drawing.Point(278, 9);
            this.lblDelay.Name = "lblDelay";
            this.lblDelay.Size = new System.Drawing.Size(34, 13);
            this.lblDelay.TabIndex = 45;
            this.lblDelay.Text = "Delay";
            this.lblDelay.Visible = false;
            // 
            // lblMode
            // 
            this.lblMode.AutoSize = true;
            this.lblMode.Location = new System.Drawing.Point(160, 11);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(63, 13);
            this.lblMode.TabIndex = 44;
            this.lblMode.Text = "Read Mode";
            // 
            // cmbDelay
            // 
            this.cmbDelay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDelay.FormattingEnabled = true;
            this.cmbDelay.Items.AddRange(new object[] {
            "100 ms",
            "200 ms",
            "500 ms",
            "1 s",
            "2 s",
            "3 s",
            "5 s"});
            this.cmbDelay.Location = new System.Drawing.Point(281, 31);
            this.cmbDelay.Name = "cmbDelay";
            this.cmbDelay.Size = new System.Drawing.Size(69, 21);
            this.cmbDelay.TabIndex = 43;
            this.cmbDelay.Visible = false;
            // 
            // cmbMode
            // 
            this.cmbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMode.FormattingEnabled = true;
            this.cmbMode.Items.AddRange(new object[] {
            "Read Manualy",
            "Continuous"});
            this.cmbMode.Location = new System.Drawing.Point(163, 24);
            this.cmbMode.Name = "cmbMode";
            this.cmbMode.Size = new System.Drawing.Size(105, 21);
            this.cmbMode.TabIndex = 42;
            this.cmbMode.SelectedIndexChanged += new System.EventHandler(this.cmbMode_SelectedIndexChanged);
            // 
            // butInitTempADC
            // 
            this.butInitTempADC.Location = new System.Drawing.Point(281, 23);
            this.butInitTempADC.Name = "butInitTempADC";
            this.butInitTempADC.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.butInitTempADC.Size = new System.Drawing.Size(69, 21);
            this.butInitTempADC.TabIndex = 20;
            this.butInitTempADC.Text = "Read Data";
            this.butInitTempADC.UseVisualStyleBackColor = true;
            this.butInitTempADC.Click += new System.EventHandler(this.butInitTempADC_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Enabled = false;
            this.groupBox2.Location = new System.Drawing.Point(12, 296);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(724, 288);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Datas";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Register,
            this.Definition,
            this.Data,
            this.Ascii,
            this.Dec,
            this.Binary});
            this.dataGridView1.Location = new System.Drawing.Point(6, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 30;
            this.dataGridView1.Size = new System.Drawing.Size(712, 266);
            this.dataGridView1.TabIndex = 0;
            // 
            // Register
            // 
            this.Register.HeaderText = "Register";
            this.Register.Name = "Register";
            this.Register.Width = 30;
            // 
            // Definition
            // 
            this.Definition.HeaderText = "Definition";
            this.Definition.Name = "Definition";
            // 
            // Data
            // 
            this.Data.HeaderText = "Data";
            this.Data.Name = "Data";
            this.Data.Width = 200;
            // 
            // Ascii
            // 
            this.Ascii.HeaderText = "Ascii";
            this.Ascii.Name = "Ascii";
            this.Ascii.Width = 150;
            // 
            // Dec
            // 
            this.Dec.HeaderText = "Decimal";
            this.Dec.Name = "Dec";
            this.Dec.Width = 300;
            // 
            // Binary
            // 
            this.Binary.HeaderText = "Binary";
            this.Binary.Name = "Binary";
            this.Binary.Width = 500;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.cmdReadRegister);
            this.groupBox3.Controls.Add(this.lblDelay);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.lblMode);
            this.groupBox3.Controls.Add(this.numericUpDown1);
            this.groupBox3.Controls.Add(this.cmbDelay);
            this.groupBox3.Controls.Add(this.cmbDeviceAdr);
            this.groupBox3.Controls.Add(this.cmbMode);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.cmdSend);
            this.groupBox3.Controls.Add(this.cmbType);
            this.groupBox3.Controls.Add(this.txtData);
            this.groupBox3.Controls.Add(this.butInitTempADC);
            this.groupBox3.Controls.Add(this.cmbReg);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.buttonClear);
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Enabled = false;
            this.groupBox3.Location = new System.Drawing.Point(379, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(357, 271);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Send Box";
            // 
            // cmdReadRegister
            // 
            this.cmdReadRegister.Location = new System.Drawing.Point(18, 98);
            this.cmdReadRegister.Name = "cmdReadRegister";
            this.cmdReadRegister.Size = new System.Drawing.Size(110, 22);
            this.cmdReadRegister.TabIndex = 49;
            this.cmdReadRegister.Text = "Read Register";
            this.cmdReadRegister.UseVisualStyleBackColor = true;
            this.cmdReadRegister.Click += new System.EventHandler(this.cmdReadRegister_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(76, 60);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 13);
            this.label11.TabIndex = 48;
            this.label11.Text = "Length";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(76, 72);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown1.TabIndex = 47;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cmbDeviceAdr
            // 
            this.cmbDeviceAdr.FormattingEnabled = true;
            this.cmbDeviceAdr.Location = new System.Drawing.Point(103, 24);
            this.cmbDeviceAdr.Name = "cmbDeviceAdr";
            this.cmbDeviceAdr.Size = new System.Drawing.Size(54, 21);
            this.cmbDeviceAdr.TabIndex = 46;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 45;
            this.label9.Text = "Device Adress :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(287, 55);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 44;
            this.label8.Text = "Data Type";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(132, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 43;
            this.label7.Text = "Data";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Register";
            // 
            // cmdSend
            // 
            this.cmdSend.Location = new System.Drawing.Point(134, 97);
            this.cmdSend.Name = "cmdSend";
            this.cmdSend.Size = new System.Drawing.Size(76, 23);
            this.cmdSend.TabIndex = 42;
            this.cmdSend.Text = "Send";
            this.cmdSend.UseVisualStyleBackColor = true;
            // 
            // cmbType
            // 
            this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbType.FormattingEnabled = true;
            this.cmbType.Items.AddRange(new object[] {
            "Hex",
            "Ascii",
            "Dec",
            "Bin"});
            this.cmbType.Location = new System.Drawing.Point(290, 70);
            this.cmbType.Name = "cmbType";
            this.cmbType.Size = new System.Drawing.Size(61, 21);
            this.cmbType.TabIndex = 41;
            this.cmbType.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(135, 71);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(149, 20);
            this.txtData.TabIndex = 40;
            // 
            // cmbReg
            // 
            this.cmbReg.FormattingEnabled = true;
            this.cmbReg.Location = new System.Drawing.Point(18, 71);
            this.cmbReg.Name = "cmbReg";
            this.cmbReg.Size = new System.Drawing.Size(52, 21);
            this.cmbReg.TabIndex = 39;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(15, 132);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(216, 16);
            this.label10.TabIndex = 38;
            this.label10.Text = "Test Commands (Click to Use)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(187, 13);
            this.label6.TabIndex = 23;
            this.label6.Text = "S91 02 Read out Temp Sensor 2 Byte";
            this.label6.Visible = false;
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 196);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(191, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "S90 00 Temp Sensor Pointer back to 0";
            this.label5.Visible = false;
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 183);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "S90 01 60 Init Temp- Sensor";
            this.label4.Visible = false;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label3.Location = new System.Drawing.Point(15, 156);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(210, 13);
            this.label3.TabIndex = 20;
            this.label3.Text = "S700047 06 5b 4f 66 to ELV- 7Seg \"1234\"";
            this.label3.Visible = false;
            this.label3.Click += new System.EventHandler(this.tempLbl_Click);
            this.label3.MouseEnter += new System.EventHandler(this.Label_MouseEnter);
            this.label3.MouseLeave += new System.EventHandler(this.Label_MouseLeave);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(216, 97);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(68, 23);
            this.buttonClear.TabIndex = 16;
            this.buttonClear.Text = "Clear";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Location = new System.Drawing.Point(18, 151);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 114);
            this.panel1.TabIndex = 50;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(748, 628);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "Form1";
            this.Text = "I2C with Hongkong Chip CH341";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button butInitTempADC;
        private System.Windows.Forms.RadioButton rB750k;
        private System.Windows.Forms.RadioButton rB400k;
        private System.Windows.Forms.RadioButton rB20k;
        private System.Windows.Forms.RadioButton rB100k;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbType;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.ComboBox cmbReg;
        private System.Windows.Forms.Button cmdSend;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDelay;
        private System.Windows.Forms.Label lblMode;
        private System.Windows.Forms.ComboBox cmbDelay;
        private System.Windows.Forms.ComboBox cmbMode;
        private System.Windows.Forms.ComboBox cmbDeviceAdr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Register;
        private System.Windows.Forms.DataGridViewTextBoxColumn Definition;
        private System.Windows.Forms.DataGridViewTextBoxColumn Data;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ascii;
        private System.Windows.Forms.DataGridViewTextBoxColumn Dec;
        private System.Windows.Forms.DataGridViewTextBoxColumn Binary;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Button btnScanAdresses;
        private System.Windows.Forms.Button cmdReadRegister;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.Button btnLoadXml;
        private System.Windows.Forms.Panel panel1;
    }
}

