using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppPaper.Adapters;
using AppPaper.Core.Models;
using AppPaper.Core.Services;
using AppPaper.Fragments;
using SQLite;

namespace AppPaper
{
    [Activity(Label = "AppPaper", MainLauncher = true, Theme = "@android:style/android:Theme.Holo.Light.DarkActionBar", LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class ListaDeNoticiaActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.ListaDeNoticia);


            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;

            var tasbHeaderAllNoticias = Resources.GetString(Resource.String.ListaDeNoticiaActivity_Tabs_AllNoticias_Header);
            var tasbHeaderSavedNoticias = Resources.GetString(Resource.String.ListaDeNoticiaActivity_Tabs_SavedNoticias_Header);

            AddTab(tasbHeaderAllNoticias, new AllNoticiasListFragment());
            AddTab(tasbHeaderSavedNoticias, new SalvarListaDeNoticiaFragment());
        }

        private void AddTab(string tituloTab, Fragment fragmento)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(tituloTab);

            tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                var fragmentoAnterior = FragmentManager.FindFragmentById(Resource.Id.listaDeNoticiaFragmentContainer);

                if (fragmentoAnterior != null)
                {
                    e.FragmentTransaction.Remove(fragmentoAnterior);
                }
                e.FragmentTransaction.Add(Resource.Id.listaDeNoticiaFragmentContainer, fragmento);
            };

            tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(fragmento);
            };

            ActionBar.AddTab(tab);
        }
    }
}