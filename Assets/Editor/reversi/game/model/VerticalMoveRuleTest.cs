using System;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using strange.extensions.injector.api;
using strange.extensions.injector.impl;

namespace reversi.game
{
	public class VerticallMoveRuleTest
	{
		const int NumRows = 8;
		const int NumCols = 8;
		const int MinimumPiecesToTake = 2;
		
		VerticalMoveRule _instance;
		Grid grid;
		IInjectionBinder injector;
		
		[SetUp]
		public void SetUp()
		{
			injector = new InjectionBinder();
			injector.Bind<CaptureMove>().To<CaptureMove>();
			
			grid = new Grid(NumRows, NumCols);
			_instance = new VerticalMoveRule(MinimumPiecesToTake);
			_instance.InjectionBinder = injector;
		}

		[Test]
		public void FindsSimplestVerticalMove()
		{
			grid.PlacePiece(0, 0, Faction.Black);
			grid.PlacePiece(1, 0, Faction.White);
			grid.PlacePiece(2, 0, Faction.White);
			
			List<IMove> moves = _instance.FindMoves(new GridCellKey(3, 0), Faction.Black, grid);
			
			Assert.AreEqual(1, moves.Count);
			
			foreach (var move in moves)
			{
				move.ApplyMove(grid);
			}
			
			Assert.AreEqual(Faction.Black, grid.GetPiece(0, 0));
			Assert.AreEqual(Faction.Black, grid.GetPiece(1, 0));
			Assert.AreEqual(Faction.Black, grid.GetPiece(2, 0));
			Assert.AreEqual(Faction.Black, grid.GetPiece(3, 0));
		}
		
		[Test]
		public void FindsLongerVerticalMove()
		{
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
	}
}