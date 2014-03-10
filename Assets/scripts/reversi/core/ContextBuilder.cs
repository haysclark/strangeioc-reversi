// ------------------------------------------------------------------------------
//  ContextBuilder
//      This FLUENT Builder create a StrangeIOC GeneratedContext and was 
//		inspired by Robotlegs 2.0's FLUENT contexts
//      
// ------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.command.api;
using strange.extensions.command.impl;
using strange.extensions.signal.impl;

public class ContextBuilder
{
	MonoBehaviour _contextView;

	public List<Action<MVCSContext>> preBindings = new List<Action<MVCSContext>>();
	public List<Action<MVCSContext>> mapBindings = new List<Action<MVCSContext>>();
	public List<Action<MVCSContext>> launchBindings = new List<Action<MVCSContext>>();

	public ContextBuilder ForContextView( MonoBehaviour contextView )
	{
		_contextView = contextView;
		return this;
	}

	public MapBinderBuilder MapBinder()
	{
		return new MapBinderBuilder( this, mapBindings );
	}

	public ContextBuilder UseSignals()
	{
		Action<MVCSContext> action = SignalsConfigurator.Setup;
		if(!preBindings.Contains( action ))
		{
			preBindings.Add( action );
		}
		return this;
	}

	public ContextBuilder SetStartSignalAndCommand<T,U>()
		where T : Signal, new()
		where U : ICommand, new()
	{
		UseSignals();
		Action<MVCSContext> action = new StartSignalMapper().SetSignalAndCommand<T,U>();
		launchBindings.Add( action );
		return this;
	}

	public MVCSContext Build()
	{
		GeneratedContext context = new GeneratedContext( _contextView, ContextStartupFlags.MANUAL_MAPPING );
		_contextView = null;

		context.onPreMapBindings = new MapperActionListExecuter(preBindings).Execute;
		context.onMapBindings = new MapperActionListExecuter(mapBindings).Execute;
		context.onLaunch = new MapperActionListExecuter(launchBindings).Execute;

		context.Start();
		return context;
	}
}

public class SignalsConfigurator
{
	public static void Setup( MVCSContext context )
	{
		context.injectionBinder.Unbind<ICommandBinder> ();
		context.injectionBinder.Bind<ICommandBinder> ().To<SignalCommandBinder> ().ToSingleton ();
	}
}

public class MapBinderBuilder
{
	private ContextBuilder _parent;
	private List<Action<MVCSContext>> _preBindings;

	public MapBinderBuilder( ContextBuilder parent, List<Action<MVCSContext>> preBindings )
	{
		_preBindings = preBindings;
		_parent = parent;
	}

	public ContextBuilder Add( Action<MVCSContext> contextAction )
	{
		_preBindings.Add( contextAction );
		return _parent;
	}

	public ContextBuilder AddFirstRunOnly( Action<MVCSContext> contextAction )
	{
		_preBindings.Add(FirstRunOnly.Do( contextAction ));
		return _parent;
	}
}

public class StartSignalMapper
{
	public Action<MVCSContext> SetSignalAndCommand<T,U>()
		where T : Signal, new()
		where U : ICommand, new()
	{
		Action<MVCSContext> response = delegate(MVCSContext context)
		{
			var commandBinder = (SignalCommandBinder)context.injectionBinder.GetInstance<ICommandBinder>();
			commandBinder.Bind<T> ().To<U> ();
			Signal startSignal = (Signal)context.injectionBinder.GetInstance<T>();
			startSignal.Dispatch();
		};
		return response;
	}
}

public class MapperActionListExecuter
{
	private List<Action<MVCSContext>> _mapContextList;
	public MapperActionListExecuter( List<Action<MVCSContext>> mapContextList )
	{
		_mapContextList = mapContextList;
	}

	public void Execute( MVCSContext context )
	{
		foreach ( Action<MVCSContext> action in _mapContextList )
		{
			action( context );
		}
		_mapContextList.Clear();
	}
}
