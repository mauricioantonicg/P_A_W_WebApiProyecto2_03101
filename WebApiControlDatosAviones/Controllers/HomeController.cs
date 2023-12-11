using CP_Datos;
using CP_Entidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApiControlDatosAviones.Controllers
{
   public class HomeController : Controller
   {
      public ActionResult Index()
      {
         ViewBag.Title = "Home Page";

         return View();
      }

      [HttpGet]
      public JsonResult pruebaConexion()
      {
         return Json(new { resultado = "Mauricio", mensaje = "Calderon" }, JsonRequestBehavior.AllowGet);
      }

      [HttpPost]
      public JsonResult pruebaConexionPost( int prueba, int prueba2)
      {
         if (prueba == 0)
         {
            return Json(new { resultado = "Mauricio", mensaje = "Calderon" }, JsonRequestBehavior.AllowGet);
         }
         else
         {
            return Json(new { resultado = "Evelin", mensaje = "Solano" }, JsonRequestBehavior.AllowGet);
         }         
      }

      [HttpPost]
      public JsonResult RegistrarNewAvionBD(avion datosAvionJason)
      {
         int resultado = 0;
         int idTipoAvion = 0;

         CNX_Avion _cnxAvion = new CNX_Avion();

         if (datosAvionJason != null)
         {

            //Obtener el tipo de avion de la base de datos de acuerdo con la marca y el modelo del avion
            idTipoAvion = _cnxAvion.ConsultarTipoAvion(Convert.ToInt32(datosAvionJason.nombreMarcaAvion), Convert.ToInt32(datosAvionJason.nombreModeloAvion));

            if (idTipoAvion != -1)
            {
               //Poner el tipo de avion a los datos del avion
               datosAvionJason.idTipoAvio = idTipoAvion;

               //Enviar los datos a la capa de datos para que se registre el nuevo avion en la base de datos
               resultado = _cnxAvion.RegistrarNuevoAvion(datosAvionJason);

               //Incrementar la cantidad de aviones 

            }
            else
            {
               resultado = 0;
            }

            //Devolver el resultado del registro 1 exitoso, 0 fallo de registro
            return Json(new { resultado = resultado }, JsonRequestBehavior.AllowGet);
         }
         else
         {
            return Json(new { resultado = resultado }, JsonRequestBehavior.AllowGet);
         }
      }

   }
}
