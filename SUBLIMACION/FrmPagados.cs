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
    public partial class FrmPagados : Form
    {
        SqlConnection conexion = new SqlConnection("server = LAPTOP-FOT3JMR1; database = SUBLIMACION; Integrated Security = true");

        public FrmPagados()
        {
            InitializeComponent();
        }

        int m, mx, my; //Coordenadas Movimiento Form

//ANIMACION MENU----------------------------------------------------------------------------------------------------------------
        private void pictureBox2_MouseMove(object sender, MouseEventArgs e)
        {
            if (m == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my); //Movimiento Form detectar posicion del mouse
            }
        }

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            m = 1; //Movimiento Form inicializar movimiento
            mx = e.X;
            my = e.Y;
        }

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0; //Movimiento Form en 0
        }
        //------------------------------------------------------------------------------------------------------------------------------        

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cbidPedidoPagado_SelectedIndexChanged(object sender, EventArgs e)
        {

            traerDatosPagados(cbidPedidoPagado.Text, txtNombreCliente, txtNombreProducto, txtNumeroTelCliente, txtDireccionCliente, txtCorreoCliente);

        }

        private void FrmPagados_Load(object sender, EventArgs e)
        {
            conexion.Open();

            SqlCommand comandoLlenarcbPedidoPagado = new SqlCommand("SELECT idPedido FROM Pedidos", conexion); //comando llenar combo box pedido

            SqlDataReader registro = comandoLlenarcbPedidoPagado.ExecuteReader();

            while (registro.Read())
            {
                cbidPedidoPagado.Items.Add(registro["idPedido"].ToString());
            }

            conexion.Close();
        }

        private void btnPagado_Click(object sender, EventArgs e)
        {

        }

        private void btnCancelarBuscado_Click(object sender, EventArgs e)
        {
            txtDireccionCliente.Text = String.Empty;
            txtCorreoCliente.Text = String.Empty;
            txtNombreCliente.Text = String.Empty;
            txtNumeroTelCliente.Text = String.Empty;
            txtEstadoDePago.Text = String.Empty;
            txtNombreProducto.Text = String.Empty;
            cbidPedidoPagado.Text = "";
        }

                                                                                                //txtEstadoDePago, txtNombreCliente, txtNombreProducto,txtNumeroTelCliente, txtDireccionCliente, txtCorreoCliente
        private void traerDatosPagados(string idPedido, TextBox txtNombreCliente, TextBox txtNombreProducto, TextBox txtNumeroTelCliente, TextBox txtDireccionCliente, TextBox txtCorreoCliente)
        {
            conexion.Open();

            string query = "SELECT Producto.nombreProducto, " +
                "Cliente.nombreCliente, Cliente.numeroTelefonicoCliente, Cliente.direccionCliente, Cliente.correoElectronicoCliente " +
                "FROM Pedidos " +
                "INNER JOIN Cliente ON Pedidos.cliente = cliente.idCliente " +
                "INNER JOIN Producto ON Pedidos.producto = Producto.idProducto WHERE idPedido = " + idPedido;

            SqlCommand comandoTraerDatos = new SqlCommand(query, conexion);
            SqlDataReader lectorTraerDatos = comandoTraerDatos.ExecuteReader();

            while (lectorTraerDatos.Read())
            {
                //txtEstadoDePago.Text = lectorTraerDatos["pagado"].ToString();
                txtNombreCliente.Text = lectorTraerDatos["nombreCliente"].ToString();
                txtNombreProducto.Text = lectorTraerDatos["nombreProducto"].ToString();
                txtNumeroTelCliente.Text = lectorTraerDatos["numeroTelefonicoCliente"].ToString();
                txtDireccionCliente.Text = lectorTraerDatos["direccionCliente"].ToString();
                txtCorreoCliente.Text = lectorTraerDatos["correoElectronicoCliente"].ToString();
            }

            conexion.Close();
        }

    }
}
