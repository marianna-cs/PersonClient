namespace PersonClient.ExternalConnectors
{
    public interface IExternalConnector
    {

        public Task<string> SendRequestAsync(string url, string httpMethod, string requestBody = null, string contentType = "application/json");

    }
}