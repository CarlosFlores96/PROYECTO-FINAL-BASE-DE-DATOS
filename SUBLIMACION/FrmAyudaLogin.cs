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
    public partial class FrmAyudaLogin : Form
    {
        public FrmAyudaLogin()
        {
            InitializeComponent();
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized; //Minimizar Form
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Close();
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

        //-----------------------------------------------------------------------------------------------------------------------
    }
}
