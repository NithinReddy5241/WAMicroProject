using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAMicroProject
{
    public partial class Bill_Master : Form
    {
        public Bill_Master()
        {
            InitializeComponent();
        }
        public static int BillN;
        private void Bill_Master_Load(object sender, EventArgs e)
        {
            textBoxBAmount.Text = BillAmount.ToString();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (Control  C in Controls)
            {
                if (C is TextBox) C.Text = "";
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public static double BillAmount=0;
        private void buttonAddItem_Click(object sender, EventArgs e)
        {         
            Bill_Master.BillN=Convert.ToInt32( textBoxBillN.Text);
            Bill_Trans BT = new Bill_Trans();
            BT.ShowDialog();

        }

        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        string Query;
        SqlConnection Con;SqlCommand Cmd;
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (textBoxBAmount.Text.Equals("0"))
            {
                MessageBox.Show("Add Items to Generate Bill. If Already Added Please Click on Refresh button");
            }
            else if (textBoxBillDate.Text.Length == 0) MessageBox.Show("Enter the Date");
            else
            {
                Query = "Insert into BillMaster values(@P1,@P2,@P3,@P4,@P5,@P6,@P7)";
                Con = new SqlConnection(Constr);
                Cmd = new SqlCommand(Query, Con); Cmd.CommandType = CommandType.Text;
                Cmd.Parameters.AddWithValue("@P1", textBoxBillN.Text);
                DateTime date = Convert.ToDateTime(textBoxBillDate.Text);
                Cmd.Parameters.AddWithValue("@P2", date);
                Cmd.Parameters.AddWithValue("@P3", Bill_Master.BillAmount);
                Cmd.Parameters.AddWithValue("@P4", GST);
                Cmd.Parameters.AddWithValue("@P5", textBoxDiscount.Text);
                Cmd.Parameters.AddWithValue("@P6", textBoxTotalBill.Text);
                Cmd.Parameters.AddWithValue("@P7", LoginForm.user);
                Con.Open(); Cmd.ExecuteNonQuery(); Con.Close();
                MessageBox.Show("New Bill Generated Sucessfully");
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Bill_Master_Load(sender, e);
        }
        double TotalBill, GST,Discount;

        private void textBoxDiscount_TextChanged(object sender, EventArgs e)
        {
            Discount = Convert.ToDouble(textBoxDiscount.Text);
            textBoxTotalBill.Text = (TotalBill - Discount).ToString();
        }

        private void textBoxBAmount_TextChanged(object sender, EventArgs e)
        {
            textBoxDiscount.Text = 0.ToString();
            textBoxSGST.Text = (Bill_Master.BillAmount * 0.3).ToString();
            textBoxCGST.Text = (Bill_Master.BillAmount * 0.3).ToString();
            GST=2* (Bill_Master.BillAmount * 0.3);
            Discount =Convert.ToDouble( textBoxDiscount.Text);
            TotalBill = BillAmount + GST;
            textBoxTotalBill.Text = TotalBill.ToString();
        }
    }
}
