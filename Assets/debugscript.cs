using UnityEngine;
using System.Collections;

public class debugscript : MonoBehaviour {

	private GUIText text;

	// Use this for initialization
	void Start () {
		text = GetComponent<GUIText>();
	}

	// Update is called once per frame
	void Update () {
		if (PlayerSpawner.active.currentPlayer != null) {
			Movement tempMove = PlayerSpawner.active.currentPlayer.GetComponent<Movement>();
			if (tempMove.horizontalMovement == 0) {
				text.text = "Not moving";
			} else {
				if (tempMove.isFacingRight) {
					text.text = "Moving Right";
				} else {
					text.text = "Moving Left";
				}
			}
		}
	}
}
