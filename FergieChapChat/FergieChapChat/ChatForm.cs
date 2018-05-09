using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChapNet;

namespace FergieChapChat
{
    public partial class ChatForm : Form, IChapChatReceiver
    {
        delegate void MessageDelegate(string username, string message);
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
                    WriteToLog(GetUsername(), messageBox.Text);
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
                WriteToLog(GetUsername(), messageBox.Text);
                messageBox.Text = "";
            }
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            messageBox.Text = "";
        }

        public void OnReceiveMessage(string sender, string receivedMessage)
        {
            this.Invoke(new MessageDelegate(WriteToLog), 
                sender, receivedMessage);

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
            LoadLog();
        }


        public void WriteToLog(string username, string message)
        {
            string formattedString = "[" + username + "] ";
            formattedString += DateTime.Now.ToString("h:mm tt");
            formattedString += " " + message;

            chatLog.Text += formattedString + Environment.NewLine;

            string path = @"chatLog.txt";
            try
            {
                using (StreamWriter writer = File.AppendText(path))
                {
                    writer.WriteLine(formattedString);
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You don't have access for the file you're trying " +
                    "to write to");
            }
            catch(ArgumentException)
            {
                MessageBox.Show("The path is invalid");
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("The path you've used is too long");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("The directory your file is in was not found");
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Your path is in an invalid format");
            }
        }

        public void LoadLog()
        {

            try
            {
                using(StreamReader reader = File.OpenText(@"chatlog.txt"))
                {
                    string line;
                    while((line = reader.ReadLine()) != null)
                    {
                        chatLog.AppendText(line + Environment.NewLine);
                    }
                }
            }
            catch (UnauthorizedAccessException)
            {
                MessageBox.Show("You don't have access for the file you're trying " +
                    "to write to");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("The path is invalid");
            }
            catch (PathTooLongException)
            {
                MessageBox.Show("The path you've used is too long");
            }
            catch (DirectoryNotFoundException)
            {
                MessageBox.Show("The directory your file is in was not found");
            }
            catch (NotSupportedException)
            {
                MessageBox.Show("Your path is in an invalid format");
            }
        }
    }
}
