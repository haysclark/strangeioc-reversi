using System;
using System.Collections.Generic;
using strange.extensions.command.impl;

namespace reversi.game
{
	public class PlacePieceCommand : Command
	{
		[Inject]
		public GridCellKey Position { get; set; }
		[Inject]
		public Faction Faction { get; set; }

		[Inject]
		public IMoveRuleFactory MoveRuleFactory { get; set; }

		[Inject]
		public Grid Grid { get; set; }

		override public void Execute()
		{
			Faction prevFaction = Grid.GetPiece(Position.row, Position.col);
			Grid.PlacePiece(Position.row, Position.col, Faction);
			List<IMove> moves = FindMoves();
			ResolveMoves(moves, prevFaction);
		}

		private List<IMove> FindMoves()
		{
			List<IMove> moves = new List<IMove>();
			List<IMoveRule> rules = MoveRuleFactory.BuildMoveRules();

			foreach (var rule in rules) {
				moves.AddRange(rule.FindMoves(Position, Faction, Grid));
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

		private void UndoPlacement(Faction prevFaction)
		{
			Grid.PlacePiece(Position.row, Position.col, prevFaction);
		}

		private void ApplyMoves(List<IMove> moves)
		{
			foreach (var move in moves) {
				move.ApplyMove(Grid);
			}
		}
	}
}