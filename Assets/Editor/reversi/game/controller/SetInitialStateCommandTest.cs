using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using strange.extensions.context.api;
using strange.extensions.command.impl;

namespace reversi.game
{
	public class SetInitialStateCommandTest
	{
		const int NumRows = 8;
		const int NumCols = 8;

		SetInitialStateCommand _instance;
		Grid grid;

		[SetUp]
		public void SetUp()
		{
			grid = new Grid(NumRows, NumCols);
			_instance = new SetInitialStateCommand();
			_instance.Grid = grid;
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
	}
}