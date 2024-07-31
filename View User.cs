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
    public partial class View_User : Form
    {
        public View_User()
        {
            InitializeComponent();
        }
        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        DataSet Ds; SqlDataAdapter Da; DataRow R;
        SqlDataReader Dr; string Query; SqlCommandBuilder Bldr;
        private void dataload()
        {
            Query = $"Select * from Users where username='{LoginForm.user}'";
            Da = new SqlDataAdapter(Query, Constr);Ds = new DataSet();
            Da.Fill(Ds, "Users");Da.FillSchema(Ds, SchemaType.Source, "Users");
            dataGridView1.DataSource = Ds.Tables[0];
        }
        private void View_User_Load(object sender, EventArgs e)
        {
            dataload();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
