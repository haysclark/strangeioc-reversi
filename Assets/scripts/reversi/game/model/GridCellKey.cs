using System;

namespace reversi.game
{
	public struct GridCellKey
	{
		public int row, col;
		
		public GridCellKey(int row, int col)
		{
			this.row = row;
			this.col = col;
		}

		override public bool Equals(Object otherObj)
		{
			if (!(otherObj is GridCellKey))
			{
				return false;
			}

			GridCellKey other = (GridCellKey)otherObj;

			return row == other.row && col == other.col;
		}

		public bool Equals(GridCellKey other)
		{
			return row == other.row && col == other.col;
		}

		override public int GetHashCode()
		{
			return row ^ col;
		}
	}
}