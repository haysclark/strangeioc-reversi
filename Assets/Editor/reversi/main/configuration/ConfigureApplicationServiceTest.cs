using System;
using NUnit.Framework;
using NSubstitute;
using strange.extensions.context.api;
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
			var mockContext = Substitute.For<ICrossContextCapable>();
			var mockBinder = Substitute.For<ICrossContextInjectionBinder>();
			mockContext.injectionBinder.Returns( mockBinder );
			var mockBinding = Substitute.For<IInjectionBinding>();
			mockBinder.Bind<IApplication>().Returns( mockBinding );
			var mockFinalBinding = Substitute.For<IInjectionBinding>();
			mockBinding.To<ApplicationWrapper>().Returns(mockFinalBinding);

			_instance.Setup( mockContext );

			mockBinder.Received().Bind<IApplication>();
			mockBinding.Received().To<ApplicationWrapper>();
			mockFinalBinding.Received().ToSingleton();
		}
	}
}

