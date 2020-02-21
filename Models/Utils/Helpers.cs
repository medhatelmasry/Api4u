using System;
using System.Collections.Generic;
using System.IO;
using Api4u.Models.Toons;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Api4u.Models.Utils
{
    public class Helpers
    {
        public static List<Picture> GetPictures(IWebHostEnvironment env, HttpRequest request)
        {
            string disneyPath = env.WebRootPath + @"\images\disney\";
            string flintstonePath = env.WebRootPath + @"\images\flintstone\";

            string[] disneyImages = Directory.GetFiles(disneyPath);
            string[] flintstoneImages = Directory.GetFiles(flintstonePath);

            List<Picture> pictures = new List<Picture>();

            string siteUrl = request.Host.Value.ToString();
            var scheme = request.Scheme;

            foreach (var item in flintstoneImages)
            {
                string fileName = item.Substring(item.LastIndexOf(@"\") + 1);
                string name = fileName.Substring(0, fileName.LastIndexOf(@"."));
                pictures.Add(new Picture
                {
                    Name = name,
                    Url = $"{scheme}://{siteUrl}/images/flintstone/{fileName}"
                });
            }

            foreach (var item in disneyImages)
            {
                string fileName = item.Substring(item.LastIndexOf(@"\") + 1);
                string name = fileName.Substring(0, fileName.LastIndexOf(@"."));
                pictures.Add(new Picture
                {
                    Name = name,
                    Url = $"{scheme}://{siteUrl}/images/disney/{fileName}"
                });
            }

            return pictures;
        }

        public static bool IsPictureInLegitToonList(string pictureUrl, IWebHostEnvironment env, HttpRequest request)
        {
            var pictures = Helpers.GetPictures(env, request);
            bool isOk = false;
            foreach (var p in pictures)
            {
                if (p.Url.Trim() == pictureUrl.Trim())
                {
                    isOk = true;
                    break;
                }
            }

            return isOk;
        }

        public static string GetFileNameFromUrl(string url)
        {
            Uri uri;
            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                uri = new Uri(url);

            return Path.GetFileName(uri.LocalPath);
        }

        public static string GetHostUrl(HttpRequest request) {
            var host = string.Format("{0}://{1}", request.Scheme, request.Host);
            return host;
        }
    }

}