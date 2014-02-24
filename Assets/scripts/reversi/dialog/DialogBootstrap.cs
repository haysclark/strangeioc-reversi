using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace reversi.dialog
{
	public class DialogBootstrap : ContextView
	{
		void Start ()
		{
			context = new ContextBuilder()
				.forContextView( this )
				.useSignals()
				.build();
		}
	}
}