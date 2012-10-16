﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlixSharp.Constants
{
    internal class NetflixConstants
    {
        public const String RequestUrl = "http://api-public.netflix.com/oauth/request_token";
        public const String AccessUrl = "http://api-public.netflix.com/oauth/access_token";
        public const String LoginUrl = "https://api-user.netflix.com/oauth/login";

        public const String CatalogTitleUrl = "http://api-public.netflix.com/catalog/titles";

    }
}