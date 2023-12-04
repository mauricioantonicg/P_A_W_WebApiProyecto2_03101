using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CP_Datos
{
   public class BDConexion
   {
      public static string conexionBD = ConfigurationManager.ConnectionStrings["DBConexion"].ToString();
   }
}
