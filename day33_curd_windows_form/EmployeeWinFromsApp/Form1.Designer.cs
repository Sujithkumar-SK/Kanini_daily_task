namespace EmployeeWinFromsApp;

partial class Form1
{
    private System.Windows.Forms.TextBox txtName;
    private System.Windows.Forms.TextBox txtRole;
    private System.Windows.Forms.TextBox txtSalary;
    private System.Windows.Forms.TextBox txtSearchRole;

    private System.Windows.Forms.Button btnAdd;
    private System.Windows.Forms.Button btnView;
    private System.Windows.Forms.Button btnUpdate;
    private System.Windows.Forms.Button btnDelete;
    private System.Windows.Forms.Button btnSearch;

    private System.Windows.Forms.DataGridView dgvEmployees;

    private System.ComponentModel.IContainer components = null;
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    private void InitializeComponent()
    {
        this.txtName = new System.Windows.Forms.TextBox();
        this.txtRole = new System.Windows.Forms.TextBox();
        this.txtSalary = new System.Windows.Forms.TextBox();
        this.txtSearchRole = new System.Windows.Forms.TextBox();

        this.btnAdd = new System.Windows.Forms.Button();
        this.btnView = new System.Windows.Forms.Button();
        this.btnUpdate = new System.Windows.Forms.Button();
        this.btnDelete = new System.Windows.Forms.Button();
        this.btnSearch = new System.Windows.Forms.Button();

        this.dgvEmployees = new System.Windows.Forms.DataGridView();

        ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).BeginInit();
        this.SuspendLayout();

        // txtName
        this.txtName.Location = new System.Drawing.Point(30, 70);
        this.txtName.Name = "txtName";
        this.txtName.PlaceholderText = "Name";
        this.txtName.Size = new System.Drawing.Size(200, 23);

        // txtRole
        this.txtRole.Location = new System.Drawing.Point(30, 110);
        this.txtRole.Name = "txtRole";
        this.txtRole.PlaceholderText = "Role";
        this.txtRole.Size = new System.Drawing.Size(200, 23);

        // txtSalary
        this.txtSalary.Location = new System.Drawing.Point(30, 150);
        this.txtSalary.Name = "txtSalary";
        this.txtSalary.PlaceholderText = "Salary";
        this.txtSalary.Size = new System.Drawing.Size(200, 23);

        // txtSearchRole
        this.txtSearchRole.Location = new System.Drawing.Point(30, 190);
        this.txtSearchRole.Name = "txtSearchRole";
        this.txtSearchRole.PlaceholderText = "Search by Role";
        this.txtSearchRole.Size = new System.Drawing.Size(200, 23);

        // btnAdd
        this.btnAdd.Location = new System.Drawing.Point(250, 30);
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new System.Drawing.Size(100, 30);
        this.btnAdd.Text = "Add";
        this.btnAdd.UseVisualStyleBackColor = true;

        // btnView
        this.btnView.Location = new System.Drawing.Point(250, 70);
        this.btnView.Name = "btnView";
        this.btnView.Size = new System.Drawing.Size(100, 30);
        this.btnView.Text = "View";
        this.btnView.UseVisualStyleBackColor = true;

        // btnUpdate
        this.btnUpdate.Location = new System.Drawing.Point(250, 110);
        this.btnUpdate.Name = "btnUpdate";
        this.btnUpdate.Size = new System.Drawing.Size(100, 30);
        this.btnUpdate.Text = "Update";
        this.btnUpdate.UseVisualStyleBackColor = true;

        // btnDelete
        this.btnDelete.Location = new System.Drawing.Point(250, 150);
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new System.Drawing.Size(100, 30);
        this.btnDelete.Text = "Delete";
        this.btnDelete.UseVisualStyleBackColor = true;

        // btnSearch
        this.btnSearch.Location = new System.Drawing.Point(250, 190);
        this.btnSearch.Name = "btnSearch";
        this.btnSearch.Size = new System.Drawing.Size(100, 30);
        this.btnSearch.Text = "Search";
        this.btnSearch.UseVisualStyleBackColor = true;

        // dgvEmployees
        this.dgvEmployees.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        this.dgvEmployees.Location = new System.Drawing.Point(30, 240);
        this.dgvEmployees.Name = "dgvEmployees";
        this.dgvEmployees.RowTemplate.Height = 25;
        this.dgvEmployees.Size = new System.Drawing.Size(700, 180);

        // Form1
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.txtName);
        this.Controls.Add(this.txtRole);
        this.Controls.Add(this.txtSalary);
        this.Controls.Add(this.txtSearchRole);

        this.Controls.Add(this.btnAdd);
        this.Controls.Add(this.btnView);
        this.Controls.Add(this.btnUpdate);
        this.Controls.Add(this.btnDelete);
        this.Controls.Add(this.btnSearch);

        this.Controls.Add(this.dgvEmployees);
        this.Name = "Form1";
        this.Text = "Employee Management";
        ((System.ComponentModel.ISupportInitialize)(this.dgvEmployees)).EndInit();
        this.ResumeLayout(false);
        this.PerformLayout();
    }
    #endregion
}
