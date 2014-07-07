using System;
using System.Collections.Generic;

namespace reversi.game
{
	public class CaptureMove : IMove
	{
		public IList<GridCellKey> Pieces { get; set; }
		public Faction TakingFaction;

		[Inject]
		public PiecePlacedSignal PiecePlaced { get; set; }

		public void ApplyMove(Grid grid)
		{
			if (null == Pieces || Faction.None == TakingFaction)
			{
				return;
			}

			foreach (var piece in Pieces)
			{
				grid.PlacePiece(piece.row, piece.col, TakingFaction);
				PiecePlaced.Dispatch(new GridCellKey(piece.row, piece.col), TakingFaction);
			}
		}
	}
}