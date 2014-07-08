using strange.extensions.mediation.impl;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace reversi.game
{
	public class BoardView : View
	{
		[Inject]
		public IResources Resources { get; set; }

		[Inject]
		public IGameObject GameObject { get; set; }

		private List<List<BoardPiece>> pieces = new List<List<BoardPiece>>();
		private GameObject diskPrefab;

		override protected void Start()
		{
			base.Start();
			diskPrefab = Resources.Load<GameObject>("Prefabs/Disk");
		}

		virtual public void SetBoardDimensions(int numRows, int numCols)
		{
			DestroySurplusPieces(numRows, numCols);
			ExpandListAndCreateNewPieces(numRows, numCols);
		}

		virtual public void SetFaction(int row, int col, Faction faction)
		{
			if (pieces.Count > row) {
				var rowPieces = pieces[row];
				if (rowPieces.Count > col) {
					rowPieces[col].SetFaction(faction);
				}
			}
		}

		private void DestroySurplusPieces(int numRows, int numCols)
		{
			for (int row = 0; row < pieces.Count; row++) {
				var rowPieces = pieces[row];
				for (int col = 0; col < rowPieces.Count; col++) {
					if (numCols >= col || numRows >= row) {
						var piece = rowPieces[col];
						rowPieces[col] = null;
						GameObject.Destroy(piece);
					}
				}
				if (numCols < rowPieces.Count) {
					rowPieces.RemoveRange(numCols, rowPieces.Count - numCols);
				}
			}
			if (numRows < pieces.Count) {
				pieces.RemoveRange(numRows, pieces.Count - numRows);
			}
		}

		private void ExpandListAndCreateNewPieces(int numRows, int numCols)
		{
			if (numRows > pieces.Count) {
				for (int i = 0; i < numRows - pieces.Count; i++) {
					pieces.Add(new List<BoardPiece>(numCols));
				}
			}
			for (int row = 0; row < pieces.Count; row++) {
				var rowPieces = pieces[row];
				for (int i = 0; i < numCols - rowPieces.Count; i++) {
					GameObject cur = (GameObject)GameObject.Instantiate(diskPrefab);
					cur.transform.position = new Vector3(1f * (numCols + i), 1f * row, 10f);
					rowPieces.Add(cur.GetComponent<BoardPiece>());
				}
			}
		}
	}
}