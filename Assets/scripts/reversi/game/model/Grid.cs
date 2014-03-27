using System;
using System.Collections.Generic;

namespace reversi.game
{
	public class Grid
	{
		private Faction[,] grid;

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
			SetupInitialState();
		}

		public void PlacePiece(int row, int col, Faction faction)
		{
			if (CanPlace(row, col, faction)) {
				grid[row, col] = faction;
				UpdateStates(row, col);
			}
		}

		public Faction GetPiece(int row, int col)
		{
			return grid[row, col];
		}

		private void SetupInitialState()
		{
			int numCols = grid.GetLength(0);
			int numRows = grid.Length / numCols;
			grid[(numRows / 2) - 1, (numCols / 2) - 1] = Faction.White;
			grid[numRows / 2, numCols / 2] = Faction.White;
			grid[numRows / 2, (numCols / 2) - 1] = Faction.Black;
			grid[(numRows / 2) - 1, numCols / 2] = Faction.Black;
		}

		private bool CanPlace(int row, int col, Faction faction)
		{
			throw new NotImplementedException ();
		}

		private void UpdateStates(int row, int col)
		{
			throw new NotImplementedException ();
		}
	}
}