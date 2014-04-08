using System;
using System.Collections.Generic;

namespace reversi.game
{
	public interface IMove
	{
		void ApplyMove(Grid grid);
	}
}