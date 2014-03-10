// ------------------------------------------------------------------------------
//  GeneratedContext
//      This Context is created by the ContextBuilder and was inspired by
//		Robotlegs 2.0's FLUENT contexts
//      
// ------------------------------------------------------------------------------
using System;
using UnityEngine;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.context.api;
using strange.extensions.context.impl;

public class GeneratedContext : MVCSContext
{
	public Action<MVCSContext> onMapBindings { get; set; }
	public Action<MVCSContext> onLaunch { get; set; }

	public GeneratedContext (MonoBehaviour contextView) : base(contextView )
	{
	}

	public GeneratedContext (MonoBehaviour view, ContextStartupFlags flags ) : base( view, flags )
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

	protected override void mapBindings ()
	{
		base.mapBindings ();
		if( onMapBindings != null )
		{
			onMapBindings( this );
		}
	}
}

// ------------------------------------------------------------------------------
//  GeneratedSignalsContext
//      This Context is created by the ContextBuilder and was inspired by
//		Robotlegs 2.0's FLUENT contexts.  Having to use Strategy Pattern
//		because addCoreComponents() needs to be modified to support signals
//		and it is called by the base class constructor which makes it hard
//		to compose.
//      
// ------------------------------------------------------------------------------
public class GeneratedSignalsContext : GeneratedContext
{
	public GeneratedSignalsContext (MonoBehaviour view, ContextStartupFlags flags ) : base( view, flags )
	{
	}

	protected override void addCoreComponents ()
	{
		base.addCoreComponents ();
		injectionBinder.Unbind<ICommandBinder>();
		injectionBinder.Bind<ICommandBinder>().To<SignalCommandBinder>().ToSingleton();
	}
}
