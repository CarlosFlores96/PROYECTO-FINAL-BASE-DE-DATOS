using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SUBLIMACION
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
            panelMenuIzq.Visible = false;
            //TOOL TIPS------------------------------------------------------------------------------------------------------
            this.toolTip1.SetToolTip(this.btnCerrar, "Cerrar ventana");
            this.toolTip1.SetToolTip(this.btnMin, "Minimizar ventana");
            this.toolTip1.SetToolTip(this.btnMenu, "Abrir menu");
            this.toolTip1.SetToolTip(this.btnMapa, "Abrir mapa");
            this.toolTip1.SetToolTip(this.btnSesion, "Administrar sesiones");
            this.toolTip1.SetToolTip(this.btnConfiguracion, "Configuración del sistema");
            this.toolTip1.SetToolTip(this.btnAyuda, "Ayuda del programa");
            //----------------------------------------------------------------------------------------------------------------------------
        }

        public void frmPrincipal_Load(object sender, EventArgs e)
        {
            conexionbd conexion = new conexionbd();
            conexion.AbrirBaseDatos();
        }

//PERSONALIZACION FORM------------------------------------------------------------------------------------------------------

        int m, mx, my; //Coordenadas Movimiento Form

        private void pictureBox2_MouseUp(object sender, MouseEventArgs e)
        {
            m = 0; //Movimiento Form en 0
        }

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
        private void btnCerrar_Click_1(object sender, EventArgs e)
        {
            Close();
            conexionbd conexion = new conexionbd();
            conexion.CerrarBaseDatos();

        }

//----------------------------------------------------------------------------------------------------------------------------

//BOTONES PRINCIPALES---------------------------------------------------------------------------------------------------------

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            FrmClientes frm = new FrmClientes();
            frm.Show();
        }

        private void btnPedidos_Click(object sender, EventArgs e)
        {
            FrmPedidos frm = new FrmPedidos();
            frm.Show();
        }

        private void btnNuevoPedido_Click(object sender, EventArgs e)
        {
            FrmEditarPedido frm = new FrmEditarPedido();
            frm.Show();
        }

        private void btnPedidosPagados_Click(object sender, EventArgs e)
        {
            FrmPagados frm = new FrmPagados();
            frm.Show();
        }

        private void btnAyuda_Click(object sender, EventArgs e)
        {
            FrmAyuda frm = new FrmAyuda();
            frm.Show();
        }

        private void btnMapa_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com.mx/maps/preview");

        }

        private void btnConfiguracion_Click(object sender, EventArgs e)
        {
            FrmConfiguracion frm = new FrmConfiguracion();
            frm.Show();
        }

        private void lblMapa_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.google.com.mx/maps/preview");
        }

        private void lblConfigu_Click(object sender, EventArgs e)
        {
            FrmConfiguracion frm = new FrmConfiguracion();
            frm.Show();
        }

        private void lblAyuda_Click(object sender, EventArgs e)
        {
            FrmAyuda frm = new FrmAyuda();
            frm.Show();
        }


        //------------------------------------------------------------------------------------------------------------------------------

        //Abrir y minimizar----------------------------------------------------------------------------------------------------------------
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MinMaxPanel();
        }

        private void lblMenu_Click(object sender, EventArgs e)
        {
            MinMaxPanel();
        }

        private void btnProductos_Click(object sender, EventArgs e)
        {
            frmProductos frm = new frmProductos();
            frm.Show();
        }

        private void btnSesion_Click(object sender, EventArgs e)
        {
            frmDatosGenerales frm = new frmDatosGenerales();
            frm.Show();
        }

        private void lblSesion_Click(object sender, EventArgs e)
        {
            frmDatosGenerales frm = new frmDatosGenerales();
            frm.Show();
        }

        //------------------------------------------------------------------------------------------------------------------------------

        //------------------------------------------------------------------------------------------------------------------------------

        //Abrir y minimizar panel menu----------------------------------------------------------------------------------------------------------------
        public void MinMaxPanel()
        {
            if (PanelMenu.Width == 62) //Ancho que tiene el panel | 70 ancho, 680 alto
            {
                //panelPrincipal.Location = new Point(348, 158);
                panelMenuIzq.Visible = true;
                PanelMenu.Width = 260;
                //64, 515
                //74, 62 ACTUAL
                //NUEVO 348, 158
            }
            else
            {
                if (PanelMenu.Width == 260)     // REGRESAR TODO POSICION y MEDIDAS DEFAULT
                {
                    //panelPrincipal.Location = new Point(74, 62);
                    panelMenuIzq.Visible = false;
                    PanelMenu.Width = 62;
                }

            }
        }
        //------------------------------------------------------------------------------------------------------------------------------

        //METODO-----------------------------------------------------------------------------------------------------

        public void EjemploMetodo()
        {

        }

        //------------------------------------------------------------------------------------------------------------------------------
    }
}
