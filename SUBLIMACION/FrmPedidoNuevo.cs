using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace SUBLIMACION
{
    public partial class FrmEditarPedido : Form
    {
        SqlConnection conexion = new SqlConnection("server = LAPTOP-FOT3JMR1; database = SUBLIMACION; Integrated Security = true");

        public FrmEditarPedido()
        {
            InitializeComponent();
            panelEliminarPedido.Enabled = false;
            panelBotonesNuevoPedido.Enabled = false;
            panelDosID.Enabled = false;
        }

        int m, mx, my; //Coordenadas Movimiento Form

        //PERSONALIZACION FORM------------------------------------------------------------------------------------------------------
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
        //---------------------------------------------------------------------------------------------------------------------------


        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtUsuario.Text = String.Empty;
            txtPassword.Text = String.Empty;
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form
        }

        private void btnAgregarPedidoNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                conexion.Open();

                int idCliente;
                int idProducto;
                string query = null;

                if (!(cbidPedido.Text == "" || cbidCliente.Text == ""))
                {
                    MessageBox.Show("Seleccione un cliente y/o un producto.");
                }
                else
                {
                    idCliente = Int32.Parse(cbidCliente.Text);
                    idProducto = Int32.Parse(cbProducto.Text);

                    query = "INSERT INTO Pedidos (cliente, producto) " +
                    "VALUES (" + idCliente + ", " + idProducto + ")";

                    SqlCommand comando = new SqlCommand(query, conexion);
                    comando.ExecuteNonQuery();

                    MessageBox.Show("Pedido agregado correctamente.");
                    eliminarCamposAgregar();
                }

            }

            catch (SqlException odbcEx)
            {
                MessageBox.Show("Verifique si seleccionó ID pedido y/o ID producto.");
            }
            catch (Exception error)
            {
                MessageBox.Show("Verifique si seleccionó ID pedido y/o ID producto.");
            }
                conexion.Close();
        }
        private void btnEliminarPedido_Click(object sender, EventArgs e)
        {
            eliminarPedidoBoton();
            eliminarCamposEliminar();
        }

        private void txtCancelarPedidoNuevo_Click(object sender, EventArgs e)
        {
            eliminarCamposAgregar();
        }

        private void checkBoxNo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxNo.Checked == true)
            {
                FrmClientes frm = new FrmClientes();
                this.Hide();
                frm.ShowDialog();
                this.Close();
            }
        }

        private void cbidCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            BuscaridCliente(cbidCliente.Text, txtNombreClienteNuevo, txtDireccionClienteNuevo, txtTelCliente);
        }

        private void cbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            buscarProducto(cbProducto.Text, txtnombreProductoNuevo, txtDiseProducto, txtCostoProducto);
        }

        private void cbidPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            traerDatosPedido(cbidPedido.Text);
        }

        private void FrmEditarPedido_Load(object sender, EventArgs e)
        {

            SqlCommand comandoLlenar = new SqlCommand
            ("SELECT idCliente FROM Cliente", conexion); //Lenar combo box id cliente

            conexion.Open();

            SqlDataReader registro = comandoLlenar.ExecuteReader();

            while (registro.Read())
            {
                cbidCliente.Items.Add(registro["idCliente"].ToString());
            }



            registro.Close();



            SqlCommand comandoLlenarcbProduc = new SqlCommand
            ("SELECT idProducto FROM Producto", conexion); //comando llenar combo box producto

            SqlDataReader registroDos = comandoLlenarcbProduc.ExecuteReader();

            while (registroDos.Read())
            {
                cbProducto.Items.Add(registroDos["idProducto"].ToString());
            }



            registroDos.Close();




            SqlCommand comandoLlenarcbPedido = new SqlCommand
            ("SELECT idPedido FROM Pedidos", conexion); //comando llenar combo box pedido

            SqlDataReader registroTres = comandoLlenarcbPedido.ExecuteReader();

            while (registroTres.Read())
            {
                cbidPedido.Items.Add(registroTres["idPedido"].ToString());
            }

            conexion.Close();
        }

        //METODOS------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------
        private void traerDatosPedido(string idPedido)
        {
            conexion.Open();

            string query = "SELECT Producto.nombreProducto, Cliente.nombreCliente, Cliente.numeroTelefonicoCliente " +
                           "FROM Pedidos " +
                           "INNER JOIN Cliente ON Pedidos.cliente = cliente.idCliente " +
                           "INNER JOIN Producto ON Pedidos.producto = Producto.idProducto WHERE idPedido = " + idPedido;

            SqlCommand comandoTraerDatos = new SqlCommand(query, conexion);

            SqlDataReader lectorTraerDatos = comandoTraerDatos.ExecuteReader();

            while (lectorTraerDatos.Read())
            {
                txtNombreProducto.Text = lectorTraerDatos["nombreProducto"].ToString();
                txtNombreCliente.Text = lectorTraerDatos["nombreCliente"].ToString();
                txtTelCliente.Text = lectorTraerDatos["numeroTelefonicoCliente"].ToString();
            }

            conexion.Close();
        }
        //-------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------
        private void eliminarPedidoBoton()
        {
            try
            {

            conexion.Open();

                int idPedido = 0;
                string query = null;

                if (cbidPedido.Text == "")
                {
                    MessageBox.Show("Seleccione un pedido.");
                }
                else
                {
                    idPedido = Int32.Parse(cbidPedido.Text);
                    query = "DELETE FROM Pedidos WHERE idPedido = " + idPedido;
                SqlCommand comandoBorrarPedido = new SqlCommand(query, conexion);
                SqlDataReader lectorEliminarPedido = comandoBorrarPedido.ExecuteReader();

                    while (lectorEliminarPedido.Read())
                    {
                        comandoBorrarPedido.ExecuteNonQuery();
                        lectorEliminarPedido.Read();
                    }
                        MessageBox.Show("Pedido eliminado correctamente.");
                }

            }

            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
                throw;
            }
            conexion.Close();
        }
        //-------------------------------------------------------------------------------------------------------------------------

        //-------------------------------------------------------------------------------------------------------------------------
        public void Login()
        {
            try
            {
                conexion.Open();
                string consultar = "SELECT * FROM Usuario WHERE nombre = '" + txtUsuario.Text + "' AND contrasenia = '" + txtPassword.Text + "'";
                SqlCommand comando = new SqlCommand(consultar, conexion);

                SqlDataReader lector;
                lector = comando.ExecuteReader();

                if (lector.HasRows == true)
                {
                    MessageBox.Show("Datos correctos.");
                    panelEliminarPedido.Enabled = true;
                    panelChecarUsuario.Visible = false;
                    txtPassword.Text = "";
                    txtUsuario.Text = "";

                }
                else
                {
                    MessageBox.Show("Usuario y/o Contraseña erroneos ó tal vez no se encuentra registrado.");
                    txtPassword.Text = "";
                    txtUsuario.Text = "";
                }
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
                conexion.Close();
        }

        private void btnCancelarPedidoEliminado_Click(object sender, EventArgs e)
        {
            eliminarCamposEliminar();
        }

        //---------------------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------------------
        private void buscarProducto(string idProducto, TextBox txtnombreProductoNuevo, TextBox txtDiseProducto, TextBox txtCostoProducto)
        {
            conexion.Open();

            string query = "SELECT nombreProducto, diseñoProducto, precioProducto " +
            "FROM Producto WHERE idProducto = " + idProducto;

            SqlCommand comando = new SqlCommand(query, conexion);

            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                txtnombreProductoNuevo.Text = lector["nombreProducto"].ToString();
                txtDiseProducto.Text = lector["diseñoProducto"].ToString();
                txtCostoProducto.Text = lector["precioProducto"].ToString();
            }

            conexion.Close();
        }
        //---------------------------------------------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------------------------------------------
        private void BuscaridCliente(string idCliente, TextBox txtNombreClienteNuevo, TextBox txtDireccionClienteNuevo, TextBox txtNumeroTelClienteNuevo)
        {
            conexion.Open();

            string query = "SELECT nombreCliente, direccionCliente, numeroTelefonicoCliente FROM Cliente WHERE idCliente = "+ idCliente;

            SqlCommand comando = new SqlCommand(query, conexion);

            SqlDataReader lector = comando.ExecuteReader();

            while (lector.Read())
            {
                txtNombreClienteNuevo.Text = lector["nombreCliente"].ToString();
                txtDireccionClienteNuevo.Text = lector["direccionCliente"].ToString();
                txtNumeroTelClienteNuevo.Text = lector["numeroTelefonicoCliente"].ToString();
            }

            conexion.Close();
        }

        private void checkBoxSi_CheckedChanged_1(object sender, EventArgs e)
        {
            if (checkBoxSi.Checked == true)
            {
                panelDosID.Enabled = true;
                panelBotonesNuevoPedido.Enabled = true;
                checkBoxNo.Enabled = false;
                cbidCliente.Text = "";
                cbProducto.Text = "";
            }
            else
            {
                panelDosID.Enabled = false;
                panelBotonesNuevoPedido.Enabled = false;
                checkBoxNo.Enabled = true;
                eliminarCamposEliminar();

            }
        }

        //---------------------------------------------------------------------------------------------------------------------------

        private void eliminarCamposAgregar()
        {
            txtDireccionClienteNuevo.Text = String.Empty;
            txtNombreClienteNuevo.Text = String.Empty;
            txtTelCliente.Text = String.Empty;
            txtnombreProductoNuevo.Text = "";
            txtDiseProducto.Text = "";
            txtCostoProducto.Text = "";
            cbidCliente.Text = "";
            cbProducto.Text = "";
            checkBoxSi.Checked = false;
        }

        private void eliminarCamposEliminar()
        {
            txtNombreCliente.Text = "";
            txtNombreProducto.Text = "";
            txtTelCliente.Text = "";
            cbidPedido.Text = "";
        }
    }
}
