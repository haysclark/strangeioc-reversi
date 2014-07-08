using UnityEngine;
using System.Collections;
using strange.extensions.mediation.api;
using strange.extensions.mediation.impl;

namespace reversi.game
{
	public class BoardViewMediator : EventMediator
	{
		[Inject]
		public BoardView View { get; set; }

		[Inject]
		public PiecePlacedSignal PiecePlaced { get; set; }

		[Inject]
		public InitializeBoardSignal InitializeBoard { get; set; }

		override public void OnRegister()
		{
			PiecePlaced.AddListener(HandlePiecePlaced);
			InitializeBoard.AddListener(HandleBoardInitialized);
		}

		override public void OnRemove()
		{
			InitializeBoard.RemoveListener(HandleBoardInitialized);
			PiecePlaced.RemoveListener(HandlePiecePlaced);
		}

		public void HandlePiecePlaced(GridCellKey position, Faction faction)
		{
			View.SetFaction(position.row, position.col, faction);
		}

		public void HandleBoardInitialized(GridCellKey boardDimensions)
		{
			View.SetBoardDimensions(boardDimensions.row, boardDimensions.col);
		}
	}
}