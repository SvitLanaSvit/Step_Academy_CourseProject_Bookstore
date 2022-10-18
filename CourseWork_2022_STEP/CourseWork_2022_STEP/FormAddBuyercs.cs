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
    public partial class FormAddBuyercs : Form
    {
        private readonly Buyer buyer;

        public FormAddBuyercs(Buyer buyer)
        {
            InitializeComponent();
            this.buyer = buyer;

            txtFirstname.Text = buyer.Name;
            txtLastname.Text = buyer.Surname;
            txtEMail.Text = buyer.E_Mail;
            txtTelNumber.Text = buyer.TelNummer;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            buyer.Name = txtFirstname.Text;
            buyer.Surname = txtLastname.Text;
            buyer.E_Mail = txtEMail.Text;
            buyer.TelNummer = txtTelNumber.Text;
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult= DialogResult.Cancel;
        }
    }
}