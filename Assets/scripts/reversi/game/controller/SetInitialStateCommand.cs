using System;
using System.Collections.Generic;
using strange.extensions.command.impl;

namespace reversi.game
{
	public class SetInitialStateCommand : Command
	{
		[Inject]
		public Grid Grid { get; set; }

		override public void Execute()
		{
			int numCols = Grid.NumCols;
			int numRows = Grid.NumRows;
			Grid.PlacePiece((numRows / 2) - 1, (numCols / 2) - 1, Faction.White);
			Grid.PlacePiece(numRows / 2, numCols / 2, Faction.White);
			Grid.PlacePiece(numRows / 2, (numCols / 2) - 1, Faction.Black);
			Grid.PlacePiece((numRows / 2) - 1, numCols / 2, Faction.Black);
		}
	}
}