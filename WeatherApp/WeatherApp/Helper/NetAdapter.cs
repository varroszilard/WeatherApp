using Newtonsoft.Json;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.Util;

namespace WeatherApp.Helper
{
    public class NetAdapter
    {
        private static NetAdapter _instance;
        public static NetAdapter Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new NetAdapter();
                }
                return _instance;
            }
        }

        /// <summary>
        /// singleton because: https://aspnetmonsters.com/2016/08/2016-08-27-httpclientwrong/
        /// </summary>
        private HttpClient client = new HttpClient();
        private SemaphoreSlim semaphore = new SemaphoreSlim(1);

        public NetAdapter()
        {
            var baseUri = "https://www.metaweather.com/api/";
            client.BaseAddress = new Uri(baseUri);
        }

        private string GetParamString(object data)
        {
            if (data == null)
                return null;
            StringBuilder sb = new StringBuilder("?");
            var fields = data.GetType().GetRuntimeProperties();
            if (fields == null || !fields.Any())
                return null;

            foreach (var field in fields)
            {
                sb.Append(String.Format("{0}={1}&", field.Name, field.GetValue(data)));
            }
            return sb.ToString();
        }


        public async Task<HttpResponseMessage> GetAsync(string url, object data = null)
        {
            var fullUrl = url;
            var obj = data.GetType().GetRuntimeProperties().FirstOrDefault();

            if (!obj.Name.Equals("woeid"))
                fullUrl += GetParamString(data);
            else
                fullUrl += obj.GetValue(data);

            var request = new HttpRequestMessage(HttpMethod.Get, fullUrl);
            HttpResponseMessage resp;

            await semaphore.WaitAsync();
            try
            {
                resp = await client.SendAsync(request);
            }
            catch (Exception e)
            {
                Debug.WriteLine("error: GET failed {0}", url);
                Debug.WriteLine(e);
                if (e.GetType().FullName.ToString() == "Java.Net.UnknownHostException")
                    throw new HttpRequestException(e.Message);
                throw e;
            }
            finally
            {
                semaphore.Release();
            }

            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException("User was not authorized on the server.");
            resp.EnsureSuccessStatusCode();
            var respText = await resp.Content.ReadAsStringAsync();

            return resp;
        }

        public async Task<T> GetAsync<T>(string url, object data = null)
        {
            var resp = await GetAsync(url, data);
            return await GetFromResponse<T>(resp);
        }

        public async Task<List<T>> GetListAsync<T>(string url, object data = null)
        {
            var resp = await GetAsync(url, data);

            var respText = await resp.Content.ReadAsStringAsync();

            if (respText.Length < 3)
                return null;

            var obj = JsonConvert.DeserializeObject<List<T>>(respText);
            return obj;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object data, object queryData = null)
        {
            var fullUrl = url;
            if (queryData != null)
            {
                fullUrl += GetParamString(queryData);
            }

            HttpContent content;
            var test = JsonConvert.SerializeObject(data);
            if (data is string)
                content = new StringContent(data as string);
            else
                content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
#pragma warning disable IDE0017 // Simplify object initialization
            var request = new HttpRequestMessage(HttpMethod.Post, fullUrl);
#pragma warning restore IDE0017 // Simplify object initialization
            request.Content = content;
            HttpResponseMessage resp;

            await semaphore.WaitAsync();
            try
            {
                resp = await client.SendAsync(request);
            }
            catch (Exception e)
            {
                Debug.WriteLine("error: Post failed {0} {1}", fullUrl, (data is string) ? data : JsonConvert.SerializeObject(data));
                Debug.WriteLine(e);
                throw e;
            }
            finally
            {
                semaphore.Release();
            }
            if (resp.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                throw new UnauthorizedAccessException("User was not authorized on the server (POST).");
            resp.EnsureSuccessStatusCode();
            return resp;
        }

        public async Task<T> PostAsync<T>(string url, object data, object queryData = null)
        {
            var resp = await PostAsync(url, data, queryData);
            return await GetFromResponse<T>(resp);
        }

        // -----------------------------------------------------------------------------------------------------------------
        private async Task<T> GetFromResponse<T>(HttpResponseMessage resp)
        {
            var respText = await resp.Content.ReadAsStringAsync();
            var obj = JsonConvert.DeserializeObject<T>(respText);
            return obj;
        }
    }

}
