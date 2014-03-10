using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;
using strange.extensions.context.api;
using reversi.main;
using reversi.game.space;

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
				.MapBinder().Add( mapAllWithImplicitBinder )
				.MapBinder().Add( mapCommands )
				.Build();
		}

		private void mapAllWithImplicitBinder ( MVCSContext context )
		{
			string[] namespacesToBind = new string[]
			{
				"reversi.game"
			};
			context.implicitBinder.ScanForAnnotatedClasses(namespacesToBind);
		}

		private void mapCommands ( MVCSContext context )
		{
			context.commandBinder.Bind<CreateGridSpacesSignal> ().To<CreateGridSpacesCommand> ();
		}

	}
}