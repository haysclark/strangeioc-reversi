using UnityEngine;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;
using strange.extensions.injector.api;

namespace reversi.game
{
	public class GameBootstrap : ContextView
	{
		public class GameContextStartSignal : Signal {}

		void Start ()
		{
			context = new ContextBuilder()
				.ForContextView(this)
				.SetStartSignalAndCommand<GameContextStartSignal, GameStartupCommand>()
				.MapBinder().Add( new reversi.main.ConfigureApplicationService().Setup )
				.MapBinder().Add(mapBinders)
				.Build();
		}

		private void mapBinders (ICrossContextCapable context)
		{
			context.injectionBinder.Bind<IInjectionBinder>().ToValue(context.injectionBinder);
			context.injectionBinder.Bind<IMoveRuleFactory>().To<MoveRuleFactory>().ToSingleton();
			context.injectionBinder.Bind<HorizontalMoveRule>().ToValue(new HorizontalMoveRule(GameConfig.MinimumPiecesToCapture));
			context.injectionBinder.Bind<CaptureMove>().To<CaptureMove>();
			context.injectionBinder.Bind<Grid>().ToValue(new Grid(GameConfig.NumGridRows, GameConfig.NumGridCols));
		}
	}
}