using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using agregados
using System.Net;
using System.Net.Mail;
using Portafolio.Models;

namespace Portafolio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Proyectos ()
        {
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpGet]
        public ActionResult Contact(string e)//recibe un parametro de url, el nombre debe ser el mismo que el parametro de url
        {
            if(e == "1")
            {
                ViewBag.Message = "1";
            }else if (e == "2")
            {
                ViewBag.Message = "2";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Contact(Email email)
        {
            if (ModelState.IsValid)
            {
                //string Remitente = Request.Form["txtEmail"];
                //string Asunto = Request.Form["txtAsunto"];
                //string Mensaje = Request.Form["txaMensaje"];
                //SendMail(Remitente, Asunto, Mensaje);
                string Remitente = email.EmisorEmail;
                string Asunto = email.Asunto;
                string Mensaje = email.Mensaje;

                if (SendMail(Remitente, Asunto, Mensaje) == true)
                {
                    return RedirectToAction("Contact", "Home", new { e = 1});//Se pasa como parametro de url la e
                                                                             //el 1 indica exito
                }
                else
                {
                    return RedirectToAction("Contact", "Home", new { e = 2 });//Se pasa como parametro de url la e
                }                                                             //el 2 indica fracaso
                
            }
            ViewBag.Message = "Antes de enviar, por favor revisa lo siguiente:";
            return View(email);
        }

        public bool SendMail(string Remitente, string Asunto, string Mensaje)
        {
            try
            {
                //instanciamos la clase MailMessage
                MailMessage _MailMessage = new MailMessage();

                //Agregamos al remitente
                string Transporte = "softwaredevelopment992@gmail.com";
                _MailMessage.From = new MailAddress(Transporte);

                //Estructura del correo
                string Destinatario = "alex.d4556@gmail.com";
                string CuerpoEmail = string.Format("<b>{0}</b>", Mensaje);

                _MailMessage.CC.Add(Destinatario);
                _MailMessage.Subject = string.Format("Remitente <<{0}>> Asunto: {1}", Remitente, Asunto);
                _MailMessage.IsBodyHtml = true;
                _MailMessage.Body = CuerpoEmail;

                //Configuracion del puerto
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                //Credenciales para enviar por SMTP seguro (Cuando el servidor lo exige)
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(Transporte, "adminnegocio2021");

                smtpClient.EnableSsl = true;
                smtpClient.Send(_MailMessage);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}