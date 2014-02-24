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

	public List<Action<ICrossContextCapable>> preBindings = new List<Action<ICrossContextCapable>>();
	public List<Action<ICrossContextCapable>> mapBindings = new List<Action<ICrossContextCapable>>();
	public List<Action<ICrossContextCapable>> launchBindings = new List<Action<ICrossContextCapable>>();

	public ContextBuilder forContextView( MonoBehaviour contextView )
	{
		_contextView = contextView;
		return this;
	}

	public ContextBuilder addContextMapper( Action<ICrossContextCapable> contextAction )
	{
		mapBindings.Add( contextAction );
		return this;
	}

	public ContextBuilder useSignals()
	{
		Action<ICrossContextCapable> action = SignalsMapper.setup;
		if(!preBindings.Contains( action ))
		{
			preBindings.Add( action );
		}
		return this;
	}

	public ContextBuilder setStartSignalAndCommand<T,U>()
		where T : Signal, new()
		where U : ICommand, new()
	{
		useSignals();
		Action<ICrossContextCapable> action = new StartSignalMapper().setSignalAndCommand<T,U>();
		launchBindings.Add( action );
		return this;
	}

	public MVCSContext build()
	{
		GeneratedContext context = new GeneratedContext( _contextView, ContextStartupFlags.MANUAL_MAPPING );
		_contextView = null;

		context.onPreMapBindings = new MapToContextList(preBindings).Map;
		context.onMapBindings = new MapToContextList(mapBindings).Map;
		context.onLaunch = new MapToContextList(launchBindings).Map;

		context.Start();
		return context;
	}
}

public class SignalsMapper
{
	public static void setup( ICrossContextCapable context )
	{
		context.injectionBinder.Unbind<ICommandBinder> ();
		context.injectionBinder.Bind<ICommandBinder> ().To<SignalCommandBinder> ().ToSingleton ();
	}
}

public class StartSignalMapper
{
	public Action<ICrossContextCapable> setSignalAndCommand<T,U>()
		where T : Signal, new()
		where U : ICommand, new()
	{
		Action<ICrossContextCapable> response = delegate(ICrossContextCapable context)
		{
			var commandBinder = (SignalCommandBinder)context.injectionBinder.GetInstance<ICommandBinder>();
			commandBinder.Bind<T> ().To<U> ();
			Signal startSignal = (Signal)context.injectionBinder.GetInstance<T>();
			startSignal.Dispatch();
		};
		return response;
	}
}

public class MapToContextList
{
	private List<Action<ICrossContextCapable>> _mapContextList;
	public MapToContextList( List<Action<ICrossContextCapable>> mapContextList )
	{
		_mapContextList = mapContextList;
	}

	public void Map( ICrossContextCapable context )
	{
		foreach ( Action<ICrossContextCapable> action in _mapContextList )
		{
			action( context );
		}
		_mapContextList.Clear();
	}
}
