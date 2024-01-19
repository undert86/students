using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsFormsApp1.Models;

namespace WindowsFormsApp1
{

    public partial class Form1 : Form
    {
        BindingList<Student> students = new BindingList<Student>();
        
        public Form1()
        {
            InitializeComponent();
            comboBox1.DataSource = new BindingSource(students, null);
            comboBox1.DisplayMember = "FullName";
            try
            {
                var file = File.ReadAllText("./save1.json");
                students = JsonSerializer.Deserialize<BindingList<Student>>(file);
            }
            catch 
            { 
                
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
            {
                var student = new Student();
                student.FullName = textBox1.Text;
                student.Grades = new Grades();
                students.Add(student);
            }
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            
            if (comboBox1.SelectedItem != null)
            {
                var student = (Student)comboBox1.SelectedItem;
                dataGridView1.DataSource = new BindingSource(student.Grades, null);
            }
                
        }

        private void False(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null) 
            { 
                students.Remove((Student)comboBox1.SelectedItem);
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var color = default(Color);
            switch (cell.Value)
            {
                case 2:
                    color = Color.Red;
                    break;
                case 3:
                    color = Color.Orange; break;
                    
                case 4: color = Color.Yellow; break;
                    
                case 5:
                    color = Color.Green;
                    break;
                default: cell.Value = 2; break;
            }
            cell.Style.BackColor = color;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var cell = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
            if (form2.DialogResult == DialogResult.Yes) 
            {
                var jsdata = JsonSerializer.Serialize(students);
                File.WriteAllText("./save.json",jsdata);
            }
            else 
            { 
                
            }
        }
    }
}
