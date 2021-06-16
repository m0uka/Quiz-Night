using System;
using System.Collections.Generic;
using System.Linq;
using QuizNight.UI;
using Sandbox;

namespace QuizNight
{
	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// 
	/// Your game needs to be registered (using [Library] here) with the same name 
	/// as your game addon. If it isn't then we won't be able to find it.
	/// </summary>
	[Library( "quiznight" )]
	public partial class Game : Sandbox.Game
	{
		public const int MaxPlayers = 8;
		public static List<Difficulty> Difficulties = Enum.GetValues( typeof(Difficulty) ).Cast<Difficulty>().ToList();

		public Game()
		{
			if ( IsServer )
			{
				_ = new HUD();
			}

			if ( IsClient )
			{

			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override async void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new GamePlayer();
			client.Pawn = player;
			player.Respawn();

		}
		
	}
}
