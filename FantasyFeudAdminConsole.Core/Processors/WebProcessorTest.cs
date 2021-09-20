using FantasyFeudAdminConsole.Core.Models;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FantasyFeudAdminConsole.Core.Configuration;

namespace FantasyFeudAdminConsole.Core.Processors
{
    public class WebProcessorTest : IWebProcessor
    {
        private static readonly Uri _eventServerUri = GlobalConfig.WebServerSettings.EventServerTest;

        public WebProcessorTest() { }

        public async Task PostEvent(QuestionModel questionModel)
        {
            var res = "";
            try
            {
                HttpResponseMessage httpResMessage = null;
                using HttpClient httpClient = new();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = JsonSerializer.Serialize(questionModel);
                Debug.WriteLine(json);

                StringContent httpContent = new(json, Encoding.UTF8, "application/json");

                httpResMessage = await httpClient.PostAsync(_eventServerUri, httpContent);

                if (httpResMessage.StatusCode == HttpStatusCode.OK)
                {
                    res = httpResMessage.Content.ReadAsStringAsync().Result;
                }
                else
                {
                    res = "Some error occured. " + httpResMessage.StatusCode;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            Debug.WriteLine(res);
        }
    }
}