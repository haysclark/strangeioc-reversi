using System;
using System.Collections.Generic;

namespace reversi.game
{
	public class HorizontalCaptureRule : ICaptureRule
	{
		public bool IsMoveValid(Move2 move, Grid grid)
		{
			throw new NotImplementedException ();
		}

		public List<Capture> GetCapturesForMove(Move2 move, Grid grid)
		{
			throw new NotImplementedException ();
		}
	}
}