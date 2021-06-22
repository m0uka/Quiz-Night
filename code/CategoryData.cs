using System.Collections.Generic;
using System.Text.Json.Serialization;
using Sandbox;

namespace QuizNight
{
	public partial class Category
	{
		public static Category All { get; set; } = new() {Id = 9999, Name = "all"};
		
		[JsonPropertyName( "id" )]
		public int Id { get; set; }
		
		[JsonPropertyName( "name" )]
		public string Name { get; set; }
	}

	public partial class CategoriesRequestData
	{
		[JsonPropertyName( "trivia_categories" )]
		public List<Category> Categories { get; set; } = new();
	}
}
