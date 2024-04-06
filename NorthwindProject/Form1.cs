using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; // allows SQL Command to be used 


namespace NorthwindProject
{
    public partial class frmEmployees : Form
    {
        public frmEmployees()
        {
            InitializeComponent();
            loadDGV(); //display data from sql database server to datagridview
            
        }

        private void loadDGV()
        {
            dgvEmployees.Rows.Clear();

            // calls dbConnect class and its function dbConnectOpen() outside the form   
            dbConnect.dbConnectOpen();

            // creates an sql command request to the database
            SqlCommand selectData = new SqlCommand("select employeeid,lastname,firstname from employees ", dbConnect.dbConnectInst);

            // extract the output of the selected data from SQLCommand "selectData" to SQLDataReader "reader"
            SqlDataReader reader = selectData.ExecuteReader();
            selectData.Dispose(); // close SQLCommand "selectData" 
            while (reader.Read()) // while loop to read the contents of SQLDataReader "reader" 
            {
                // transfer the contents of SQLDataReader "reader" to the datagridview in this form 
                dgvEmployees.Rows.Add(reader["employeeid"].ToString(), reader["lastname"].ToString(), reader["firstname"].ToString());
            }
            reader.Close();   // close SQLDataReader "reader"
            reader.Dispose(); // destroy SQLDataReader "reader"

            // calls dbConnect class and its function dbConnectOpen() outside the form, this will close the whole sql connection
            dbConnect.dbConnectClose();          
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // calls dbConnect class and its function dbConnectOpen() outside the form   
            dbConnect.dbConnectOpen();

            // creates an sql command request to the database
            SqlCommand insertData = new SqlCommand("insert into employees(lastname,firstname)select '" + tbLastName.Text + "','" + tbFirstName.Text + "'", dbConnect.dbConnectInst);

            // execute insert statement "insertData"
            insertData.ExecuteNonQuery();

            // calls dbConnect class and its function dbConnectOpen() outside the form, this will close the whole sql connection
            dbConnect.dbConnectClose();

            // restart the datagridview to display the latest record added in the insert statement 
            loadDGV();
        }

        public int EmployeeId = 0; // declare variable for the EmployeeId to be used to update/delete employee data in the employee table 

        private void btnUpdate_Click(object sender, EventArgs e)
        {


            // calls dbConnect class and its function dbConnectOpen() outside the form   
            dbConnect.dbConnectOpen();

            // creates an sql command request to the database
            SqlCommand updateData = new SqlCommand("update e set lastname = '" + tbLastName.Text + "' ,firstname = '" + tbFirstName.Text + "' from employees e where EmployeeId = '" + EmployeeId + "'", dbConnect.dbConnectInst);

            // execute insert statement "insertData"
            updateData.ExecuteNonQuery();

            // calls dbConnect class and its function dbConnectOpen() outside the form, this will close the whole sql connection
            dbConnect.dbConnectClose();

            // restart the datagridview to display the latest record added in the insert statement 
            loadDGV();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // calls dbConnect class and its function dbConnectOpen() outside the form   
            dbConnect.dbConnectOpen();

            // creates an sql command request to the database
            SqlCommand deleteData = new SqlCommand("delete from employees where EmployeeId = '" + EmployeeId + "'", dbConnect.dbConnectInst);

            // execute insert statement "insertData"
            deleteData.ExecuteNonQuery();

            // calls dbConnect class and its function dbConnectOpen() outside the form, this will close the whole sql connection
            dbConnect.dbConnectClose();

            // restart the datagridview to display the latest record added in the insert statement 
            loadDGV();
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e) // if the grid is clicked event
        {
            tbLastName.Text  = dgvEmployees.Rows[e.RowIndex].Cells[1].Value.ToString();       // in this current row it will transfer data from cell 1 to txtbox Last Name
            tbFirstName.Text = dgvEmployees.Rows[e.RowIndex].Cells[2].Value.ToString();       // in this current row it will transfer data from cell 2 to txtbox First Name
            EmployeeId       = Convert.ToInt32(dgvEmployees.Rows[e.RowIndex].Cells[0].Value); // in this current row it will transfer data from cell 2 to txtbox First Name
        }

    }
}
