using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace reversi.game
{
	public class Capture
	{
		private List<GridCellKey> pieces;
		private Faction takingFaction;

		public ReadOnlyCollection<GridCellKey> Pieces
		{
			get {
				return pieces.AsReadOnly();
			}
		}

		public Faction TakingFaction
		{
			get {
				return takingFaction;
			}
		}

		public Capture(List<GridCellKey> pieces, Faction takingFaction)
		{
			this.pieces = pieces;
			this.takingFaction = takingFaction;
		}
	}
}