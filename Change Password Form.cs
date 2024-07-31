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
    public partial class Change_Password_Form : Form
    {
        public Change_Password_Form()
        {
            InitializeComponent();
        }
        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        SqlConnection Con; SqlCommand Cmd;
        SqlDataReader Dr; string Query;
        private void labelHintA_Click(object sender, EventArgs e)
        {

        }

        public string user { set; get; }
        private void radioButtonHintQ_CheckedChanged(object sender, EventArgs e)
        {
            comboBoxHintQ.Visible = true;
            Query = $"Select HintQuestion from Users where Username='{LoginForm.user}'";
            Con = new SqlConnection(Constr);
            Cmd = new SqlCommand(Query, Con);
            Cmd.CommandType = CommandType.Text;
            Con.Open();Dr=Cmd.ExecuteReader();
            while (Dr.Read())
            {
                comboBoxHintQ.Items.Add(Dr[0]);
            }Con.Close();
            labelHintA.Visible = true;
            labelSelectHintQ.Visible = true;
            textBoxHintA.Visible = true;
            textBoxOldP.Visible = false;
            labelOldP.Visible = false;
        }


        private void Change_Password_Form_Load(object sender, EventArgs e)
        {
            textBoxOldP.Visible = true;
            labelOldP.Visible= true;
            labelHintA.Visible = false;
            comboBoxHintQ.Visible = false;
            labelSelectHintQ.Visible = false;
            textBoxHintA.Visible = false;
            LoginForm LG = new LoginForm();
            label1.Text = LoginForm.user;
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioButtonOldPassword_CheckedChanged(object sender, EventArgs e)
        {
            labelHintA.Visible = false;
            comboBoxHintQ.Visible = false;
            labelSelectHintQ.Visible = false;
            textBoxHintA.Visible = false;
            labelOldP.Visible = true;
            textBoxOldP.Visible = true;
        }
        public bool Check()
        {
            if (textBoxNewP.Text.Equals(textBoxReEnterP.Text)) return true;
            else return false;
        }
        public void updateH()
        {
            Query = $"Update Users set Password='{textBoxReEnterP.Text}' where username='{LoginForm.user}'";
            Con = new SqlConnection(Constr);
            Cmd = new SqlCommand(Query, Con);
            Cmd.CommandType = CommandType.Text;
            Con.Open(); Cmd.ExecuteNonQuery();Con.Close();
           MessageBox.Show($"Password Sucessfully Changed");
        }

        private void buttonChangeP_Click(object sender, EventArgs e)
        {       
            Con = new SqlConnection(Constr);
            
            if (radioButtonHintQ.Checked)
            {
                Query = "SELECT COUNT(*) FROM USERS WHERE HintQuestion=@P1 AND HintAnswer=@P2";
                Cmd = new SqlCommand(Query, Con);
                Cmd.CommandType = CommandType.Text;                 
                Cmd.Parameters.AddWithValue("@P1", comboBoxHintQ.Text);
                Cmd.Parameters.AddWithValue("@P2", textBoxHintA.Text);
                Con.Open(); int R = (int)Cmd.ExecuteScalar(); Con.Close();
                if (R != 0)
                {                    
                    if (Check()) updateH();
                    else MessageBox.Show("Entered Password and ReEntered Password shoud be equal");
                }
                else MessageBox.Show("Wrong Answer.Try Again");
            }
            if (radioButtonOldPassword.Checked)
            {
                Query = $"Select Count(*) from users where username='{LoginForm.user}' and password='{textBoxOldP.Text}'";
                Cmd = new SqlCommand(Query, Con);
                Cmd.CommandType = CommandType.Text;
                Con.Open();int R=(int)Cmd.ExecuteScalar();Con.Close();
                if (R != 0)
                {
                    if (Check()) updateH();
                    else MessageBox.Show("Entered Password and ReEntered Password shoud be equal");                    
                }
                else MessageBox.Show("Wrong Password.Try Again");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            foreach (Control item in Controls)
            {
                if (item is TextBox) item.Text = "";
            }
        }
    }
}
