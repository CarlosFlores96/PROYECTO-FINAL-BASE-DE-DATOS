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
    public partial class FrmPedidos : Form
    {
        SqlConnection conexion = new SqlConnection("server = LAPTOP-FOT3JMR1; database = SUBLIMACION; Integrated Security = true");//CONEXION

        public FrmPedidos()
        {
            InitializeComponent();
            tablaPedidos.AllowUserToAddRows = false;
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form
        }
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            BUSCAR();
        }

        //PERSONALIZACION FORM---------------------------------------------------------------------------------------------------------------------

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
        //-------------------------------------------------------------------------------------------------------------------------------------------

        private void BUSCAR()
        {
            conexion.Open();

            string query = "SELECT Pedidos.idPedido 'Id del pedido:', Cliente.nombreCliente 'Nombre del cliente: '," +
            " Producto.nombreProducto 'Nombre del producto: ', Producto.diseñoProducto 'Diseño: '" +
            "FROM Pedidos " +
            "INNER JOIN Cliente ON Pedidos.cliente = Cliente.idCliente " +
            "INNER JOIN Producto ON Pedidos.producto = Producto.idProducto";

            SqlCommand comando = new SqlCommand(query, conexion);


                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = comando;

                DataTable Tabla = new DataTable();
                adapter.Fill(Tabla);

            tablaPedidos.DataSource = Tabla;
            conexion.Close();
        }
    }
}
