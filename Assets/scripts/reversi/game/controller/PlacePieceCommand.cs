using System;
using System.Collections.Generic;
using strange.extensions.command.impl;

namespace reversi.game
{
	public class PlacePieceCommand : Command
	{
		public GridCellKey position;
		public Faction faction;

		[Inject]
		public IMoveRuleFactory MoveRuleFactory { get; set; }
		[Inject]
		public Grid Grid { get; set; }

		override public void Execute()
		{
			Faction prevFaction = Grid.GetPiece(position.row, position.col);
			Grid.PlacePiece(position.row, position.col, faction);
			List<IMove> moves = FindMoves();
			if (0 >= moves.Count)
			{
				Grid.PlacePiece(position.row, position.col, prevFaction);
			}
			else
			{
				foreach (var move in moves)
				{
					move.ApplyMove(Grid);
				}
			}
		}

		private List<IMove> FindMoves()
		{
			List<IMove> moves = new List<IMove>();
			List<IMoveRule> rules = MoveRuleFactory.BuildMoveRules();

			foreach (var rule in rules)
			{
				moves.AddRange(rule.FindMoves(position, faction, Grid));
			}
			return moves;
		}
	}
}