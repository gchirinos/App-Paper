﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AppPaper.Core.Models
{
    public class Response
    {
        public string Contenido { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}