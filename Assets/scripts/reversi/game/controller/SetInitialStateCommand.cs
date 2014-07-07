using System;
using System.Collections.Generic;
using strange.extensions.command.impl;

namespace reversi.game
{
	public class SetInitialStateCommand : Command
	{
		[Inject]
		public Grid Grid { get; set; }

		[Inject]
		public PiecePlacedSignal PiecePlacedSignal { get; set; }

		override public void Execute()
		{
			int numCols = Grid.NumCols;
			int numRows = Grid.NumRows;
			PlacePiece((numRows / 2) - 1, (numCols / 2) - 1, Faction.White);
			PlacePiece(numRows / 2, numCols / 2, Faction.White);
			PlacePiece(numRows / 2, (numCols / 2) - 1, Faction.Black);
			PlacePiece((numRows / 2) - 1, numCols / 2, Faction.Black);
		}

		private void PlacePiece(int row, int col, Faction faction)
		{
			Grid.PlacePiece(row, col, faction);
			PiecePlacedSignal.Dispatch(new GridCellKey(row, col), faction);
		}
	}
}