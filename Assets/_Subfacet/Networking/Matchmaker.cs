using UnityEngine;
using System.Collections;

public class Matchmaker : MonoBehaviour {

	private GameObject player = null;
	
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void Update () {
		if (Input.GetKeyDown(KeyCode.R) && player == null) {
			SpawnPlayer();
		}
	}

	void OnGUI() {
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	void OnJoinedLobby() {
		// Join a random room as soon as we join the lobby
		PhotonNetwork.JoinRandomRoom();
	}

	void OnPhotonRandomJoinFailed() {
		// Create a room if there are none
		PhotonNetwork.CreateRoom(null);
	}

	void OnJoinedRoom() {
		SpawnPlayer();
	}

	public void SpawnPlayer() {
		// This will create a player that shows up for everyone connected
		player = PhotonNetwork.Instantiate("PlayerNetworked", Vector3.zero, Quaternion.identity, 0);
		// Now set the local player to be controllable
		player.GetComponent<PlayerNetworked>().ActiveLocalPlayer = true;
	}
}
