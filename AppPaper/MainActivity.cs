using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AppPaper.Core.Models;
using AppPaper.Core.Services;
using Square.Picasso;

namespace AppPaper
{
    [Activity(Label = "AppPaper", Icon = "@drawable/icon", ParentActivity = typeof(ListaDeNoticiaActivity))]
    public class MainActivity : AppCompatActivity
    {
        internal static string Key_Id = "Key_Id";
        private Noticia _noticia;
        private readonly string KEY_BODY = "Key_Body";
        private readonly string KEY_TITLE = "Key_Title";
        private readonly string KEY_IMAGE_NAME = "Key_Image_Name";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            PrepareActionBar();

            var id = Intent.Extras.GetInt(Key_Id);

            if (savedInstanceState == null)
            {
                var noticiaServicio = new NoticiaServicio();
                _noticia = noticiaServicio.GetNoticiaById(id);
            }

            else
            {
                _noticia = new Noticia();
                _noticia.Id = savedInstanceState.GetInt(Key_Id);
                _noticia.Cuerpo = savedInstanceState.GetString(KEY_BODY);
                _noticia.NombreImagen = savedInstanceState.GetString(KEY_IMAGE_NAME);
                _noticia.Titulo = savedInstanceState.GetString(KEY_TITLE);
            }
            
            var noticiaTitulo = FindViewById<TextView>(Resource.Id.tituloNoticia);
            var noticiaCuerpo = FindViewById<TextView>(Resource.Id.textView2);
            var noticiaImagen = FindViewById<ImageView>(Resource.Id.imagenNoticia);

            var display = WindowManager.DefaultDisplay;
            Android.Graphics.Point point = new Android.Graphics.Point();
            display.GetSize(point);

            var imageUrl = string.Concat(ValuesService.ImageBaseUrl, _noticia.NombreImagen);

            Picasso.With(ApplicationContext)
                .Load(imageUrl)
                .Resize(point.X, 0)
                .Into(noticiaImagen);

            noticiaTitulo.Text = _noticia.Titulo;
            noticiaCuerpo.Text = _noticia.Cuerpo;
        }

        private void PrepareActionBar()
        {
            Android.Support.V7.App.ActionBar actionBar = SupportActionBar;
            actionBar.SetDisplayHomeAsUpEnabled(true);
        }

        protected override void OnSaveInstanceState(Bundle outState)
        {
            outState.PutString(KEY_BODY, _noticia.Cuerpo);
            outState.PutInt(Key_Id, _noticia.Id);
            outState.PutString(KEY_IMAGE_NAME, _noticia.NombreImagen);
            outState.PutString(KEY_TITLE, _noticia.Titulo);

            base.OnSaveInstanceState(outState);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.noticiasActionMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_read_later:
                    HandleReadLater();
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void HandleReadLater()
        {
            try
            {
                var noticiasLocalService = new NoticiasLocalService();
                noticiasLocalService.Save(_noticia);
                Toast.MakeText(this, "Saved", ToastLength.Short).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, "error: " + ex.Message, ToastLength.Long).Show();
            }
        }
    }
}