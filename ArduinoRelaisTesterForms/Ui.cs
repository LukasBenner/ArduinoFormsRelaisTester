using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO.Ports;
using System.Diagnostics;
using Timer = System.Windows.Forms.Timer;

namespace ArduinoRelaisTesterForms
{

    public partial class Ui : Form
    {
        readonly object queueLock = new object();
        bool isConnected = false;
        bool isRunning = false;
        const int ListMaxSize = 20;
        String[] ports;
        SerialPort port;
        int[] baudRates = { 9600, 19200, 38400 };
        SerialParser parser = new SerialParser();
        Timer updateTimer = new Timer();
        

        public Ui()
        {
            InitializeComponent();
            disableControls();
            initializeUI();
            updateTimer.Interval = 100;
            updateTimer.Tick += new EventHandler(updateUi);
            updateTimer.Start();
            progressBar.Maximum = 100;
        }


        private void connect_btn_Click(object sender, EventArgs e)
        {
            if (!isConnected)
            {
                connectToArduino();
            }
            else
            {
                disconnectFromArduino();
            }
        }

        private void start_stop_btn_Click(object sender, EventArgs e)
        {
            if (!isRunning)
            {
                setApplicationStart();
                port.WriteLine("<REP:" + repetitionsField.Text + "><START>");
            }
            else
            {
                port.WriteLine("<STOP>");
                start_stop_btn.Text = "Start";
                isRunning = false;
                setApplicationConnected();
            }

        }


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            lock (queueLock)
            {
                if (isConnected && isRunning)
                {
                    String buffer = port.ReadExisting();
                    parser.serialParse(buffer);
                }
            }
        }

        private void updateUi(object sender, EventArgs e)
        {
            if (parser.instructionAvailable() && isRunning && isConnected)
            {
                var instruction = parser.getInstruction();
                if (instruction != null)
                {
                    String command = parser.getCommand(instruction);
                    String value = parser.getValue(instruction);

                    if (command == "INFO")
                    {
                        AddMonitorMessage("This is a INFO" + ": " + value);
                    }

                    if (command == "PINS")
                    {
                        AddMonitorMessage("Checking Pins" + ": " + value);
                    }

                    if (command == "PROGRESS")
                    {
                        updateProgressBar(value);
                    }

                    else if (command == "FINISHED")
                    {
                        changeButtonText("Start", start_stop_btn);
                        isRunning = false;
                        setApplicationFinished();
                    }
                }
            }
        }



        void getAvailableComPorts()
        {
            ports = SerialPort.GetPortNames();
        }

        private void updateProgressBar(String value)
        {
            progressBar.Value=(int)double.Parse(value);
        }

        private void changeButtonText(String text, Button button)
        {
            button.Text = text;
        }

        private void AddMonitorMessage(String message)
        {
            monitor.Items.Add(message);

            while (monitor.Items.Count > ListMaxSize)
            {
                monitor.Items.RemoveAt(0);
            }
            monitor.TopIndex = monitor.Items.Count - 1;
        }

        private void connectToArduino()
        {
            string selectedPort = comboBoxPort.GetItemText(comboBoxPort.SelectedItem);
            int selectedBaudRate = int.Parse(comboBoxBaudRate.GetItemText(comboBoxBaudRate.SelectedItem));
            port = new SerialPort(selectedPort, 9600, Parity.None, 8, StopBits.One);
            port.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            port.Open();
            connect_btn.Text = "Disconnect";
            enableControls();
            setApplicationConnected();
        }


        private void disconnectFromArduino()
        {
            port.WriteLine("<STOP>");
            port.Close();
            connect_btn.Text = "Connect";
            disableControls();
            setApplicationDisconnect();
            port.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
        }

        private void enableControls()
        {
            start_stop_btn.Enabled = true;
            progressBar.Enabled = true;
        }

        private void disableControls()
        {
            start_stop_btn.Enabled = false;
            progressBar.Enabled = false;
        }

        private void setApplicationConnected()
        {
            monitor.Items.Clear();
            progressBar.Value = 0;
            start_stop_btn.Text = "Start";
            isConnected = true;
            isRunning = false;
            port.DiscardInBuffer();
            port.DiscardOutBuffer();
        }

        private void setApplicationFinished()
        {
            start_stop_btn.Text = "Start";
            isConnected = true;
            isRunning = false;
            port.DiscardInBuffer();
            port.DiscardOutBuffer();
        }

        private void setApplicationStart()
        {
            monitor.Items.Clear();
            progressBar.Value = 0;
            start_stop_btn.Text = "Stop";
            isConnected = true;
            isRunning = true;
        }

        private void initializeUI()
        {
            getAvailableComPorts();

            repetitionsField.Text = "1";

            foreach (string port in ports)
            {
                comboBoxPort.Items.Add(port);
                if (ports[0] != null)
                {
                    comboBoxPort.SelectedItem = ports[0];
                }
            }

            foreach (int baudRate in baudRates)
            {
                comboBoxBaudRate.Items.Add(baudRate);
                comboBoxBaudRate.SelectedItem = baudRates[0];
            }
        }

        private void setApplicationDisconnect()
        {
            monitor.Items.Clear();
            progressBar.Value = 0;
            repetitionsField.Text = "1";
            isConnected = false;
            isRunning = false;

        }
        
    }

    public enum States
    {
        StateWaitingInstruction,
        StateReadingCommand,
        StateReadingValue,
    }

    public enum Transitions
    {
        TransitionOpeningBracket,
        TransitionClosingBracket,
        TransitionColon,
        TransitionUndefinedLetter
    }

    public class SerialParser
    {
        private String[] instruction = new String[2];
        private States state = States.StateWaitingInstruction;
        private Transitions transition = Transitions.TransitionUndefinedLetter;
        private ConcurrentQueue<string[]> intructionQueue = new ConcurrentQueue<string[]>();
        readonly object queueLock = new object();

        public void serialParse(String buffer)
        {
            lock (queueLock)
            {
                foreach (var inChar in buffer)
                {
                    if (inChar == '<')
                    {
                        transition = Transitions.TransitionOpeningBracket;
                    }
                    else if (inChar == '>')
                    {
                        transition = Transitions.TransitionClosingBracket;
                    }
                    else if (inChar == ':')
                    {
                        transition = Transitions.TransitionColon;
                    }
                    else
                        transition = Transitions.TransitionUndefinedLetter;


                    if (state == States.StateWaitingInstruction && transition == Transitions.TransitionOpeningBracket)
                    {
                        state = States.StateReadingCommand;
                        instruction = new string[] { "", "" };
                    }
                    else if (state == States.StateReadingCommand && transition == Transitions.TransitionUndefinedLetter)
                    {
                        instruction[0] += inChar;
                    }
                    else if (state == States.StateReadingCommand && transition == Transitions.TransitionClosingBracket)
                    {
                        state = States.StateWaitingInstruction;
                        intructionQueue.Enqueue(instruction);
                    }
                    else if (state == States.StateReadingCommand && transition == Transitions.TransitionColon)
                    {
                        state = States.StateReadingValue;
                    }
                    else if (state == States.StateReadingValue && transition == Transitions.TransitionUndefinedLetter)
                    {
                        instruction[1] += inChar;
                    }
                    else if (state == States.StateReadingValue && transition == Transitions.TransitionClosingBracket)
                    {
                        state = States.StateWaitingInstruction;
                        intructionQueue.Enqueue(instruction);
                    }
                }
            }
            
        }

        public String getCommand(string[] instruction)
        {
            return instruction[0];
        }
        public String getValue(string[] instruction)
        {
            return instruction[1];
        }
        public string[] getInstruction()
        {
            string[] instruction;
            bool isSuccessful = intructionQueue.TryDequeue(out instruction);
            if (isSuccessful)
                return instruction;
            else
                return null;
        }
        public bool instructionAvailable()
        {
            return !intructionQueue.IsEmpty;
        }

        public void discardBuffer()
        {
            intructionQueue = new ConcurrentQueue<string[]>();
        }
    }

}
