using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace QuizNight
{
	public class Category
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }
		
		[JsonPropertyName("name")]
		public string Name { get; set; }
	}

	public class CategoriesRequestData
	{
		[JsonPropertyName("trivia_categories")]
		public List<Category> Categories { get; set; }
	}
}
