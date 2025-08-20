using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UsersBussinessLayer;

namespace Driver_Licence_Project
{
    public partial class ctrlPasswordChange : UserControl
    {
        public int _PersonID { get; set; }
        public ctrlPasswordChange()
        {
            InitializeComponent();
        }

        public void FillInfos()
        {
            ucPersonInformationCard1.LoadInfosCard(_PersonID);
            clsUser User = clsUser.FindUserByPersonID(_PersonID);
            lbUserName.Text = User.UserID.ToString();
            label3.Text = User.UserName;
            if (User.isActive == 0)
            {
                label5.Text = "No";
            }
            else
            {
                label5.Text = "Yes";
            }
        }
        private void ctrlPasswordChange_Load(object sender, EventArgs e)
        {
            FillInfos();
        }
    }
}
