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

	public ContextBuilder ForContextView( MonoBehaviour contextView )
	{
		_contextView = contextView;
		return this;
	}

	public MapBinderBuilder MapBinder()
	{
		return new MapBinderBuilder( this, mapBindings );
	}

	public ContextBuilder AddMapBinder( Action<ICrossContextCapable> contextAction )
	{
		mapBindings.Add( contextAction );
		return this;
	}

	public ContextBuilder UseSignals()
	{
		Action<ICrossContextCapable> action = SignalsConfigurator.Setup;
		if(!preBindings.Contains( action ))
		{
			preBindings.Add( action );
		}
		return this;
	}

	public ContextBuilder SetStartSignalAndCommand<T,U>()
		where T : TestableSignal, new()
		where U : ICommand, new()
	{
		UseSignals();
		Action<ICrossContextCapable> action = new StartSignalMapper().SetSignalAndCommand<T,U>();
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
	public static void Setup( ICrossContextCapable context )
	{
		context.injectionBinder.Unbind<ICommandBinder> ();
		context.injectionBinder.Bind<ICommandBinder> ().To<SignalCommandBinder> ().ToSingleton ();
	}
}

public class MapBinderBuilder
{
	private ContextBuilder _parent;
	private List<Action<ICrossContextCapable>> _preBindings;

	public MapBinderBuilder( ContextBuilder parent, List<Action<ICrossContextCapable>> preBindings )
	{
		_preBindings = preBindings;
		_parent = parent;
	}

	public ContextBuilder Add( Action<ICrossContextCapable> contextAction )
	{
		_preBindings.Add( contextAction );
		return _parent;
	}

	
	public ContextBuilder AddFirstRunOnly( Action<ICrossContextCapable> contextAction )
	{
		_preBindings.Add(FirstRunOnly.Do( contextAction ));
		return _parent;
	}
}

public class StartSignalMapper
{
	public Action<ICrossContextCapable> SetSignalAndCommand<T,U>()
		where T : TestableSignal, new()
		where U : ICommand, new()
	{
		Action<ICrossContextCapable> response = delegate(ICrossContextCapable context)
		{
			var commandBinder = (SignalCommandBinder)context.injectionBinder.GetInstance<ICommandBinder>();
			commandBinder.Bind<T> ().To<U> ();
			TestableSignal startSignal = (TestableSignal)context.injectionBinder.GetInstance<T>();
			startSignal.Dispatch();
		};
		return response;
	}
}

public class MapperActionListExecuter
{
	private List<Action<ICrossContextCapable>> _mapContextList;
	public MapperActionListExecuter( List<Action<ICrossContextCapable>> mapContextList )
	{
		_mapContextList = mapContextList;
	}

	public void Execute( ICrossContextCapable context )
	{
		foreach ( Action<ICrossContextCapable> action in _mapContextList )
		{
			action( context );
		}
		_mapContextList.Clear();
	}
}

public class FirstRunOnly
{
	public static Action<ICrossContextCapable> Do( Action<ICrossContextCapable> action )
	{
		Action<ICrossContextCapable> wrapper = delegate( ICrossContextCapable context )
		{
			if( Context.firstContext == context )
			{
				action( context );
			}
		};
		return wrapper;
	}
}


