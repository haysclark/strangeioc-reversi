using System;
using System.Collections.Generic;
using strange.extensions.command.impl;

namespace reversi.game
{
	public class PlacePieceCommand : Command
	{
		[Inject]
		public GridCellKey position { get; set; }
		[Inject]
		public Faction faction { get; set; }

		[Inject]
		public IMoveRuleFactory MoveRuleFactory { get; set; }

		[Inject]
		public Grid Grid { get; set; }

		override public void Execute()
		{
			Faction prevFaction = Grid.GetPiece(position.row, position.col);
			Grid.PlacePiece(position.row, position.col, faction);
			List<IMove> moves = FindMoves();
			ResolveMoves(moves, prevFaction);
		}

		private List<IMove> FindMoves()
		{
			List<IMove> moves = new List<IMove>();
			List<IMoveRule> rules = MoveRuleFactory.BuildMoveRules();

			foreach (var rule in rules) {
				moves.AddRange(rule.FindMoves(position, faction, Grid));
			}
			return moves;
		}

		private void ResolveMoves(List<IMove> moves, Faction prevFaction)
		{
			if (0 >= moves.Count) {
				UndoPlacement(prevFaction);
			} else {
				ApplyMoves(moves);
			}
		}

		void UndoPlacement(Faction prevFaction)
		{
			Grid.PlacePiece(position.row, position.col, prevFaction);
		}

		void ApplyMoves(List<IMove> moves)
		{
			foreach (var move in moves) {
				move.ApplyMove(Grid);
			}
		}
	}
}