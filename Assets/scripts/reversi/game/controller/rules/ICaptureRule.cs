using System.Collections.Generic;

namespace reversi.game
{
	public interface ICaptureRule
	{
		bool IsMoveValid(Move2 move, Grid grid);
		List<Capture> GetCapturesForMove(Move2 move, Grid grid);
	}
}