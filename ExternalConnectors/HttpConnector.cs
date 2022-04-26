using System.Net.Http.Headers;

namespace PersonClient.ExternalConnectors
{
    public class HttpConnector : IExternalConnector
    {

        public async Task<string> SendRequestAsync(string url, string httpMethod, string requestBody = null, string contentType = "application/json")
        {
            switch (httpMethod)
            {
                case "GET": return await SendGetRequestAsync(url, contentType);
                case "POST": return await SendPostRequestAsync(url, requestBody, "application/json");
                default: throw new Exception("Method not found");
            }


        }

        private async Task<string> SendGetRequestAsync(string url, string contentType)
        {
            HttpClient client = new HttpClient();
            var data = await client.GetStreamAsync(url);
            using (var sr = new StreamReader(data))
            {
                var bufer = sr.ReadToEnd();
                //Console.WriteLine("response={0}", bufer);
                return bufer;
            }
        }

        private async Task<string> SendPostRequestAsync(string url, string requestBody, string contentType)
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(url);
            request.Method = HttpMethod.Post;
            request.Headers.Add("Accept", contentType);

            request.Content = new StringContent(requestBody);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            var resp = await client.SendAsync(request);
            return await resp.Content.ReadAsStringAsync();
        }
    }

}
