using Impendulo.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Impendulo.AllComponentDemosAndExamples
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<DateTime> PublicHolidays = new List<DateTime>();

            using (var Dbconnection = new MCDEntities())
            {
                PublicHolidays = (from a in Dbconnection.PublicHolidays
                                  select a.PublicHolidayDate).ToList<DateTime>();
            };

            if (PublicHolidays.Contains(dateTimePicker1.Value.Date))
            {
                MessageBox.Show("Is Public Holiday");
            }
            else
            {
                MessageBox.Show("Is NOT NOT NOT Public Holiday!!!!!!!!");
            }
        }
    }
}
