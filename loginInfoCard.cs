using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsersBussinessLayer;

namespace Driver_Licence_Project
{
    public partial class loginInfoCard : UserControl
    {
        private int _PersonID;
        public loginInfoCard()
        {
            InitializeComponent();
        }

        private void loginInfoCard_Load(object sender, EventArgs e)
        {
            ucPersonInformationCard1.LoadInfosCard(_PersonID);
            clsUser User = clsUser.FindUserByPersonID(_PersonID);
            labelId.Text = User.UserID.ToString();
            labelUsername.Text = User.UserName;
            if (User.isActive == 0)
            {
                labelActive.Text = "No";
            }
            else
            {
                labelActive.Text = "Yes";
            }
        }
    }
}
