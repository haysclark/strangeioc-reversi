using UnityEngine;
using System.Collections;

namespace reversi.game
{
	[Implements(typeof(SpaceModel))]
	public class SpaceModel
	{
		public SpaceModel ()
		{
		}

		public void Log()
		{
			Debug.Log( "Hello SpaceModel!" );
		}
	}
}
