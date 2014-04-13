using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;

namespace reversi.game
{
	public class CollectionMoveRuleTest
	{
		CollectionMoveRule _instance;
		IMoveRule rule0;
		IMoveRule rule1;

		[SetUp]
		public void SetUp()
		{
			rule0 = Substitute.For<IMoveRule>();
			rule1 = Substitute.For<IMoveRule>();

			_instance = new CollectionMoveRule();
			_instance.AddRule(rule0);
			_instance.AddRule(rule1);
		}

		[Test]
		public void ReturnsMovesFoundByAllRules()
		{
			GridCellKey position = new GridCellKey(2, 2);
			Faction faction = Faction.White;
			Grid grid = new Grid(8, 8);

			IMove move0 = Substitute.For<IMove>();
			List<IMove> moves0 = new List<IMove>();
			moves0.Add(move0);
			rule0.FindMoves(position, faction, grid).Returns(moves0);

			IMove move1 = Substitute.For<IMove>();
			List<IMove> moves1 = new List<IMove>();
			moves1.Add(move1);
			rule1.FindMoves(position, faction, grid).Returns(moves1);

			List<IMove> result = _instance.FindMoves(position, faction, grid);
			Assert.AreEqual(2, result.Count);
			Assert.Contains(move0, result);
			Assert.Contains(move1, result);
		}
	}
}