using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FergieChapChat
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(usernameText.Text))
            {
                MessageBox.Show("Please enter a Username");
            }
            else
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void usernameText_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter))
            {
                if (string.IsNullOrEmpty(usernameText.Text))
                {
                    MessageBox.Show("Please enter a Username");
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
