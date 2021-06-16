using System.Collections.Generic;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace QuizNight.UI.Lobby
{
	public class SettingsContainer : Panel
	{
		public Panel DifficultyContainer { get; set; }
		public Panel CategoryContainer { get; set; }
		
		public Panel CategoriesContentPanel { get; set; }
		public List<Panel> CategoriesContentColumns { get; set; } = new();
		
		public Dictionary<Difficulty, Button> DifficultyButtons { get; set; } = new();
		public Dictionary<Category, Button> CategoryButtons { get; set; } = new();
		
		public Difficulty SelectedDifficulty { get; set; }
		public Category SelectedCategory { get; set; }

		public const int Columns = 3;
		private int currentColumn = 0;
		
		public SettingsContainer()
		{
			Add.Label( "Game Settings", "title" );

			DifficultyContainer = Add.Panel( "container difficulty" );
			DifficultyContainer.Add.Label( "Difficulty selection", "label" );
			var difficultyPanel = DifficultyContainer.Add.Panel( "contents" );

			foreach ( var difficulty in Game.Difficulties )
			{
				var button = difficultyPanel.Add.Button( difficulty.ToString(), "btn", () =>
				{
					SelectDifficulty( difficulty );
				} );

				DifficultyButtons[difficulty] = button;
			}

			SelectDifficulty( Difficulty.Easy );
			
			CategoryContainer = Add.Panel( "container category" );
			CategoryContainer.Add.Label( "Category selection", "label" );
			CategoryContainer.Add.Label( "If no category is selected, all will be used. You can unselect a category by clicking on it again.", "desc" );
			CategoriesContentPanel = CategoryContainer.Add.Panel( "contents categoryContents" );

			for ( int i = 0; i < Columns; i++ )
			{
				CategoriesContentColumns.Add( CategoriesContentPanel.Add.Panel( "column" ) );
			}

			FetchCategories();

			Add.Button( "Start", "startBtn" );
		}

		async void FetchCategories()
		{
			var categories = await CategoryFetcher.FetchCategories();
			foreach ( var category in categories.Categories )
			{
				currentColumn++;
				if ( currentColumn + 1 > Columns )
				{
					currentColumn = 0;
				}

				var panel = CategoriesContentColumns[currentColumn];
				var row = panel.Add.Panel( "row" );
				var button = row.Add.Button( category.Name, "rowBtn", () =>
				{
					SelectCategory( category );
				} );

				CategoryButtons[category] = button;
			}
		}

		void SelectCategory( Category category )
		{
			foreach ( var button in CategoryButtons )
			{
				button.Value.RemoveClass("selected");
			}

			if ( SelectedCategory == category )
			{
				SelectedCategory = null;
				return;
			}

			SelectedCategory = category;
			CategoryButtons[category].AddClass( "selected" );
		}

		void SelectDifficulty(Difficulty difficulty)
		{
			foreach ( var button in DifficultyButtons )
			{
				button.Value.RemoveClass("selected");
			}
			
			SelectedDifficulty = difficulty;
			DifficultyButtons[difficulty].AddClass( "selected" );
		}
	}
}
