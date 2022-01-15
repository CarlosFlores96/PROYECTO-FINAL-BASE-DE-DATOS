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

namespace SUBLIMACION
{
    public partial class frmListaClientes : Form
    {
        SqlConnection conexion = new SqlConnection("server = LAPTOP-FOT3JMR1; database = SUBLIMACION; Integrated Security = true;"); //CONEXION

        public frmListaClientes()
        {
            InitializeComponent();
            TablaClientes.AllowUserToAddRows = false;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            actualizarTabla();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {

            conexion.Open();

                if (TablaClientes.Rows.Count == 0)
                {
                    MessageBox.Show("No se seleccionó ningún cliente en la lista.");
                }
                else
                {
                    string idCliente = TablaClientes.CurrentRow.Cells[0].Value.ToString();
                    string query = ("DELETE FROM Cliente WHERE idCliente = " + idCliente);
                    SqlCommand comando = new SqlCommand(query, conexion);

                    comando.ExecuteNonQuery();
                    MessageBox.Show("Cliente borrado exitosamente.");
                    actualizarTabla();
                }
            }

            catch (SqlException odbcEx)
            {
                MessageBox.Show("Asegúrese que ningun cliente tiene algún pedido.");
                
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
            conexion.Close();

        }

        //PERSONALIZACION FORM------------------------------------------------------------------------------------------------------

        int m, mx, my; //Coordenadas Movimiento Form

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            m = 1; //Movimiento Form inicializar movimiento
            mx = e.X;
            my = e.Y;
        }
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (m == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my); //Movimiento Form detectar posicion del mouse
            }
        }

        private void frmListaClientes_Load(object sender, EventArgs e)
        {
            actualizarTabla();
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0; //Movimiento Form en 0
        }
        //Fin--------------------------------------------------------------------------------------------------------------------


        //Actualizar tabla-------------------------------------------------------------------------------------------------------
        public void actualizarTabla()
        {

            SqlCommand comando = new SqlCommand("SELECT Cliente.idCliente 'id Cliente:', Cliente.nombreCliente 'Nombre:', " +
                "Cliente.direccionCliente 'Direccion: ', Cliente.numeroTelefonicoCliente 'Numero Telefono: ', Cliente.correoElectronicoCliente 'Correo electrónico: ' FROM Cliente", conexion);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = comando;

            DataTable Tabla = new DataTable();
            adapter.Fill(Tabla);

            try
            {
                TablaClientes.DataSource = Tabla;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
        //Fin---------------------------------------------------------------------------------------------------------------------
    }
}
