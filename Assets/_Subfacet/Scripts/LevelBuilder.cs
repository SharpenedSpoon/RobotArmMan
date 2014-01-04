using UnityEngine;
using System.Collections;
using System.IO;

public class LevelBuilder : MonoBehaviour {

	public GameObject block = null;
	public GameObject player = null;
	public float gridSize = 32.0f;

	public TextAsset levelFile;

	private string level = "" +
			"," +
			"," +
			"000000000000000000000000000001," +
			"000000000000000001000000000001," +
			"00P000001000000001000100100001," +
			"1111011111111111111111111001111111," +
			"0000100000000000000000000001," +
			"0000000000000000000000000001," +
			"0000000000000000000000000111,";

	void Start () {
		/*var sr = new StreamReader(Application.dataPath + "/" + fileName);
		var fileContents = sr.ReadToEnd();
		sr.Close();
		
		var lines = fileContents.Split("\n"[0]);
		for (line in lines) {
            print (line);
        }*/
        
		BuildWorld(levelFile);
    }
    
	private void BuildWorld(TextAsset worldFile) {
		// seperate each row using a comma
		BuildWorld(worldFile.ToString());
	}

	private void BuildWorld(string worldString) {
		// seperate each row with a comma
		BuildWorld(worldString.Split(','));
	}
	
	private void BuildWorld(string[] worldStringArray) {
		/*
		 * worldString is an array of strings of characters, detailed below
		 * 
		 * Characters:
		 *     "0" - nothing
		 *     "1" - block
		 *     "P" - player spawn point
		 */

		int currentRow = worldStringArray.Length;
		int currentCol = 0;
		foreach (string row in worldStringArray) {
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
