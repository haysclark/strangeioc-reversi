using System;
using strange.extensions.command.impl;
using UnityEngine;

namespace reversi.game.space
{
	public class CreateGridSpacesCommand : Command
	{
		[Inject]
		public string name{ get; set; }

		[Inject]
		public Vector2 size{ get; set; }

		[Inject]
		public SpaceModel spaceModel { get; set; }

		public override void Execute ()
		{
			Debug.Log( "Create " + (int) size.x + ", " + (int) size.y + " GridSpacesCommand" );
		}
	}
}
