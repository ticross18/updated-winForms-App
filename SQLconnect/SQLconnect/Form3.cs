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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-M7U7OLU;Initial Catalog=sqlWinforms;User ID=Tiffany;Password=abc123;MultipleActiveResultSets = true";
            SqlConnection cnn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            /* increments and finds lowest ID number available to add the employee under */
            cmd.CommandText = "declare @highest AS INT SET @highest = (select max(empid) from employees) + 1 declare @rowNum AS INT SET @rowNum = 1 "
            + "WHILE ( @rowNum <= @highest ) BEGIN  IF EXISTS(SELECT * FROM EMPLOYEES WHERE empID = @rowNum) SET @rowNum = @rowNum + 1 ELSE BREAK; END " +
            "INSERT INTO EMPLOYEES (empID, empNAME, empADDRESS, empPHONE, empEMAIL, empBIRTHDATE) VALUES ( @rowNum ,'" + txtName.Text + "', '" + txtAddress.Text + "'," + txtPhone.Text + ", '" + txtEmail.Text + "', '" + txtBirthdate.Text + "')";

            try
            {
                cmd.Connection = cnn;
                cnn.Open();
                cmd.ExecuteNonQuery();
                cnn.Close();
                MessageBox.Show("Row Added");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.ToString()); //useful for developer debugging
                //MessageBox.Show("incorrect input");
            }
        }
    }   
}
