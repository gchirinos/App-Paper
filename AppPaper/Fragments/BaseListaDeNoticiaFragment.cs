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

namespace AppPaper.Fragments
{
   internal class BaseListaDeNoticiaFragment : Fragment, ISelectedChecker
    {
        protected ListView _noticiaslListView;
        protected List<Noticia> _noticias;
        protected List<Noticia> _selectedNoticias;
        protected ListaNoticiaAdapter _listaNoticiaAdapter;

        public BaseListaDeNoticiaFragment()
        {
            _noticias = new List<Noticia>();
            _selectedNoticias = new List<Noticia>();
        }

        public override void OnCreate(Bundle savedInstaceState)
        {
            _selectedNoticias.Clear();
            base.OnCreate(savedInstaceState);
            SetHasOptionsMenu(true);
        }

        protected void SetupFragment()
        {
            SetupViews();

            SetupEvents();

            SetupAdapter();
        }

        protected void SetupAdapter()
        {
            _listaNoticiaAdapter = new ListaNoticiaAdapter(Activity, _noticias, this);
            _noticiaslListView.Adapter = _listaNoticiaAdapter;
        }

        protected void SetupViews()
        {
            _noticiaslListView = View.FindViewById<ListView>(Resource.Id.listaNoticiasView);
        }

        protected void SetupEvents()
        {
            _noticiaslListView.ItemClick += noticiaslListView_ItemClick;
            _noticiaslListView.ItemLongClick += _noticiaslListView_ItemLogClick;
        }

        private void _noticiaslListView_ItemLogClick(object sender, AdapterView.ItemLongClickEventArgs e)
        {
            var id = (int) e.Id;
            View view = e.View;

            RelativeLayout rl = view.FindViewById<RelativeLayout>(Resource.Id.ListaDeNoticiaRow_RelativeLayout);

            if (_selectedNoticias.Select(x => x.Id).Contains(id))
            {
                // deseleciona elemento
                var colorForUnselected = Resources.GetString(Resource.Color.listitemunselected);
                rl.SetBackgroundColor(Android.Graphics.Color.ParseColor(colorForUnselected));
                _selectedNoticias.Remove(_listaNoticiaAdapter[e.Position]);
            }

            else
            {
                // seleccionar elemento
                var colorForSelected = Resources.GetString(Resource.Color.listitemselected);
                rl.SetBackgroundColor(Android.Graphics.Color.ParseColor(colorForSelected));
                _selectedNoticias.Add(_listaNoticiaAdapter[e.Position]);
            }

            // forza a android a ejecutar onCreateOptionsMenu
            Activity.InvalidateOptionsMenu();
        }

        private void noticiaslListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intento = new Intent(Activity, typeof(MainActivity));
            var id = (int) e.Id;
            intento.PutExtra(MainActivity.Key_Id, id);
            StartActivity(intento);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            return inflater.Inflate(Resource.Layout.ListaDeNoticiaFragment, container, false);
        }

        protected void UnselectedElements()
        {
            int count = _noticiaslListView.ChildCount;

            for (int i = 0; i < count; i++)
            {
                View row = _noticiaslListView.GetChildAt(i);
                var rl = row.FindViewById<RelativeLayout>(Resource.Id.ListaDeNoticiaRow_RelativeLayout);
                var color = Resources.GetString(Resource.Color.listitemunselected);
                rl.SetBackgroundColor(Android.Graphics.Color.ParseColor(color));
            }
        }

        public bool IsItemSelected(int id)
        {
            return _selectedNoticias.Select(x => x.Id).Contains(id);
        }
    }
}