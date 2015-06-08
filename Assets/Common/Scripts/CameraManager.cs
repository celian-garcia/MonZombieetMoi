using UnityEngine;
using System.Collections;
using System.Linq;

public class CameraManager : MonoBehaviour {
	public Camera initialCamera; 

	private Camera transition_camera;
	private Camera current_camera;
	private Camera next_camera;


	public void Awake() {
		if (this.initialCamera != null) 
			this.setCamera (initialCamera);
	}

	public GameObject[] getAllCameras () {
		GameObject[] fp = GameObject.FindGameObjectsWithTag ("FPCamera");
		GameObject[] tp = GameObject.FindGameObjectsWithTag ("TPCamera");
		return fp.Concat(tp).ToArray();
	}

	public void DisableAll () {
		GameObject[] cameras = getAllCameras ();
		foreach (GameObject c in cameras) {
			c.GetComponent<Camera>().enabled = false;
		}
	}

	public void setCamera (string camera) {
		// Set the current camera
		this.setCamera(GameObject.Find(camera).GetComponent<Camera>());
	}

	public void setCamera (Camera camera) {
		// Disable all cameras
		DisableAll ();
		
		// Set the current camera
		this.current_camera = camera;
		// and enable it
		this.current_camera.enabled = true;
	}
}
