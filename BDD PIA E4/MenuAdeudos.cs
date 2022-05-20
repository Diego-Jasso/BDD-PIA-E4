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
using BDD_PIA_E4;

namespace CshaepBDD
{
    public partial class MenuAdeudos : Form
    {
        public MenuAdeudos()
        {
            InitializeComponent();
            dataGridView1.DataSource = llenar_Grid();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion.Conectar();
        }
        public DataTable llenar_Grid()
        {
            Conexion.Conectar();
            DataTable dt = new DataTable();
            string consulta = "select * from Adeudos";
            SqlCommand cmd = new SqlCommand(consulta, Conexion.Conectar());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox2.Text = ((DateTime)dataGridView1.CurrentRow.Cells[2].Value).ToString("yyyy-MM-dd");
                textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                textBox8.Text = (Decimal.Parse(textBox5.Text) * 0.16m).ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal sacar_iva;
            Conexion.Conectar();
            string insertar = "Insert into Adeudos(Cita_id,FechaExpedido,NumeroTarjeta,empleado_id,Precio,IVA,CURP_Paciente) values(@Cita_id,@FechaExpedido,@Numero,@empleado_id,@Precio,@IVA,@CURP)";
            SqlCommand cmdl = new SqlCommand(insertar, Conexion.Conectar());
            cmdl.Parameters.AddWithValue("@Cita_id", textBox6.Text);
            cmdl.Parameters.AddWithValue("@FechaExpedido", textBox2.Text);
            cmdl.Parameters.AddWithValue("@Numero", textBox3.Text);
            cmdl.Parameters.AddWithValue("@empleado_id", textBox4.Text);
            cmdl.Parameters.Add("@Precio", SqlDbType.Money).Value = Decimal.Parse(textBox5.Text); 
            sacar_iva = Decimal.Parse(textBox5.Text) * 0.16m;
            cmdl.Parameters.Add("@IVA", SqlDbType.Money).Value = sacar_iva;
            cmdl.Parameters.AddWithValue("@CURP", textBox7.Text);

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
            decimal sacar_iva;
            Conexion.Conectar();
            string insertar = "update Adeudos " +
                "set Cita_id = @Cita_id ,FechaExpedido = @FechaExpedido, NumeroTarjeta = @Numero, empleado_id = @empleado_id, Precio = @Precio, IVA = @IVA, CURP_Paciente = @CURP  " +
                "where Adeudo_Id = @Adeudo_ID";
            SqlCommand cmdl = new SqlCommand(insertar, Conexion.Conectar());
            cmdl.Parameters.AddWithValue("@Adeudo_ID", textBox1.Text);
            cmdl.Parameters.AddWithValue("@Cita_id", textBox6.Text);
            cmdl.Parameters.AddWithValue("@FechaExpedido", textBox2.Text);
            cmdl.Parameters.AddWithValue("@Numero", textBox3.Text);
            cmdl.Parameters.AddWithValue("@empleado_id", textBox4.Text);
            cmdl.Parameters.Add("@Precio", SqlDbType.Money).Value = Decimal.Parse(textBox5.Text);
            sacar_iva = Decimal.Parse(textBox5.Text) * 0.16m;
            cmdl.Parameters.Add("@IVA", SqlDbType.Money).Value = sacar_iva;
            cmdl.Parameters.AddWithValue("@CURP", textBox7.Text);

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
            string eliminar = "delete from  Adeudos " +
                "where Adeudo_Id = @Adeudos_id";
            SqlCommand cmdl = new SqlCommand(eliminar, Conexion.Conectar());
            cmdl.Parameters.AddWithValue("@Adeudos_id", textBox1.Text);
            try
            {
                cmdl.ExecuteNonQuery();
                MessageBox.Show(" Los datos fueron eliminados exitosamente");
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            dataGridView1.DataSource = llenar_Grid();
        }
    }
}
