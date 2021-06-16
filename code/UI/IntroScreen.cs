using Sandbox.UI;
using Sandbox.UI.Construct;

namespace QuizNight.UI
{
	public class IntroScreen : Panel
	{
		public static IntroScreen Instance { get; set; }
		
		public Label Title { get; set; }
		public Label Subtitle { get; set; }
		
		public Button Continue { get; set; }

		public IntroScreen()
		{
			var content = Add.Panel( "content" );

			var container = content.Add.Panel( "container" );

			var mainTitle = container.Add.Panel( "mainTitle" );
			Title = mainTitle.Add.Label("Welcome to Quiz Night!", "title");
			Subtitle = mainTitle.Add.Label( "Play the classic party quiz game with your friends or solo.", "subtitle" );

			var buttonContainer = container.Add.Panel( "buttonContainer" );
			Continue = buttonContainer.Add.Button( "Start playing", "button solo", async () =>
			{
				Delete(  );
				LobbyScreen.Instance.SetVisible( true );
			} );
			
			StyleSheet.Load( "/UI/IntroScreen.scss" );

			Instance = this;
		}

		public void SetVisible(bool visible)
		{
			Style.Display = visible ? DisplayMode.Flex : DisplayMode.None;
		}
	}
}
