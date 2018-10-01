namespace RN487xTerm
{
    partial class RN487xTermF
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
            this.buttonScanFt234x = new System.Windows.Forms.Button();
            this.comboBoxFt234x = new System.Windows.Forms.ComboBox();
            this.buttonCmdMode = new System.Windows.Forms.Button();
            this.buttonGattMode = new System.Windows.Forms.Button();
            this.buttonOpenFt234x = new System.Windows.Forms.Button();
            this.buttonScanBle = new System.Windows.Forms.Button();
            this.buttonStopScanBle = new System.Windows.Forms.Button();
            this.comboBoxBle = new System.Windows.Forms.ComboBox();
            this.buttonConnectBle = new System.Windows.Forms.Button();
            this.buttonDisconnectBle = new System.Windows.Forms.Button();
            this.buttonCloseFt234x = new System.Windows.Forms.Button();
            this.textBoxConsole = new System.Windows.Forms.TextBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.buttonSaveFile = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonScanFt234x
            // 
            this.buttonScanFt234x.Location = new System.Drawing.Point(323, 12);
            this.buttonScanFt234x.Name = "buttonScanFt234x";
            this.buttonScanFt234x.Size = new System.Drawing.Size(108, 23);
            this.buttonScanFt234x.TabIndex = 0;
            this.buttonScanFt234x.Text = "Scan FT234X";
            this.buttonScanFt234x.UseVisualStyleBackColor = true;
            this.buttonScanFt234x.Click += new System.EventHandler(this.buttonScanFt234x_Click);
            // 
            // comboBoxFt234x
            // 
            this.comboBoxFt234x.FormattingEnabled = true;
            this.comboBoxFt234x.Location = new System.Drawing.Point(12, 12);
            this.comboBoxFt234x.Name = "comboBoxFt234x";
            this.comboBoxFt234x.Size = new System.Drawing.Size(288, 20);
            this.comboBoxFt234x.TabIndex = 1;
            // 
            // buttonCmdMode
            // 
            this.buttonCmdMode.Location = new System.Drawing.Point(323, 42);
            this.buttonCmdMode.Name = "buttonCmdMode";
            this.buttonCmdMode.Size = new System.Drawing.Size(75, 23);
            this.buttonCmdMode.TabIndex = 2;
            this.buttonCmdMode.Text = "CMD Mode";
            this.buttonCmdMode.UseVisualStyleBackColor = true;
            this.buttonCmdMode.Click += new System.EventHandler(this.buttonCmdMode_Click);
            // 
            // buttonGattMode
            // 
            this.buttonGattMode.Location = new System.Drawing.Point(404, 42);
            this.buttonGattMode.Name = "buttonGattMode";
            this.buttonGattMode.Size = new System.Drawing.Size(75, 23);
            this.buttonGattMode.TabIndex = 3;
            this.buttonGattMode.Text = "GATT Mode";
            this.buttonGattMode.UseVisualStyleBackColor = true;
            this.buttonGattMode.Click += new System.EventHandler(this.buttonGattMode_Click);
            // 
            // buttonOpenFt234x
            // 
            this.buttonOpenFt234x.Location = new System.Drawing.Point(13, 41);
            this.buttonOpenFt234x.Name = "buttonOpenFt234x";
            this.buttonOpenFt234x.Size = new System.Drawing.Size(109, 23);
            this.buttonOpenFt234x.TabIndex = 4;
            this.buttonOpenFt234x.Text = "Open FT234X";
            this.buttonOpenFt234x.UseVisualStyleBackColor = true;
            this.buttonOpenFt234x.Click += new System.EventHandler(this.buttonOpenFt234x_Click);
            // 
            // buttonScanBle
            // 
            this.buttonScanBle.Location = new System.Drawing.Point(13, 70);
            this.buttonScanBle.Name = "buttonScanBle";
            this.buttonScanBle.Size = new System.Drawing.Size(109, 23);
            this.buttonScanBle.TabIndex = 5;
            this.buttonScanBle.Text = "Scan BLE";
            this.buttonScanBle.UseVisualStyleBackColor = true;
            this.buttonScanBle.Click += new System.EventHandler(this.buttonScanBle_Click);
            // 
            // buttonStopScanBle
            // 
            this.buttonStopScanBle.Location = new System.Drawing.Point(128, 70);
            this.buttonStopScanBle.Name = "buttonStopScanBle";
            this.buttonStopScanBle.Size = new System.Drawing.Size(109, 23);
            this.buttonStopScanBle.TabIndex = 6;
            this.buttonStopScanBle.Text = "Stop Scan BLE";
            this.buttonStopScanBle.UseVisualStyleBackColor = true;
            this.buttonStopScanBle.Click += new System.EventHandler(this.buttonStopScanBle_Click);
            // 
            // comboBoxBle
            // 
            this.comboBoxBle.FormattingEnabled = true;
            this.comboBoxBle.Location = new System.Drawing.Point(12, 100);
            this.comboBoxBle.Name = "comboBoxBle";
            this.comboBoxBle.Size = new System.Drawing.Size(288, 20);
            this.comboBoxBle.TabIndex = 7;
            // 
            // buttonConnectBle
            // 
            this.buttonConnectBle.Location = new System.Drawing.Point(13, 127);
            this.buttonConnectBle.Name = "buttonConnectBle";
            this.buttonConnectBle.Size = new System.Drawing.Size(109, 23);
            this.buttonConnectBle.TabIndex = 8;
            this.buttonConnectBle.Text = "Connect BLE";
            this.buttonConnectBle.UseVisualStyleBackColor = true;
            this.buttonConnectBle.Click += new System.EventHandler(this.buttonConnectBle_Click);
            // 
            // buttonDisconnectBle
            // 
            this.buttonDisconnectBle.Location = new System.Drawing.Point(128, 127);
            this.buttonDisconnectBle.Name = "buttonDisconnectBle";
            this.buttonDisconnectBle.Size = new System.Drawing.Size(109, 23);
            this.buttonDisconnectBle.TabIndex = 9;
            this.buttonDisconnectBle.Text = "Disconnect BLE";
            this.buttonDisconnectBle.UseVisualStyleBackColor = true;
            this.buttonDisconnectBle.Click += new System.EventHandler(this.buttonDisconnectBle_Click);
            // 
            // buttonCloseFt234x
            // 
            this.buttonCloseFt234x.Location = new System.Drawing.Point(127, 42);
            this.buttonCloseFt234x.Name = "buttonCloseFt234x";
            this.buttonCloseFt234x.Size = new System.Drawing.Size(110, 23);
            this.buttonCloseFt234x.TabIndex = 10;
            this.buttonCloseFt234x.Text = "Close FT234X";
            this.buttonCloseFt234x.UseVisualStyleBackColor = true;
            this.buttonCloseFt234x.Click += new System.EventHandler(this.buttonCloseFt234x_Click);
            // 
            // textBoxConsole
            // 
            this.textBoxConsole.Location = new System.Drawing.Point(13, 157);
            this.textBoxConsole.Multiline = true;
            this.textBoxConsole.Name = "textBoxConsole";
            this.textBoxConsole.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxConsole.Size = new System.Drawing.Size(466, 323);
            this.textBoxConsole.TabIndex = 11;
            this.textBoxConsole.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxConsole_KeyPress);
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "csv";
            // 
            // buttonSaveFile
            // 
            this.buttonSaveFile.Location = new System.Drawing.Point(370, 111);
            this.buttonSaveFile.Name = "buttonSaveFile";
            this.buttonSaveFile.Size = new System.Drawing.Size(109, 39);
            this.buttonSaveFile.TabIndex = 12;
            this.buttonSaveFile.Text = "Save Text";
            this.buttonSaveFile.UseVisualStyleBackColor = true;
            this.buttonSaveFile.Click += new System.EventHandler(this.buttonSaveFile_Click);
            // 
            // RN487xTermF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(491, 492);
            this.Controls.Add(this.buttonSaveFile);
            this.Controls.Add(this.textBoxConsole);
            this.Controls.Add(this.buttonCloseFt234x);
            this.Controls.Add(this.buttonDisconnectBle);
            this.Controls.Add(this.buttonConnectBle);
            this.Controls.Add(this.comboBoxBle);
            this.Controls.Add(this.buttonStopScanBle);
            this.Controls.Add(this.buttonScanBle);
            this.Controls.Add(this.buttonOpenFt234x);
            this.Controls.Add(this.buttonGattMode);
            this.Controls.Add(this.buttonCmdMode);
            this.Controls.Add(this.comboBoxFt234x);
            this.Controls.Add(this.buttonScanFt234x);
            this.Name = "RN487xTermF";
            this.Text = "RN487xTermF";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonScanFt234x;
        private System.Windows.Forms.ComboBox comboBoxFt234x;
        private System.Windows.Forms.Button buttonCmdMode;
        private System.Windows.Forms.Button buttonGattMode;
        private System.Windows.Forms.Button buttonOpenFt234x;
        private System.Windows.Forms.Button buttonScanBle;
        private System.Windows.Forms.Button buttonStopScanBle;
        private System.Windows.Forms.ComboBox comboBoxBle;
        private System.Windows.Forms.Button buttonConnectBle;
        private System.Windows.Forms.Button buttonDisconnectBle;
        private System.Windows.Forms.Button buttonCloseFt234x;
        private System.Windows.Forms.TextBox textBoxConsole;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button buttonSaveFile;
    }
}