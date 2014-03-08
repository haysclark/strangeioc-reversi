using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;

namespace reversi.main
{
	public class ConfigureApplicationService 
	{
		public void Setup( MVCSContext context )
		{
			context.injectionBinder.Bind<IApplication> ().To<ApplicationWrapper>().ToSingleton ();
			context.injectionBinder.Bind<IResources> ().To<ResourcesWrapper>().ToSingleton ();
			context.injectionBinder.Bind<IGameObject> ().To<GameObjectWrapper>().ToSingleton ();
		}
	}
}
