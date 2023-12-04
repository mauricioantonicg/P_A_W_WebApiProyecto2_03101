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
   }
}
