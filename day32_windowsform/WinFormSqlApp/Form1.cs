using System;
using System.Data;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
namespace WinFormSqlApp;

public partial class Form1 : Form
{
    private string conString = "Server=SKEMPIRE;Database=CompanyDB;Integrated Security=True;TrustServerCertificate=True;";
    public Form1()
    {
        InitializeComponent();
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        using (SqlConnection con = new SqlConnection(conString))
        {
            con.Open();

            string query = "SELECT * FROM Employee";

            SqlDataAdapter adapter = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            dataGridView1.DataSource = dt;
        }
    }
    private void btnAdd_Click(object sender, EventArgs e)
    {
        decimal salary;

        if (!decimal.TryParse(txtSalary.Text, out salary))
        {
            MessageBox.Show("Invalid Salary");
            return;
        }

        string name = txtName.Text.Trim();
        string role = txtRole.Text.Trim();

        using (SqlConnection con = new SqlConnection(conString))
        {
            con.Open();
            string insertQuery = "INSERT INTO Employee (Name, Role, Salary) VALUES (@Name, @Role, @Salary)";
            using (SqlCommand cmd = new SqlCommand(insertQuery, con))
            {
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@Role", role);
                cmd.Parameters.AddWithValue("@Salary", salary);

                int result = cmd.ExecuteNonQuery();
                MessageBox.Show(result > 0 ? "Employee Added!" : "Insert Failed");
                btnLoad_Click(null, null); // Refresh table
            }
        }
    }
}

