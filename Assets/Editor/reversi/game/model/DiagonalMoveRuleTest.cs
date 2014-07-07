using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;

namespace reversi.game
{
	public class DiagonalMoveRuleTest
	{
		const int NumRows = 8;
		const int NumCols = 8;
		const int MinimumPiecesToTake = 2;
		
		IMoveRule _instance;
		Grid grid;
		IInjectionBinder injector;
		
		[SetUp]
		public void SetUp()
		{
			injector = new InjectionBinder();
			injector.Bind<CaptureMove>().To<CaptureMove>();
			injector.Bind<IInjectionBinder>().ToValue(injector);
			injector.Bind<PiecePlacedSignal>().To<PiecePlacedSignal>();
			
			MoveRuleFactory ruleFactory = new MoveRuleFactory();
			ruleFactory.InjectionBinder = injector;
			
			grid = new Grid(NumRows, NumCols);
			_instance = ruleFactory.BuildDiagonalMoveRule();
		}
		
		[Test]
		public void FindsSimplestDiagonalMove()
		{
			grid.PlacePiece(0, 0, Faction.Black);
			grid.PlacePiece(1, 1, Faction.White);
			grid.PlacePiece(2, 2, Faction.White);
			
			List<IMove> moves = _instance.FindMoves(new GridCellKey(3, 3), Faction.Black, grid);
			
			Assert.AreEqual(1, moves.Count);
			
			foreach (var move in moves)
			{
				move.ApplyMove(grid);
			}
			
			Assert.AreEqual(Faction.Black, grid.GetPiece(0, 0));
			Assert.AreEqual(Faction.Black, grid.GetPiece(1, 1));
			Assert.AreEqual(Faction.Black, grid.GetPiece(2, 2));
			Assert.AreEqual(Faction.Black, grid.GetPiece(3, 3));
		}
		
		[Test]
		public void FindsLongerDiagonalMove()
		{
			grid.PlacePiece(2, 2, Faction.Black);
			grid.PlacePiece(3, 3, Faction.Black);
			grid.PlacePiece(4, 4, Faction.Black);
			grid.PlacePiece(5, 5, Faction.White);
			
			List<IMove> moves = _instance.FindMoves(new GridCellKey(1, 1), Faction.White, grid);
			
			Assert.AreEqual(1, moves.Count);
			
			foreach (var move in moves)
			{
				move.ApplyMove(grid);
			}
			
			Assert.AreEqual(Faction.White, grid.GetPiece(1, 1));
			Assert.AreEqual(Faction.White, grid.GetPiece(2, 2));
			Assert.AreEqual(Faction.White, grid.GetPiece(3, 3));
			Assert.AreEqual(Faction.White, grid.GetPiece(4, 4));
			Assert.AreEqual(Faction.White, grid.GetPiece(5, 5));
		}

		[Test]
		public void FindsOppositeSimplesDiagonalMove()
		{
			grid.PlacePiece(0, 7, Faction.Black);
			grid.PlacePiece(1, 6, Faction.White);
			grid.PlacePiece(2, 5, Faction.White);
			
			List<IMove> moves = _instance.FindMoves(new GridCellKey(3, 4), Faction.Black, grid);
			
			Assert.AreEqual(1, moves.Count);
			
			foreach (var move in moves)
			{
				move.ApplyMove(grid);
			}
			
			Assert.AreEqual(Faction.Black, grid.GetPiece(0, 7));
			Assert.AreEqual(Faction.Black, grid.GetPiece(1, 6));
			Assert.AreEqual(Faction.Black, grid.GetPiece(2, 5));
			Assert.AreEqual(Faction.Black, grid.GetPiece(3, 4));
		}

		[Test]
		public void FindsOppositeLongDiagonalMove()
		{
			grid.PlacePiece(2, 5, Faction.Black);
			grid.PlacePiece(3, 4, Faction.Black);
			grid.PlacePiece(4, 3, Faction.Black);
			grid.PlacePiece(5, 2, Faction.White);
			
			List<IMove> moves = _instance.FindMoves(new GridCellKey(1, 6), Faction.White, grid);
			
			Assert.AreEqual(1, moves.Count);
			
			foreach (var move in moves)
			{
				move.ApplyMove(grid);
			}
			
			Assert.AreEqual(Faction.White, grid.GetPiece(1, 6));
			Assert.AreEqual(Faction.White, grid.GetPiece(2, 5));
			Assert.AreEqual(Faction.White, grid.GetPiece(3, 4));
			Assert.AreEqual(Faction.White, grid.GetPiece(4, 3));
			Assert.AreEqual(Faction.White, grid.GetPiece(5, 2));
		}
	}
}