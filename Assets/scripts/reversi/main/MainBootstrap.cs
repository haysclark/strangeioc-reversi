//Every context starts by attaching a ContextView to a GameObject.
//The main job of this ContextView is to instantiate the Context.
//Remember, if the GameObject is destroyed, the Context and all your
//bindings go with it.

//This ContextView has two jobs:
//1. Provide the Cross-Context dependencies (see MainContext)
//2. Load the other Contexts (see MainStartupCommand)

using UnityEngine;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;

namespace reversi.main
{
	public class MainContextStartSignal : Signal{}

	public class MainBootstrap : ContextView
	{
		// Build a Context
		void Start ()
		{
			context = new ContextBuilder()
				.ForContextView( this )
				.SetStartSignalAndCommand<MainContextStartSignal, MainStartupCommand>()
				.MapBinder().AddFirstRunOnly( new ConfigureApplicationService().Setup )
				.Build();
		}
	}
}
