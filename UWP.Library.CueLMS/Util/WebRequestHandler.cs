﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.CueLMS
{
        public class WebRequestHandler
        {
            private HttpClient Client { get; }
            public WebRequestHandler()
            {
                Client = new HttpClient();
            }
            public async Task<string> Get(string url)
            {
                try
                {
                    using (var client = new HttpClient())
                    {
                        var response = await
                        client.GetStringAsync(url).ConfigureAwait(false);
                        return response;
                    }
                }
                catch (Exception e)
                {
                }
                return null;
            }
            public async Task<string> Post(string url, object obj, HttpMethod method) //now takes in the method so it can do delete as well as post
            {
                using (var client = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(method, url))
                    {
                        var json = JsonConvert.SerializeObject(obj);
                        using (var stringContent = new StringContent(json,
                        Encoding.UTF8, "application/json"))
                        {
                            request.Content = stringContent;
                            using (var response = await client
                            .SendAsync(request,
                            HttpCompletionOption.ResponseHeadersRead)
                            .ConfigureAwait(false))
                            {
                                if (response.IsSuccessStatusCode)
                                {
                                    return await response.Content.ReadAsStringAsync();
                                }
                                return "ERROR";
                            }
                        }
                    }
                }
            }
        }
}
