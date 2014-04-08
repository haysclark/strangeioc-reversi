namespace reversi.game
{
	public struct Move2
	{
		public GridCellKey position;
		public Faction faction;

		public Move2(GridCellKey position, Faction faction)
		{
			this.position = position;
			this.faction = faction;
		}
	}
}