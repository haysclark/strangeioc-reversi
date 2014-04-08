using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;

namespace reversi.game
{
	public class HorizontalMoveRuleTest
	{
		const int NumRows = 8;
		const int NumCols = 8;
		const int MinimumPiecesToTake = 2;

		HorizontalMoveRule _instance;
		Grid grid;
		IInjectionBinder injector;

		[SetUp]
		public void SetUp()
		{
			injector = new InjectionBinder();
			injector.Bind<CaptureMove>().To<CaptureMove>();

			grid = new Grid(NumRows, NumCols);
			_instance = new HorizontalMoveRule(MinimumPiecesToTake);
			_instance.InjectionBinder = injector;
		}

		[Test]
		public void FindsSimplestHorizontalMove()
		{
			grid.PlacePiece(0, 0, Faction.Black);
			grid.PlacePiece(0, 1, Faction.White);
			grid.PlacePiece(0, 2, Faction.White);

			List<IMove> moves = _instance.FindMoves(new GridCellKey(0, 3), Faction.Black, grid);

			Assert.AreEqual(1, moves.Count);

			foreach (var move in moves)
			{
				move.ApplyMove(grid);
			}

			Assert.AreEqual(Faction.Black, grid.GetPiece(0, 0));
			Assert.AreEqual(Faction.Black, grid.GetPiece(0, 1));
			Assert.AreEqual(Faction.Black, grid.GetPiece(0, 2));
			Assert.AreEqual(Faction.Black, grid.GetPiece(0, 3));
		}

		[Test]
		public void FindsLongerHorizontalMove()
		{
			grid.PlacePiece(1, 2, Faction.Black);
			grid.PlacePiece(1, 3, Faction.Black);
			grid.PlacePiece(1, 4, Faction.Black);
			grid.PlacePiece(1, 5, Faction.White);
			
			List<IMove> moves = _instance.FindMoves(new GridCellKey(1, 1), Faction.White, grid);
			
			Assert.AreEqual(1, moves.Count);

			foreach (var move in moves)
			{
				move.ApplyMove(grid);
			}

			Assert.AreEqual(Faction.White, grid.GetPiece(1, 1));
			Assert.AreEqual(Faction.White, grid.GetPiece(1, 2));
			Assert.AreEqual(Faction.White, grid.GetPiece(1, 3));
			Assert.AreEqual(Faction.White, grid.GetPiece(1, 4));
			Assert.AreEqual(Faction.White, grid.GetPiece(1, 5));
		}
	}
}