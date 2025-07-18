using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace EmployeeWinFromsApp
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Server=SKEMPIRE;Database=CompanyDB;Integrated Security=True;TrustServerCertificate=True");

        public Form1()
        {
            InitializeComponent();
            con.Open();

            btnAdd.Click += (s, e) => AddEmployee(con);
            btnView.Click += (s, e) => ViewEmployee(con);
            btnUpdate.Click += (s, e) => UpdateEmployee(con);
            btnDelete.Click += (s, e) => DeleteEmployee(con);
            btnSearch.Click += (s, e) => SearchByRole(con);

            this.FormClosed += (s, e) => con.Close(); 
        }

        void AddEmployee(SqlConnection con)
        {
            string name = txtName.Text;
            string role = txtRole.Text;
            decimal salary = decimal.Parse(txtSalary.Text);

            string query = "insert into employee (name, role, salary) values (@name, @role, @salary)";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@salary", salary);

            int result = cmd.ExecuteNonQuery();
            MessageBox.Show(result > 0 ? "Employee Added!" : "Insert Failed");
            ClearFields();
        }

        void ViewEmployee(SqlConnection con)
        {
            string query = "select * from employee";
            using SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable table = new DataTable();
            adapter.Fill(table);
            dgvEmployees.DataSource = table;
        }

        void UpdateEmployee(SqlConnection con)
        {
            int id = Convert.ToInt32(dgvEmployees.CurrentRow!.Cells["id"].Value);
            string name = txtName.Text;
            string role = txtRole.Text;
            decimal salary = decimal.Parse(txtSalary.Text);

            string query = "update employee set name=@name, role=@role, salary=@salary where id=@id";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@name", name);
            cmd.Parameters.AddWithValue("@role", role);
            cmd.Parameters.AddWithValue("@salary", salary);

            int result = cmd.ExecuteNonQuery();
            MessageBox.Show(result > 0 ? "Employee Updated" : "Update Failed");
            ClearFields();
        }

        void DeleteEmployee(SqlConnection con)
        {
            int id = Convert.ToInt32(dgvEmployees.CurrentRow!.Cells["id"].Value);
            string query = "delete from employee where id=@id";
            using SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);

            int result = cmd.ExecuteNonQuery();
            MessageBox.Show(result > 0 ? "Employee Deleted" : "Delete Failed");
            ClearFields();
        }

        void SearchByRole(SqlConnection con)
        {
            string role = txtSearchRole.Text;
            string query = "select * from employee where role like @role";
            using SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            adapter.SelectCommand.Parameters.AddWithValue("@role", "%" + role + "%");

            DataTable table = new DataTable();
            adapter.Fill(table);
            dgvEmployees.DataSource = table;
        }

        void ClearFields()
        {
            txtName.Clear();
            txtRole.Clear();
            txtSalary.Clear();
            txtSearchRole.Clear();
        }
    }
}
