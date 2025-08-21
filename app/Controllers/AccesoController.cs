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
        // Método helper para obtener la connection string procesada
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["cnn"].ConnectionString;
            string serverName = Environment.GetEnvironmentVariable("SQL_SERVER_NAME") ?? ".";
            return connectionString.Replace("{SERVER_NAME}", serverName);
        }

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
                using (SqlConnection cn = new SqlConnection(GetConnectionString()))
                using (SqlCommand cmd = new SqlCommand("dbo.AutenticarUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", oEmpleado.usuario?.Trim());
                    cmd.Parameters.AddWithValue("@clave", oEmpleado.clave?.Trim());
                    cmd.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@mensaje", SqlDbType.NVarChar, 200).Direction = ParameterDirection.Output;
                    cn.Open();
                    cmd.ExecuteNonQuery();
                    oEmpleado.id = cmd.Parameters["@id"].Value != DBNull.Value
                                    ? Convert.ToInt32(cmd.Parameters["@id"].Value)
                                    : 0;
                    string mensaje = cmd.Parameters["@mensaje"].Value?.ToString() ?? "Sin mensaje";
                    if (oEmpleado.id > 0)
                    {
                        return RedirectToAction("Index", "Home"); // login exitoso
                    }
                    else
                    {
                        // Usuario no encontrado o campos incorrectos
                        ViewBag.Mensaje = mensaje + " <a href='" + Url.Action("Registro", "Acceso") + "'>Regístrate aquí</a>";
                        ViewBag.AbrirModal = true; // Indicador para abrir modal
                        return View("Autenticar");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensaje = "Ocurrió un error al autenticar: " + ex.Message;
                ViewBag.AbrirModal = true;
                return View("Autenticar");
            }
        }
    }
}