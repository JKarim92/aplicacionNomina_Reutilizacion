using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

//Importar libreria para conectar a SQL
using System.Data;
using System.Data.SqlClient;

//Importar otras funcionalidades
using System.Security.Cryptography;
using System.Configuration;

namespace aplicacionNomina.Controllers
{
    public class AccesoController : Controller
    {
        public ActionResult Autenticar()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registro()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
