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
    public partial class Add_Item : Form
    {
        public Add_Item()
        {
            InitializeComponent();
        }
        string Constr = @"Data Source=LAPTOP-ARS6P22C;Integrated Security=True; database=MicroProject";
        SqlDataAdapter Da; string Query; DataSet Ds;DataRow R;SqlCommandBuilder Bildr;
        private void Add_Item_Load(object sender, EventArgs e)
        {
            Query = "Select * from Items";
            Da = new SqlDataAdapter(Query, Constr);
            Ds = new DataSet();Da.Fill(Ds, "Items");
            Da.FillSchema(Ds, SchemaType.Source, "Items");
            Bildr = new SqlCommandBuilder(Da);
            dataGridViewItems.DataSource = Ds.Tables["Items"];
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            R = Ds.Tables["Items"].NewRow();
            if ((textBoxPrice.TextLength == 0) || (textBoxItem.TextLength == 0)  )
            {
                MessageBox.Show("Item or Price is empty! Cannot add empty ");
            }
            else
            {

                R[0] = textBoxItem.Text;
                R[1] = Convert.ToDouble(textBoxPrice.Text);
                Ds.Tables["Items"].Rows.Add(R);
                MessageBox.Show(textBoxItem.Text + " Items is Added Successfully!");
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (Ds.HasChanges())
            {
                Da.Update(Ds, "Items");
                MessageBox.Show("Data Saved to DataBase");
            }
            else MessageBox.Show("No Changes have been made to update the DataBase");
        }
    }
}
