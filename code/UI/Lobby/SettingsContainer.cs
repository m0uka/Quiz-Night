using System.Collections.Generic;
using System.Linq;
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
		
		private GamePlayer LocalPlayer { get; set; }

		public SettingsContainer()
		{
			Add.Label( "Game Settings", "title" );

			DifficultyContainer = Add.Panel( "container difficulty" );
			DifficultyContainer.Add.Label( "Difficulty selection", "label" );
			var difficultyPanel = DifficultyContainer.Add.Panel( "contents" );

			foreach ( var difficulty in Game.Instance.Difficulties )
			{
				var button = difficultyPanel.Add.Button( difficulty.ToString(), $"btn", () =>
				{
					if ( !LocalPlayer.IsHost ) return;
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

		}

		public void PlayerReady( GamePlayer player )
		{
			FetchCategories();
			
			var btn = Add.Button( "Start", "startBtn" );
			
			if ( !player.IsHost )
			{
				btn.Text = "Waiting for host";
				btn.AddClass("disabled");

				foreach ( var buttonPair in DifficultyButtons )
				{
					buttonPair.Value.AddClass("disabled");
				}
				
				foreach ( var buttonPair in CategoryButtons )
				{
					buttonPair.Value.AddClass("disabled");
				}
			}

			LocalPlayer = player;
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
					if ( !LocalPlayer.IsHost ) return;
					SelectCategory( category );
				} );

				CategoryButtons[category] = button;
			}

			if ( LocalPlayer != null && !LocalPlayer.IsHost )
			{
				foreach ( var buttonPair in CategoryButtons )
				{
					buttonPair.Value.AddClass("disabled");
				}
			}
		}

		public override void Tick()
		{
			base.Tick();

			if ( !((GamePlayer)Local.Pawn).IsHost )
			{
				if (Game.Instance.Settings.Category == null) return;
				
				// Category
				foreach ( var button in CategoryButtons )
				{
					button.Value.RemoveClass("selected");
				}

				if ( Game.Instance.Settings.Category != Category.All )
				{
					SelectedCategory = Game.Instance.Settings.Category;
					CategoryButtons[Game.Instance.Settings.Category].AddClass( "selected" );
				}
				else
				{
					SelectedCategory = null;
				}
				
				// Difficulty
				foreach ( var button in DifficultyButtons )
				{
					button.Value.RemoveClass("selected");
				}
				
				SelectedDifficulty = Game.Instance.Settings.Difficulty;
				DifficultyButtons[Game.Instance.Settings.Difficulty].AddClass( "selected" );
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
				ConsoleSystem.Run( "select_category", "all" );
				
				return;
			}

			SelectedCategory = category;
			CategoryButtons[category].AddClass( "selected" );

			ConsoleSystem.Run( "select_category", category.Name );
		}

		void SelectDifficulty(Difficulty difficulty)
		{
			foreach ( var button in DifficultyButtons )
			{
				button.Value.RemoveClass("selected");
			}
			
			SelectedDifficulty = difficulty;
			DifficultyButtons[difficulty].AddClass( "selected" );
			
			ConsoleSystem.Run( "select_difficulty", difficulty.ToString() );
		}
	}
}
