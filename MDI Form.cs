using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAMicroProject
{
    public partial class MDI_Form : Form
    {
        public MDI_Form()
        {
            InitializeComponent();
        }

        private void addItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Add_Item AI = new Add_Item();
            AI.Show();
        }

        private void MDI_Form_Load(object sender, EventArgs e)
        {
            LoginForm L = new LoginForm();
            label1.Text = LoginForm.user;
            menuStrip1.Focus();
        }

        private void deleteItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete_Item DI = new Delete_Item();DI.ShowDialog();
        }

        private void editItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Edit_Item EI = new Edit_Item();EI.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginForm LF = new LoginForm();LF.Close();
            this.Close();
            
        }

       
        private void AddtoolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Create_User CU = new Create_User();
            CU.ShowDialog();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Delete_User DU = new Delete_User();
            DU.ShowDialog();
        }

        private void changeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Change_Password_Form CP = new Change_Password_Form();
            CP.ShowDialog();
        }

        private void viewUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_User VU = new View_User();
            VU.ShowDialog();
        }

        private void newBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bill_Master BM = new Bill_Master();
            BM.ShowDialog();
        }

        private void viewBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            View_Bill VB = new View_Bill();
            VB.ShowDialog();
        }
    }
}
