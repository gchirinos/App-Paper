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

namespace AppPaper.Fragments
{
    internal class SalvarListaDeNoticiaFragment : BaseListaDeNoticiaFragment
    {
        private NoticiasLocalService _noticiasLocalService;

        public SalvarListaDeNoticiaFragment()
        {
            _noticiasLocalService = new NoticiasLocalService();
        }

        public override void OnActivityCreated(Bundle savedInstaceState)
        {
            base.OnActivityCreated(savedInstaceState);

            FillNoticias();

            SetupFragment();
        }

        private void FillNoticias()
        {
            _noticias = _noticiasLocalService.GetAllSavedForReadLater();
        }

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            if (_selectedNoticias.Count > 0)
            {
                inflater.Inflate(Resource.Menu.savedNoticiaActionMenu, menu);
            }

            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.delete_saved_noticias:
                    DeleteSelectedNews(_selectedNoticias);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void DeleteSelectedNews(List<Noticia> selectedNoticias)
        {
            try
            {
                foreach (var noticia in selectedNoticias)
                {
                    _noticiasLocalService.Delete(noticia.Id);
                }

                Toast.MakeText(Activity, $"{selectedNoticias.Count} news deleted", ToastLength.Long);

                selectedNoticias.Clear();
                FillNoticias();
                SetupAdapter();
                Activity.InvalidateOptionsMenu();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Activity, ex.Message, ToastLength.Long).Show();
            }
        }
    }
}