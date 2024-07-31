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
    public partial class Delete_Item : Form
    {
        public Delete_Item()
        {
            InitializeComponent();
        }
        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        SqlDataAdapter Da; string Query; DataSet Ds; DataRow R; SqlCommandBuilder Bildr;
        private void Delete_Item_Load(object sender, EventArgs e)
        {
            Query = "Select * from Items";
            Da = new SqlDataAdapter(Query, Constr);
            Ds = new DataSet(); Da.Fill(Ds, "Items");
            Da.FillSchema(Ds, SchemaType.Source, "Items");
            Bildr = new SqlCommandBuilder(Da);
            dataGridViewItems.DataSource = Ds.Tables["Items"];
            textBoxItem.Enabled = false;
            textBoxPrice.Enabled = false;
        }

        private void dataGridViewItems_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            R = Ds.Tables[0].Rows.Find(dataGridViewItems.SelectedCells[0].Value);
            textBoxItem.Text = R[0].ToString();
            textBoxPrice.Text = R[1].ToString();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {  
            if (Ds.HasChanges())
            {
                Bildr = new SqlCommandBuilder(Da);
                Da.Update(Ds,"Items");
                
                MessageBox.Show("Data Saved to DataBase");
            }
            else MessageBox.Show("No Changes have been made to update the DataBase");
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (textBoxItem.TextLength == 0)
            {
                MessageBox.Show("Select the Item to Delete");
            }
            else
            {
                R = Ds.Tables[0].Rows.Find(textBoxItem.Text);
                MessageBox.Show(textBoxItem.Text + " Item is deleted Sucessfully"); R.Delete();
            }
        }
    }
}
