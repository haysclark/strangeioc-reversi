using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.command.impl;

namespace reversi.game
{
	[TestFixture]
	public class SetInitialStateCommandTest
	{
		const int NumRows = 8;
		const int NumCols = 8;

		SetInitialStateCommand _instance;
		Grid grid;
		PiecePlacedSignal piecePlacedSignal;

		[SetUp]
		public void SetUp()
		{
			grid = new Grid(NumRows, NumCols);
			piecePlacedSignal = Substitute.For<PiecePlacedSignal>();
			_instance = new SetInitialStateCommand();
			_instance.Grid = grid;
			_instance.PiecePlacedSignal = piecePlacedSignal;
		}

		[Test]
		public void SetsFourCenterTilesAsCheckerboard()
		{
			_instance.Execute();
			Assert.AreEqual(Faction.White, grid.GetPiece((NumRows / 2) - 1, (NumCols / 2) - 1));
			Assert.AreEqual(Faction.White, grid.GetPiece(NumRows / 2, NumCols / 2));
			Assert.AreEqual(Faction.Black, grid.GetPiece(NumRows / 2, (NumCols / 2) - 1));
			Assert.AreEqual(Faction.Black, grid.GetPiece((NumRows / 2) - 1, NumCols / 2));
		}

		[Test]
		public void FiresSignalForFourCenterTiles()
		{
			_instance.Execute();
			piecePlacedSignal.Received().Dispatch(new GridCellKey((NumRows / 2) - 1, (NumCols / 2) - 1), Faction.White);
			piecePlacedSignal.Received().Dispatch(new GridCellKey(NumRows / 2, NumCols / 2), Faction.White);
			piecePlacedSignal.Received().Dispatch(new GridCellKey(NumRows / 2, (NumCols / 2) - 1), Faction.Black);
			piecePlacedSignal.Received().Dispatch(new GridCellKey((NumRows / 2) - 1, NumCols / 2), Faction.Black);
		}
	}
}