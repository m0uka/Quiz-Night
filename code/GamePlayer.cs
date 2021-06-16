using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Sandbox;

namespace QuizNight
{
	public partial class GamePlayer : Player
	{
		[Net]
		public Color PlayerColor { get; set; }

		public static List<Color> PossibleColors = new()
		{
			Color.Parse("#3B82F6").Value,
			Color.Parse("#10B981").Value,
			Color.Parse("#F59E0B").Value,
			Color.Parse("#EF4444").Value,
			Color.Parse("#8B5CF6").Value,
			Color.Parse("#EC4899").Value,
			Color.Parse("#EC4899").Value,
			Color.Parse("#78350F").Value,
			Color.Parse("#FFFFFF").Value,
		};
		
		/// <summary>
		/// Called when the player spawns.
		/// </summary>
		public override void Respawn()
		{
			// SetModel( "models/citizen/citizen.vmdl" );

			// TODO: Probably use custom controllers later
			Controller = new WalkController();
			Animator = new StandardPlayerAnimator();
			Camera = new FirstPersonCamera();
			
			// Assign a random color!
			if ( IsServer )
			{
				PlayerColor = GetUniqueColor();
			}
		}

		/// <summary>
		/// Gets an unique color that no-one else has.
		/// </summary>
		public Color GetUniqueColor()
		{
			foreach ( var color in PossibleColors )
			{
				if ( All.OfType<GamePlayer>().All( x => x.PlayerColor != color ) )
				{
					return color;
				}
			}

			Log.Warning("No unique color found, returning first!");
			return PossibleColors[0];
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );
		}
	}
}
