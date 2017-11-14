using Impendulo.Data.Models;
using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inpendulo.Development.Files.Files.StudentFiles
{
    public partial class frmStudentFiles : MetroForm
    {
        public frmStudentFiles()
        {
            InitializeComponent();
        }

        private void frmStudentFiles_Load(object sender, EventArgs e)
        {

            using (var Dbconnection = new MCDEntities())
            {
                studentBindingSource.DataSource = (from a in Dbconnection.Students
                                                   select a).ToList();
            };
        }
    }
}
