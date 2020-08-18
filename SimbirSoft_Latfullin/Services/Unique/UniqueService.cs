using HtmlAgilityPack;
using SimbirSoft_Latfullin.Domain;
using SimbirSoft_Latfullin.Domain.Entities;
using SimbirSoft_Latfullin.ViewModels.Unique;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace SimbirSoft_Latfullin.Services.Unique
{
    public class UniqueService : IUniqueService
    {
        private ApplicationContext _context;

        public UniqueService(ApplicationContext context)
        {
            _context = context;
        }

        public UriResult GetTextFromPage(string uri)
        {
            var uriResult = new UriResult { Uri = uri };
            var text = GetUniqueWords(uriResult.Uri);
            uriResult.Result = text.Split(';');
            UpdateData(uri, text);
            return uriResult;
        }

        private string GetHtml(string uri)
        {
            string html;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                using (StreamReader stream = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8))
                {
                    html = stream.ReadToEnd();
                }

                response.Close();
            }
            else
            {
                response.Close();
                throw new Exception("Requested address is invalid.");
            }

            return html;
        }

        private string GetUniqueWords(string uri)
        {
            string html = "";
            try
            {
                html = GetHtml(uri);
            }
            catch(Exception e)
            {
                return e.Message;
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            string text = doc.DocumentNode.SelectSingleNode("//body").InnerText;

            Dictionary<string, int> counter = new Dictionary<string, int>(); 
            foreach(var word in text.Split(' ', ',', '.', '!', '?','"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t'))
            {
                if (counter.ContainsKey(word))
                {
                    counter[word]++;
                }
                else if (word != null && word != "" && word.All(x => Char.IsLetter(x)))
                {
                    counter.Add(word, 1);
                }

            }

            string result = "";
            foreach(var key in counter.Keys)
            {
                result += key + " - " + counter[key].ToString() + ";";
            }

            return result;
        }

        private void UpdateData(string uri, string text)
        {
            if (_context.UniqueResults.Any(x => x.Uri == uri))
            {
                var data = _context.UniqueResults.First(x => x.Uri == uri);
                data.Result = text;
                _context.UniqueResults.Update(data);
                _context.SaveChanges();
            }
            else
            {
                var data = new UniqueResult { Result = text, Uri = uri, Id = Guid.NewGuid().ToString() };
                _context.UniqueResults.Add(data);
                _context.SaveChanges();
            }
        }
    }
}
