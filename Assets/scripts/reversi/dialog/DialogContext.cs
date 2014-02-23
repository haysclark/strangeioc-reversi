using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;

namespace reversi.dialog
{
	public class DialogContext : SignalContext
	{
		public DialogContext (MonoBehaviour contextView) : base ( contextView )
		{
		}
		
		protected override void mapBindings ()
		{
			base.mapBindings ();
			if( isFirstContext() )
			{
				mapFirstBindings();
			}
		}
		
		private void mapFirstBindings ()
		{

		}
		
		private bool isFirstContext()
		{
			return Context.firstContext == this;
		}
	}
}