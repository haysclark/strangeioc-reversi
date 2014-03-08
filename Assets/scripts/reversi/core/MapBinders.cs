using System;
using strange.extensions.context.api;
using strange.extensions.context.impl;

public class FirstRunOnly
{
	public static Action<MVCSContext> Do( Action<MVCSContext> action )
	{
		Action<MVCSContext> wrapper = delegate( MVCSContext context )
		{
			if( Context.firstContext == context )
			{
				action( context );
			}
		};
		return wrapper;
	}
}
