using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;

namespace reversi.game
{
	public class DirectionalMoveRuleTest
	{
		const int NumRows = 8;
		const int NumCols = 8;
		const int MinimumPiecesToTake = 2;
		
		DirectionalMoveRule _instance;
		Grid grid;
		IInjectionBinder injector;
		
		[SetUp]
		public void SetUp()
		{
			injector = new InjectionBinder();
			injector.Bind<CaptureMove>().To<CaptureMove>();
			
			grid = new Grid(NumRows, NumCols);
		}

		private void BuildTestObj(GridCellKey direction)
		{
			_instance = new DirectionalMoveRule(MinimumPiecesToTake, direction);
			_instance.InjectionBinder = injector;
		}
		
		[Test]
		public void FindsVerticalMove()
		{
			BuildTestObj(new GridCellKey(1, 0));

			grid.PlacePiece(2, 1, Faction.Black);
			grid.PlacePiece(3, 1, Faction.Black);
			grid.PlacePiece(4, 1, Faction.Black);
			grid.PlacePiece(5, 1, Faction.White);
			
			List<IMove> moves = _instance.FindMoves(new GridCellKey(1, 1), Faction.White, grid);
			
			Assert.AreEqual(1, moves.Count);
			
			foreach (var move in moves)
			{
				move.ApplyMove(grid);
			}
			
			Assert.AreEqual(Faction.White, grid.GetPiece(1, 1));
			Assert.AreEqual(Faction.White, grid.GetPiece(2, 1));
			Assert.AreEqual(Faction.White, grid.GetPiece(3, 1));
			Assert.AreEqual(Faction.White, grid.GetPiece(4, 1));
			Assert.AreEqual(Faction.White, grid.GetPiece(5, 1));
		}

		[Test]
		public void FindsHorizontalMove()
		{
			BuildTestObj(new GridCellKey(0, -1));

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
		public void FindsDiagonalMove()
		{
			BuildTestObj(new GridCellKey(1, 1));

			grid.PlacePiece(5, 5, Faction.White);
			grid.PlacePiece(6, 6, Faction.White);
			grid.PlacePiece(7, 7, Faction.Black);

			List<IMove> moves = _instance.FindMoves(new GridCellKey(4, 4), Faction.Black, grid);

			foreach (var move in moves)
			{
				move.ApplyMove(grid);
			}

			Assert.AreEqual(Faction.Black, grid.GetPiece(4, 4));
			Assert.AreEqual(Faction.Black, grid.GetPiece(5, 5));
			Assert.AreEqual(Faction.Black, grid.GetPiece(6, 6));
			Assert.AreEqual(Faction.Black, grid.GetPiece(7, 7));
		}
	}
}