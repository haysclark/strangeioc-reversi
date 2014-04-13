using System;
using System.Collections.Generic;
using strange.extensions.injector.api;

namespace reversi.game
{
	public class DirectionalMoveRule : IMoveRule
	{
		private int minimumPiecesToCapture;
		private GridCellKey direction;
		
		[Inject]
		public IInjectionBinder InjectionBinder { get; set; }
		
		public DirectionalMoveRule(int minimumPiecesToCapture, GridCellKey direction)
		{
			this.minimumPiecesToCapture = minimumPiecesToCapture;
			this.direction = direction;
		}
		
		public List<IMove> FindMoves(GridCellKey position, Faction faction, Grid grid)
		{
			List<IMove> moves = new List<IMove>();
			
			List<GridCellKey> pieces = FindPiecesInMove(position, faction, grid);
			if (HasEnoughPiecesForValidMove(pieces))
			{
				AddMove(pieces, faction, moves);
			}
			
			return moves;
		}

		private List<GridCellKey> FindPiecesInMove(GridCellKey position, Faction faction, Grid grid)
		{
			List<GridCellKey> pieces = new List<GridCellKey>();
			pieces.Add(position);
			GridCellKey currentPos = new GridCellKey(position.row + direction.row, position.col + direction.col);
			while (currentPos.row >= 0 && currentPos.row < grid.NumRows && currentPos.col >= 0 && currentPos.col < grid.NumCols)
			{
				Faction curFaction = grid.GetPiece(currentPos.row, currentPos.col);
				if (Faction.None == curFaction)
				{
					break;
				}
				pieces.Add(new GridCellKey(currentPos.row, currentPos.col));
				if (faction == curFaction)
				{
					break;
				}
				currentPos.row += direction.row;
				currentPos.col += direction.col;
			}
			return pieces;
		}

		private bool HasEnoughPiecesForValidMove(List<GridCellKey> pieces)
		{
			return 2 + minimumPiecesToCapture <= pieces.Count;
		}
		
		private void AddMove(List<GridCellKey> pieces, Faction faction, List<IMove> moves)
		{
			CaptureMove move = InjectionBinder.GetInstance<CaptureMove>();
			move.Pieces = pieces;
			move.TakingFaction = faction;
			moves.Add(move);
		}
	}
}