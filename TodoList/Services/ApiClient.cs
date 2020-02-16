using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
		public const string GET_PLACE = "places/{placeId}"; // GET place detail

		public const string CREATE_PLACE = "places"; //POST Create a place
		public const string CREATE_COMMENT = "places/{placeId}/comments"; //POST Add a comment

		public const string CREATE_IMAGE = "images"; //POST upload an image
		public const string GET_IMAGE = "images/{imageId}"; //GET retrieve image data
		
		public const string IMAGE_NOT_FOUND = nameof(IMAGE_NOT_FOUND);

		public const string EMAIL_ALREADY_EXISTS = nameof(EMAIL_ALREADY_EXISTS);

	    HttpClient httpClient;

        public ApiClient()
        {
            httpClient = new HttpClient();

        }

        public async void postLogin()
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

        public async Task<List<PlaceItem>> getPlaces()
        {
            var uri = new Uri(URL + LIST_PLACES);

            Debug.WriteLine("DUH : "+URL + LIST_PLACES);
            
            var response = await httpClient.GetAsync(uri);
            

            if (response.IsSuccessStatusCode)
            {
                String content = await response.Content.ReadAsStringAsync();
                JObject jSonObject = JObject.Parse(content);
                List<PlaceItem> res = JsonConvert.DeserializeObject<List<PlaceItem>>(jSonObject.GetValue("data").ToString());
                return res;
            }
            return null;
        }
    }
}