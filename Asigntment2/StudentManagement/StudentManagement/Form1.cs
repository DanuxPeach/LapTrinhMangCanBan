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

namespace StudentManagement
{
    public partial class Form1 : Form
    {
        SqlConnection connection;
        SqlCommand command;
        string str = "Data Source=LAPTOP-KD8MQ4J1;Initial Catalog=StudentManagement;Integrated Security=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable table = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            connection = new SqlConnection(str);
            connection.Open();
            loadData();
        }
        #region methods
        void loadData()
        {

            command = connection.CreateCommand();
            command.CommandText = "select * from Student";
            adapter.SelectCommand = command;
            table.Clear();
            adapter.Fill(table);
            dataGridView.DataSource = table;
        }
        void addStudent()
        {
            command = connection.CreateCommand();
            command.CommandText = "Insert Into Student values('" +textBoxID.Text+ "', N'" +textBoxName.Text+ "', '" +comboBoxSex.Text+ "', '" +dateTimePickerBirth.Text+ "', '" +comboBoxFaculty.Text+ "', N'" +textBoxAddress.Text+ "' )";
            command.ExecuteNonQuery();
            loadData();
        }
        void updateStudent()
        {
            command = connection.CreateCommand();
            command.CommandText = "Update Student set Name = N'" + textBoxName.Text + "', Sex = '" +comboBoxSex.Text+ "', Birth = '" +dateTimePickerBirth.Text+ "', Faculty = '" +comboBoxFaculty.Text+ "', Address = N'" +textBoxAddress.Text+ "' where ID = '"+textBoxID.Text+"' ";
            command.ExecuteNonQuery();
            loadData();
        }

        void deleteStudent()
        {
            command = connection.CreateCommand();
            command.CommandText = "Delete From Student where ID = '"+textBoxID.Text+"'";
            command.ExecuteNonQuery();
            loadData();
        }

        #endregion

        #region events
        private void addStudent_Click(object sender, EventArgs e)
        {
            addStudent();
        }

        private void updateStudent_Click(object sender, EventArgs e)
        {
            updateStudent();
        }

        private void deleteStudent_Click(object sender, EventArgs e)
        {
            deleteStudent();
        }
        private void resetField_Click(object sender, EventArgs e)
        {
            textBoxID.Text = "";
            textBoxName.Text = "";
            comboBoxSex.Text = "";
            dateTimePickerBirth.Text = "1/1/2023";
            comboBoxFaculty.Text = "";
            textBoxAddress.Text = "";
        }

        #endregion

        private void dataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dataGridView.CurrentRow.Index;
            textBoxID.Text = dataGridView.Rows[i].Cells[0].Value.ToString();
            textBoxName.Text = dataGridView.Rows[i].Cells[1].Value.ToString();
            comboBoxSex.Text = dataGridView.Rows[i].Cells[2].Value.ToString();
            dateTimePickerBirth.Text = dataGridView.Rows[i].Cells[3].Value.ToString();
            comboBoxFaculty.Text = dataGridView.Rows[i].Cells[4].Value.ToString();
            textBoxAddress.Text = dataGridView.Rows[i].Cells[5].Value.ToString();
        }


    }
}
