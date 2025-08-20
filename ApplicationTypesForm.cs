using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ApplicationTypesBussinessLayer;

namespace Driver_Licence_Project
{
    public partial class frmApplicationTypes : Form
    {
        public frmApplicationTypes()
        {
            InitializeComponent();
        }

        private void frmApplicationTypes_Load(object sender, EventArgs e)
        {
           dgvApplicationTypes.DataSource = clsApplicationTypes.GetAllApplicationTypes();
            if (dgvApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 50;
                  dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 350;
                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 100;

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApplicationType frm = new EditApplicationType((int)dgvApplicationTypes.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            dgvApplicationTypes.DataSource = clsApplicationTypes.GetAllApplicationTypes();
        }
    }
}
