using UnityEngine;
using System.Collections;

public class Simulator : MonoBehaviour {

	public Camera initialCamera;
	public OVRCameraRig initialOVRCameraRig;
	public bool useTracking;

	private InputManager input_manager;
	private MovementManager mover;
	private OVRManager oculus_manager;
	private CameraManager camera_manager;


	// Use this for initialization
	void Start () {

		// Retrieve managers
		this.input_manager = GetComponent<InputManager> ();
		this.mover = GameObject.Find ("char_ethan").GetComponent<MovementManager> ();
		this.oculus_manager = GameObject.Find("OVRCameraRig").GetComponent<OVRManager>();
		this.camera_manager = GetComponent<CameraManager>();

		// Set initial camera according to the presence or not of real oculus rift
		if (this.oculus_manager.isVRPresent) {
			this.camera_manager.setOculusCamera(initialOVRCameraRig);
		}
		else {
			this.camera_manager.setMainCamera(initialCamera);
		}

		if (this.useTracking) {
			// Unactive the wake up script in use tracking mode
			GameObject.Find ("char_ethan").GetComponent<WakeUpScript>().enabled = false;

			GetComponent<FaderScript>().BeginFade(-1);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!useTracking) {
			mover.Move(
				this.input_manager.getControlType(),
				this.input_manager.getDirection(),
				this.input_manager.getOrientation()
			);
		}
	}
}
