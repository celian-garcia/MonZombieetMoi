using UnityEngine;
using System.Collections;

public class Simulator : MonoBehaviour {
	
	public bool useOculus;
	public bool useTracking;
	public bool checkTheHierarchy;

	private InputManager input_manager;
	private MovementManager mover;
	private OVRManager oculus_manager;
	private CameraManager camera_manager;
	private GameObject cam_or_oculus;

	void Awake () {
		if (checkTheHierarchy)
		{
			checkHierarchy();
		}
	}

	// Use this for initialization
	void Start () {

		// Retrieve managers
		this.input_manager = GetComponent<InputManager> ();
		this.mover = GameObject.Find ("char_ethan").GetComponent<MovementManager> ();

		if (useOculus) 
		{
			GameObject.Find ("Main Camera").SetActive(false);
		}
		else 
		{
			GameObject.Find ("OVRCameraRig").SetActive(false);
		}


		if (!useTracking)
		{
			GameObject.Find ("OptiTrack").SetActive(false);
			GameObject.Find ("char_ethan/char_ethan_skeleton_tracked").SetActive(false);
		}
		else
		{
			GameObject.Find ("char_ethan/char_ethan_skeleton").SetActive(false);
		}

	}

	void checkHierarchy () {

		bool check = true;
		if (GameObject.Find ("Main Camera") == null) 
		{
			Debug.LogError("\"Main Camera\" Game Object not present !");
			check = false;
		}
		if (GameObject.Find ("OVRCameraRig") == null) 
		{
			Debug.LogError("\"OVRCameraRig\" Game Object not present !");
			check = false;
		}
		if (GameObject.Find ("OptiTrack") == null) 
		{
			Debug.LogError("\"OptiTrack\" Game Object not present !");
			check = false;
		}

		if (!check) 
		{
			Debug.LogError("Please resolve Game Object issues");
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (!useTracking) {
			mover.Move(
				this.input_manager.getControlType(),
				this.input_manager.getDirection(),
				this.input_manager.getOrientation(),
				false
			);
		}
	}
}
