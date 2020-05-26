using AppPaper.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AppPaperApi.Models;

namespace AppPaperApi.Data
{
    public class NoticiasInMemoryRespository
    {
        private List<Noticias> _noticias;
        private int _tamaño = 10;

        public NoticiasInMemoryRespository()
        {
            _noticias = new List<Noticias>();

            for (int i = 1; i < 30; i++)
            {
                var nuevasNoticias = new Noticias();

                nuevasNoticias.Cuerpo = "Cuerpo " + i;
                nuevasNoticias.Titulo = "Titulo " + i;
                nuevasNoticias.Id = i;
                nuevasNoticias.NombreImagen = ChoseImage(i);
                _noticias.Add(nuevasNoticias);
            }

            string ChoseImage(int discriminante)
            {
                switch (discriminante % 3)
                {
                    case 0:
                        return "noticia1.png";
                    case 1:
                        return "noticia2.png";
                    case 2:
                        return "noticia3.png";
                    default:
                        throw new NotImplementedException();
                }
            }
        }

        public List<Noticias> GetNoticias(int page)
        {
            return _noticias.Skip((page - 1) * _tamaño).Take(_tamaño)
                .Select(x => new Noticias {Id = x.Id, NombreImagen = x.NombreImagen, Titulo = x.Titulo }).ToList();
        }

        public Noticias GetNoticiaById(int Id)
        {
            return _noticias.FirstOrDefault(x => x.Id == Id);
        }
    }
}