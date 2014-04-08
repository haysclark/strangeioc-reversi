using System;
using System.Collections.Generic;

namespace reversi.game
{
	public class CaptureMove : IMove
	{
		public IList<GridCellKey> Pieces { get; set; }
		public Faction TakingFaction;

		public void ApplyMove(Grid grid)
		{
			if (null == Pieces || Faction.None == TakingFaction)
			{
				return;
			}

			foreach (var piece in Pieces)
			{
				grid.PlacePiece(piece.row, piece.col, TakingFaction);
			}
		}
	}
}