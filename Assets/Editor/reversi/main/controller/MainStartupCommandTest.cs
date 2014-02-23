//Test MainStartupCommand
using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using UnityEngine;

namespace reversi.main
{
	public class MainStartupCommandTest
	{
		MainStartupCommand _instance;
		IApplication _mockApplication;

		[SetUp]
		public void SetUp()
		{
			_instance = new MainStartupCommand();

			_mockApplication = Substitute.For<IApplication>();
			_instance.application = _mockApplication;
		}
		
		[TearDown]
		public void TearDown()
		{
			_instance = null;
		}

		[Test]
		public void shouldBeCommand ()
		{
			Assert.True( _instance is Command );
		}

		[Test]
		public void testExecuteShouldLoadGameLevel ()
		{
			_instance.Execute();

			_mockApplication.Received().LoadLevelAdditive ("dialog");
		}
	}
}