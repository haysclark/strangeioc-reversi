using System;
using NUnit.Framework;
using NSubstitute;
using strange.extensions.context.api;
using strange.extensions.context.impl;
using strange.extensions.injector.api;

namespace reversi.main
{
	public class ConfigureApplicationServiceTest
	{
		ConfigureApplicationService _instance;

		[SetUp]
		public void SetUp()
		{
			_instance = new ConfigureApplicationService();
		}
		
		[TearDown]
		public void TearDown()
		{
			_instance = null;
		}

		[Test]
		public void testMapBindsIApplicationToApplicationWrapperAsASingleton ()
		{
			var mockBinder = Substitute.For<ICrossContextInjectionBinder>();
			var fakeContext = new MVCSContext();
			fakeContext.injectionBinder = mockBinder;

			var mockBinding = Substitute.For<IInjectionBinding>();
			mockBinder.Bind<IApplication>().Returns( mockBinding );
			mockBinder.Bind<IResources>().Returns( mockBinding );
			mockBinder.Bind<IGameObject>().Returns( mockBinding );

			var mockFinalBinding = Substitute.For<IInjectionBinding>();
			mockBinding.To<ApplicationWrapper>().Returns(mockFinalBinding);

			_instance.Setup( fakeContext );

			mockBinder.Received().Bind<IApplication>();
			mockBinding.Received().To<ApplicationWrapper>();

			mockBinder.Received().Bind<IResources>();
			mockBinding.Received().To<ResourcesWrapper>();

			mockBinder.Received().Bind<IGameObject>();
			mockBinding.Received().To<GameObjectWrapper>();
			mockFinalBinding.Received().ToSingleton();
		}
	}
}