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

		}

		public List<IMoveRule> BuildMoveRules()
		{
			return rules;
		}

		public IMoveRule BuildHorizontalMoveRule()
		{
			CollectionMoveRule rules = new CollectionMoveRule();

			DirectionalMoveRule rule = new DirectionalMoveRule(GameConfig.MinimumPiecesToCapture, new GridCellKey(0, -1));
			InjectionBinder.injector.Inject(rule, false);
			rules.AddRule(rule);

			rule = new DirectionalMoveRule (GameConfig.MinimumPiecesToCapture, new GridCellKey(0, 1));
			InjectionBinder.injector.Inject(rule, false);
			rules.AddRule(rule);

			return rules;
		}

		public IMoveRule BuildVerticalMoveRule()
		{
			CollectionMoveRule rules = new CollectionMoveRule();
			
			DirectionalMoveRule rule = new DirectionalMoveRule(GameConfig.MinimumPiecesToCapture, new GridCellKey(-1, 0));
			InjectionBinder.injector.Inject(rule, false);
			rules.AddRule(rule);
			
			rule = new DirectionalMoveRule (GameConfig.MinimumPiecesToCapture, new GridCellKey(1, 0));
			InjectionBinder.injector.Inject(rule, false);
			rules.AddRule(rule);
			
			return rules;
		}

		public IMoveRule BuildDiagonalMoveRule()
		{
			CollectionMoveRule rules = new CollectionMoveRule();
			
			DirectionalMoveRule rule = new DirectionalMoveRule(GameConfig.MinimumPiecesToCapture, new GridCellKey(-1, -1));
			InjectionBinder.injector.Inject(rule, false);
			rules.AddRule(rule);
			
			rule = new DirectionalMoveRule (GameConfig.MinimumPiecesToCapture, new GridCellKey(1, 1));
			InjectionBinder.injector.Inject(rule, false);
			rules.AddRule(rule);

			rule = new DirectionalMoveRule(GameConfig.MinimumPiecesToCapture, new GridCellKey(-1, 1));
			InjectionBinder.injector.Inject(rule, false);
			rules.AddRule(rule);
			
			rule = new DirectionalMoveRule (GameConfig.MinimumPiecesToCapture, new GridCellKey(1, -1));
			InjectionBinder.injector.Inject(rule, false);
			rules.AddRule(rule);
			
			return rules;
		}
	}
}