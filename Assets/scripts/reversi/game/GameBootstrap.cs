using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;
using strange.extensions.injector.api;
using strange.extensions.command.api;

namespace reversi.game
{
	public class GameBootstrap : ContextView
	{
		void Start ()
		{
			context = new ContextBuilder()
				.ForContextView(this)
				.SetStartSignalAndCommand<GameContextStartSignal, GameStartupCommand>()
				.MapBinder().Add(new reversi.main.ConfigureApplicationService().Setup)
				.MapBinder().Add(mapInjections)
				.MapBinder().Add(mapCommands)
				.Build();
		}

		private void mapInjections(ICrossContextCapable context)
		{
			context.injectionBinder.Bind<IInjectionBinder>().ToValue(context.injectionBinder);
			context.injectionBinder.Bind<IMoveRuleFactory>().To<MoveRuleFactory>().ToSingleton();
			context.injectionBinder.Bind<CaptureMove>().To<CaptureMove>();
			context.injectionBinder.Bind<Grid>().ToValue(new Grid(GameConfig.NumGridRows, GameConfig.NumGridCols));
		}

		private void mapCommands(ICrossContextCapable context)
		{
			ICommandBinder commandBinder = context.injectionBinder.GetInstance<ICommandBinder>();
			commandBinder.Bind<SetInitialStateSignal>().To<SetInitialStateCommand>();
		}

	}
}