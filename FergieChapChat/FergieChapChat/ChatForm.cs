using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChapNet;

namespace FergieChapChat
{
    public partial class ChatForm : Form, IChapChatReceiver
    {
        delegate void MessageDelegate(string message);
        delegate int AddUsernameDelegate(object username);
        delegate void RemoveUsernameDelegate(object username);
        LoginForm loginForm;
        ChapChatNetDriver netDriver;
        public ChatForm()
        {
            InitializeComponent();

            loginForm = new LoginForm();
            DialogResult result = loginForm.ShowDialog();
            netDriver = new ChapChatNetDriver(this);
            onlineUsers.Items.Add(loginForm.usernameText.Text);
            netDriver.OnUserConnect = AddUsername;
            netDriver.OnUserDisconnect = RemoveUsername;
            netDriver.SendConnect();
        }

        private void message_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.Shift && e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(messageBox.Text))
                {
                    netDriver.SendMessage(messageBox.Text);
                    chatLog.Text += FormatChatString(GetUsername(), messageBox.Text);
                    messageBox.Text = "";
                }
                e.SuppressKeyPress = true;
            }
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(messageBox.Text))
            {
                netDriver.SendMessage(messageBox.Text);
                chatLog.Text  += FormatChatString(GetUsername(), messageBox.Text);
                messageBox.Text = "";
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            messageBox.Text = "";
        }

        public void OnReceiveMessage(string sender, string receivedMessage)
        {
            this.Invoke(new MessageDelegate(chatLog.AppendText), 
                FormatChatString(sender, receivedMessage));

            if (!onlineUsers.Items.Contains(sender))
            {
                this.Invoke(new AddUsernameDelegate(onlineUsers.Items.Add), sender);
            }
        }

        public string GetUsername()
        {
            return loginForm.usernameText.Text;
        }
        
        public void AddUsername(string username)
        {
            if (!onlineUsers.Items.Contains(username))
            {
                this.Invoke(new AddUsernameDelegate(onlineUsers.Items.Add), username);
            }
        }

        public void RemoveUsername(string username)
        {
            if (onlineUsers.Items.Contains(username))
            {
                this.Invoke(new RemoveUsernameDelegate(onlineUsers.Items.Remove),
                    username);
            }
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            netDriver.SendDisconnect();
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            if(loginForm.DialogResult == DialogResult.Cancel)
            {
                this.Close();
            }
        }

        public string FormatChatString(string username, string message)
        {
            string formattedString = "[" + username + "] ";
            formattedString += DateTime.Now.ToString("h:mm tt");
            return formattedString+= " " + message + Environment.NewLine;
        }
    }
}
