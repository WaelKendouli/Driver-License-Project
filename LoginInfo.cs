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
    public partial class frmLoginInfo : Form
    {
        private int _PersonId;
        public frmLoginInfo(int PersonID)
        {
            InitializeComponent();
            _PersonId = PersonID;
        }

        private void frmLoginInfo_Load(object sender, EventArgs e)
        {
            
            
        }

        private void ctrlPasswordChange1_Load(object sender, EventArgs e)
        {
            ctrlPasswordChange1._PersonID = _PersonId;
            ctrlPasswordChange1.FillInfos();
        }
    }
}
