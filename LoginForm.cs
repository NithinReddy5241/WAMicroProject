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
    
    public partial class LoginForm : Form
    {
        public static string user="";
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        SqlConnection Con; SqlCommand Cmd;
        string Query;
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Query = "SELECT COUNT(*) FROM USERS WHERE USERNAME=@P1 AND PASSWORD=@P2"; Con = new SqlConnection(Constr);
            Cmd = new SqlCommand(Query, Con);
            Cmd.CommandType = CommandType.Text;
            Cmd.Parameters.AddWithValue("@P1", textBoxUName.Text);
            Cmd.Parameters.AddWithValue("@P2", textBoxPassword.Text);
            Con.Open(); int R = (int)Cmd.ExecuteScalar(); Con.Close();
            if (R != 0)
            {
                MessageBox.Show($"Welcome Mr/Ms {textBoxUName.Text}");
                LoginForm.user = textBoxUName.Text;

                labelUName.Text =LoginForm.user;
                MDI_Form MDI = new MDI_Form();
                MDI.Show();
            }
            else
            {
                MessageBox.Show("User Not Exit");LoginForm.user = "n";
            }
        }
    }
}
