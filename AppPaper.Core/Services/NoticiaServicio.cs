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
using AppPaper.Core.Data;
using AppPaper.Core.Models;

namespace AppPaper.Core.Services
{
    public class NoticiaServicio
    {
        private NoticiaRepositorio _noticiaRepositorio;

        public NoticiaServicio()
        {
            _noticiaRepositorio = new NoticiaRepositorio();
        }

        public List<Noticia> GetNoticias(int pagina)
        {
            return _noticiaRepositorio.GetNoticias(pagina);
        }

        public Noticia GetNoticiaById(int Id)
        {
            return _noticiaRepositorio.GetNoticiaById(Id);
        }
    }
}