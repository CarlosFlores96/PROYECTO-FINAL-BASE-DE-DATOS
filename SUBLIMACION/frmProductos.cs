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
    public partial class frmProductos : Form
    {
        SqlConnection conexion = new SqlConnection("server = LAPTOP-FOT3JMR1; database = SUBLIMACION; Integrated Security = true");

        public frmProductos()
        {
            InitializeComponent();
            panelEditarCliente.Enabled = false;
            listaProductos.AllowUserToAddRows = false;
        }

        //MOVER FORM-----------------------------------------------------------------------------------------------------------------------

        int m, mx, my;

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
        //MOVER FORM-----------------------------------------------------------------------------------------------------------------------

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form

        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btnCancelarProducto_Click(object sender, EventArgs e)
        {
            eliminarcampos();
        }
        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Login();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtUsuario.Text = "";
        }
        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
                string nombreProducto;
                string materialProducto;
                string diseñoProducto;
                string precioProducto;
                
                nombreProducto = txtNombreProducto.Text;
                materialProducto = txtMaterialProducto.Text;
                diseñoProducto = txtDisenio.Text;
                precioProducto = txtPrecio.Text;
                    conexion.Open();
            try
            {
                using (SqlCommand querys = new SqlCommand(
                "INSERT INTO Producto (nombreProducto,materialProducto,diseñoProducto,precioProducto) " +
                "VALUES ('" + nombreProducto + "','" + materialProducto + "','" + diseñoProducto + "','" + precioProducto + "')" +
                "DELETE FROM Producto WHERE idProducto IS NOT NULL AND nombreProducto = '' AND materialProducto = '' AND diseñoProducto = '' AND precioProducto = 0 ", conexion))
                {
                    
                    using (SqlDataReader lectorProducto = querys.ExecuteReader())
                    {
                        //while (lectorProducto.Read())
                        //{
                        //    MessageBox.Show(lectorProducto.GetString(0), "producto.nombreproducto" + "producto.materialproducto" + "producto.diseñoproducto" + "producto.precioproducto");
                        //    MessageBox.Show("Producto agregado correctamente.");
                        //    eliminarcampos();
                        //    actualizarTabla();
                        //}
                        if (lectorProducto.NextResult())
                        {
                            while (lectorProducto.Read())
                            {
                                MessageBox.Show(lectorProducto.GetString(0), "Producto.idProducto");
                                eliminarcampos();
                            }
                        }
                        lectorProducto.Close();
                        MessageBox.Show("Producto agregado correctamente.");
                        actualizarTabla();
                        eliminarcampos();
                    }

                    conexion.Close();
                }
            }
            catch (Exception error)
            {
                eliminarcampos();
                MessageBox.Show(error.ToString());
                throw;
            }
        }

        private void btnActualizarTAbla_Click(object sender, EventArgs e)
        {
            actualizarTabla();
        }

        //---------------------------------------------------------------------------------------------------------------------------------

        // METODOS
        public void Login()
        {
            try
            {
                conexion.Open();

                string consultar = "SELECT * FROM Usuario WHERE " +
                "nombre = '" + txtUsuario.Text + "' AND contrasenia = '" + txtPassword.Text + "'";

                SqlCommand comando = new SqlCommand(consultar, conexion);

                SqlDataReader lectorLogin;
                lectorLogin = comando.ExecuteReader();

                lectorLogin.Read();

                if (lectorLogin.HasRows == true)
                {
                    MessageBox.Show("Datos correctos.");
                    panelEditarCliente.Enabled = true;
                    panelChecarUsuario.Visible = false;
                    lectorLogin.Close();
                    actualizarTabla();
                }
                else
                {
                    lectorLogin.Read();
                    MessageBox.Show("Usuario y/o Contraseña erroneos ó tal vez no se encuentra registrado.");
                    txtPassword.Text = "";
                    txtUsuario.Text = "";
                    lectorLogin.Close();
                }

                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            conexion.Close();
                try
                {

                    conexion.Open();

                    if (listaProductos.Rows.Count == 0)
                    {
                        MessageBox.Show("No se seleccionó ningún producto en la lista.");
                    }
                    else
                    {
                        string idProducto = listaProductos.CurrentRow.Cells[0].Value.ToString();
                        string query = ("DELETE FROM Producto WHERE idProducto = " + idProducto);

                        SqlCommand comando = new SqlCommand(query, conexion);


                        comando.ExecuteNonQuery();
                        MessageBox.Show("Producto borrado exitosamente.");
                        actualizarTabla();
                    }
                    conexion.Close();
                }
                    catch (SqlException odbcEx)
                    {
                        MessageBox.Show("Verifique si el producto no tiene un pedido registrado.");
                    }

                     catch (Exception error)
                        {
                            MessageBox.Show(error.ToString());
                        }
        }

        //---------------------------------------------------------------------------------------------------------

        private void eliminarcampos()
        {
            txtDisenio.Text = "";
            txtMaterialProducto.Text = "";
            txtNombreProducto.Text = "";
            txtPrecio.Text = "";
        }

        //Actualizar tabla-------------------------------------------------------------------------------------------------------
        public void actualizarTabla()
        {

            SqlCommand comando = new SqlCommand
            ("SELECT Producto.idProducto 'idProducto: ', Producto.nombreProducto 'Nombre: ', Producto.materialProducto 'Material: ', Producto.diseñoProducto 'Diseño: ', Producto.precioProducto 'Precio: ' FROM Producto" , conexion);

            SqlDataAdapter adapter = new SqlDataAdapter();
            adapter.SelectCommand = comando;

            DataTable Tabla = new DataTable();
            adapter.Fill(Tabla);

            try
            {
                listaProductos.DataSource = Tabla;
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }
        //Fin---------------------------------------------------------------------------------------------------------------------


    }
}
