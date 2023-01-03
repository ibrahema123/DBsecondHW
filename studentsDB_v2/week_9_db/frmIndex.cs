using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace week_9_db
{
    public partial class frmIndex : Form
    {
        string connectionString = "Provider=SQLOLEDB;Data Source=DESKTOP-OTT897E\\SQLEXPRESS;Initial Catalog=week9;Integrated Security=True;Integrated Security=SSPI";
        OleDbConnection con;
        int currentRowNo = 0;
        public frmIndex()
        {
            InitializeComponent();
            con = new OleDbConnection(connectionString);
        }

        private void frmIndex_Load(object sender, EventArgs e)
        {
            displayDBContant();
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            string insertCommand = $"Insert Into Students([studentNo], [studentFirst], [studentLast]) values('{txtStudentNo.Text}', '{txtStudentFirst.Text}', '{txtStudentLast.Text}')";

            OleDbCommand command = new OleDbCommand(insertCommand, con);
            con.Open();
            command.ExecuteNonQuery();
            con.Close();
            displayDBContant();
            clearData();
        }

        private void displayDBContant()
        {
            OleDbCommand command = new OleDbCommand("Select * from Students", con);
            OleDbDataAdapter da = new OleDbDataAdapter(command);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgvStudents.DataSource = dt;
        }

        private void clearData()
        {
            txtStudentNo.Clear();
            txtStudentFirst.Clear();
            txtStudentLast.Clear();
            txtStudentNo.Focus();
        }

        private void dgvStudents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = 0;

            DataGridViewRow currentPos = dgvStudents.Rows[row];

            Student student = new Student();

            student.studentId = int.Parse(currentPos.Cells[0].Value.ToString());
            student.studentNo = currentPos.Cells[1].Value.ToString();
            student.studentFirst = currentPos.Cells[2].Value.ToString();
            student.studentLast = currentPos.Cells[3].Value.ToString();

            currentRowNo = student.studentId;
            txtStudentNo.Text = student.studentNo;
            txtStudentFirst.Text = student.studentFirst;
            txtStudentLast.Text = student.studentLast;

        }

        private void updateStudent(Student student)
        {
            string insertCommand = $"Update Students Set [studentNo] = '{student.studentNo}', " +
                $"[studentFirst] = '{student.studentFirst}', " +
                $"[studentLast] = '{student.studentLast}'" +
                $"Where [studentId] = {student.studentId}";

            OleDbCommand command = new OleDbCommand(insertCommand, con);


            con.Open();
            command.ExecuteNonQuery();
            con.Close();

        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.studentId = currentRowNo;
            student.studentNo = txtStudentNo.Text;
            student.studentFirst = txtStudentFirst.Text;
            student.studentLast = txtStudentLast.Text;

            updateStudent(student);
            displayDBContant();
            clearData();
        }

        private void deleteStudent(Student student)
        {
            string insertCommand = $"Delete from Students Where [studentId] = {student.studentId}";

            OleDbCommand command = new OleDbCommand(insertCommand, con);


            con.Open();
            command.ExecuteNonQuery();
            con.Close();

        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            Student student = new Student();
            student.studentId = currentRowNo;
            student.studentNo = txtStudentNo.Text;
            student.studentFirst = txtStudentFirst.Text;
            student.studentLast = txtStudentLast.Text;

            deleteStudent(student);
            displayDBContant();
            clearData();
        }
    }
}
