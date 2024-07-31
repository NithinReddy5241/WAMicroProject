using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WAMicroProject
{
    public partial class Bill_Trans : Form
    {
        public Bill_Trans()
        {
            InitializeComponent();
        }
        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        DataSet Ds; SqlDataAdapter Da; DataRow R;
        string Query; SqlCommandBuilder Bldr;
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1 || numericUpDown1.Value == 0)
            {
                MessageBox.Show("Select the Item from the DropDown List or Set Quantity");
            }
            else
            {
                R = Ds.Tables[0].NewRow();
                R[0] = Bill_Master.BillN.ToString(); R[1] = comboBox1.SelectedItem.ToString();
                R[2] = textBoxPrice.Text; R[3] = numericUpDown1.Value;
                R[4] = textBoxTotalPrice.Text; Bill_Master.BillAmount += Convert.ToInt32(textBoxTotalPrice.Text);
                Ds.Tables[0].Rows.Add(R); Bldr = new SqlCommandBuilder(Da);
                Da.Update(Ds, "BillTrans"); numericUpDown1.Value = 0;
            }
        }
        private void Bill_Trans_Load(object sender, EventArgs e)
        {
            Query = $"Select * from BillTrans where BillNumber='{Bill_Master.BillN}';Select * from Items";Ds = new DataSet();
            Da = new SqlDataAdapter(Query, Constr);Da.Fill(Ds, "BillTrans");
            Da.FillSchema(Ds, SchemaType.Source, "BillTrans");
            dataGridView1.DataSource = Ds.Tables[0];
            int count = Ds.Tables[1].Rows.Count;
            for (int i = 0; i < count; i++)
            {
                string st = Ds.Tables[1].Rows[i][0].ToString();
                comboBox1.Items.Add(st);
            }

        }
        double Price;Double TotalPrice;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet DsC = new DataSet();
            Query = $"select * from items Where Itemname='{comboBox1.SelectedItem.ToString()}'";
            SqlDataAdapter DaC = new SqlDataAdapter(Query, Constr);DaC.Fill(DsC, "Items");
            DaC.FillSchema(DsC, SchemaType.Source, "Items");
           // Bldr = new SqlCommandBuilder(DaC);
            textBoxPrice.Text = DsC.Tables[0].Rows[0][1].ToString();
            Price = Convert.ToInt32(DsC.Tables[0].Rows[0][1]);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            TotalPrice = Convert.ToInt32(numericUpDown1.Value) * Price;
            textBoxTotalPrice.Text = TotalPrice.ToString();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
            
        }
    }
}
