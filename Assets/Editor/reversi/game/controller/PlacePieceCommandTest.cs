using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.command.impl;

namespace reversi.game
{
	public class PlacePieceCommandTest
	{
		const int NumRows = 8;
		const int NumCols = 8;

		PlacePieceCommand _instance;

		IMoveRule finderRule;
		IMoveRuleFactory finderFactory;
		Grid grid;

		[SetUp]
		public void SetUp()
		{
			finderRule = Substitute.For<IMoveRule>();
			finderFactory = Substitute.For<IMoveRuleFactory>();
			List<IMoveRule> finderRules = new List<IMoveRule>();
			finderRules.Add(finderRule);
			finderFactory.BuildMoveRules().Returns(finderRules);

			grid = new Grid(NumRows, NumCols);

			_instance = new PlacePieceCommand();
			_instance.MoveRuleFactory = finderFactory;
			_instance.Grid = grid;
			_instance.position = new GridCellKey();
			_instance.faction = Faction.White;
		}

		[Test]
		public void ExecutePlacesPieceIfAllowedByRules()
		{
			Assert.AreEqual(Faction.None, grid.GetPiece(0, 0));

			List<IMove> foundMoves = new List<IMove>();
			foundMoves.Add(Substitute.For<IMove>());
			finderRule.FindMoves(_instance.position, _instance.faction, grid).Returns(foundMoves);

			_instance.Execute();

			Assert.AreEqual(Faction.White, grid.GetPiece(0, 0));
			foundMoves[0].Received().ApplyMove(grid);
		}

		[Test]
		public void ExecuteDoesNotPlacePieceIfNotAllowedByRules()
		{
			Assert.AreEqual(Faction.None, grid.GetPiece(0, 0));
			
			List<IMove> foundMoves = new List<IMove>();
			finderRule.FindMoves(_instance.position, _instance.faction, grid).Returns(foundMoves);
			
			_instance.Execute();
			
			Assert.AreEqual(Faction.None, grid.GetPiece(0, 0));
		}
	}
}