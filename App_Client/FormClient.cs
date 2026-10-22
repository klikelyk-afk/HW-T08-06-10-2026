using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace App_Client
{
    public partial class FormClient : Form
    {
        private NamedPipeClientStream pipeClient;
        private string clientId;

        public FormClient()
        {
            InitializeComponent();
            clientId = "Client_" + new Random().Next(1000, 9999);
            this.Text = $"Client - {clientId}";
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            Task.Run(() => Connect());
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            Task.Run(() => Send(textBoxOutput.Text));
        }

        private void Connect()
        {
            try
            {
                buttonConnect.Invoke(new Action(() => buttonConnect.Enabled = false));

                pipeClient = new NamedPipeClientStream(".", "Server511", PipeDirection.InOut, PipeOptions.Asynchronous);
                pipeClient.Connect(2000);

                Debug.WriteLine($"Client connected.");

                Send($"ID:{clientId}");

                Task.Run(() => Receive());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Connection error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                buttonConnect.Invoke(new Action(() => buttonConnect.Enabled = true));
            }
        }

        private void Receive()
        {
            try
            {
                while (pipeClient != null && pipeClient.IsConnected)
                {
                    byte[] buffer = new byte[256];
                    int bytesRead = pipeClient.Read(buffer, 0, buffer.Length);

                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    if (message.Trim() == "Ping")
                    {
                        Send("Pong");
                        textBoxInput.Invoke(new Action(() => textBoxInput.Text += "[Server]: Ping -> Auto-sent Pong" + Environment.NewLine));
                    }
                    else
                    {
                        textBoxInput.Invoke(new Action(() => textBoxInput.Text += message + Environment.NewLine));
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error while receiving: {ex.Message}");
            }
        }

        private void Send(string message)
        {
            try
            {
                if (pipeClient != null && pipeClient.IsConnected && !string.IsNullOrEmpty(message))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(message);
                    pipeClient.Write(bytes, 0, bytes.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while sending: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}