﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace Impendulo.Development.Enquiry
{
    public partial class frmInitailDocumentation : MetroForm
    {
        public Boolean UseEmail { get; set; }
        public Boolean IsCanceled { get; set; }
        public frmInitailDocumentation()
        {
            InitializeComponent();
            IsCanceled = false;
        }

        private void btnManuallyGiven_Click(object sender, EventArgs e)
        {
            UseEmail = false;
            this.Close();
        }

        private void btnEmailDocumentation_Click(object sender, EventArgs e)
        {
            UseEmail = true;
            this.Close();
        }

        private void frmAprenticeshipInitailDocumentation_Load(object sender, EventArgs e)
        {

        }

        private void frmInitailDocumentation_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsCanceled = true;
            this.Close();
        }
    }
}
