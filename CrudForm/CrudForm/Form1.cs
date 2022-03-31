using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CrudForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection conexion = new SqlConnection("Data Source=DESKTOP-2SRQGUH;Initial Catalog=Tienda;Integrated Security=True");

        public void llenarTabla()
        {
            string Select = "Select * from cliente";
            SqlDataAdapter adapter = new SqlDataAdapter(Select, conexion);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string Select = "Select * from cliente";
            SqlDataAdapter adapter = new SqlDataAdapter(Select,conexion);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conexion.Open();
            string query = "Update Cliente set Nombre='" + txtNombre.Text + "',Apellido='" + txtApellido.Text + "',Edad=" + txtEdad.Text + "where idCliente=" +id.Text+" ";
            SqlCommand comando = new SqlCommand(query,conexion);
            int cant;
            cant = comando.ExecuteNonQuery();
            if (cant > 0)
            {
                MessageBox.Show("Registro modificado");
            }
            llenarTabla();
            conexion.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string query = "INSERT INTO Cliente (Nombre,Apellido,Edad,Sexo) VALUES (@Nombre,@Apellido,@Edad,@Sexo)";
            conexion.Open();
            SqlCommand comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            comando.Parameters.AddWithValue("@Apellido",txtApellido.Text);
            comando.Parameters.AddWithValue("@Edad", txtEdad.Text);
            comando.Parameters.AddWithValue("@Sexo", txtSexo.Text);
            comando.ExecuteNonQuery();
            MessageBox.Show("Insertado");
            llenarTabla();

            conexion.Close();

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.CurrentRow.Selected = true;
            id.Text = dataGridView1.SelectedCells[0].Value.ToString();
            txtNombre.Text = dataGridView1.SelectedCells[1].Value.ToString();
            txtApellido.Text = dataGridView1.SelectedCells[2].Value.ToString();
            txtEdad.Text = dataGridView1.SelectedCells[3].Value.ToString();
            txtSexo.Text = dataGridView1.SelectedCells[4].Value.ToString();


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
                txtSexo.Text = "Masculino";           
                
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
                txtSexo.Text = "Femenino";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conexion.Open();

            string query = "DELETE FROM Cliente WHERE idCliente="+id.Text+"";
            SqlCommand comando = new SqlCommand(query,conexion);
            comando.ExecuteNonQuery();
            MessageBox.Show("Se ha eliminado con exito");
            llenarTabla();
            conexion.Close();         
        }
    }
}