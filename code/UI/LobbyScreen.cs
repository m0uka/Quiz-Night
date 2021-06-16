using System;
using QuizNight.UI.Lobby;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;

namespace QuizNight.UI
{
	public partial class LobbyScreen : Panel
	{
		public static LobbyScreen Instance { get; set; }
		
		public Panel Container { get; set; }
		public SettingsContainer SettingsContainer { get; set; }
		public PlayerList PlayerList { get; set; }
		

		public LobbyScreen()
		{
			Container = Add.Panel( "container" );
			SettingsContainer = Container.AddChild<SettingsContainer>();
			
			PlayerList = Container.AddChild<PlayerList>();

			PlayerScore.OnPlayerAdded += entry =>
			{
				PlayerList.UpdatePlayers();
			};

			PlayerScore.OnPlayerRemoved += entry =>
			{
				PlayerList.UpdatePlayers();
			};

			PlayerScore.OnPlayerUpdated += entry =>
			{
				PlayerList.UpdatePlayers();
			};

			StyleSheet.Load( "/UI/LobbyScreen.scss" );
			Instance = this;
		}

		public void SetVisible(bool visible)
		{
			Style.Display = visible ? DisplayMode.Flex : DisplayMode.None;
			Style.PointerEvents = visible ? "all" : "none";

			if ( visible )
			{
				PlayerList.UpdatePlayers();
			}
		}
	}
}
