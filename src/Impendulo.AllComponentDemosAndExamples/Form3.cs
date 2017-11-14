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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Venue x = new Venue()
            {
                VenueID = 0,
                VenueName = "",
                VenueMaxCapacity = 0
            };

            using (var Dbconnection = new MCDEntities())
            {
                Dbconnection.Venues.Add(x);
                Dbconnection.SaveChanges();
            };
        }
    }
}
