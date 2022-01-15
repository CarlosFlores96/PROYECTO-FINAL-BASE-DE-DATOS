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
using System.Configuration;

namespace SUBLIMACION
{
    public partial class FrmClientes : Form
    {

        SqlConnection conexion = new SqlConnection("server = LAPTOP-FOT3JMR1; database = SUBLIMACION; Integrated Security = true;");

        public FrmClientes()
        {
            InitializeComponent();
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

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0; //Movimiento Form en 0
        }

        //Fin--------------------------------------------------------------------------------------------------------------------


        private void btnAgregarCliente_Click(object sender, EventArgs e)                            //AGREGAR CLIENTE
        {
            string nombreCliente;
            string direccionCliente;
            string numeroTelefonicoCliente;
            string correoElectronicoCliente;

            nombreCliente = txtNombreClienteNuevo.Text;
            direccionCliente = txtDireccionClienteNuevo.Text;
            numeroTelefonicoCliente = txtNumeroTelClienteNuevo.Text;
            correoElectronicoCliente = txtCorreoClienteNuevo.Text;

            try
            {
                using (SqlCommand querys = new SqlCommand

                    ("INSERT INTO Cliente (nombreCliente,direccionCliente,numeroTelefonicoCliente,correoElectronicoCliente) " +
                    "VALUES ('" + nombreCliente + "','" + direccionCliente + "','" + numeroTelefonicoCliente + "','" + correoElectronicoCliente + "'); " +
                    "DELETE FROM Cliente WHERE idCliente IS NOT NULL AND nombreCliente = '' AND direccionCliente = '' " +
                    "AND numeroTelefonicoCliente= '' AND correoElectronicoCliente = ''",conexion))
                {
                        conexion.Open();

                    using (SqlDataReader lector = querys.ExecuteReader())
                    {
                        //while (lector.Read())
                        //{
                        //    MessageBox.Show(lector.GetString(0), "Clientes.nombreCliente" + "Clientes.direccionCliente" + "Clientes.numeroTelefonicoCliente" + "Clientes.correoElectronicoCliente");
                        //}
                        if (lector.NextResult())
                        {
                            while (lector.Read())
                            {
                                MessageBox.Show(lector.GetString(0), "Cliente.idCliente");
                                eliminarCampos();
                            }
                        }
                            MessageBox.Show("Datos ingresados correctamente.");
                            eliminarCampos();
                    }
                        conexion.Close();
                }

            }
            catch (Exception error)
            {
                eliminarCampos();
                MessageBox.Show(error.ToString());
            }

        }
        private void btnCancelarCliente_Click(object sender, EventArgs e)
        {
            txtCorreoClienteNuevo.Text = String.Empty;
            txtDireccionClienteNuevo.Text = String.Empty;
            txtNombreClienteNuevo.Text = String.Empty;
            txtNumeroTelClienteNuevo.Text = String.Empty;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            frmListaClientes frm = new frmListaClientes();
            frm.Show();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void eliminarCampos()
        {
            txtCorreoClienteNuevo.Text = "";
            txtDireccionClienteNuevo.Text = "";
            txtNombreClienteNuevo.Text = "";
            txtNumeroTelClienteNuevo.Text = "";
        }

        private void mostrarClienteadd()
        {
            MessageBox.Show("Datos ingresados correctamente.");
        }
    }
}
