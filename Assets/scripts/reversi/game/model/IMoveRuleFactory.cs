using System;
using System.Collections.Generic;

namespace reversi.game
{
	public interface IMoveRuleFactory
	{
		List<IMoveRule> BuildMoveRules();
	}
}