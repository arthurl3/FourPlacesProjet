using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TD.Api.Dtos;
using Xamarin.Forms;

namespace TodoList.Services
{
    public class ApiClient
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
		public const string CREATE_COMMENT = "places/{placeId}/comments"; //POST Add a comment

		public const string CREATE_IMAGE = "images"; //POST upload an image
		public const string GET_IMAGE = "images/"; //GET retrieve image data
		
		public const string IMAGE_NOT_FOUND = nameof(IMAGE_NOT_FOUND);

		public const string EMAIL_ALREADY_EXISTS = nameof(EMAIL_ALREADY_EXISTS);

	    HttpClient httpClient;

        public ApiClient()
        {
            httpClient = new HttpClient();

        }

        public async void PostLogin()
        {
            //var uri = new Uri(URL + LOGIN);

            //LoginRequest loginRequest = new LoginRequest();

            //var response = await httpClient.PostAsync(uri, );
            //if (response.IsSuccessStatusCode)
            //{
            //    var content = await response.Content.ReadAsStringAsync();
            //    LoginResult loginResult = JsonConvert.DeserializeObject<LoginResult>(content);
            //    //return loginResult.TokenType;
            //}
        }

        public async Task<List<PlaceItem>> GetPlaces()
        {
            Uri uri = new Uri(URL + LIST_PLACES);            
            HttpResponseMessage response = await httpClient.GetAsync(uri);
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
            HttpResponseMessage response = await httpClient.GetAsync(uri);
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
            //TODO add OAuth 2.0 Token 

            Uri uri = new Uri(URL+ CREATE_PLACE);
            string json = JsonConvert.SerializeObject(place);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            response = await httpClient.PostAsync(uri, content);

            Console.WriteLine(response);

            return (response.IsSuccessStatusCode);
        }
    }
}