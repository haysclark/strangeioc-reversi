using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;
using strange.extensions.command.impl;

namespace reversi.game
{
	public class GameStartupCommand : Command
	{
		[Inject]
		public SetInitialStateSignal SetInitialState { get; set; }

		[Inject]
		public InitializeBoardSignal InitializeBoard { get; set; }

		[Inject]
		public Grid Grid { get; set; }

		override public void Execute()
		{
			InitializeBoard.Dispatch(new GridCellKey(Grid.NumRows, Grid.NumCols));
			SetInitialState.Dispatch();
		}
	}
}