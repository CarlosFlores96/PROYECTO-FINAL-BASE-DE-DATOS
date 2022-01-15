using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; //LIBRERIA PARA MANIPULAR CODIGO SQL

namespace SUBLIMACION
{
    class conexionbd
    {
        String Cadena = "Data Source = LAPTOP-FOT3JMR1" +
                        ";Initial Catalog = PruebaBaseDatos" +
                        "; Integrated Security = True";

        public SqlConnection ConectarBD = new SqlConnection();

        public conexionbd()
        {
            ConectarBD.ConnectionString = Cadena; //Convertir la conexion a string (String Cadena)
        }

        public void AbrirBaseDatos() //Metodo para abrir la base de datos
        {
            try
            {
                ConectarBD.Open(); //Accion abrir base de datos
                Console.WriteLine("Conexión Abierta");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al abrir la Base de Datos " + ex.Message);
                throw;
            }
        }

        public void CerrarBaseDatos() //Clase para cerrar la base de datos
        {
            try
            {
                ConectarBD.Close(); //Accion cerrar base de datos
                Console.WriteLine("Conexión Cerrada");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cerrar la Base de Datos " + ex.Message);
                throw;
            }
        }
    }
}
