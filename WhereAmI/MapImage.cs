﻿using System;
using System.Device.Location;
using System.Diagnostics;
using System.Net;

namespace WhereAmI
{
    class MapImage
    {
        public static void Show(GeoCoordinate location)
        {
            string filename = $"{location.Latitude:##.000},{location.Longitude:##.000},{location.HorizontalAccuracy:####}m.jpg";

            DownloadMapImage(builduri(location), filename);

            OpenWithDefaultApp(filename);
        }

        private static void DownloadMapImage(Uri target, string filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadFile(target, filename);
            }
        }

        /// <summary>
        /// Map Image REST API by HERE Location Services
        /// </summary>
        /// <remarks>
        /// https://developer.here.com/
        /// </remarks>
        private static Uri builduri(GeoCoordinate location)
        {
            #region here app id & app code
            string hereapi_appid = ""; // I removed my app id and app code for security reason. Run this application by placing your appID and AppCode
            string hereapi_appcode = ""; // You can generate your own app id and app code at wwww.developerhere.com
            #endregion

            var hereapi_dns = "image.maps.cit.api.here.com";
            var hereapi_url = $"https://{hereapi_dns}/mia/1.6/mapview";
            var hereapi_secrets = $"&app_id={hereapi_appid}&app_code={hereapi_appcode}";

            var latlon = $"&lat={location.Latitude}&lon={location.Longitude}";

            return new Uri(hereapi_url + $"?u={location.HorizontalAccuracy}" + hereapi_secrets + latlon);
        }

        private static void OpenWithDefaultApp(string filename)
        {
            var si = new ProcessStartInfo()
            {
                FileName = "cmd.exe",
                Arguments = $"/C start {filename}",
                WindowStyle = ProcessWindowStyle.Hidden
            };
            Process.Start(si);
        }
    }
}
