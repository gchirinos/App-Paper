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
using AppPaper.Adapters;
using AppPaper.Core.Models;
using AppPaper.Core.Services;
using AppPaper.Fragments;

namespace AppPaper.Fragments
{
    internal class AllNoticiasListFragment : BaseListaDeNoticiaFragment, INotify
    {
        private NoticiaServicio _noticiaServicio;
        public int CurrentPage { get; set; }

        public AllNoticiasListFragment()
        {
            _noticiaServicio = new NoticiaServicio();
        }

        public override void OnActivityCreated(Bundle savedInstaceState)
        {
            base.OnActivityCreated(savedInstaceState);

            if (!_noticias.Any())
            {
                CurrentPage = 1;
                _noticias = _noticiaServicio.GetNoticias(CurrentPage);
            }

            SetupFragment();

            _listaNoticiaAdapter.RegisterLoadObserver(this);
        }

        public void NotifyObserver()
        {
            CurrentPage++;
            var proximaNoticia = _noticiaServicio.GetNoticias(CurrentPage);
            if (proximaNoticia.Any())
            {
                _noticias.AddRange(proximaNoticia);
                _listaNoticiaAdapter.AddNoticias(_noticias);
                _listaNoticiaAdapter.NotifyDataSetChanged();
            }
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            if (_selectedNoticias.Count > 0)
            {
                inflater.Inflate(Resource.Menu.noticiasActionMenu, menu);
            }

            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_read_later:
                    SaveSelectedNews(_selectedNoticias);
                    return true;
                    default:
                        return base.OnOptionsItemSelected(item);
            }
        }

        private void SaveSelectedNews(List<Noticia> selectedNoticias)
        {
            try
            {
                var noticiaLocalService = new NoticiasLocalService();
                foreach (var noticias in selectedNoticias)
                {
                    noticiaLocalService.Save(noticias);
                }

                Toast.MakeText(Activity, $"{selectedNoticias.Count} news saved", ToastLength.Short);
                selectedNoticias.Clear();
                Activity.InvalidateOptionsMenu();
                UnselectedElements();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Activity, ex.Message, ToastLength.Short).Show();
            }
        }
    }
}