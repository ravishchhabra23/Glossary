using GlossaryWebUI.Models;
using Microsoft.Win32.SafeHandles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace GlossaryWebUI.Helpers
{
    public interface IApiRestClient
    {
        Task<GlossaryModel> CreateGlossary(GlossaryModel glossaryModel);
        Task<IEnumerable<GlossaryModel>> GetGlossaries();
        Task<GlossaryModel> GetGlossary(int termId);
        Task<string> UpdateGlossary(int termId, GlossaryModel glossaryModel);
        Task<string> DeleteGlossary(int termId);
    }

    public class ApiRestClient : IApiRestClient
    {
        bool disposed = false;
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        private static readonly string url = Convert.ToString(ConfigurationManager.AppSettings["GlossaryWebAPIUrl"]);
        private readonly ISessionHelper _sessionHelper;

        public ApiRestClient(ISessionHelper sessionHelper)
        {
            _sessionHelper = sessionHelper;
        }

        public static HttpClient CreateClient(string accessToken)
        {
            //while implementng authentication
            //if (!string.IsNullOrWhiteSpace(accessToken))
            //{
            //    client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            //}
            var client = new HttpClient();
            client.BaseAddress = new Uri(url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
            return client;
        }

        public Task<GlossaryModel> CreateGlossary(GlossaryModel glossaryModel)
        {
            var accessToken = string.Empty;
            using (var client = CreateClient(accessToken))
            {
                var response = client.PostAsJsonAsync(url + "Glossary/SaveGlossary",glossaryModel).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return null;
                }
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                if (response.IsSuccessStatusCode)
                {
                    var glossary = JsonConvert.DeserializeObject<GlossaryModel>(response.Content.ReadAsStringAsync().Result, serSettings);
                    return Task.FromResult(glossary);
                }
                else
                    return Task.FromResult<GlossaryModel>(null);
            }
        }

        public Task<string> UpdateGlossary(int id, GlossaryModel glossaryModel)
        {
           var accessToken = string.Empty;
           using (var client = CreateClient(accessToken))
           {
               var response = client.PutAsJsonAsync(url + "Glossary/UpdateGlossary/"+id, glossaryModel).Result;
               if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
               {
                   return null;
               }
               JsonSerializerSettings serSettings = new JsonSerializerSettings();
               serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
               if (response.IsSuccessStatusCode)
               {
                   var glossary = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result, serSettings);
                   return Task.FromResult(glossary);
               }
               else
                   return Task.FromResult<string>(null);
           }
        }

        public Task<string> DeleteGlossary(int id)
        {
            var accessToken = string.Empty;
            using (var client = CreateClient(accessToken))
            {
                var response = client.DeleteAsync(url + "Glossary/DeleteGlossary/" + id).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return null;
                }
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                if (response.IsSuccessStatusCode)
                {
                    var glossary = JsonConvert.DeserializeObject<string>(response.Content.ReadAsStringAsync().Result, serSettings);
                    return Task.FromResult(glossary);
                }
                else
                    return Task.FromResult<string>(null);
            }
        }


        public Task<IEnumerable<GlossaryModel>> GetGlossaries()
        {
            var accessToken = string.Empty;
            using (var client = CreateClient(accessToken))
            {
                var response = client.GetAsync(url + "Glossary/GetTerms/").Result;
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return null;
                }
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                if (response.IsSuccessStatusCode)
                {
                    var glossary = JsonConvert.DeserializeObject<IEnumerable<GlossaryModel>>(response.Content.ReadAsStringAsync().Result, serSettings);
                    return Task.FromResult(glossary);
                }
                else
                    return Task.FromResult<IEnumerable<GlossaryModel>>(null);
            }
        }

        public Task<GlossaryModel> GetGlossary(int id)
        {
            var accessToken = string.Empty;
            using (var client = CreateClient(accessToken))
            {
                var response = client.GetAsync(url + "Glossary/GetTermById/"+id).Result;
                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return null;
                }
                JsonSerializerSettings serSettings = new JsonSerializerSettings();
                serSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                if (response.IsSuccessStatusCode)
                {
                    var glossary = JsonConvert.DeserializeObject<GlossaryModel>(response.Content.ReadAsStringAsync().Result, serSettings);
                    return Task.FromResult(glossary);
                }
                else
                    return Task.FromResult<GlossaryModel>(null);
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }
            disposed = true;
        }

    }
}