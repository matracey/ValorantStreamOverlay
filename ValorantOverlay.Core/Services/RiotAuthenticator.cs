using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using ValorantOverlay.Core.Models;
using System.Web;
using System.Text;
using ValorantOverlay.Core.Exceptions;
using System.Net;
using System.Linq;

namespace ValorantOverlay.Core.Services
{
    public interface IRiotAuthenticator
    {
        Task GetAuthorizationAsync();
        Task<RiotAuthentication> GetAuthenticationTokensAsync(string user, string pass);
        Task<string> GetEntitlementsTokenAsync(string accessToken);
        void VoidCookies();
    }

    public class RiotAuthenticator : IRiotAuthenticator
    {
        private const string REDIRECT_URI = "https://playvalorant.com/opt_in";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly CookieContainer _cookieContainer;

        public RiotAuthenticator(IHttpClientFactory clientFactory, CookieContainer cookieContainer)
        {
            _httpClientFactory = clientFactory;
            _cookieContainer = cookieContainer;
        }

        public async Task GetAuthorizationAsync()
        {
            var client = _httpClientFactory.CreateClient(HttpClientName.AuthRiot);
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/v1/authorization");
            var content = JsonSerializer.Serialize(new
            {
                client_id = "play-valorant-web-prod",
                nonce = "1",
                redirect_uri = REDIRECT_URI,
                response_type = "token id_token",
                scope = "account openid"
            });
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.SendAsync(request);
            } catch (HttpRequestException ex)
            {
                throw new RiotAuthenticationException("An error occurred while attempting to authenticate.", ex);
            }
        }

        public async Task<RiotAuthentication> GetAuthenticationTokensAsync(string user, string pass)
        {
            var client = _httpClientFactory.CreateClient(HttpClientName.AuthRiot);
            var request = new HttpRequestMessage(HttpMethod.Put, "/api/v1/authorization");
            var content = JsonSerializer.Serialize(new
            {
                type = "auth",
                username = user,
                password = pass
            });
            request.Content = new StringContent(content, Encoding.UTF8, "application/json");

            try
            {
                var result = await client.SendAsync(request);
                var authResponseJson = await result.Content.ReadAsStringAsync();
                var authResponse = JsonSerializer.Deserialize<AuthenticationResponse>(authResponseJson);
                if (authResponse.Type == "error")
                {
                    throw new AuthenticationErrorException($"An error occurred while attempting to authenticate. {authResponse.Error}");
                }
                if (authResponse.Error == "auth_failure")
                {
                    throw new UserLoginInvalidException();
                }
                var parts = authResponse?.Response?.Parameters?.Uri?.Split('#');
                var queryParams = HttpUtility.ParseQueryString(parts?[1]);
                return new RiotAuthentication
                {
                    AccessToken = queryParams["access_token"],
                    IdToken = queryParams["id_token"]
                };

            }
            catch (Exception ex) when (ex is HttpRequestException || ex is AuthenticationErrorException)
            {
                throw new RiotAuthenticationException("An error occurred while attempting to authenticate.", ex);
            }
        }

        public async Task<string> GetEntitlementsTokenAsync(string accessToken)
        {
            var client = _httpClientFactory.CreateClient(HttpClientName.EntitlementsAuthRiot);
            var request = new HttpRequestMessage(HttpMethod.Post, "/api/token/v1");

            request.Headers.Add("Authorization", $"Bearer {accessToken}");
            request.Content = new StringContent("{}", Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await client.SendAsync(request);
            }
            catch (HttpRequestException ex)
            {
                throw new RiotAuthenticationException("An error occurred while attempting to load entitlements authentication token.", ex);
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var entitlementTokenResponse = JsonSerializer.Deserialize<EntitlementsAuthenticationResponse>(responseBody);

            return entitlementTokenResponse.Token;
        }

        public void VoidCookies()
        {
            var authClient = _httpClientFactory.CreateClient(HttpClientName.AuthRiot);
            var entitlementsClient = _httpClientFactory.CreateClient(HttpClientName.EntitlementsAuthRiot);

            _cookieContainer.GetCookies(authClient.BaseAddress).Cast<Cookie>().ToList().ForEach(c => c.Expired = true);
            _cookieContainer.GetCookies(entitlementsClient.BaseAddress).Cast<Cookie>().ToList().ForEach(c => c.Expired = true);
        }
    }
}
