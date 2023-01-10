namespace Better.Tools.HttpFactory
{
    public class HttpFactoryOptions
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public IDictionary<string, string> Header { get; set; }
    }
}
