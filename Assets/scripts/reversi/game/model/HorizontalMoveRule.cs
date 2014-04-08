using System;
using System.Collections.Generic;
using strange.extensions.injector.api;

namespace reversi.game
{
	public class HorizontalMoveRule : IMoveRule
	{
		private int minimumPiecesToCapture;

		[Inject]
		public IInjectionBinder InjectionBinder { get; set; }

		public HorizontalMoveRule(int minimumPiecesToCapture)
		{
			this.minimumPiecesToCapture = minimumPiecesToCapture;
		}

		public List<IMove> FindMoves(GridCellKey position, Faction faction, Grid grid)
		{
			List<IMove> moves = new List<IMove>();

			FindMoves(position, faction, grid, moves);

			return moves;
		}

		private void FindMoves(GridCellKey position, Faction faction, Grid grid, List<IMove> moves)
		{
			FindForwardMove(position, faction, grid, moves);
			FindBackwardMove(position, faction, grid, moves);
		}

		private void FindForwardMove(GridCellKey position, Faction faction, Grid grid, List<IMove> moves)
		{
			if (position.col <= grid.NumCols - 2 - minimumPiecesToCapture)
			{
				List<GridCellKey> pieces = new List<GridCellKey>();
				pieces.Add(position);
				for (int col = position.col + 1; col < grid.NumCols; col++)
				{
					Faction curFaction = grid.GetPiece(position.row, col);
					if (Faction.None == curFaction)
					{
						break;
					}
					pieces.Add(new GridCellKey(position.row, col));
					if (faction == curFaction)
					{
						break;
					}
				}
				if (2 + minimumPiecesToCapture <= pieces.Count)
				{
					CaptureMove move = InjectionBinder.GetInstance<CaptureMove>();
					move.Pieces = pieces;
					move.TakingFaction = faction;
					moves.Add(move);
				}
			}
		}

		private void FindBackwardMove(GridCellKey position, Faction faction, Grid grid, List<IMove> moves)
		{
			if (position.col >= 1 + minimumPiecesToCapture)
			{
				List<GridCellKey> pieces = new List<GridCellKey>();
				pieces.Add(position);
				for (int col = position.col - 1; col >= 0; col--)
				{
					Faction curFaction = grid.GetPiece(position.row, col);
					if (Faction.None == curFaction)
					{
						break;
					}
					pieces.Add(new GridCellKey(position.row, col));
					if (faction == curFaction)
					{
						break;
					}
				}
				if (2 + minimumPiecesToCapture <= pieces.Count)
				{
					CaptureMove move = InjectionBinder.GetInstance<CaptureMove>();
					move.Pieces = pieces;
					move.TakingFaction = faction;
					moves.Add(move);
				}
			}
		}
	}
}