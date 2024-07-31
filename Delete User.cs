using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;using System.Data.SqlClient;

namespace WAMicroProject
{
    public partial class Delete_User : Form
    {
        public Delete_User()
        {
            InitializeComponent();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        DataSet Ds; SqlDataAdapter Da; DataRow R;
        string Query; SqlCommandBuilder Bldr;
        private void Delete_User_Load(object sender, EventArgs e)
        {
            Query = "Select * from Users";
            Da = new SqlDataAdapter(Query, Constr); Ds = new DataSet();
            Da.Fill(Ds, "Users"); Da.FillSchema(Ds, SchemaType.Source, "Users");
            Bldr = new SqlCommandBuilder(Da);
            dataGridView1.DataSource = Ds.Tables[0];
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            R = Ds.Tables[0].Rows.Find(dataGridView1.SelectedRows[0].Cells[0].Value);
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (R != null)
            {
                R.Delete(); Da.Update(Ds, "Users"); MessageBox.Show("1 User Deleted");
            }
            else MessageBox.Show("Select the Row to Delete the User");
        }
    }
}
