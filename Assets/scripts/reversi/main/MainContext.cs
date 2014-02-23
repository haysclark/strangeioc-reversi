//Write in all the Cross-Context bindings

using System;
using UnityEngine;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;

namespace reversi.main
{
	public class MainContextStartSignal : Signal{}

	public class MainContext : SignalContext
	{
		public MainContext (MonoBehaviour contextView) : base ( contextView )
		{

		}

		protected override void mapBindings ()
		{
			base.mapBindings ();
			if( isFirstContext() )
			{
				new ConfigureApplicationService().map( this );
			}

			bindStartCommand<MainContextStartSignal, MainStartupCommand>();
		}

		private bool isFirstContext()
		{
			return Context.firstContext == this;
		}
	}
}

