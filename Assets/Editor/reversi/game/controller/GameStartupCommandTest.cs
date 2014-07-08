using NUnit.Framework;
using NSubstitute;
using NSubstitute.Experimental;
using System;

namespace reversi.game
{
	[TestFixture]
	public class GameStartupCommandTest
	{
		const int NumRows = 8;
		const int NumCols = 8;

		GameStartupCommand _instance;
		SetInitialStateSignal setInitialStateSignal;
		InitializeBoardSignal initializeBoardSignal;
		Grid grid;

		[SetUp]
		public void SetUp()
		{
			setInitialStateSignal = Substitute.For<SetInitialStateSignal>();
			initializeBoardSignal = Substitute.For<InitializeBoardSignal>();
			grid = new Grid(NumRows, NumCols);

			_instance = new GameStartupCommand();
			_instance.SetInitialState = setInitialStateSignal;
			_instance.InitializeBoard = initializeBoardSignal;
			_instance.Grid = grid;
		}

		[Test]
		public void ExecuteInitializesBoardAndSetsInitialBoardStateInCorrectOrder()
		{
			_instance.Execute();

			Received.InOrder(() => {
				initializeBoardSignal.Dispatch(new GridCellKey(NumRows, NumCols));
				setInitialStateSignal.Dispatch();
			});
		}
	}
}