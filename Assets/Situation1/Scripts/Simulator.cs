using UnityEngine;
using System.Collections;

public class Simulator : MonoBehaviour {
	private InputManager input_manager;
	private MovementManager mover;

	// Use this for initialization
	void Start () {
		// Retrieve managers
		this.input_manager = GetComponent<InputManager> ();
		this.mover = GameObject.Find ("char_ethan").GetComponent<MovementManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		mover.Move(
			this.input_manager.getControlType(),
			this.input_manager.getDirection(),
			this.input_manager.getOrientation()
		);
	}
}
