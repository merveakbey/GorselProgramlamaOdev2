using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FinalOdevHazirlikMaui.Models
{
    public class NewsService
    {
        public static async Task<string> GetNews(string rss)
        {
            // API URL'si
            string api = $"https://api.rss2json.com/v1/api.json?rss_url={rss}";

            using HttpClient client = new HttpClient();

            // User-Agent Header ekleme (opsiyonel)
            client.DefaultRequestHeaders.Add("User-Agent", "MyApp/1.0");

            try
            {
                using HttpResponseMessage response = await client.GetAsync(api);
                string responseBody = await response.Content.ReadAsStringAsync();

                // Yanıtı logla
                Console.WriteLine($"Status Code: {response.StatusCode}");
                Console.WriteLine($"Response Body: {responseBody}");

                // HTTP hatasını fırlatmadan önce yanıtı kontrol et
                response.EnsureSuccessStatusCode();

                return responseBody;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP Hatası: {ex.Message}");
                throw new Exception($"Haberler yüklenemedi: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Beklenmeyen Hata: {ex.Message}");
                throw;
            }
        }
    }

    // Haber modeli için yardımcı sınıflar
    public class Enclosure
    {
        public string link { get; set; }
        public string type { get; set; }
    }

    public class Feed
    {
        public string url { get; set; }
        public string title { get; set; }
        public string link { get; set; }
        public string author { get; set; }
        public string description { get; set; }
        public string image { get; set; }
    }

    public class Item
    {
        public string title { get; set; }
        public string pubDate { get; set; }
        public string link { get; set; }
        public string guid { get; set; }
        public string author { get; set; }
        public string thumbnail { get; set; }
        public string description { get; set; }
        public string content { get; set; }
        public Enclosure enclosure { get; set; }
        public List<object> categories { get; set; }
    }

    public class HaberRoot
    {
        public string status { get; set; }
        public Feed feed { get; set; }
        public List<Item> items { get; set; }
    }
}
