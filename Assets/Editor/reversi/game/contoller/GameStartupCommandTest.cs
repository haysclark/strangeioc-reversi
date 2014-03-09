using System;
using NUnit.Framework;
using NSubstitute;

namespace reversi.game
{
	public class GameStartupCommandTest
	{
		GameStartupCommand _instance;

		IResources mockResources;
		IGameObject mockGameObject;

		[SetUp]
		public void SetUp()
		{
			mockResources = Substitute.For<IResources>();
			mockGameObject = Substitute.For<IGameObject>();

			_instance = new GameStartupCommand();
			_instance.resources = mockResources;
			_instance.gameObject = mockGameObject;
		}
		
		[TearDown]
		public void TearDown()
		{
			_instance = null;
		}

		[Test]
		public void testExecuteShoudBlaBla ()
		{
			//Todo... _instance.Execute();
		}
	}
}