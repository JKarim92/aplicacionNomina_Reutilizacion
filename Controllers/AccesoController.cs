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
using aplicacionNomina.Models;
using System.Text;
using System.Web.UI.WebControls;

namespace aplicacionNomina.Controllers
{
    public class AccesoController : Controller
    {
        //Metodos Get - Donde obtenemos información
        [HttpGet]
        public ActionResult Autenticar()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpGet]
        public ActionResult Registro()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        //Metodos Post - enviamos información
        [HttpPost]
        public ActionResult Autenticar(Empleado oEmpleado)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cnn"].ConnectionString))
                {
                    SqlCommand cmd = new SqlCommand("select * from tbl_employees");
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    cn.Close();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error" + e);
                return View();
            }
        }
    }
}
