using Newtonsoft.Json;
using StoreManagement.Application.Interfaces.IApiClientServices;
using Microsoft.Extensions.Configuration;
using Google.Apis.Auth.OAuth2;
using Mscc.GenerativeAI;

namespace StoreManagement.Infrastructure.ApiClient
{
    public class GoogleAPI : IGoogleAPI
    {
        private readonly string _apiKey;
        //private readonly string _serviceAccountPath = "D:\\Code Project\\StackProject\\store-management\\core\\StoreManagement\\google.json";
        public GoogleAPI(IConfiguration configuration) {
            _apiKey = configuration["GoogleAPI:APIKeyGemini"] ?? "";
        }
        public async Task<dynamic> Gemini(string prompt)
        {
            var googleAI = new GoogleAI(apiKey: _apiKey);
            var model = googleAI.GenerativeModel(model: Model.Gemini15Pro);

            var response = await model.GenerateContent(prompt);
            Console.WriteLine(response.Text);
            return response.Text;
        }

        // Lấy Access Token từ Google
        /*private async Task<string> GetAccessTokenAsync()
        {
            GoogleCredential credential = GoogleCredential.FromFile(_serviceAccountPath)
                .CreateScoped(new[] { "https://www.googleapis.com/auth/generative-language" });

            return await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();
        }*/
    }
}
