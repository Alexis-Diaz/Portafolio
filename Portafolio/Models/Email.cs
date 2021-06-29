using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using 
using System.ComponentModel.DataAnnotations;

namespace Portafolio.Models
{
    public class Email
    {
        [DataType(DataType.EmailAddress,ErrorMessage = "Debe ingresar un correo válido.")]
        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string EmisorEmail { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Asunto { get; set; }
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Mensaje { get; set; }
    }
}