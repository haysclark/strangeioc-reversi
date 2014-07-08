using NUnit.Framework;
using System;
using NSubstitute;
using UnityEngine;

namespace reversi.game
{
	[TestFixture]
	public class BoardViewMediatorTest
	{
		BoardViewMediator _instance;
		GameObject go;
		BoardView view;
		InitializeBoardSignal initializeBoardSignal;
		PiecePlacedSignal piecePlacedSignal;
		GridCellKey position;
		Faction faction;

		[SetUp]
		public void SetUp()
		{
			view = Substitute.For<BoardView>();
			piecePlacedSignal = Substitute.For<PiecePlacedSignal>();
			initializeBoardSignal = Substitute.For<InitializeBoardSignal>();

			System.Random r = new System.Random();
			position = new GridCellKey(r.Next(8), r.Next(8));
			faction = r.NextDouble() > .5 ? Faction.Black : Faction.White;

			go = GameObject.CreatePrimitive(PrimitiveType.Cube);
			_instance = go.AddComponent<BoardViewMediator>();
			_instance.View = view;
			_instance.PiecePlaced = piecePlacedSignal;
			_instance.InitializeBoard = initializeBoardSignal;
		}

		private void SetUpRealSignal()
		{
			piecePlacedSignal = new PiecePlacedSignal();
			_instance.PiecePlaced = piecePlacedSignal;

			initializeBoardSignal = new InitializeBoardSignal();
			_instance.InitializeBoard = initializeBoardSignal;
		}

		[TearDown]
		public void TearDown()
		{
			_instance = null;
			GameObject.DestroyImmediate(go);
		}

		[Test]
		public void OnRegisterAddsSignalListeners()
		{
			_instance.OnRegister();

			piecePlacedSignal.Received().AddListener(_instance.HandlePiecePlaced);
			initializeBoardSignal.Received().AddListener(_instance.HandleBoardInitialized);
		}

		[Test]
		public void OnRemoveRemovesSignalListeners()
		{
			_instance.OnRegister();

			_instance.OnRemove();

			piecePlacedSignal.Received().RemoveListener(_instance.HandlePiecePlaced);
			initializeBoardSignal.Received().RemoveListener(_instance.HandleBoardInitialized);
		}

		[Test]
		public void PiecePlacedSignalFiringUpdatesExpectedPiece()
		{
			SetUpRealSignal();
			_instance.OnRegister();

			piecePlacedSignal.Dispatch(position, faction);

			view.Received().SetFaction(position.row, position.col, faction);
		}

		[Test]
		public void InitializeBoardSignalFiringInitializesBoardView()
		{
			int expectedRows = 8;
			int expectedCols = 8;

			SetUpRealSignal();
			_instance.OnRegister();

			initializeBoardSignal.Dispatch(new GridCellKey(expectedRows, expectedCols));

			view.Received().SetBoardDimensions(expectedRows, expectedCols);
		}
	}
}