using System;
using System.Collections.Generic;

namespace reversi.game
{
	public class Grid
	{
		private Faction[,] grid;

		public int NumRows
		{
			get {
				return grid.Length / NumCols;
			}
		}

		public int NumCols
		{
			get {
				return grid.GetLength(0);
			}
		}

		public Grid(int numRows, int numCols)
		{
			grid = new Faction[numRows, numCols];
			for (int row = 0; row < numRows; row++)
			{
				for (int col = 0; col < numCols; col++)
				{
					grid[row, col] = Faction.None;
				}
			}
		}

		public void PlacePiece(int row, int col, Faction faction)
		{
			grid[row, col] = faction;
		}

		public Faction GetPiece(int row, int col)
		{
			return grid[row, col];
		}
	}
}