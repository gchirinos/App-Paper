using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPaperApi.Models
{
    public class Noticias
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Cuerpo { get; set; }
        public string NombreImagen { get; set; }
    }
}