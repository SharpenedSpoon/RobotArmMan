using UnityEngine;
using System.Collections;

public class LevelBuilder : MonoBehaviour {

	public GameObject block = null;
	public GameObject player = null;
	public float gridSize = 32.0f;

	private string level = "" +
			"," +
			"," +
			"000000000000000000000000000001," +
			"000000000000000001000000000001," +
			"00P000001000000001000100100001," +
			"1111011111111111111111111001111111," +
			"0000100000000000000000000001000000," +
			"0000000000000000000000000001000000," +
			"0000000000000000000000000111000000,";

	void Start () {
		string[] rows = level.Split(',');
		int currentRow = rows.Length;
		int currentCol = 0;
		foreach (string row in rows) {
			currentCol = 0;
			foreach (char ch in row) {
				if (ch == '1') {
					GameObject currentBlock = Instantiate(block, GridToWorld(currentRow, currentCol), Quaternion.identity) as GameObject;
					currentBlock.transform.parent = transform;
				} else if (ch == 'P') {
					PlayerSpawner.active.spawnPosition = GridToWorld(currentRow, currentCol);
				}
				currentCol++;
			}
			currentRow--;
		}
	}

	private Vector3 GridToWorld(int row, int col) {
		return new Vector3(col * gridSize, row * gridSize, 0.0f);
	}
}
