// ------------------------------------------------------------------------------
//  GeneratedContext
//      This Context is created by the ContextBuilder and is inspired by
//		Robotlegs 2.0's FLUENT contexts
//      
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

public class GeneratedContext : MVCSContext
{
	public Action<ICrossContextCapable> onPreMapBindings { get; set; }
	public Action<ICrossContextCapable> onMapBindings { get; set; }
	public Action<ICrossContextCapable> onLaunch { get; set; }

	public GeneratedContext (MonoBehaviour contextView) : base(contextView )
	{
	}

	public GeneratedContext (MonoBehaviour view, ContextStartupFlags flags) : base(view, flags)
	{
	}

	public override void Launch ()
	{
		base.Launch ();
		if( onLaunch != null )
		{
			onLaunch( this );
		}
	}

	protected override void addCoreComponents ()
	{
		base.addCoreComponents ();
	}

	protected override void mapBindings ()
	{
		//Put here because addCoreComponents() is called during contruction
		if( onPreMapBindings != null )
		{
			onPreMapBindings( this );
		}

		base.mapBindings ();
		if( onMapBindings != null )
		{
			onMapBindings( this );
		}
	}
}
