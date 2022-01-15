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
    public partial class FrmLogin : Form
    {
        SqlConnection conexion = new SqlConnection("server = LAPTOP-FOT3JMR1; database = SUBLIMACION; Integrated Security = true");

        public FrmLogin()
        {
            InitializeComponent();
            
            //TOOL TIPS--------------------------------------------------------------
            this.toolTip1.SetToolTip(this.txtPassword, "Ingrese su contraseña");
            this.toolTip1.SetToolTip(this.txtUsuario, "Ingrese su usuario");
            this.toolTip1.SetToolTip(this.btnCerrarVentana, "Cerrar ventana");
            this.toolTip1.SetToolTip(this.btnEntrar, "Ingresar");
            this.toolTip1.SetToolTip(this.btnCerrar, "Cerrar ventana");
            this.toolTip1.SetToolTip(this.CheckBMostrarPass, "Mostrar contraseña");
            this.toolTip1.SetToolTip(this.button1, "Minimizar ventana");
            this.toolTip1.SetToolTip(this.btnOlvidarContraseña, "Ayuda");
            //------------------------------------------------------------------------

        }
        

        //PERSONALIZACION FORM------------------------------------------------------------------------------------------------------

        int m, mx, my; //Coordenadas Movimiento Form

        private void PanelLoginMover_MouseDown_1(object sender, MouseEventArgs e)
        {
            m = 1; //Movimiento Form inicializar movimiento
            mx = e.X;
            my = e.Y;
        }

        private void PanelLoginMover_MouseUp_1(object sender, MouseEventArgs e)
        {
            m = 0; //Movimiento Form en 0
        }

        private void PanelLoginMover_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (m == 1)
            {
                this.SetDesktopLocation(MousePosition.X - mx, MousePosition.Y - my); //Movimiento Form detectar posicion del mouse
            }
        }
        //-----------------------------------------------------------------------------------------------------------------------

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Login();
        }

        private void btnCerrarVentana_Click(object sender, EventArgs e)
        {
            Close();
            conexion.Close();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
            conexion.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form
        }

        private void btnOlvidarContraseña_Click(object sender, EventArgs e)
        {
            FrmAyudaLogin frm = new FrmAyudaLogin();
            frm.Show();
        }

        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form
        }

        private void CheckBMostrarPass_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBMostrarPass.Checked == true)
            {
                if (txtPassword.PasswordChar == '*')
                {
                    txtPassword.PasswordChar = '\0';
                }
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        public void Login()
        {
            try
            {
                conexion.Open();

                string consultar = "SELECT * FROM Usuario WHERE " +
                "nombre = '" + txtUsuario.Text + "' AND contrasenia = '" + txtPassword.Text + "'";

                SqlCommand comando = new SqlCommand(consultar,conexion);

                SqlDataReader lector;
                lector = comando.ExecuteReader();

                if (lector.HasRows == true)
                {
                    MessageBox.Show("Bienvenido a Sublimación FF.");

                    frmPrincipal frm = new frmPrincipal();
                    this.Hide();
                    frm.ShowDialog();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario y/o Contraseña erroneos ó tal vez no se encuentra registrado.");
                    MessageBox.Show("Escribir en las dos casillas sin oprimir enter.");
                    txtPassword.Text = "";
                    txtUsuario.Text = "";
                }
                conexion.Close();
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString());
            }
        }


        private void lanzarmensaje()
        {
            MessageBox.Show("Escribir en las dos casillas sin oprimir enter.");
        }

    }
}
