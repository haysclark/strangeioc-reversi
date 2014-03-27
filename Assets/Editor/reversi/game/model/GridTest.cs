using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections;
using strange.extensions.context.api;
using strange.extensions.command.impl;
using UnityEngine;

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
		public void FillsMiddleFourCellsInitially()
		{
			Assert.AreEqual(Faction.White, _instance.GetPiece((NumRows / 2) - 1, (NumCols / 2) - 1));
			Assert.AreEqual(Faction.White, _instance.GetPiece(NumRows / 2, NumCols / 2));
			Assert.AreEqual(Faction.Black, _instance.GetPiece(NumRows / 2, (NumCols / 2) - 1));
			Assert.AreEqual(Faction.Black, _instance.GetPiece((NumRows / 2) - 1, NumCols / 2));
		}
	}
}