using System;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using Portafolio.Models;
using Portafolio.Env;
using System.Collections.Generic;

namespace Portafolio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Proyectos()
        {
            return View();
        }
        public ActionResult About()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Contact(string e)//recibe un parametro de url, el nombre debe ser el mismo que el parametro de url
        {
            if (e == "1")
            {
                ViewBag.Message = "1";
            }
            else if (e == "2")
            {
                ViewBag.Message = "2";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Email email)
        {
            if (ModelState.IsValid)
            {
                //string Remitente = Request.Form["txtEmail"];
                //string Asunto = Request.Form["txtAsunto"];
                //string Mensaje = Request.Form["txaMensaje"];
                //SendMail(Remitente, Asunto, Mensaje);
                string Remitente = email.Remitente;
                string Nombre = email.Nombre;
                string Asunto = email.Asunto;
                string Mensaje = email.Mensaje;
                string m = SendMail(Remitente, Nombre, Asunto, Mensaje);
                if (m == "")
                {
                    return RedirectToAction("Contact", "Home", new { e = 1 });//Se pasa como parametro de url la e
                                                                              //el 1 indica exito
                }
                else
                {
                    TempData["Error"] = m;
                    return RedirectToAction("Contact", "Home", new { e = 2 });//Se pasa como parametro de url la e
                }                                                             //el 2 indica fracaso

            }
            ViewBag.Message = "Antes de enviar, por favor revisa lo siguiente:";
            return View(email);
        }

        public string SendMail(string Remitente, string Nombre, string Asunto, string Mensaje)
        {
            try
            {
                //https://www.youtube.com/watch?v=ExqdE1IzpZ0
                //Instanciamos la clase MailMessage para preparar el correo.
                MailMessage _MailMessage = new MailMessage
                {
                    //Agregamos al remitente
                    From = new MailAddress(Credenciales.Transport, Nombre),
                    Subject = $"{Remitente}||{Asunto}",
                    IsBodyHtml = true,
                    //Cuerpo del correo
                    Body = string.Format("<p style=\"font-size: 1em; font-family: Arial, Helvetica, sans-serif;\">{0}</p>", Mensaje)
                };

                //Destinatarios
                _MailMessage.To.Add(Credenciales.Destinatario);
                //Con copia
                //_MailMessage.CC.Add(Remitente);


                //Configuracion del puerto por medio de la clase SmtpClient
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"))
                {
                    //Credenciales para enviar por SMTP seguro (Cuando el servidor lo exige)
                    UseDefaultCredentials = false,
                    EnableSsl = true,//especifica si se usara una ruta ssl
                    Credentials = new NetworkCredential(Credenciales.Transport, Credenciales.Frase)
                };

                smtpClient.Send(_MailMessage);
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}