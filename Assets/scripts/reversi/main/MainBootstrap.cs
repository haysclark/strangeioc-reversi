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
	public class MainContextStartSignal : TestableSignal{}

	public class MainBootstrap : ContextView
	{
		// Build a Context
		void Start ()
		{
			context = new ContextBuilder()
				.ForContextView( this )
				.SetStartSignalAndCommand<MainContextStartSignal, MainStartupCommand>()
				// Original version...
				//.AddMapBinder( FirstRunOnly.Do( new ConfigureApplicationService().Setup ) )
				//.AddMapBinder( localMapBinder )
				//
				// I think I like this better...
				.MapBinder().Add( localMapBinder )
				.MapBinder().AddFirstRunOnly( new ConfigureApplicationService().Setup )
				.Build();
		}

		private void localMapBinder( ICrossContextCapable context )
		{

		}
	}
}
