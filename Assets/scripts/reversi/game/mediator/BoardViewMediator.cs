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

		override public void OnRegister()
			{

		}
	}
}