using System.Collections.Generic;
using System.Linq;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace QuizNight.UI.Lobby
{
	public class PlayerList : Panel
	{
		public List<Panel> Entries { get; set; }
		
		public Label Title { get; set; }
		public Label MaxPlayers { get; set; }
		public Panel PlayerContainer { get; set; }
		public Panel PlayerEntryContainer { get; set; }
		
		public PlayerList()
		{
			Title = Add.Label( "Players in party", "title" );
			PlayerContainer = Add.Panel( "playerContainer" );
			PlayerEntryContainer = PlayerContainer.Add.Panel( "entries" );

			var bottomContainer = PlayerContainer.Add.Panel( "bottom" );
			MaxPlayers = bottomContainer.Add.Label( $"1/{Game.MaxPlayers} players");
		}

		private void AddPlayer(GamePlayer player)
		{
			var client = player.GetClientOwner();
			
			var playerEntry = PlayerEntryContainer.Add.Panel( "entry" );
			playerEntry.Add.Image($"avatar:{client.SteamId}", "avatar");
			playerEntry.Add.Label( client.Name, "name" );

			playerEntry.Style.BackgroundColor = player.PlayerColor;
		}

		public async void UpdatePlayers()
		{
			PlayerEntryContainer.DeleteChildren();

			// Wait for the client to become available
			await Task.Delay( 50 );

			MaxPlayers.Text = $"{Client.All.Count}/{Game.MaxPlayers} players";
			foreach ( var player in Entity.All.OfType<GamePlayer>() )
			{
				AddPlayer( player );
			}
		}
	}
}
