using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AppPaper.Core.Models;
using AppPaperApi.Data;
using AppPaperApi.Models;

namespace AppPaperApi.Controllers
{
    public class AppPaperController : ApiController
    {

        private NoticiasInMemoryRespository _respository;
        public AppPaperController()
        {
            _respository = new NoticiasInMemoryRespository();
        }

        public IEnumerable<Noticias> GetNoticias()
        {
            var paginaValor = GetQueryStringValueOfDefault("pagina", "1");
            int.TryParse(paginaValor, out int pagina);
            var noticias = _respository.GetNoticias(pagina);
            return noticias;
        }

        public IHttpActionResult GetNoticias(int Id)
        {
            var noticias = _respository.GetNoticiaById(Id);

            if (noticias == null)
            {
                return NotFound();
            }

            return Ok(noticias);
        }

        private string GetQueryStringValueOfDefault(string key, string defaultValue)
        {
            var queryString = Request.GetQueryNameValuePairs().FirstOrDefault(x => x.Key == key);
            if (queryString.Key == null)
            {
                return defaultValue;
            }

            return queryString.Value;
        }
    }
}
