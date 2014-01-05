using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerSpawner : MonoBehaviour {

	public GameObject player;
	public Vector3 spawnPosition = new Vector3(-7.0f, 1.2f, 0.0f);

	private List<GameObject> playerList = new List<GameObject>();
	public GameObject currentPlayer { get; private set; }

	public new static PlayerSpawner active;

	void Awake() {
		active = this;
		currentPlayer = null;
	}

	void Start () {
		SpawnPlayer();
	}

	void Update () {
		// remove any dead players
		playerList.RemoveAll(item => item == null);

		if (Input.GetKeyDown(KeyCode.R)) {
			SpawnPlayer();
		}

		if (Input.GetKeyDown(KeyCode.K)) {
			// kill self
			if (currentPlayer != null && currentPlayer.GetComponent<Health>() != null) {
				currentPlayer.GetComponent<Health>().Die();
			}
		}

		if (currentPlayer != null) {
			// fix camera to newest player
			Camera.main.transform.position = new Vector3(currentPlayer.transform.position.x, currentPlayer.transform.position.y, Camera.main.transform.position.z);
		} else {
			// try and make the most recently created dude the current player
			if (playerList.Count > 0) {
				currentPlayer = playerList[playerList.Count - 1];
			}
		}
	}

	private void SpawnPlayer() {
		GameObject newPlayer = Instantiate(player, spawnPosition, Quaternion.identity) as GameObject;
		playerList.Add(newPlayer);
		currentPlayer = newPlayer;
	}
}
