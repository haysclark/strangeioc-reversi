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

		override public void OnRegister()
		{
			PiecePlaced.AddListener(HandlePiecePlaced);
		}

		override public void OnRemove()
		{
			PiecePlaced.RemoveListener(HandlePiecePlaced);
		}

		public void HandlePiecePlaced(GridCellKey position, Faction faction)
		{

		}
	}
}