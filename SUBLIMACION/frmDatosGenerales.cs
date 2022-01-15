using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SUBLIMACION
{
    public partial class frmDatosGenerales : Form
    {
        SqlConnection conexion = new SqlConnection("server = LAPTOP-FOT3JMR1; database = SUBLIMACION; Integrated Security = true");

        public frmDatosGenerales()
        {
            InitializeComponent();
            invisibleLabels();
        }

        int m, mx, my;

        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            m = 1; //Movimiento Form inicializar movimiento
            mx = e.X;
            my = e.Y;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnCantClientes_Click(object sender, EventArgs e)
        {
            cantClientes();
        }

        private void btnCantProductos_Click(object sender, EventArgs e)
        {
            buscarInfoProducto();
        }
        private void btnCantPedidos_Click(object sender, EventArgs e)
        {
            buscarInfoPedidos();
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

        private void buscarInfoProducto()
        {
            try
            {
            conexion.Open();

            SqlCommand ComandoUno = new SqlCommand("SELECT COUNT(idProducto) FROM Producto", conexion ); //CANTIDAD PRODUCTOS
            Int32 contadorUno = Convert.ToInt32(ComandoUno.ExecuteScalar());
            if (contadorUno > 0)
            {
                labelCantProductos.Visible = true;
                labelCantProductos.Text = Convert.ToString(contadorUno.ToString()) + " Productos.";
            }
            else
            {
                labelCantProductos.Text = "0";
            }

            SqlCommand comandoDos = new SqlCommand("SELECT SUM(precioProducto) FROM Producto", conexion); //SUMA TOTAL PRODUCTOS
            Int32 contadorDos = Convert.ToInt32(comandoDos.ExecuteScalar());
            if (contadorDos > 0)
            {
                labelSumaTotProductos.Visible = true;       //PRODUCTOS
                labelSumaTotProductos.Text = Convert.ToString(contadorDos.ToString()) + " Pesos.";
            }
            else
            {
                labelSumaTotProductos.Text = "0";
            }

            SqlCommand comandoTres = new SqlCommand("SELECT AVG(precioProducto) FROM Producto", conexion); //PROMEDIO TOTAL PRODUCTOS
            Int32 contadorTres = Convert.ToInt32(comandoTres.ExecuteScalar());
            if (contadorTres > 0)
            {
                labelPromedioPrecios.Visible = true;
                labelPromedioPrecios.Text = Convert.ToString(contadorTres.ToString()) + " Pesos.";
            }
            else
            {
                labelPromedioPrecios.Text = "0";
            }

            conexion.Close();
            }

            catch (SqlException odbcEx)
            {
                MessageBox.Show("No hay datos que mostrar.");
            }
            catch (Exception)
            {
                MessageBox.Show("No hay datos que mostrar.");
            }

        }


        private void buscarInfoPedidos()
        {
            try
            {

            conexion.Open();

            SqlCommand ComandoCuatro = new SqlCommand("SELECT COUNT(idPedido) FROM Pedidos", conexion); //CANTIDAD PEDIDOS
            Int32 contadorCuatro = Convert.ToInt32(ComandoCuatro.ExecuteScalar());

            if (contadorCuatro > 0)
            {
                labelCantidadPedidos.Visible = true;        //PEDIDOS
                labelCantidadPedidos.Text = Convert.ToString(contadorCuatro.ToString()) + " Pedidos.";
            }
            else
            {
                labelCantidadPedidos.Text = "0";
            }

            SqlCommand comandoCinco = new SqlCommand("SELECT COUNT(idPedido) FROM Pedidos", conexion); //PROMEDIO PEDIDOS
            Int32 contadorCinco = Convert.ToInt32(comandoCinco.ExecuteScalar());

            if (contadorCinco > 0)
            {
                labelPromedioPedidos.Visible = true;
                labelPromedioPedidos.Text = Convert.ToString(contadorCinco.ToString()) + " Pedidos.";
            }
            else
            {
                labelPromedioPedidos.Text = "0";
            }

            conexion.Close();
            }

            catch (SqlException odbcEx)
            {
                MessageBox.Show("No hay datos que mostrar.");
            }
            catch (Exception)
            {
                MessageBox.Show("No hay datos que mostrar.");
            }

        }


        private void cantClientes()
        {
            try
            {

            conexion.Open();

            SqlCommand ComandoCuatro = new SqlCommand("SELECT COUNT(idCliente) FROM Cliente", conexion); //CANTIDAD PEDIDOS
            Int32 contadorCuatro = Convert.ToInt32(ComandoCuatro.ExecuteScalar());

            if (contadorCuatro > 0)
            {
                labelCantidadCliente.Visible = true;        //PEDIDOS
                labelCantidadCliente.Text = Convert.ToString(contadorCuatro.ToString()) + " Clientes.";
            }
            else
            {
                labelCantidadCliente.Text = "0";
            }

            conexion.Close();
            }
            catch (SqlException odbcEx)
            {
                MessageBox.Show("No hay datos que mostrar.");
            }

            catch (Exception)
            {
                MessageBox.Show("No hay datos que mostrar.");
            }
        }

        private void invisibleLabels()
        {
            labelCantProductos.Visible = false;      
            labelSumaTotProductos.Visible = false;       //PRODUCTOS
            labelPromedioPrecios.Visible = false;


            labelPromedioPedidos.Visible = false;        //PEDIDOS
            labelCantidadPedidos.Visible = false;


            labelCantidadCliente.Visible = false;        //CLIENTE
        }
    }

}
