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

namespace AppPaper.Core.Services
{
    public class ValuesService
    {
        public static readonly string ImageBaseUrl = "http://mirepogavilanch2.azurewebsites.net/images/";
        public static readonly string NoticiasApiUrl = "http://192.168.0.24:7010/api/AppPaper";
        public static readonly string DbName = "AppPaperDB.db";

        public static string GetDbPath()
        {
            string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(folder, DbName);
        }
    }
}