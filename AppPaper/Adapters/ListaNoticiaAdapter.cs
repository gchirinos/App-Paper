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
using Square.Picasso;

namespace AppPaper.Adapters
{
    class ListaNoticiaAdapter : BaseAdapter<Noticia>
    {
        private Activity _contexto;
        private List<Noticia> _noticias;
        private ISelectedChecker _selectedChecker;
        private INotify _loadObserver;

        public ListaNoticiaAdapter(Activity contexto, List<Noticia> noticias, ISelectedChecker selectedChecker)
        {
            _contexto = contexto;
            _noticias = noticias;
            _selectedChecker = selectedChecker;
        }

        public override Noticia this[int position] => _noticias[position];

        public override int Count => _noticias.Count;

        public override long GetItemId(int position)
        {
            return this[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup convertViewGroup)
        {
            var item = this[position];

            if (convertView == null)
            {
                convertView = _contexto.LayoutInflater.Inflate(Resource.Layout.ListaDeNoticiaRow, null);
            }

            else
            {
                var id = (int) GetItemId(position);
                RelativeLayout rl = convertView.FindViewById<RelativeLayout>(Resource.Id.ListaDeNoticiaRow_RelativeLayout);

                if (_selectedChecker.IsItemSelected(id))
                {
                    var colorForSelected = _contexto.Resources.GetString(Resource.Color.listitemselected);
                    rl.SetBackgroundColor(Android.Graphics.Color.ParseColor(colorForSelected));
                }

                else
                {
                    var colorForUnselected = _contexto.Resources.GetString(Resource.Color.listitemunselected);
                    rl.SetBackgroundColor(Android.Graphics.Color.ParseColor(colorForUnselected));
                }

                if (IsEndOfList(position))
                {
                    NotifyLoadObserver();
                }
            }

            convertView.FindViewById<TextView>(Resource.Id.notTitulo).Text = item.Titulo;
            var imagenNoticia = convertView.FindViewById<ImageView>(Resource.Id.notImagen);

            var imagenUrl = string.Concat(ValuesService.ImageBaseUrl, item.NombreImagen);

            Picasso.With(_contexto)
                .Load(imagenUrl)
                .Placeholder(_contexto.GetDrawable(Resource.Drawable.Icon))
                .Into(imagenNoticia);

            return convertView;
        }

        internal void AddNoticias(List<Noticia> noticias)
        {
            _noticias = noticias;
        }

        private bool IsEndOfList(int position)
        {
            return position == Count - 1;
        }

        public void RegisterLoadObserver(INotify loadObserver)
        {
            _loadObserver = loadObserver;
        }

        private void NotifyLoadObserver()
        {
            if (_loadObserver != null)
            {
                _loadObserver.NotifyObserver();
            }
        }
    }
}