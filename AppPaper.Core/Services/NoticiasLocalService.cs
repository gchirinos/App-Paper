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
    public class NoticiasLocalService
    {
        private NoticiasLocalRepositorio _noticiasLocalRepositorio;

        public NoticiasLocalService()
        {
            _noticiasLocalRepositorio = new NoticiasLocalRepositorio(ValuesService.GetDbPath());
        }

        public void Save(Noticia noticia)
        {
            _noticiasLocalRepositorio.Save(noticia);
        }

        public List<Noticia> GetAllSavedForReadLater()
        {
            return _noticiasLocalRepositorio.GetAll();
        }

        public void Delete(int id)
        {
            _noticiasLocalRepositorio.Delete(id);
        }

        public void Delete(List<int> ids)
        {
           ids.ForEach(x => Delete(x));
        }

        public void DeleteAll()
        {
            _noticiasLocalRepositorio.DeleteAll();
        }
    }
}