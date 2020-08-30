using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using SimbirSoft_Latfullin.Domain;
using SimbirSoft_Latfullin.Domain.Entities;
using SimbirSoft_Latfullin.ViewModels.Unique;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SimbirSoft_Latfullin.Services.Unique
{
    public class UniqueService : IUniqueService
    {
        private readonly ApplicationContext _context;
        private readonly ILogger<UniqueService> _logger;
        public UniqueService(ApplicationContext context, ILogger<UniqueService> logger)
        {
            _context = context;
            _logger = logger;
        }
        //<summary>
        //Метод GetTextFromPage
        //получает URL на входе
        //и отдаёт весь видимый текст на странице
        //</summary>
        //<param name="uri">Аргумент метода GetTextFromPage()</param>
        public async Task<UriResult> GetTextFromPage(string uri)
        {
            var uriResult = new UriResult { Uri = uri };

            //получение уникальных слов
            var text = GetUniqueWords(uriResult.Uri);
            uriResult.Result = text.Split(';');

            //обновление данных в бд
            try
            {
                await UpdateData(uri, text);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message, uri);
            }
            return uriResult;
        }

        //Example module
        public async Task <List<UriResult>> GetExample()
        {
            var results = new List<UriResult>
            {
                await GetTextFromPage("https://habr.com/ru/"),
                await GetTextFromPage("https://www.simbirsoft.com/"),
                await GetTextFromPage("https://docs.microsoft.com/ru-ru/aspnet/core/?view=aspnetcore-3.1")
            };
            return results;
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
                _logger.LogInformation(e.Message, uri);
                return e.Message;
            }

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);
            string text = doc.DocumentNode.SelectSingleNode("//body").InnerText;

            Dictionary<string, int> counter = new Dictionary<string, int>(); 
            foreach(var word in text.Split(' ', ',', '.', '!', '?','"', ';', ':', '[', ']', '(', ')', '\n', '\r', '\t'))
            {
                if (counter.ContainsKey(word.ToLower()))
                {
                    counter[word.ToLower()]++;
                }
                else if (word != null && word != "" && word.All(x => Char.IsLetterOrDigit(x)))
                {
                    counter.Add(word.ToLower(), 1);
                }

            }

            string result = "";
            foreach(var key in counter.Keys)
            {
                result += key + " - " + counter[key].ToString() + ";";
            }

            return result;
        }

        private async Task UpdateData(string uri, string text)
        {
            if (_context.UniqueResults.Any(x => x.Uri == uri))
            {
                var data = _context.UniqueResults.First(x => x.Uri == uri);
                data.Result = text;
                _context.UniqueResults.Update(data);
                await _context.SaveChangesAsync();
            }
            else
            {
                var data = new UniqueResult { Result = text, Uri = uri, Id = Guid.NewGuid().ToString() };
                _context.UniqueResults.Add(data);
                await _context.SaveChangesAsync();
            }
        }
    }
}
