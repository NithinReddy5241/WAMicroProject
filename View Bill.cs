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
    public partial class View_Bill : Form
    {
        public View_Bill()
        {
            InitializeComponent();
        }
        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        DataSet Ds;
        SqlDataAdapter Da;
        string Query;SqlDataReader Dr;
        //SqlCommandBuilder Bldr;
        SqlConnection Con; SqlCommand Cmd;
        private void View_Bill_Load(object sender, EventArgs e)
        {
            Query = "Select BillNumber from BillMaster";
            Con = new SqlConnection(Constr);
            Cmd = new SqlCommand(Query, Con);
            Cmd.CommandType = CommandType.Text;
            Con.Open();Dr=Cmd.ExecuteReader();
            while (Dr.Read())
            {
                comboBox1.Items.Add(Dr[0]);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Ds = new DataSet();
            Query = $"Select * from BillMaster where BillNumber={comboBox1.SelectedItem};Select * from BillTrans where BillNumber={comboBox1.SelectedItem}";
            Da = new SqlDataAdapter(Query, Constr);
            Da.Fill(Ds, "Bill");
            dataGridView1.DataSource = Ds.Tables[0];            
            dataGridView2.DataSource = Ds.Tables[1];
        }
    }
}
