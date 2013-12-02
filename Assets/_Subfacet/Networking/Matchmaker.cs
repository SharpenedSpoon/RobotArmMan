using UnityEngine;
using System.Collections;

public class Matchmaker : MonoBehaviour {
	
	void Start () {
		PhotonNetwork.ConnectUsingSettings("0.1");
	}

	void Update () {
	
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
		// This will create a player that shows up for everyone connected
		GameObject player = PhotonNetwork.Instantiate("PlayerNetworked", Vector3.zero, Quaternion.identity, 0);
		// Now set the local player to be controllable
		player.GetComponent<PlayerNetworked>().ActiveLocalPlayer = true;
	}
}
