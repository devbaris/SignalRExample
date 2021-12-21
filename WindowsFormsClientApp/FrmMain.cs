using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsClientApp
{
    public partial class FrmMain : Form
    {
        private HubConnection _hubConnection;

        public FrmMain()
        {
            InitializeComponent();
        }

        private async void btnConnect_Click(object sender, EventArgs e)
        {
            _hubConnection = new HubConnectionBuilder().WithUrl(txtUrl.Text).Build();
            _hubConnection.Closed += _hubConnection_Closed;
            _hubConnection.On<MessageDto>("receiveMessage", result =>
            {
                txtReceive.Text += ("Username: " + result.Username + " --> Message: " + result.Message + "  \r\n");
            });

            try
            {
                await _hubConnection.StartAsync();
                txtUserName.Enabled = false;
                txtUrl.Enabled = false;
            }
            catch (Exception exception)
            {
                txtReceive.Text += exception.Message.ToString();
            }
        }

        private async Task _hubConnection_Closed(Exception arg)
        {
            await _hubConnection.StartAsync();
            txtReceive.Text += arg.Message.ToString();
            txtUserName.Enabled = true;
            txtUrl.Enabled = true;
        }

        private async void BtnSendMessage_Click(object sender, EventArgs e)
        {
            var messageDto = new MessageDto()
            {
                Username = txtUserName.Text,
                Message = txtSendMessage.Text,
                ConnectionId = _hubConnection.ConnectionId,
            };

            await _hubConnection.SendAsync("sendMessage", messageDto);
            txtSendMessage.Text = "";
        }
    }
}