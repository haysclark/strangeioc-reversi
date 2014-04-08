using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.command.impl;

namespace reversi.game
{
	public class GridTest
	{
		const int NumRows = 8;
		const int NumCols = 8;

		Grid _instance;

		[SetUp]
		public void SetUp()
		{
			_instance = new Grid(NumRows, NumCols);
		}

		[Test]
		public void PiecesAreNoneByDefault()
		{
			for (int row = 0; row < NumRows; row++)
			{
				for (int col = 0; col < NumCols; col++)
				{
					Assert.AreEqual(Faction.None, _instance.GetPiece(row, col));
				}
			}
		}

		[Test]
		public void PlacePieceUpdatesGrid()
		{
			_instance.PlacePiece(0, 0, Faction.Black);
			Assert.AreEqual(Faction.Black, _instance.GetPiece(0, 0));
			_instance.PlacePiece(1, 1, Faction.White);
			Assert.AreEqual(Faction.White, _instance.GetPiece(1, 1));
		}

		[Test]
		public void NumRowsReturnsExepctedResult()
		{
			Assert.AreEqual(NumRows, _instance.NumRows);
		}

		[Test]
		public void NumColsReturnsExpectedResult()
		{
			Assert.AreEqual(NumCols, _instance.NumCols);
		}
	}
}