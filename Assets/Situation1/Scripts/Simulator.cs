using UnityEngine;
using System.Collections;

public class Simulator : MonoBehaviour {

	public Camera initialCamera;

	private InputManager input_manager;
	private MovementManager mover;
	private CameraManager camera_manager;

	// Use this for initialization
	void Start () {
		// Retrieve managers
		this.input_manager = GetComponent<InputManager> ();
		this.mover = GameObject.Find ("char_ethan").GetComponent<MovementManager> ();
		this.camera_manager = this.GetComponent<CameraManager> ();

		// Initialize the camera
		this.camera_manager.setMainCamera(initialCamera);
	}
	
	// Update is called once per frame
	void Update () {
		mover.Move(
			this.input_manager.getControlType(),
			this.input_manager.getDirection(),
			this.input_manager.getOrientation(),
			false // ethan will not keep his position
		);
	}
}
