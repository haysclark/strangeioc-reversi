using System;
using System.Collections.Generic;
using strange.extensions.injector.api;

namespace reversi.game
{
	public class VerticalMoveRule : IMoveRule
	{
		private int minimumPiecesToCapture;
		
		[Inject]
		public IInjectionBinder InjectionBinder { get; set; }
		
		public VerticalMoveRule(int minimumPiecesToCapture)
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
			FindDownMove(position, faction, grid, moves);
			FindUpMove(position, faction, grid, moves);
		}
		
		private void FindDownMove(GridCellKey position, Faction faction, Grid grid, List<IMove> moves)
		{
			if (position.row <= grid.NumRows - 2 - minimumPiecesToCapture)
			{
				List<GridCellKey> pieces = new List<GridCellKey>();
				pieces.Add(position);
				for (int row = position.row + 1; row < grid.NumRows; row++)
				{
					Faction curFaction = grid.GetPiece(row, position.col);
					if (Faction.None == curFaction)
					{
						break;
					}
					pieces.Add(new GridCellKey(row, position.col));
					if (faction == curFaction)
					{
						break;
					}
				}
				if (HasEnoughPiecesForValidMove(pieces))
				{
					AddMove (pieces, faction, moves);
				}
			}
		}
		
		private void FindUpMove(GridCellKey position, Faction faction, Grid grid, List<IMove> moves)
		{
			if (position.row >= 1 + minimumPiecesToCapture)
			{
				List<GridCellKey> pieces = new List<GridCellKey>();
				pieces.Add(position);
				for (int row = position.row - 1; row >= 0; row--)
				{
					Faction curFaction = grid.GetPiece(row, position.col);
					if (Faction.None == curFaction)
					{
						break;
					}
					pieces.Add(new GridCellKey(row, position.col));
					if (faction == curFaction)
					{
						break;
					}
				}
				if (HasEnoughPiecesForValidMove(pieces))
				{
					AddMove(pieces, faction, moves);
				}
			}
		}
		
		private bool HasEnoughPiecesForValidMove(List<GridCellKey> pieces)
		{
			return 2 + minimumPiecesToCapture <= pieces.Count;
		}
		
		private void AddMove(List<GridCellKey> pieces, Faction faction, List<IMove> moves)
		{
			CaptureMove move = InjectionBinder.GetInstance<CaptureMove> ();
			move.Pieces = pieces;
			move.TakingFaction = faction;
			moves.Add(move);
		}
	}
}