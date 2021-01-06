using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace BlogPost.Utility
{
    public static class CommonUtility
    {
        public static string GetHtmlFilePath(string FileName, IWebHostEnvironment _env)
        {
            string filename = FileName;
            //string filename = "/usertyped_htmlcontent.html";
            string fullpath = Path.Combine(_env.WebRootPath + "/StoryFiles/", filename.TrimStart('/'));
            return fullpath;
        }

        public static string GetFirstImagesInHTML(string FileName, IWebHostEnvironment _env)
        {
            string firstimage = string.Empty;
            var fullpath = GetHtmlFilePath(FileName, _env);
            var HtmlCode = System.IO.File.ReadAllText(fullpath);
            var imageList = GetImagesInHTMLString(HtmlCode);
            if (imageList != null && imageList.Count() > 0)
                firstimage = imageList.FirstOrDefault();
            return firstimage;
        }

        public static string GetFirstParaGrapthInHTML(string FileName, IWebHostEnvironment _env)
        {
            string firstpara = string.Empty;
            var fullpath = GetHtmlFilePath(FileName, _env);
            var HtmlCode = System.IO.File.ReadAllText(fullpath);
            firstpara = GetFirstParagraph(HtmlCode);
            return firstpara;
        }
        public static List<string> GetImagesInHTMLString(string htmlString)
        {
            List<string> images = new List<string>();
            string pattern = @"<(img)\b[^>]*>";

            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(htmlString);

            for (int i = 0, l = matches.Count; i < l; i++)
            {
                images.Add(matches[i].Value);
            }
            return images;
        }

        public static string GetFirstParagraph(string htmlString)
        {
            Regex regex = new Regex("(<.*?>\\s*)+", RegexOptions.Singleline);
            var html = htmlString.Replace("\r", "").Replace("\n", " ").Replace("&nbsp;", " ");
            string resultText = regex.Replace(html, " ").Trim();
            string firstCharOfString = string.Empty;
            if (resultText.Length >= 50)
                firstCharOfString = resultText.Substring(0, 100) + "..";
            else
                firstCharOfString = resultText;

            return firstCharOfString;
        }

        public static string UploadedFile(IFormFile file, IWebHostEnvironment _env)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public static bool DeleteFile(string filename, IWebHostEnvironment _env)
        {
            bool IsDeleted = false;
            if (!string.IsNullOrEmpty(filename))
            {
                string uploadsFolder = Path.Combine(_env.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, filename);
                if (File.Exists(filePath)) 
                {
                    File.Delete(filePath);
                    IsDeleted = true;
                }
            }
            return IsDeleted;
        }
    }
}
