﻿using ShopLibrary;
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
    public partial class FormAddGenre : Form
    {
        private readonly Genre genre;

        public FormAddGenre(Genre genre)
        {
            InitializeComponent();
            this.genre = genre;

            txtName.Text = genre.Name;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            genre.Name = txtName.Text;
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult=DialogResult.Cancel;
        }
    }
}