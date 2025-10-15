﻿using Newtonsoft.Json;
using PackingApplication.Models.CommonEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PackingApplication.Helper
{
    public class HTTPMethod
    {
        public string GetCallApi(string WebApiurl)
        {
            var request = (HttpWebRequest)WebRequest.Create(WebApiurl);

            request.Method = "GET";
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            request.Headers.Add("Authorization", "Bearer " + SessionManager.AuthToken);
            var content = string.Empty;

            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var sr = new StreamReader(stream))
                    {
                        content = sr.ReadToEnd();
                    }
                }
            }

            return content;
        }
        public async Task<string> PostCallApi(string path, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(path);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.AuthToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json"))
            {
                var response = client.PostAsync(path, content).Result;
                //use await it has moved in some context on .core 6.0
                if (response.IsSuccessStatusCode == true)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }

        }

        public async Task<string> PutCallApi(string path, object data)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(path);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.AuthToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            using (var content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json"))
            {
                var response = client.PutAsync(path, content).Result;
                //use await it has moved in some context on .core 6.0
                if (response.IsSuccessStatusCode == true)
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    return response.Content.ReadAsStringAsync().Result;
                }
            }

        }

        public async Task<List<T>> PostAsync<T>(string path, object data)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(path);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.AuthToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(path, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserialize JSON into List<T>
                var result = JsonSerializer.Deserialize<List<T>>(responseBody,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return result ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calling API: {ex.Message}");
                return new List<T>();
            }
        }
    }
}
