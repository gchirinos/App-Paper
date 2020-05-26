using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppPaper.Core.Models;

namespace AppPaper.Core.Services
{
    public class WebServices
    {
        public Response Get(string Url)
        {
            var request = WebRequest.Create(Url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (var httpResponse = request.GetResponse() as HttpWebResponse)
            {
                return BuilResponse(httpResponse);
            }
        }

        private static Response BuilResponse(HttpWebResponse httpResponse)
        {
            using (StreamReader reader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var content = reader.ReadToEnd();
                var response = new Response();
                response.Contenido = content;
                response.HttpStatusCode = httpResponse.StatusCode;
                return response;
            }
        }

        public async Task<Response> GetAsync(string Url)
        {
            var request = WebRequest.Create(Url);
            request.ContentType = "application/json";
            request.Method = "GET";
            using (HttpWebResponse httpResponse = await request.GetResponseAsync() as HttpWebResponse)
            {
                return BuilResponse(httpResponse);
            }
        }
    }
}