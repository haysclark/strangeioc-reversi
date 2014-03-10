using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;
using strange.extensions.context.api;
using reversi.main;

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
	}
}