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

namespace BDD_PIA_E4
{
    public partial class menuRecetas : Form
    {
        public menuRecetas()
        {
            InitializeComponent();
            dataGridView1.DataSource = llenar_Grid();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
        }
        public DataTable llenar_Grid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "select * from Receta_conceptos";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string insertar = "Insert into Receta_Conceptos(Reporte_id,Medicamento_id,Cantidad) values(@Reporte_id , @Medicamento_id , @Cantidad)";
            SqlCommand cmdl = new SqlCommand(insertar, Conexion.Conectar());
            cmdl.Parameters.AddWithValue("@Reporte_id", textBox1.Text);
            cmdl.Parameters.AddWithValue("@Medicamento_id", textBox2.Text);
            cmdl.Parameters.Add("@Cantidad", SqlDbType.Money).Value = Decimal.Parse(textBox3.Text);

            try
            {
                cmdl.ExecuteNonQuery();
                MessageBox.Show("Los datos fueron agregados exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            dataGridView1.DataSource = llenar_Grid();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string insertar = "update Receta_conceptos " +
                "set Reporte_id = @Reporte_id, Medicamento_id = @Medicamento_id , Cantidad = @Cantidad " +
                "where Reporte_id = @Reporte_id";
            SqlCommand cmdl = new SqlCommand(insertar, Conexion.Conectar()); 
            cmdl.Parameters.AddWithValue("@Reporte_id", textBox1.Text);
            cmdl.Parameters.AddWithValue("@Medicamento_id", textBox2.Text);
            cmdl.Parameters.Add("@Cantidad", SqlDbType.Money).Value = Decimal.Parse(textBox3.Text);

            try
            {
                cmdl.ExecuteNonQuery();
                MessageBox.Show("Los datos fueron editados exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            dataGridView1.DataSource = llenar_Grid();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Conexion.Conectar();
            string eliminar = "delete from Receta_conceptos " +
               "where Reporte_id = @Reporte_id";
            SqlCommand cmdl = new SqlCommand(eliminar, Conexion.Conectar());
            cmdl.Parameters.AddWithValue("@Reporte_id", textBox1.Text);
            cmdl.Parameters.AddWithValue("@Medicamento_id", textBox2.Text);
            cmdl.Parameters.Add("@Cantidad", SqlDbType.Money).Value = Decimal.Parse(textBox3.Text);

            try
            {
                cmdl.ExecuteNonQuery();
                MessageBox.Show("Los datos fueron eliminados exitosamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


            dataGridView1.DataSource = llenar_Grid();
        }

        private void menuRecetas_Load(object sender, EventArgs e)
        {

        }
    }
}
