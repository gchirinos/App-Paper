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
using AppPaper.Core.Models;
using AppPaper.Core.Services;

namespace AppPaper.Core.Data
{
    internal class NoticiaRepositorio
    {
        private WebServices _webServices;
        public NoticiaRepositorio()
        {
            _webServices = new WebServices();
        }
        public List<Noticia> GetNoticias(int page)
        {
            var queryString = "?page=" + page;
            var response = _webServices.Get(ValuesService.NoticiasApiUrl + queryString);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<Noticia>>(response.Contenido);
        }

        public Noticia GetNoticiaById(int Id)
        {
            var response = _webServices.Get(ValuesService.NoticiasApiUrl + "/" + Id);

            if (response.HttpStatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new ApplicationException("Noticia no encontrada");
            }

            return Newtonsoft.Json.JsonConvert.DeserializeObject<Noticia>(response.Contenido);
        }
    }

 }