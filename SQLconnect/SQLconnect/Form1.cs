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

namespace SQLconnect
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            string connectionString = "Data Source=DESKTOP-M7U7OLU;Initial Catalog=sqlWinforms;User ID=Tiffany;Password=abc123";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();

            //string sql = "SELECT * FROM employees";
            string sql = "SELECT empID as ID, empName as Name, empAddress as Address, empPhone as Phone, empEmail as Email, empBirthdate as Birthdate from employees  WHERE empName LIKE '" + txtSearch.Text + "%'";
            SqlCommand command = new SqlCommand(sql, cnn);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, cnn);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dgvNames.ReadOnly = true;
            dgvNames.AllowUserToAddRows = false;
            dgvNames.DataSource = ds.Tables[0];

            if (ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("No results found");
            }

            cnn.Close();
        }

    }
}
