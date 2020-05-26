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
using SQLite;

namespace AppPaper.Core.Data
{
    internal class NoticiasLocalRepositorio
    {
        private string _dbPath;

        public NoticiasLocalRepositorio(string dbPath)
        {
            _dbPath = dbPath;
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.CreateTable<Noticia>();
            }
        }

        public void Save(Noticia noticia)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.InsertOrReplace(noticia);
            }
        }

        public List<Noticia> GetAll()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                return _db.Table<Noticia>().ToList();
            }
        }

        public void Delete(int Id)
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.Delete<Noticia>(Id);
            }
        }

        public void DeleteAll()
        {
            using (var _db = new SQLiteConnection(_dbPath))
            {
                _db.DeleteAll<Noticia>();
            }
        }
    }
}