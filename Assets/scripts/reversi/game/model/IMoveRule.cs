using System;
using System.Collections.Generic;

namespace reversi.game
{
	public interface IMoveRule
	{
		List<IMove> FindMoves(GridCellKey position, Faction faction, Grid grid);
	}
}