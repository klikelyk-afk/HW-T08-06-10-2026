using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;

namespace App_Server
{
    public partial class FormServer : Form
    {
        public class ClientInfo
        {
            public string Id { get; set; } = "Unknown";
            public NamedPipeServerStream Stream { get; set; }
        }

        private ConcurrentBag<NamedPipeServerStream> allServers = new();
        // List<NamedPipeServerStream> Add/Remove (thread safe usage)
        //NamedPipeServerStream

        private List<ClientInfo> connectedClients = new();
        private System.Windows.Forms.Timer pingTimer;

        public FormServer()
        {
            InitializeComponent();

            pingTimer = new System.Windows.Forms.Timer();
            pingTimer.Interval = 5000;
            pingTimer.Tick += PingTimer_Tick;
            pingTimer.Start();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            // Task + Run() -> delegate -> Equals to controlled Thread

            //Task.Run(delegate () { StartPipeServer(); });
            Task.Run(() => StartPipeServer()); // StartPipeServer method will be started in a different Thread
            // delegate to lambdas:"delegate" -> " => "
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            string message = textBoxOutput.Text;
            if (string.IsNullOrEmpty(message)) return;

            if (listBoxClients.SelectedIndex != -1 && listBoxClients.SelectedItem?.ToString() != "All clients")
            {
                SendToSingleClient(listBoxClients.SelectedItem.ToString(), message);
            }
            else
            {
                SendToAllWithMessage(message);
            }

            textBoxOutput.Clear();
        }

        private void StartPipeServer()
        {
            while (true)
            {
                var server = new NamedPipeServerStream("Server511",
                    PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances, PipeTransmissionMode.Byte, PipeOptions.Asynchronous);
                server.WaitForConnection();
                //
                allServers.Add(server);

                Task.Run(() => HandleClient(server));
            }
        }

        private void HandleClient(NamedPipeServerStream server)
        {
            ClientInfo clientInfo = new ClientInfo { Stream = server };

            try
            {
                while (server.IsConnected)
                {
                    Debug.WriteLine($"Server is waiting for data.");

                    byte[] buffer = new byte[256];
                    int bytesRead = server.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);

                    Debug.WriteLine($"Client: {message}" + Environment.NewLine);

                    if (message.StartsWith("ID:"))
                    {
                        clientInfo.Id = message.Replace("ID:", "");
                        lock (connectedClients)
                        {
                            connectedClients.Add(clientInfo);
                        }
                        UpdateClientList();
                        AppendLog($"[System]: {clientInfo.Id} connected" + Environment.NewLine);
                    }
                    else if (message.Trim() == "Pong")
                    {
                        AppendLog($"[{clientInfo.Id}]: Pong" + Environment.NewLine);
                    }
                    else
                    {
                        AppendLog($"[{clientInfo.Id}]: {message}" + Environment.NewLine);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while connecting: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                lock (connectedClients)
                {
                    connectedClients.Remove(clientInfo);
                }
                UpdateClientList();
                AppendLog($"[System]: {clientInfo.Id} disconnected" + Environment.NewLine);

                allServers.TryTake(out server!);
                server?.Dispose();
            }
        }

        private void Send(NamedPipeServerStream server)
        {
            string message = textBoxOutput.Text;

            if (!string.IsNullOrEmpty(message))
            {
                byte[] bytes = Encoding.UTF8.GetBytes(message);
                server.Write(bytes, 0, bytes.Length);
            }
        }

        private void SendToAll()
        {
            // Parallel.ForEach(allServers, s => Send(s));

            if (!allServers.IsEmpty)
            {
                NamedPipeServerStream server1 = allServers.First();
                foreach (NamedPipeServerStream server in allServers)
                {
                    Send(server);
                }
            }
        }

        private void PingTimer_Tick(object sender, EventArgs e)
        {
            byte[] bytes = Encoding.UTF8.GetBytes("Ping");
            lock (connectedClients)
            {
                foreach (var client in connectedClients)
                {
                    if (client.Stream.IsConnected)
                    {
                        try { client.Stream.Write(bytes, 0, bytes.Length); } catch { }
                    }
                }
            }
        }

        private void SendToSingleClient(string clientId, string message)
        {
            lock (connectedClients)
            {
                foreach (var client in connectedClients)
                {
                    if (client.Id == clientId && client.Stream.IsConnected)
                    {
                        byte[] bytes = Encoding.UTF8.GetBytes($"[Server -> {client.Id}]: {message}");
                        client.Stream.Write(bytes, 0, bytes.Length);
                        AppendLog($"[Server -> {client.Id}]: {message}" + Environment.NewLine);
                        break;
                    }
                }
            }
        }

        private void SendToAllWithMessage(string message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes($"[Server]: {message}");
            lock (connectedClients)
            {
                foreach (var client in connectedClients)
                {
                    if (client.Stream.IsConnected)
                    {
                        try { client.Stream.Write(bytes, 0, bytes.Length); } catch { }
                    }
                }
            }
            AppendLog($"[Server -> All]: {message}" + Environment.NewLine);
        }

        private void UpdateClientList()
        {
            if (listBoxClients.InvokeRequired)
            {
                listBoxClients.Invoke(new Action(UpdateClientList));
                return;
            }

            listBoxClients.Items.Clear();
            listBoxClients.Items.Add("All clients");

            lock (connectedClients)
            {
                foreach (var client in connectedClients)
                {
                    listBoxClients.Items.Add(client.Id);
                }
            }
        }

        private void AppendLog(string text)
        {
            if (textBoxInput.InvokeRequired)
            {
                textBoxInput.Invoke(new Action(() => textBoxInput.AppendText(text)));
                return;
            }
            textBoxInput.AppendText(text);
        }
    }
}