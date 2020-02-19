
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;


namespace TodoList.Services
{
    public sealed class ApiClient
    {
        public const string URL = "https://td-api.julienmialon.com/";
        public const string LOGIN = "auth/login"; // POST login with email/password
        public const string REFRESH = "auth/refresh"; // POST refresh a token
        public const string REGISTER = "auth/register"; // POST register a user
        public const string ME = "me"; // GET User profile
        public const string UPDATE_PROFILE = "me"; // PATCH Update profile (firstname, lastname)
        public const string UPDATE_PASSWORD = "me/password"; // PATCH Update password

        public const string LIST_PLACES = "places"; // GET List of places
        public const string GET_PLACE = "places/"; // GET place detail

        public const string CREATE_PLACE = "places"; //POST Create a place
        public const string CREATE_COMMENT = "/comments"; //POST Add a comment

        public const string CREATE_IMAGE = "images"; //POST upload an image
        public const string GET_IMAGE = "images/"; //GET retrieve image data

        public const string IMAGE_NOT_FOUND = nameof(IMAGE_NOT_FOUND);

        public const string EMAIL_ALREADY_EXISTS = nameof(EMAIL_ALREADY_EXISTS);

        private string _accessToken;

        private readonly HttpClient _httpClient;

        private static readonly Lazy<ApiClient> apiClient = new Lazy<ApiClient>(() => new ApiClient());


        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        public static ApiClient ApiInstance { get { return apiClient.Value; } }

        public async Task<UserItem> GetUserSession()
        {
            Uri uri = new Uri(URL + ME);
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject jSonObject = JObject.Parse(content);
                UserItem res = JsonConvert.DeserializeObject<UserItem>(jSonObject.GetValue("data").ToString());
                return res;
            }
            return null;
        }

        public async Task<List<PlaceItem>> GetPlaces()
        {
            Uri uri = new Uri(URL + LIST_PLACES);
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject jSonObject = JObject.Parse(content);
                List<PlaceItem> res = JsonConvert.DeserializeObject<List<PlaceItem>>(jSonObject.GetValue("data").ToString());
                return res;
            }
            return null;
        }

        public async Task<List<CommentItem>> GetCommentsPlace(int idPlace)
        {
            Uri uri = new Uri(URL + GET_PLACE + idPlace);
            HttpResponseMessage response = await _httpClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject jSonObject = JObject.Parse(content);
                List<CommentItem> res = JsonConvert.DeserializeObject<List<CommentItem>>(((JObject)(jSonObject.GetValue("data"))).GetValue("comments").ToString());
                return res;
            }
            return null;
        }

        public async Task<bool> CreatePlace(PlaceItem place)
        {
            Uri uri = new Uri(URL + CREATE_PLACE);
            string json = JsonConvert.SerializeObject(place);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            response = await _httpClient.PostAsync(uri, content);

            return (response.IsSuccessStatusCode);
        }

        public async Task<bool> CreateRegisterRequest(RegisterRequest registerRequest)
        {
            Uri uri = new Uri(URL + REGISTER);
            string json = JsonConvert.SerializeObject(registerRequest);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            response = await _httpClient.PostAsync(uri, content);

            return (response.IsSuccessStatusCode);
        }

        public async Task<bool> Authentification(string email, string password)
        {
            Uri uri = new Uri(URL + LOGIN);

            LoginRequest loginRequest = new LoginRequest(email, password);
            string json = JsonConvert.SerializeObject(loginRequest);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            response = await _httpClient.PostAsync(uri, content);
            if (response.IsSuccessStatusCode)
            {
                string contentResponse = await response.Content.ReadAsStringAsync();
                JObject jSonObject = JObject.Parse(contentResponse);
                LoginResult res = JsonConvert.DeserializeObject<LoginResult>(((JObject)(jSonObject.GetValue("data"))).ToString());
                _accessToken = res.AccessToken;
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            }

            return (response.IsSuccessStatusCode);
        }

        /* Add comment to a place */
        public async Task<bool> CreateComment(int placeId, CreateCommentRequest createCommentRequest)
        {
            Uri uri = new Uri(URL + CREATE_PLACE + "/" + placeId+ CREATE_COMMENT);
            string json = JsonConvert.SerializeObject(createCommentRequest);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            response = await _httpClient.PostAsync(uri, content);

            return (response.IsSuccessStatusCode);
        }

        public async Task<bool> UpdateProfile(UpdateProfileRequest updateProfileRequest)
        {
            Uri uri = new Uri(URL + UPDATE_PROFILE);
            string json = JsonConvert.SerializeObject(updateProfileRequest);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, uri)
            {
                Content = content
            };
            var response = await _httpClient.SendAsync(request);

            return (response.IsSuccessStatusCode);
        }

        public async Task<bool> UpdatePassword(UpdatePasswordRequest updatePasswordRequest)
        {
            Uri uri = new Uri(URL + UPDATE_PASSWORD);
            string json = JsonConvert.SerializeObject(updatePasswordRequest);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, uri)
            {
                Content = content
            };
            var response = await _httpClient.SendAsync(request);

            return (response.IsSuccessStatusCode);
        }

        public async Task<int> SendImage(byte[] imageData)
        {
            HttpClient client = new HttpClient();

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");

            MultipartFormDataContent requestContent = new MultipartFormDataContent();

            var imageContent = new ByteArrayContent(imageData);
            imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

            // Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
            requestContent.Add(imageContent, "file", "file.jpg");

            request.Content = requestContent;

            HttpResponseMessage response = await client.SendAsync(request);

            await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                JObject jSonObject = JObject.Parse(content);
                int res = JsonConvert.DeserializeObject<int>(((JObject)(jSonObject.GetValue("data"))).GetValue("id").ToString());
                Console.WriteLine("OUIIIIIIIIIIIIIII");
                Console.WriteLine(res);
                return res;
            }
            return -1;
        }
    }
}