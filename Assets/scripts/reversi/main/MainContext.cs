//Write in all the Cross-Context bindings

using System;
using UnityEngine;
using strange.extensions.context.impl;

namespace reversi.main
{
	public class MainContext : SignalContext
	{
		public MainContext (MonoBehaviour contextView) : base (contextView)
		{
		}

		protected override void mapBindings ()
		{
			base.mapBindings ();
			if( isFirstContext() )
			{
				mapFirstBindings();
			}

			commandBinder.Bind<StartSignal> ()
				.To<MainStartupCommand> ();
		}

		protected void mapFirstBindings ()
		{
			new ConfigureApplicationService().map( this );
		}

		private bool isFirstContext()
		{
			return Context.firstContext == this;
		}
	}
}

