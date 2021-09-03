
namespace ArduinoRelaisTesterForms
{
    partial class Ui
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.serial_ctn = new System.Windows.Forms.GroupBox();
            this.baudRate_lbl = new System.Windows.Forms.Label();
            this.port_lbl = new System.Windows.Forms.Label();
            this.comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.comboBoxPort = new System.Windows.Forms.ComboBox();
            this.connect_btn = new System.Windows.Forms.Button();
            this.start_stop_btn = new System.Windows.Forms.Button();
            this.relaisTest_ctn = new System.Windows.Forms.GroupBox();
            this.repetitionsField = new System.Windows.Forms.TextBox();
            this.repetitions_lbl = new System.Windows.Forms.Label();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.monitor = new System.Windows.Forms.ListBox();
            this.serial_ctn.SuspendLayout();
            this.relaisTest_ctn.SuspendLayout();
            this.SuspendLayout();
            // 
            // serial_ctn
            // 
            this.serial_ctn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.serial_ctn.Controls.Add(this.baudRate_lbl);
            this.serial_ctn.Controls.Add(this.port_lbl);
            this.serial_ctn.Controls.Add(this.comboBoxBaudRate);
            this.serial_ctn.Controls.Add(this.comboBoxPort);
            this.serial_ctn.Controls.Add(this.connect_btn);
            this.serial_ctn.Location = new System.Drawing.Point(14, 401);
            this.serial_ctn.Margin = new System.Windows.Forms.Padding(5);
            this.serial_ctn.Name = "serial_ctn";
            this.serial_ctn.Padding = new System.Windows.Forms.Padding(5);
            this.serial_ctn.Size = new System.Drawing.Size(756, 93);
            this.serial_ctn.TabIndex = 0;
            this.serial_ctn.TabStop = false;
            this.serial_ctn.Text = "Serial Connection";
            // 
            // baudRate_lbl
            // 
            this.baudRate_lbl.AutoSize = true;
            this.baudRate_lbl.Location = new System.Drawing.Point(396, 42);
            this.baudRate_lbl.Name = "baudRate_lbl";
            this.baudRate_lbl.Size = new System.Drawing.Size(95, 22);
            this.baudRate_lbl.TabIndex = 4;
            this.baudRate_lbl.Text = "Baud Rate";
            // 
            // port_lbl
            // 
            this.port_lbl.AutoSize = true;
            this.port_lbl.Location = new System.Drawing.Point(184, 42);
            this.port_lbl.Name = "port_lbl";
            this.port_lbl.Size = new System.Drawing.Size(43, 22);
            this.port_lbl.TabIndex = 3;
            this.port_lbl.Text = "Port";
            // 
            // comboBoxBaudRate
            // 
            this.comboBoxBaudRate.FormattingEnabled = true;
            this.comboBoxBaudRate.Location = new System.Drawing.Point(492, 39);
            this.comboBoxBaudRate.Name = "comboBoxBaudRate";
            this.comboBoxBaudRate.Size = new System.Drawing.Size(146, 28);
            this.comboBoxBaudRate.TabIndex = 2;
            // 
            // comboBoxPort
            // 
            this.comboBoxPort.FormattingEnabled = true;
            this.comboBoxPort.Location = new System.Drawing.Point(233, 39);
            this.comboBoxPort.Name = "comboBoxPort";
            this.comboBoxPort.Size = new System.Drawing.Size(112, 28);
            this.comboBoxPort.TabIndex = 1;
            // 
            // connect_btn
            // 
            this.connect_btn.Location = new System.Drawing.Point(25, 37);
            this.connect_btn.Name = "connect_btn";
            this.connect_btn.Size = new System.Drawing.Size(116, 31);
            this.connect_btn.TabIndex = 0;
            this.connect_btn.Text = "Connect";
            this.connect_btn.UseVisualStyleBackColor = true;
            this.connect_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // start_stop_btn
            // 
            this.start_stop_btn.Location = new System.Drawing.Point(25, 35);
            this.start_stop_btn.Name = "start_stop_btn";
            this.start_stop_btn.Size = new System.Drawing.Size(96, 31);
            this.start_stop_btn.TabIndex = 5;
            this.start_stop_btn.Text = "Start";
            this.start_stop_btn.UseVisualStyleBackColor = true;
            this.start_stop_btn.Click += new System.EventHandler(this.start_stop_btn_Click);
            // 
            // relaisTest_ctn
            // 
            this.relaisTest_ctn.Controls.Add(this.repetitionsField);
            this.relaisTest_ctn.Controls.Add(this.repetitions_lbl);
            this.relaisTest_ctn.Controls.Add(this.progressBar);
            this.relaisTest_ctn.Controls.Add(this.start_stop_btn);
            this.relaisTest_ctn.Location = new System.Drawing.Point(14, 304);
            this.relaisTest_ctn.Name = "relaisTest_ctn";
            this.relaisTest_ctn.Size = new System.Drawing.Size(756, 89);
            this.relaisTest_ctn.TabIndex = 2;
            this.relaisTest_ctn.TabStop = false;
            this.relaisTest_ctn.Text = "Relais Test";
            // 
            // repetitionsField
            // 
            this.repetitionsField.Location = new System.Drawing.Point(265, 37);
            this.repetitionsField.Name = "repetitionsField";
            this.repetitionsField.Size = new System.Drawing.Size(49, 27);
            this.repetitionsField.TabIndex = 8;
            // 
            // repetitions_lbl
            // 
            this.repetitions_lbl.AutoSize = true;
            this.repetitions_lbl.Location = new System.Drawing.Point(148, 40);
            this.repetitions_lbl.Name = "repetitions_lbl";
            this.repetitions_lbl.Size = new System.Drawing.Size(100, 22);
            this.repetitions_lbl.TabIndex = 7;
            this.repetitions_lbl.Text = "Repetitions";
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(331, 35);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(402, 31);
            this.progressBar.TabIndex = 6;
            // 
            // monitor
            // 
            this.monitor.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.monitor.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(0)));
            this.monitor.ForeColor = System.Drawing.SystemColors.Window;
            this.monitor.FormattingEnabled = true;
            this.monitor.ItemHeight = 18;
            this.monitor.Location = new System.Drawing.Point(14, 13);
            this.monitor.Name = "monitor";
            this.monitor.Size = new System.Drawing.Size(756, 274);
            this.monitor.TabIndex = 3;
            // 
            // Ui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 508);
            this.Controls.Add(this.monitor);
            this.Controls.Add(this.relaisTest_ctn);
            this.Controls.Add(this.serial_ctn);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "Ui";
            this.Text = "Form1";
            this.serial_ctn.ResumeLayout(false);
            this.serial_ctn.PerformLayout();
            this.relaisTest_ctn.ResumeLayout(false);
            this.relaisTest_ctn.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox serial_ctn;
        private System.Windows.Forms.Button connect_btn;
        private System.Windows.Forms.ComboBox comboBoxPort;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.Label port_lbl;
        private System.Windows.Forms.Label baudRate_lbl;
        private System.Windows.Forms.Button start_stop_btn;
        private System.Windows.Forms.GroupBox relaisTest_ctn;
        private System.Windows.Forms.Label repetitions_lbl;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.TextBox repetitionsField;
        private System.Windows.Forms.ListBox monitor;
    }
}

