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
    public partial class frmDriverCard : Form
    {
        private int App_ID;
        public frmDriverCard(int LDLA_ApplicationID)
        {
            InitializeComponent();
            App_ID = LDLA_ApplicationID;
        }

        private void frmDriverCard_Load(object sender, EventArgs e)
        {
            driversInfo1.LoadDriversCardInfo(App_ID);
        }
    }
}
