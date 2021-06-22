using System.Linq;
using Sandbox;

namespace QuizNight
{
	public partial class GameSettings : NetworkComponent
	{
		[Net]
		public Difficulty Difficulty { get; set; }
		
		
		// This sucks, but networkable classes are broken right now
		[Net] public string SelectedCategory { get; set; }
		public Category Category
		{
			get
			{
				return Game.Instance.Categories.FirstOrDefault( x => x.Name == SelectedCategory );
			}
			set
			{
				SelectedCategory = value.Name;
			}
		}

		[ServerCmd("select_difficulty")]
		public static void SelectDifficulty(string difficultyName)
		{
			if ( ConsoleSystem.Caller == null ) return;
			if ( !((GamePlayer)ConsoleSystem.Caller.Pawn).IsHost ) return;
			if ( !Game.Instance.Difficulties.Exists( x => x.ToString() == difficultyName ) ) return;

			var difficulty = Game.Instance.Difficulties.FirstOrDefault( x => x.ToString() == difficultyName );
			Game.Instance.Settings.Difficulty = difficulty;
		}
		
		[ServerCmd("select_category")]
		public static void SelectCategory(string categoryName)
		{
			if ( ConsoleSystem.Caller == null ) return;
			if ( !((GamePlayer)ConsoleSystem.Caller.Pawn).IsHost ) return;
			if ( !Game.Instance.Categories.Exists( x => x.Name == categoryName ) ) return;

			Game.Instance.Settings.SelectedCategory = categoryName;
		}
	}
}
