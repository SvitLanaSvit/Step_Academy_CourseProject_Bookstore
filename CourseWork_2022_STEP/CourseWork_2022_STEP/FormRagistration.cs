using ShopLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork_2022_STEP
{
    public partial class FormRagistration : Form
    {
        private readonly Login login;

        public FormRagistration(Login login)
        {
            InitializeComponent();
            this.login = login;
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //if (txtLoginName.Text != "" && txtPassword.Text != "" && txtConfirmPassword.Text != "")
            {
                if (string.Equals(txtConfirmPassword.Text, txtPassword.Text))
                {
                    login.Username = txtLoginName.Text;
                    login.Password = txtPassword.Text;
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Enter confirm password!");
                    txtConfirmPassword.Text = "";
                }
            }
            //else
            //{
            //    MessageBox.Show("Enter all fields!");
            //}
        }

        private void btnLoginNow_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormLogin login = new FormLogin();
            login.ShowDialog();
        }

        private void txtLoginName_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty( txtLoginName.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                btnRegister.Enabled = true;
            }
            else
            {
                btnRegister.Enabled = false;
            }
        }

        private void FormRagistration_Load(object sender, EventArgs e)
        {
            btnRegister.Enabled = false;
        }
    }
}
