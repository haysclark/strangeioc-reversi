using System;
using System.Collections.Generic;
using strange.extensions.injector.api;

namespace reversi.game
{
	public class MoveRuleFactory : IMoveRuleFactory
	{
		private List<IMoveRule> rules = new List<IMoveRule>();

		[Inject]
		public IInjectionBinder InjectionBinder { get; set; }

		[PostConstruct]
		public void PostInject()
		{
			rules.Add(InjectionBinder.GetInstance<HorizontalMoveRule>());
		}

		public List<IMoveRule> BuildMoveRules()
		{
			return rules;
		}
	}
}