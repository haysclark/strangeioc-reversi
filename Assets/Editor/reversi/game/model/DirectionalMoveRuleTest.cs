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
		PiecePlacedSignal piecePlacedSignal;
		
		[SetUp]
		public void SetUp()
		{
			injector = new InjectionBinder();
			injector.Bind<CaptureMove>().To<CaptureMove>();

			piecePlacedSignal = Substitute.For<PiecePlacedSignal>();
			injector.Bind<PiecePlacedSignal>().To(piecePlacedSignal);
			
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
			
			VerifyGridUpdatedAndSignalFired(Faction.White, 1, 1);
			VerifyGridUpdatedAndSignalFired(Faction.White, 2, 1);
			VerifyGridUpdatedAndSignalFired(Faction.White, 3, 1);
			VerifyGridUpdatedAndSignalFired(Faction.White, 4, 1);
			VerifyGridUpdatedAndSignalFired(Faction.White, 5, 1);
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
			
			VerifyGridUpdatedAndSignalFired(Faction.Black, 0, 0);
			VerifyGridUpdatedAndSignalFired(Faction.Black, 0, 1);
			VerifyGridUpdatedAndSignalFired(Faction.Black, 0, 2);
			VerifyGridUpdatedAndSignalFired(Faction.Black, 0, 3);
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

			VerifyGridUpdatedAndSignalFired(Faction.Black, 4, 4);
			VerifyGridUpdatedAndSignalFired(Faction.Black, 5, 5);
			VerifyGridUpdatedAndSignalFired(Faction.Black, 6, 6);
			VerifyGridUpdatedAndSignalFired(Faction.Black, 7, 7);
		}

		private void VerifyGridUpdatedAndSignalFired(Faction faction, int row, int col)
		{
			Assert.AreEqual(faction, grid.GetPiece(row, col));
			piecePlacedSignal.Received().Dispatch(new GridCellKey(row, col), faction);
		}
	}
}