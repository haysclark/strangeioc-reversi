﻿using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;

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
				.MapBinder().Add(mapBinders)
				.Build();
		}

		private void mapBinders (strange.extensions.context.api.ICrossContextCapable obj)
		{

		}
	}
}