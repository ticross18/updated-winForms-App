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
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            /* shows all rows on opening */
            string connectionString = "Data Source=DESKTOP-M7U7OLU;Initial Catalog=sqlWinforms;User ID=Tiffany;Password=abc123";
            SqlConnection cnn = new SqlConnection(connectionString);
            cnn.Open();

            string sql = "SELECT * FROM employees";
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            /* reused code from search employee so user can select from their custom list */
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

        private void btnDelete_Click(object sender, EventArgs e)
        {

            if (dgvNames.SelectedRows != null)
            {
                int rowSelected = (dgvNames.CurrentCell.RowIndex + 1);

                string connectionString = "Data Source=DESKTOP-M7U7OLU;Initial Catalog=sqlWinforms;User ID=Tiffany;Password=abc123";
                SqlConnection cnn = new SqlConnection(connectionString);


                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "DELETE FROM EMPLOYEES WHERE empID = " + rowSelected;
                try
                {
                    cmd.Connection = cnn;
                    cnn.Open();
                    cmd.ExecuteNonQuery();
                    cnn.Close();
                    MessageBox.Show("Row Deleted");
                    btnSearch.PerformClick();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.ToString()); //useful for developer debugging
                    //MessageBox.Show("Incorrect Selection");
                }

            }
        }
    }
}
