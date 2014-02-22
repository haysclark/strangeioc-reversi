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

		[SetUp]
		public void SetUp()
		{
			_instance = new MainStartupCommand();
			_instance.application = Substitute.For<IApplication>();
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

		/**
		[Test]
		public void testExecuteShouldLoadGameLevel ()
		{
			_instance.Execute();

			_instance.application.Received().LoadLevelAdditive ("game");
		}
		**/
	}
}