namespace HR.LeaveManagement.BlazorUI.Services.Base
{
    public partial interface IClient
    {
        public HttpClient HttpClient { get; }
    }
    public partial class Client : IClient
    {
        public HttpClient HttpClient
        {
            get
            {
                return _httpClient;
            }
        }
    }
}
