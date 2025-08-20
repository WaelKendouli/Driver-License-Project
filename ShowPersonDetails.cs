using PeopleBussinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driver_Licence_Project
{
    public partial class frmShowPersonDetails : Form
    {
        public frmShowPersonDetails()
        {
            InitializeComponent();
        }
        public frmShowPersonDetails(int PersonID)
        {
            InitializeComponent();
            ucPersonInformationCard1.LoadInfosCard(PersonID);
        }
        public frmShowPersonDetails( ref clsPerson Person)
        {
            InitializeComponent();
            ucPersonInformationCard ucPersonInformationCard1 = new ucPersonInformationCard(ref Person);
            
        }

        private void ucPersonInformationCard1_Load(object sender, EventArgs e)
        {

        }

        private void frmShowPersonDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
