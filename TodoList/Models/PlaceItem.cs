using System.Collections.Generic;
using System.Windows.Input;
using Newtonsoft.Json;

namespace TD.Api.Dtos
{
	public class PlaceItem
	{
		[JsonProperty("id")]
		public int Id { get; set; }
		
		[JsonProperty("title")]
		public string Title { get; set; }
		
		[JsonProperty("description")]
		public string Description { get; set; }
		
		[JsonProperty("image_id")]
		public int ImageId { get; set; }
		
		[JsonProperty("latitude")]
		public double Latitude { get; set; }
		
		[JsonProperty("longitude")]
		public double Longitude { get; set; }

		public PlaceItem(string title, string description, int imageId, double latitude, double longitude)
		{
			Title = title;
			Description = description;
			ImageId = imageId;
			Latitude = latitude;
			Longitude = longitude;
		}
	}
}