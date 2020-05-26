using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace AppPaper.Core.Models
{
    public class Noticia
    {
        [PrimaryKey]
        public int Id { get; set; }
        public string Titulo { get; set; }
        [Ignore]
        public string Cuerpo { get; set; }
        public string NombreImagen { get; set; }
    }
}