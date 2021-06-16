using Sandbox;
using Sandbox.UI;

namespace QuizNight.UI
{
	[Library]
	public class HUD : HudEntity<RootPanel>
	{
		public HUD()
		{
			if ( !IsClient )
				return;
			
			RootPanel.AddChild<ChatBox>();
			RootPanel.AddChild<VoiceList>();

			RootPanel.AddChild<LobbyScreen>();
			RootPanel.AddChild<IntroScreen>();
		}
	}
}
