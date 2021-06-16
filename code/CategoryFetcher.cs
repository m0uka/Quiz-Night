using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Sandbox;

namespace QuizNight
{
	public static class CategoryFetcher
	{
		public static async Task<CategoriesRequestData> FetchCategories()
		{
			var http = new Sandbox.Internal.Http(new Uri("https://opentdb.com/api_category.php"));
			var stream = await http.GetStreamAsync();

			return await JsonSerializer.DeserializeAsync<CategoriesRequestData>(stream);
		}
	}
}
