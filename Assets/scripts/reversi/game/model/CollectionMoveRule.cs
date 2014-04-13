using System;
using System.Collections.Generic;
using strange.extensions.injector.api;

namespace reversi.game
{
	public class CollectionMoveRule : IMoveRule
	{
		List<IMoveRule> rules = new List<IMoveRule>();

		public List<IMove> FindMoves(GridCellKey position, Faction faction, Grid grid)
		{
			List<IMove> moves = new List<IMove>();

			foreach (var rule in rules)
			{
				moves.AddRange(rule.FindMoves(position, faction, grid));
			}

			return moves;
		}

		public void AddRule(IMoveRule rule)
		{
			rules.Add(rule);
		}
	}
}