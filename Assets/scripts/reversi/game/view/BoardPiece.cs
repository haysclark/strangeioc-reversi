using UnityEngine;
using System.Collections;
using reversi.game;

namespace reversi.game
{
	[RequireComponent(typeof(MeshRenderer))]
	public class BoardPiece : MonoBehaviour
	{
		public Color neutralColor;
		public Color blackColor;
		public Color whiteColor;

		void Start()
		{
			renderer.material.color = neutralColor;
		}

		public void SetFaction(Faction faction)
		{
			if (Faction.Black == faction) {
				renderer.material.color = blackColor;
			} else if (Faction.White == faction) {
				renderer.material.color = whiteColor;
			} else {
				renderer.material.color = neutralColor;
			}
		}
	}
}