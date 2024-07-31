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
    public partial class Create_User : Form
    {
        public Create_User()
        {
            InitializeComponent();
        }
        //string HintQuestion = "Favorite Question?";
        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        SqlConnection Con; SqlCommand Cmd;
        SqlDataReader Dr; string Query;
      
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Create_User_Load(object sender, EventArgs e)
        {
            Query = "Select HintQuestion from Users";
            Con = new SqlConnection(Constr);
            Cmd = new SqlCommand(Query, Con);Cmd.CommandType = CommandType.Text;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            comboBoxHintQ.Items.Clear();
            this.Close();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            comboBoxHintQ.Items.Clear();
            foreach (Control C in Controls)
            {
                if (C is TextBox) C.Text = "";
            }
        }
        private bool Check()
        {
            bool F=true;
            foreach (Control C in this.Controls)
            {
                if(C is TextBox)
                {
                    TextBox T=(TextBox)C;
                    if (T.TextLength == 0 && comboBoxHintQ.Text.Length==0)
                    {
                        F = false; break;
                    }
                }
            }
            return F;
        }
        private bool CheckP()
        {
            if (textBoxConfirmP.Equals(textBoxPassword.Text)) return true;
            else return false;
        }
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            if (this.Check())
            {
                if (this.CheckP())
                {
                    Query = "Insert into Users values(@P1,@P2,@P3,@P4,@P5,@P6)";
                    Con = new SqlConnection(Constr);
                    Cmd = new SqlCommand(Query, Con); Cmd.CommandType = CommandType.Text;
                    Cmd.Parameters.AddWithValue("P1", textBoxUsername.Text);
                    Cmd.Parameters.AddWithValue("P2", textBoxPassword.Text);
                    Cmd.Parameters.AddWithValue("P3", textBoxFName.Text);
                    Cmd.Parameters.AddWithValue("P4", textBoxLName.Text);
                    
                    Cmd.Parameters.AddWithValue("P5", comboBoxHintQ.Text);
                    Cmd.Parameters.AddWithValue("P6", textBoxHintA.Text);
                    Con.Open(); int R = Cmd.ExecuteNonQuery(); Con.Close();
                    MessageBox.Show("User created");
                }
                else
                {
                    MessageBox.Show("Enter Password and Confirm Password Should be same");
                }
            }
            else
                MessageBox.Show("Entering All Fields are Mandatory!");

        }
    }
}
